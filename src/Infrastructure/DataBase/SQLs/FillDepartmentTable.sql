/* Fill Tables */
INSERT INTO Departments (Id, ParentId, Title) VALUES
(NEWID(), NULL, 'IT Department'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'IT Department'), 'Software Development'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Software Development'), 'Backend Development'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Software Development'), 'Frontend Development'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Software Development'), 'QA & Testing'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'IT Department'), 'Infrastructure'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Infrastructure'), 'Network Administration'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Infrastructure'), 'Systems Administration'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'IT Department'), 'Data Science & Analytics'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Data Science & Analytics'), 'Data Engineering'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'Data Science & Analytics'), 'Data Analysis'),
(NEWID(), (SELECT Id FROM Departments WHERE Title = 'IT Department'), 'IT Support');


INSERT INTO Employees (Id, FirstName, LastName, DepartmentId) VALUES
(NEWID(), 'Emma', 'Smith', (SELECT Id FROM Departments WHERE Title = 'Backend Development')),
(NEWID(), 'Ava', 'Johnson', (SELECT Id FROM Departments WHERE Title = 'Software Development')),
(NEWID(), 'Olivia', 'Williams', (SELECT Id FROM Departments WHERE Title = 'QA & Testing')),
(NEWID(), 'Sophia', 'Brown', (SELECT Id FROM Departments WHERE Title = 'Software Development')),
(NEWID(), 'Mia', 'Jones', (SELECT Id FROM Departments WHERE Title = 'Frontend Development')),
(NEWID(), 'Noah', 'Miller', (SELECT Id FROM Departments WHERE Title = 'IT Department')),
(NEWID(), 'Liam', 'Davis', (SELECT Id FROM Departments WHERE Title = 'Infrastructure')),
(NEWID(), 'Oliver', 'Garcia', (SELECT Id FROM Departments WHERE Title = 'Network Administration')),
(NEWID(), 'Elijah', 'Wilson', (SELECT Id FROM Departments WHERE Title = 'Systems Administration')),
(NEWID(), 'James', 'Martinez', (SELECT Id FROM Departments WHERE Title = 'Data Science & Analytics')),
(NEWID(), 'Isabella', 'Anderson', (SELECT Id FROM Departments WHERE Title = 'Data Engineering')),
(NEWID(), 'William', 'Thompson', (SELECT Id FROM Departments WHERE Title = 'Data Analysis')),
(NEWID(), 'Sophia', 'White', (SELECT Id FROM Departments WHERE Title = 'IT Support'));

