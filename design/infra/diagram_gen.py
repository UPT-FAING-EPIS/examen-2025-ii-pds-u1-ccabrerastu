# design/infra/diagram_gen.py
from diagrams import Diagram, Cluster
from diagrams.aws.compute import EC2
from diagrams.aws.database import RDS
from diagrams.aws.network import ELB

with Diagram("Infraestructura", show=False, outformat="png", filename="infra_diagram"):
    with Cluster("VPC"):
        lb = ELB("load balancer")
        web = [EC2("web1"), EC2("web2")]
        db = RDS("database")

        lb >> web >> db
