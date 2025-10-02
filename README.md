# Database Schema Migrations – Student Management

Dette repo demonstrerer database schema migrations i .NET 8 med **Entity Framework Core** i et *Student Management System*.

---

## Formål
Projektet viser:
- Hvordan man bruger **change-based migrations** (trin-for-trin via `dotnet ef migrations`)
- Hvordan man arbejder med **feature branches + Pull Requests** for at versionere ændringer
- Hvordan man holder styr på en **evolution af database schemaet** gennem git-historik
- Hvordan controllers og API’er bindes til det udviklende schema

---

## Setup

1. Klon projektet:
   ```bash
   git clone https://github.com/Uguraz/DatabaseSchemaMigrations.git
   cd DatabaseSchemaMigrations/src/StudentManagement.Api

---

## Migrations-historik
### InitialCreate
- Oprettede Students tabel
### AddCoursesAndEnrollments
- Tilføjede Courses og Enrollments tabeller
- Mange-til-mange relation mellem Students ↔ Courses
### AddDepartmentsAndInstructors
- Tilføjede Departments og Instructors tabeller
- Seedede default værdier (General, TBD Instructor)
- Tilføjede Foreign Keys til Courses

---

## Controllers & Endpoints

### StudentsController (/api/Students)
- GET /api/Students – hent alle studerende
- GET /api/Students/{id} – hent en bestemt studerende
- POST /api/Students – opret en studerende
- DELETE /api/Students/{id} – slet en studerende

### CoursesController (/api/Courses)
- GET /api/Courses – hent alle kurser
- GET /api/Courses/{id} – hent et bestemt kursus
- POST /api/Courses – opret et kursus (kræver DepartmentId, InstructorId)
- DELETE /api/Courses/{id} – slet et kursus

### EnrollmentsController (/api/Enrollments)
- GET /api/Enrollments – hent alle tilmeldinger
- GET /api/Enrollments/{id} – hent en specifik tilmelding
- POST /api/Enrollments – opret en tilmelding (Student ↔ Course)
- DELETE /api/Enrollments/{id} – fjern en tilmelding

### DepartmentsController (/api/Departments)
- GET /api/Departments – hent alle afdelinger
- GET /api/Departments/{id} – hent en specifik afdeling
- POST /api/Departments – opret en afdeling
- DELETE /api/Departments/{id} – slet en afdeling

### InstructorsController (/api/Instructors)
- GET /api/Instructors – hent alle undervisere
- GET /api/Instructors/{id} – hent en specifik underviser
- POST /api/Instructors – opret en underviser
- DELETE /api/Instructors/{id} – slet en underviser

---

## Teknologier
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI
- GitHub Flow (feature branches + Pull requests)