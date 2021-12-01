create database Passenger;

use Passenger;

create table Users
(
    Id        uniqueidentifier primary key not null,
    Email     nvarchar(100)                not null,
    Password  nvarchar(200)                not null,
    Salt      nvarchar(200)                not null,
    Username  nvarchar(200)                not null,
    Fullname  nvarchar(200),
    Role      nvarchar(10)                 not null,
    CreatedAt datetime                     not null,
    UpdatedAt datetime                     not null
)

delete from Users;