output "db_endpoint" { value = aws_db_instance.mysql.address }
output "vpc_id" { value = aws_vpc.main.id }