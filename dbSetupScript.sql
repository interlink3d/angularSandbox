
/*
1. create DB
create database AngularSandbox

2. create tables
create table dbo.Department(
DepartmentId int identity(1,1),
DepartmentName varchar(500)
)

create table dbo.Employee(
EmployeeId int identity(1,1),
EmployeeName varchar(500),
Department varchar(500),
DateOfJoining date,
PhotoFileName varchar(500)
)

3. verify existence of new tables
select * from dbo.Department
select * from dbo.Employee

4. insert values
insert into dbo.Department values 
('SUPPORT')

insert into dbo.Employee values
('Edgar','IT','2021-10-01', 'anonymous.png')

5. verify existence of new values added
select * from dbo.Department
select * from dbo.Employee

*/


