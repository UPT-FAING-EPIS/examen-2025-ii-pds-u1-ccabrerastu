resource "aws_vpc" "main" {
cidr_block = "10.0.0.0/16"
tags = { Name = "${var.project}-vpc" }
}


data "aws_availability_zones" "az" {}


resource "aws_subnet" "public" {
count = 2
vpc_id = aws_vpc.main.id
cidr_block = cidrsubnet(aws_vpc.main.cidr_block, 8, count.index)
availability_zone = data.aws_availability_zones.az.names[count.index]
tags = { Name = "${var.project}-subnet-${count.index}" }
}


resource "aws_security_group" "app_sg" {
name = "${var.project}-sg"
vpc_id = aws_vpc.main.id
ingress {
from_port = 80
to_port = 80
protocol = "tcp"
cidr_blocks = ["0.0.0.0/0"]
}
egress {
from_port = 0
to_port = 0
protocol = "-1"
cidr_blocks = ["0.0.0.0/0"]
}
}


resource "aws_db_subnet_group" "dbsub" {
name = "${var.project}-db-subnet"
subnet_ids = aws_subnet.public[*].id
}


resource "aws_db_instance" "mysql" {
identifier = "${var.project}-mysql"
allocated_storage = 20
engine = "mysql"
engine_version = "8.0"
instance_class = "db.t3.micro"
db_name = var.db_name
username = var.db_username
password = var.db_password
db_subnet_group_name = aws_db_subnet_group.dbsub.name
vpc_security_group_ids = [aws_security_group.app_sg.id]
skip_final_snapshot = true
}


output "db_endpoint" { value = aws_db_instance.mysql.address }