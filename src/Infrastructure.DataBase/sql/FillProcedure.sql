CREATE PROCEDURE FillEmployeeDepartment
    @DepartmentTitle NVARCHAR(255),
    @FirstName NVARCHAR(255),
    @LastName NVARCHAR(255),
    @StartDate DATE
AS
BEGIN
    DECLARE @DepartmentId UNIQUEIDENTIFIER;
    DECLARE @EmployeeId UNIQUEIDENTIFIER;

    SELECT @DepartmentId = Id
    FROM Departments
    WHERE Title = @DepartmentTitle;

    IF @DepartmentId IS NOT NULL
    BEGIN
        INSERT INTO Employees(Id, FirstName, LastName, DepartmentId)
        VALUES(NEWID(), @FirstName, @LastName, @DepartmentId);
        
        SELECT @EmployeeId = Id
        FROM Employees
        WHERE FirstName = @FirstName
            AND LastName = @LastName;

        INSERT INTO History (Id, EmployeeId, DepartmentId, StartDate)
        VALUES (NEWID(), @EmployeeId, @DepartmentId, @StartDate);
    END
    ELSE
    BEGIN
        PRINT 'Department not found.';
    END
END;