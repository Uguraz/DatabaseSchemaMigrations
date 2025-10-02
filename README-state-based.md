# State-based migrations (SQL scripts) – Student Management

Denne README beskriver en **state-based** strategi som alternativ til EF’s change-based.
I en state-based tilgang versionerer vi **hele skemaets ønskede tilstand** som SQL-scripts og udruller ved at anvende det seneste *state* (evt. som diff mod nuværende).

> I dette projekt kan state-based demonstreres med SQLite via simple scripts.

---

## Hvorfor state-based?
-  **Single source of truth = schema as code**: ét samlet skema pr. version (nemt at læse/validere).
-  **Databasteam uden ORM**: DBA’er kan reviewe/ejerskab i ren SQL.
-  **Let at bootstrappe**: nyt miljø = kør *v seneste* skema + seed.

---

## Valg vi tog – og hvorfor
- Fx SQLite, feature branches, seed defaults (change-based).
- Fx SQL scripts, review i ren SQL, evt. værktøjer som Flyway (state-based).

## Setup
1. Kør seneste SQL script i `migrations/sql` for at bootstrappe databasen.
2. Start API’et:
   ```bash
   dotnet run --project src/StudentManagement.Api

---

## Migrations-historik
### V1__InitialCreate.sql
- Oprettede Students tabel
### V2__AddCoursesAndEnrollments.sql
- Tilføjede Courses og Enrollments tabeller
- Mange-til-mange relation mellem Students ↔ Courses
### V3__AddDepartmentsAndInstructors.sql
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
- SQLite
- SQL scripts (state-based migrations)
- Swagger / OpenAPI
- GitHub Flow (feature branches + PRs)