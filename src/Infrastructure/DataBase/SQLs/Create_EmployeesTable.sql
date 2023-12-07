DROP TABLE IF EXISTS Employees;
CREATE TABLE Employees(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	LastName VARCHAR(255) NOT NULL,
	FirstName VARCHAR(255) NOT NULL,	
	DepartmentId uniqueidentifier NOT NULL,	
	CONSTRAINT FK_Department FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE CASCADE
);