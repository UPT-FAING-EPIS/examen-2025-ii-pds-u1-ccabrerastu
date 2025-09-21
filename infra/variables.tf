variable "aws_region" {
  type    = string
  default = "us-east-1"
}



variable "project" {
  type    = string
  default = "examapp"
}

variable "db_username" {
  type    = string
  default = "labsafe"
}

variable "db_password" {
  type    = string
  default = "labsafe"
}

variable "db_name" {
  type    = string
  default = "examsdb"
}

variable "instance_type" {
  type    = string
  default = "t3.micro"
}
