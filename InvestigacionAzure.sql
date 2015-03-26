--Instituto Tecnologico de Costa Rica
--Bases de Datos II - Investigacion SQL Azure
--Allan Rojas Duran 200941386
--allanjose91@gmail.com

use _practica
--idDepto, nbrDepto, idEncargado, ubicación.
Create table Departments
(
     IdDepto int primary key identity,
     nbrDepto nvarchar(50),
     idEncargado int,
     ubicacion nvarchar(50)
)
GO
--drop table Departments;


--Drop TABLE Employees;
--idEmpleado, nbrEmpleado, idDepto a que pertenece, fecha de ingreso, foto.
Create table Employees
(
     IdEmpleado int primary key identity,
     nbrEmpleado nvarchar(50),
     IdDepto int foreign key references Departments(IdDepto),
	 fechaIngreso nvarchar(45) ,
	 fotoStr nvarchar(100)
	
)
GO

go
Alter Table Employees
 ADD CONSTRAINT fk_Employees Foreign Key (IdDepto) References Departments(IdDepto)
  ON DELETE Cascade ON UPDATE Cascade
	
go
Insert into Departments values (1, 'New York',206, 'New York')
Insert into Departments values (2,'HR', 110 ,'London')
Insert into Departments values (3,'Payroll', 199,'Sydney')
GO

Insert into Employees values ('Mark',4, 'Hastings', 'Male')
Insert into Employees values ('Steve', 2,'Pound', 'Male')
Insert into Employees values ('Ben', 2,'Hoskins', 'Male')
Insert into Employees values ('Philip',4, 'Hastings', 'Male')
Insert into Employees values ('Mary',2, 'Lambeth', 'Female')
Insert into Employees values ('Valarie',2, 'Vikings', 'Female')
Insert into Employees values ('John', 4,'Stanmore', 'Male')
GO




select * from Employees

go

create procedure listarxDeptoNmbre 
 @dpto varchar(50)
as
  select e.nbrEmpleado , e.IdEmpleado , e.fechaIngreso
  from Employees e Inner Join Departments d on
         e.IdDepto = d.IdDepto

  where d.nbrDepto = @dpto

go
--listarxDeptoNmbre
create procedure listarxDeptoId
 @dpto int
 as
 select e.nbrEmpleado , e.IdEmpleado , e.fechaIngreso
  from Employees e Inner Join Departments d on
         e.IdDepto = d.IdDepto

  where d.IdDepto = @dpto
  go

go

--listarxDeptoId 2


-- Delete From Departments where IdDepto = 3

Update Departments
SET IdDepto = 4
Where IdDepto = 5



select count(*)  from Departments ;
select * from Employees

go
--drop Procedure EmpxDep;
go
Create Procedure EmpxDep 
@dpto int
As
   
 select count(*) as Total
  from Employees e Inner Join Departments d on
         e.IdDepto = d.IdDepto

  where d.IdDepto = @dpto
go

Create Procedure TotalEmp

As
   
 select count(*) as Total
  from Employees 

go

exec totalEmp 

go
listarxDeptoId 4
go

exec EmpxDep 4

go
--listarxDeptoNmbre
create procedure listarxDeptoI
 @dpto int
 as
 select e.nbrEmpleado , e.IdEmpleado , e.fechaIngreso
  from Employees e Inner Join Departments d on
         e.IdDepto = d.IdDepto

  where d.IdDepto = @dpto
  go

go

--select count(*) listarxDeptoId 4