provider "google" {
  project = var.project_id
  region  = var.region
}

# Red VPC para el clúster
resource "google_compute_network" "vpc" {
  name                    = "gke-network"
  auto_create_subnetworks = false

  lifecycle {
    prevent_destroy = true
    ignore_changes  = all
  }
}

# Subred para el clúster GKE
resource "google_compute_subnetwork" "subnet" {
  name          = "gke-subnet"
  ip_cidr_range = "10.0.0.0/24"
  region        = var.region
  network       = google_compute_network.vpc.id

  lifecycle {
    prevent_destroy = true
    ignore_changes  = all
  }
}

# Cluster GKE
resource "google_container_cluster" "primary" {
  name     = "project-management-cluster"
  location = var.region

  remove_default_node_pool = true
  initial_node_count       = 1

  network    = google_compute_network.vpc.name
  subnetwork = google_compute_subnetwork.subnet.name

  private_cluster_config {
    enable_private_nodes    = true
    enable_private_endpoint = false
    master_ipv4_cidr_block  = "172.16.0.0/28"
  }

  ip_allocation_policy {
    cluster_ipv4_cidr_block  = "10.1.0.0/16"
    services_ipv4_cidr_block = "10.2.0.0/16"
  }

  workload_identity_config {
    workload_pool = "${var.project_id}.svc.id.goog"
  }

  lifecycle {
    prevent_destroy = true
    ignore_changes  = all
  }
}

# Node Pool para el clúster
resource "google_container_node_pool" "primary_nodes" {
  name       = "primary-node-pool"
  location   = var.region
  cluster    = google_container_cluster.primary.name
  node_count = var.gke_num_nodes

  node_config {
    oauth_scopes = [
      "https://www.googleapis.com/auth/logging.write",
      "https://www.googleapis.com/auth/monitoring",
      "https://www.googleapis.com/auth/devstorage.read_only",
      "https://www.googleapis.com/auth/cloud-platform"
    ]

    labels = {
      env = var.project_id
    }

    machine_type = "e2-standard-2"
    disk_size_gb = 50
    disk_type    = "pd-standard"

    workload_metadata_config {
      mode = "GKE_METADATA"
    }
  }
}

# Instancia MySQL en Cloud SQL
resource "google_sql_database_instance" "mysql" {
  name               = "project-management-db"
  database_version    = "MYSQL_8_0"
  region              = var.region
  deletion_protection = false

  settings {
    tier = "db-custom-1-3840"

    backup_configuration {
      enabled    = true
      start_time = "02:00"
      location   = "us"
    }

    ip_configuration {
      ipv4_enabled    = false
      private_network = google_compute_network.vpc.id
    }

    insights_config {
      query_insights_enabled  = true
      query_string_length     = 1024
      record_application_tags = true
      record_client_address   = true
    }
  }

  lifecycle {
    prevent_destroy = true
    ignore_changes  = all
  }
}

# Base de datos MySQL
resource "google_sql_database" "appdb" {
  name     = "project_management_db"
  instance = google_sql_database_instance.mysql.name
}

# Usuario para la base de datos MySQL
resource "google_sql_user" "db_user" {
  name     = "app_user"
  instance = google_sql_database_instance.mysql.name
  password = var.db_password
}

# Service Account para Cloud SQL Auth Proxy
resource "google_service_account" "cloudsql_proxy" {
  account_id   = "cloudsql-proxy"
  display_name = "Cloud SQL Auth Proxy Service Account"

  lifecycle {
    prevent_destroy = true
    ignore_changes  = all
  }
}

# Asignación de rol para Cloud SQL Auth Proxy
resource "google_project_iam_binding" "cloudsql_client" {
  project = var.project_id
  role    = "roles/cloudsql.client"

  members = [
    "serviceAccount:${google_service_account.cloudsql_proxy.email}",
  ]
}

# Workload Identity para Cloud SQL Auth Proxy
resource "google_service_account_iam_binding" "workload_identity_binding" {
  service_account_id = google_service_account.cloudsql_proxy.name
  role               = "roles/iam.workloadIdentityUser"

  members = [
    "serviceAccount:${var.project_id}.svc.id.goog[default/cloudsql-proxy]",
  ]
}
