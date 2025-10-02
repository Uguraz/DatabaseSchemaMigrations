-- V2: AddCoursesAndEnrollments
CREATE TABLE Courses (
                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
                         Title TEXT NOT NULL,
                         Credits INTEGER NOT NULL
);

CREATE TABLE Enrollments (
                             Id INTEGER PRIMARY KEY AUTOINCREMENT,
                             StudentId INTEGER NOT NULL,
                             CourseId INTEGER NOT NULL,
                             EnrolledAt TEXT NOT NULL,
                             Grade INTEGER NULL,
                             CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StudentId) REFERENCES Students (Id) ON DELETE CASCADE,
                             CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CourseId) REFERENCES Courses (Id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IX_Enrollments_StudentId_CourseId ON Enrollments(StudentId, CourseId);
