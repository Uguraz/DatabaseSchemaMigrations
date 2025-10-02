-- V3: AddDepartmentsAndInstructors
CREATE TABLE Departments (
                             Id INTEGER PRIMARY KEY AUTOINCREMENT,
                             Name TEXT NOT NULL,
                             Budget DECIMAL(18,2) NOT NULL
);

CREATE TABLE Instructors (
                             Id INTEGER PRIMARY KEY AUTOINCREMENT,
                             FirstName TEXT NOT NULL,
                             LastName TEXT NOT NULL,
                             HireDate TEXT NOT NULL
);

ALTER TABLE Courses ADD COLUMN DepartmentId INTEGER NOT NULL DEFAULT 1;
ALTER TABLE Courses ADD COLUMN InstructorId INTEGER NOT NULL DEFAULT 1;

INSERT INTO Departments (Id, Name, Budget) VALUES (1, 'General', 0.0);
INSERT INTO Instructors (Id, FirstName, LastName, HireDate) VALUES (1, 'TBD', 'Instructor', '2000-01-01');

CREATE INDEX IX_Courses_DepartmentId ON Courses (DepartmentId);
CREATE INDEX IX_Courses_InstructorId ON Courses (InstructorId);
