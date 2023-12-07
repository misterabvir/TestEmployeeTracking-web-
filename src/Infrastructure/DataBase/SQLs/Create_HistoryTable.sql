DROP TABLE IF EXISTS History;
CREATE TABLE History(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	EmployeeId uniqueidentifier NOT NULL,
	DepartmentId uniqueidentifier NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NULL,
	CONSTRAINT FK_Employee FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE CASCADE
);