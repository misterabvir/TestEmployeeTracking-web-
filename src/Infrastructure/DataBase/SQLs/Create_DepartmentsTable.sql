DROP TABLE IF EXISTS Departments;

CREATE TABLE Departments(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	ParentId uniqueidentifier NULL,
	Title VARCHAR(255),
	CONSTRAINT FK_ParentDepartment FOREIGN KEY (ParentId) REFERENCES Departments(Id)
);