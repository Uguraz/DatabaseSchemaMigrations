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
