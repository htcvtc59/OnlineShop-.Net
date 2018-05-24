create database OnlineShop
go 
use OnlineShop
go
create table ProductCategory
(
ID bigint identity primary key,
Name nvarchar(250),
MetaTitle varchar(250),
ParentID bigint,
DisplayOrder int default 0,
SeoTitle nvarchar(250),
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
MetaKeywords nvarchar(250),
MetaDescriptions nchar(250),
Status bit not null default 1,
ShowOnHome bit default 0
)
go
select * from ProductCategory
go
create table Product
(
ID bigint identity primary key ,
Name nvarchar(250),
Code varchar(20),
MetaTitle varchar(250),
Description ntext,
Images nvarchar(250),
MoreImages xml,
Price decimal(18,0) default 0,
Quantity int default 0,
CategoryID bigint,
Detail ntext,
Warranty int,
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
MetaKeywords nvarchar(250),
MetaDescriptions nchar(250),
Status bit not null default 1,
TopHot datetime
)
go
select * from Product
go
create table Category
(
ID bigint identity primary key,
Name nvarchar(250),
MetaTitle varchar(250),
ParentID bigint,
DisplayOrder int default 0,
SeoTitle nvarchar(250),
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
MetaKeywords nvarchar(250),
MetaDescriptions nchar(250),
Status bit not null default 1,
ShowOnHome bit default 0
)
go
create table Content
(
ID bigint identity primary key ,
Name nvarchar(250),
MetaTitle varchar(250),
Description nvarchar(550),
Images nvarchar(250),
CategoryID bigint,
Detail ntext,
Warranty int,
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
MetaKeywords nvarchar(250),
MetaDescriptions nchar(250),
Status bit not null default 1,
TopHot datetime
)
go 
create table Feedback
(
ID int identity primary key,
Name nvarchar(50),
Phone nvarchar(50),
Email nvarchar(50),
Address nvarchar(50),
Content nvarchar(250),
CreatedDate datetime default getdate(),
Status bit not null default 1
)
go
create table Slide
(
ID int identity primary key,
Image nvarchar(250),
DisplayOrder int default 1,
Link nvarchar(250),
Description nvarchar(50),
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
Status bit not null default 1

)
go

create table Ads
(
ID int identity primary key,
Image nvarchar(250),
DisplayOrder int default 1,
Link nvarchar(250),
Description nvarchar(50),
CreatedDate datetime default getdate(),
CreatedBy varchar(50),
ModifiedDate datetime,
ModifiedBy varchar(50),
Status bit not null default 1

)
go

create table MenuType
(
ID int identity primary key,
Name nvarchar(50)
)
go
create table Menu
(
ID int identity primary key,
Text nvarchar(50),
Link nvarchar(250),
DisplayOrder int ,
Target nvarchar(50),
Status bit not null default 1,
TypeID int 
)

go
create table Footer
(
ID varchar(50) primary key,
Content ntext,
Status bit not null default 1
)
go
create table SystemConfig
(
ID varchar(50),
Name nvarchar(50),
Type varchar(50),
Value nvarchar(250),
Status bit not null default 1 
)
go
create table OrderBy
(
ID bigint identity primary key,
CreatedDate datetime default getdate(),
CustomerID bigint,
ShipName nvarchar(50),
ShipMobile varchar(50),
ShipAddress nvarchar(250),
ShipEmail nvarchar(100),
Status bit not null default 1
)
go
create table OrderDetail
(
ID bigint identity primary key,
ProductID bigint not null,
OrderID bigint not null,
Quantity int default 0,
Price decimal default 0,
Status bit not null default 1
)

go
Create table Bill
(
ID bigint identity primary key,
ClientID varchar(50),
CreatedDate datetime default getdate(),
ProductID bigint,
Quantity int default 0,
Price decimal default 0,
TotalPrice decimal default 0,
Status bit not null default 1
)
go
create table Account
(
ID bigint identity primary key ,
RealName nvarchar(100) ,
UserName nvarchar(100) ,
Password varchar(50),
Email varchar(100) ,
LevelUser varchar(100) default 'client',
Status bit not null default 1
)
go
truncate table Account
delete from Account
select * from Account
go
select * from ProductCategory
go
select * from Menu
go
select * from Category
go
select * from Content
go
select * from Product
go
select * from OrderDetail
go
select * from OrderBy
go

select COUNT(ProductID)as Abc,ProductID from OrderDetail group by ProductID 

insert into Account(RealName,UserName,Password,Email) 
values (N'Nguyễn Đức Trí','tri','tri123','tri@tgmail.com')
go
insert into Account(RealName,UserName,Password,Email) 
values (N'Nguyễn Đức Trí','tri','b85593ca6abda3f203e0af8239beb228','tri@tgmail.com')
go
insert into Account(RealName,UserName,Password,Email,LevelUser) 
values(N'Đinh Văn Trơn','admin','4a1b9957d604c17c8e2490afc75aa6d8','htcvtc59@gmail.com','admin'),
(N'Đinh Bảo','bao','bao123','bao@gmail.com','client'),
(N'Đinh Tiến','tien','tien213','tien@gmail.com','client'),
(N'Vũ Lê Minh','minh','minh123','minh@gmail.com','client');
go
--values(N'Đinh Văn Trơn','admin','230697','htcvtc59@gmail.com','admin'),
--http://www.md5.cz/ 