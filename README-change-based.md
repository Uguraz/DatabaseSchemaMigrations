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

## Hvorfor change-based?
- **Historik pr. ændring**: hver models/DB-ændring registreres i en lille, kørbar migration.
- **Sikker udrulning**: EF håndterer rækkefølge, FK-konstraints, indextilføjelser m.m.
- **Nem lokal test**: `dotnet ef database update` opbygger DB lokalt identisk med prod.
- **God til teamflow**: passer til feature branches + PR’er; konflikter ses tidligt.
---
## Valg vi tog – og hvorfor

- **SQLite** i udvikling: ingen serverkrav, lynhurtig feedback.
- **Feature branches** (`feature/add-courses`, `feature/add-departments-instructors`): hver skema-ændring isoleres og dokumenteres via PR.
- **Seed + FK default-værdier i migration** ved tilføjelse af `DepartmentId`/`InstructorId` til `Courses`:
   - ellers fejler SQLite ved “table rebuild” når eksisterende rækker ikke opfylder nye FK’er.
   - vi indsatte `Department(Id=1, "General")` og `Instructor(Id=1, "TBD")` først, og gav kolonnerne default `1`. Så kan eksisterende kurser migreres uden fejl.
- **CRUD-API + Swagger**: gør det let at validere at skemaet matcher app-adfærden.

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