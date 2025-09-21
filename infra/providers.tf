terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0" # usa la versión que tengas en tu lockfile
    }
  }
}

provider "aws" {
  region = var.aws_region  # Ej. "us-east-1"
}
