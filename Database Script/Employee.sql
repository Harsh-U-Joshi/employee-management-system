--DROP DATABASE IF EXISTS employeemanagement;
--CREATE DATABASE employeemanagement;

USE employeemanagement;

DROP TABLE IF EXISTS ApplicationUser;
CREATE TABLE ApplicationUser (
    Id NVARCHAR(150) PRIMARY KEY, 
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    UserName NVARCHAR(100) NOT NULL,
	PasswordHash NVARCHAR(100) NOT NULL,
	CreatedDateTimeUTC DATETIME NOT NULL
);

DROP TABLE IF EXISTS EmployeeDetail;
CREATE TABLE EmployeeDetail (
    Id NVARCHAR(150) PRIMARY KEY,  
    FirstName NVARCHAR(100) NOT NULL,
    MiddleName NVARCHAR(100) NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    DateOfJoining DATE NOT NULL,
    Gender INT NOT NULL, 
    DepartmentId NVARCHAR(150) NOT NULL,
    PositionId NVARCHAR(150) NOT NULL,
    Status INT NOT NULL, 
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDateTimeUTC DATETIME NOT NULL,
	CreatedBy NVARCHAR(150) NOT NULL,
	LastModifiedDateTimeUTC DATETIME NOT NULL,
    LastModifiedBy NVARCHAR(150) NOT NULL,
);

DROP TABLE IF EXISTS Department;
CREATE TABLE Department (
    Id NVARCHAR(150) PRIMARY KEY, 
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(100) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDateTimeUTC DATETIME NOT NULL,
	CreatedBy NVARCHAR(150) NOT NULL,
	LastModifiedDateTimeUTC DATETIME NOT NULL,
    LastModifiedBy NVARCHAR(150) NOT NULL,
);

DROP TABLE IF EXISTS Position;
CREATE TABLE Position (
    Id NVARCHAR(150) PRIMARY KEY, 
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(100) NULL,
    IsDeleted BIT NOT NULL DEFAULT 0,
    CreatedDateTimeUTC DATETIME NOT NULL,
	CreatedBy NVARCHAR(150) NOT NULL,
	LastModifiedDateTimeUTC DATETIME NOT NULL,
    LastModifiedBy NVARCHAR(150) NOT NULL,
);

DROP TABLE IF EXISTS ErrorLog;
CREATE TABLE ErrorLog (
    Id NVARCHAR(150) PRIMARY KEY, 
    Message TEXT,
    StackTrace TEXT,
	Timestamp DATETIME,
	Context TEXT
);

DECLARE @DefaultPassword NVARCHAR(50) = 'QnIHcnjZ/DkkIuWxgTkJ9A==:66pjUSk4RybxB4Zn0TWzHg==' -- Psspl@12345

-- Insert dummy data into ApplicationUser
INSERT INTO ApplicationUser (Id, FirstName, LastName, Email, UserName, PasswordHash, CreatedDateTimeUTC) 
VALUES 
(LOWER(LEFT(NEWID(), 130)), 'John', 'Doe', 'john.doe@example.com', 'johndoe', @DefaultPassword, GETUTCDATE()),
(LOWER(LEFT(NEWID(), 130)), 'Jane', 'Smith', 'jane.smith@example.com', 'janesmith', @DefaultPassword, GETUTCDATE()),
(LOWER(LEFT(NEWID(), 130)), 'Alice', 'Johnson', 'alice.johnson@example.com', 'alicej', @DefaultPassword, GETUTCDATE()),
(LOWER(LEFT(NEWID(), 130)), 'Bob', 'Brown', 'bob.brown@example.com', 'bobbrown', @DefaultPassword, GETUTCDATE()),
(LOWER(LEFT(NEWID(), 130)), 'Charlie', 'Davis', 'charlie.davis@example.com', 'charlied', @DefaultPassword, GETUTCDATE());

DECLARE @IdentityId NVARCHAR(150) = '';

SELECT TOP 1 @IdentityId = Id FROM ApplicationUser;

INSERT INTO Department 
(Id, Name, Description, IsDeleted, CreatedDateTimeUTC, CreatedBy, LastModifiedDateTimeUTC, LastModifiedBy)
VALUES
(LOWER(LEFT(NEWID(), 130)), 'Development', 'Handles Development-related tasks', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId),
(LOWER(LEFT(NEWID(), 130)), 'Human Resources', 'Handles HR-related tasks', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId),
(LOWER(LEFT(NEWID(), 130)), 'Devops', 'Handles Devops-related tasks', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId);


INSERT INTO Position 
(Id, Name, Description, IsDeleted, CreatedDateTimeUTC, CreatedBy, LastModifiedDateTimeUTC, LastModifiedBy)
VALUES
(LOWER(LEFT(NEWID(), 130)), 'CEO', 'Run Company', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId),
(LOWER(LEFT(NEWID(), 130)), 'TPM', 'Manage Team', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId),
(LOWER(LEFT(NEWID(), 130)), 'TL', 'Help Team', 0, GETUTCDATE(), @IdentityId, GETUTCDATE(), @IdentityId);


DECLARE @DepartmentId NVARCHAR(150) = '';
DECLARE @PositionId NVARCHAR(150) = '';

SELECT TOP 1 @DepartmentId = Id FROM Department;
SELECT TOP 1 @PositionId = Id FROM Position;

INSERT INTO EmployeeDetail
(Id,FirstName,MiddleName,LastName,DateOfBirth,Email,Phone,DateOfJoining,Gender,
DepartmentId,PositionId,Status,IsDeleted,
CreatedDateTimeUTC,CreatedBy,LastModifiedDateTimeUTC,LastModifiedBy)
VALUES
(LOWER(LEFT(NEWID(), 130)),'Ram','Krishna','Mehta','1999-03-26','ram.mehta@google.com','8780343684','2021-01-01',1,
@DepartmentId,@PositionId,1,0,
GETUTCDATE(),@IdentityId,GETUTCDATE(),@IdentityId);

SELECT * FROM ApplicationUser
SELECT * FROM EmployeeDetail;
SELECT * FROM Department;
SELECT * FROM Position;