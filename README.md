[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/A-aUFMBb)
[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=20616744)


# Proyecto: Aplicación de Exámenes en Línea

### Estudiante: Camila Fernanda Cabrera Catari


## Objetivo
Desarrollar una plataforma web que permita la creación, gestión y realización de exámenes en línea para estudiantes y docentes. El sistema debe ser seguro, flexible y fácil de usar tanto en backend como en frontend.

## Funcionalidades Principales
- Creación y edición de exámenes por parte de los docentes.
- Banco de preguntas con soporte para preguntas de opción múltiple, verdadero/falso y abiertas.
- Asignación de exámenes a grupos o estudiantes específicos.
- Realización de exámenes en línea con temporizador.
- Visualización de resultados y reportes.

## Backend (API)
- Framework sugerido: .NET Core.
- Endpoints RESTful:
- `POST /exams` — Crear examen.
- `GET /exams` — Listar exámenes disponibles.
- `GET /exams/{id}` — Detalle de examen.
- `POST /questions` — Crear pregunta.
- `GET /questions/{examId}` — Listar preguntas de un examen.
- `POST /submissions` — Enviar respuestas de un examen.
- `GET /results/{userId}` — Ver resultados de un usuario.
- Base de datos relacional (ej: SQL Server, PostgreSQL).
- Pruebas unitarias y de integración.

## Frontend
- Framework sugerido: Angular, React o Vue.
- Funcionalidades:
- Panel de usuario para ver exámenes asignados y resultados.
- Interfaz para responder exámenes con temporizador y navegación entre preguntas.
- Panel de docente para crear y asignar exámenes, y revisar resultados.

## Criterios de Evaluación
1. Calidad y organización del código, código limpio, principios de diseño aplicados.
2. Crear la infraestrutura utilizando IaC (Terraform).
3. Crear una automatización para para el despliegue de la infraestructura en Github (infra.yml)
4. Crear una automatizaciòn para generar el diagrama de infraestructura en el repositorio (infra_diagram.yml)
5. Crear una automatizaciòn para generar el diagrama de clases de la aplicaciòn (class_diagram.yml)
6. Crear una automatizaciòn para generar la documentaciòn del còdigo en Github Page (publish_doc.yml)
7. Creaciòn una automatizaciòn para realizar el escaneo de la aplicaciòn con SonarQube (sonar.yml) - 0 bugs, 0 vulnerabilidades, 0 hotspots, 90% de cobertura, 10 lineas de codigo duplicado
8. Crear una automatizaciòn para el despliegue del frontend y del backend (deploy_app.yml)
9. Crear una automatizaciòn para la creaciòn del release (release.yml)

## Final
1. Responder con la URL de la aplicaciòn desplegada: https://examen-2025-ii-pds-u1-ccabrerastu.vercel.app/
2. Responder con la URL del repositorio Github: https://github.com/UPT-FAING-EPIS/examen-2025-ii-pds-u1-ccabrerastu
