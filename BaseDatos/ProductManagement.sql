create database ProductManagement;
GO
CREATE TABLE Products
(
  ProductId int primary key identity(1,1),
  Name varchar(60),
  StatusName  varchar(30),
  Stock decimal not null,
  Description varchar(100),
  Price decimal not null,
  Discount decimal not null,
  FinalPrice decimal not null,
  DateRegistration Datetime,
  DateUpdate Datetime
);