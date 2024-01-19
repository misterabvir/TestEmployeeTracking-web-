CREATE TABLE History(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	EmployeeId uniqueidentifier NULL,
	DepartmentId uniqueidentifier NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NULL,
	CONSTRAINT FK_HistoryEmployeeId FOREIGN KEY (EmployeeId)REFERENCES Employees(Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_HistoryDepartmentId FOREIGN KEY (DepartmentId)REFERENCES Departments(Id) ON DELETE CASCADE ON UPDATE CASCADE,
);