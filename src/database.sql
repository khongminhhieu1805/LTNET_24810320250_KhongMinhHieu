-- 1. Tạo Database
CREATE DATABASE QuanLyDaoTao;
GO

USE QuanLyDaoTao;
GO

-- 2. Tạo bảng Users cho Bài 4.1 (Login Form)
CREATE TABLE Users (
    Username NVARCHAR(50) PRIMARY KEY,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100)
);

-- Thêm tài khoản mẫu để test đăng nhập
INSERT INTO Users (Username, Password, FullName) 
VALUES ('hieu', '180506', N'Khổng Minh Hiếu');

-- 3. Tạo bảng Courses cho Bài 4.2 (Khóa học nạp vào ComboBox)
CREATE TABLE Courses (
    CourseID NVARCHAR(20) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL
);

INSERT INTO Courses (CourseID, CourseName) VALUES 
('HV01', N'Khổng Minh Hiếu'),
('HV02', N'Trần Thị Bích Ngọc'),
('HV03', N'Khổng Khánh My');

-- 4. Tạo bảng Students cho Bài 4.2 (Lưu thông tin đăng ký học viên)
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100),
    Gender NVARCHAR(10),
    BirthDate DATE,
    Phone NVARCHAR(20),
    CourseID NVARCHAR(20),
    IsMember BIT,
    RegistrationDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
GO

SELECT*FROM Users;
SELECT*FROM Courses;