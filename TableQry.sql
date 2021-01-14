CREATE TABLE accounts
(
    Id int identity(1,1) primary key,
    Alias varchar(75),
    Number varchar(50) not null,
    OwnerId int not null,
    AccountTypeId int not null,
    CreationDate datetime default getdate(),
    LastUpdate datetime,
    LastTransation datetime,
    AccountManagerId int,
    CurrencyTypeId int,
    StatusId int,
    Balance float
)
GO

CREATE PROCEDURE InsertOrUpdateAccount
(
    @Id int,
    @Alias varchar(75),
    @Number varchar(50),
    @OwnerId int ,
    @AccountTypeId int ,
    @LastUpdate datetime,
    @LastTransation datetime,
    @AccountManagerId int,
    @CurrencyTypeId int,
    @StatusId int,
    @Balance float,
    @NewId int output

)
As
if exists(select 1 from accounts where Number = @Number)
update accounts set Alias = @Alias,AccountTypeId=@AccountTypeId,
LastUpdate = @LastUpdate,LastTransation=@LastTransation,AccountManagerId=@AccountManagerId,
CurrencyTypeId=@CurrencyTypeId,StatusId=@StatusId,Balance=@Balance
where Number = @Number or Id = @Id
else
insert into accounts
(Alias,AccountTypeId,AccountManagerId,CurrencyTypeId,StatusId,Balance,Number)
values
(@Alias,@AccountTypeId,@AccountManagerId,@CurrencyTypeId,@StatusId,@Balance,@Number)
select @NewId = SCOPE_IDENTITY()
GO

CREATE PROCEDURE GetAccounts
(
    @Id int = null,
    @OwnerId int = null
)
AS
select * from accounts

CREATE TABLE cards
(
    Id int identity(1,1) not null primary key,
    OwnerId int not null,
    Number varchar(75),
    CreationDate datetime default Getdate(),
    CutOffDate datetime,
    PaymentLimitDate datetime,
    StatusId int,
    Limit float,
    CurrencyTypes int,
    Balance float
)
GO

CREATE PROCEDURE InsertOrUpdateCard
(
    @Id int,
    @OwnerId int ,
    @Number varchar(75),
    @CutOffDate datetime,
    @PaymentLimitDate datetime,
    @StatusId int,
    @Limit float,
    @CurrencyTypes int,
    @Balance float,
    @NewId int output
)
AS
if exists(select 1 from cards where Number = @Number)
update cards set CutOffDate = @CutOffDate,PaymentLimitDate=@PaymentLimitDate,
StatusId=@StatusId,Limit=@Limit,CurrencyTypes=@CurrencyTypes,Balance=@Balance
where Number=@Number or Id=@Id
else
insert into cards (Number,CutOffDate,PaymentLimitDate,StatusId,Limit,CurrencyTypes,Balance)
values
(@Number,@CutOffDate,@PaymentLimitDate,@StatusId,@Limit,@CurrencyTypes,@Balance)
select @NewId = SCOPE_IDENTITY()
GO

CREATE TABLE clients
(
    Id int identity(1,1) not null primary key,
    Name varchar(150) not null,
    LastName varchar(150) not null,
    IdentificationTypeId int,
    Identification varchar(13),
    Telephone varchar(13),
    Address varchar(700),
    GenderId int,
    CurrencyTypeId int,
    StatusId int
)
GO

CREATE PROCEDURE InsertOrUpdateClient
(
    @Id int,
    @Name varchar(150) ,
    @LastName varchar(150),
    @IdentificationTypeId int,
    @Identification varchar(13),
    @Telephone varchar(13),
    @Address varchar(700),
    @GenderId int,
    @StatusId int,
    @NewId int output
)
AS
if exists(select 1 from clients where Identification=@Identification)
update clients set Name = @Name,LastName=@LastName,IdentificationTypeId=@IdentificationTypeId,
Telephone=@Telephone,Address=@Address,GenderId=@GenderId,StatusId=@StatusId
where Identification = @Identification or Id=@Id
else
insert into clients
(Name,LastName,IdentificationTypeId,Identification,Telephone,Address,GenderId,StatusId)
values
(@Name,@LastName,@IdentificationTypeId,@Identification,@Telephone,@Address,@GenderId,@StatusId)
select @NewId = SCOPE_IDENTITY()
GO


CREATE TABLE transactions (
    Id int identity(1,1) primary key,
    PayerId int ,
    PayerAccount varchar(75),
    PayerIdentification varchar(13),
    PayeeId int not null,
    PayeeAccount varchar(75) not null,
    PayeeIdentification varchar(13) not null,
    PayeeBankId int not null,
    TransactionTypeId int,
    CreationDate datetime default getdate(),
    Number varchar(75),
    Concept varchar(700),
    Debit float,
    Credit float,
    CurrencyTypeId int,
    Balance float,
    ReferenceNumber varchar(75),
    EffectiveDate datetime,
    StatusId int
)
GO

CREATE PROCEDURE InsertOrUpdateTransaction
(
 
    @Id int ,
    @PayerId int ,
    @PayerAccount varchar(75),
    @PayerIdentification varchar(13)=null,
    @PayeeId int,
    @PayeeAccount varchar(75),
    @PayeeIdentification varchar(13) =null,
    @PayeeBankId int ,
    @TransactionTypeId int,
    @Number varchar(75),
    @Concept varchar(700)=null,
    @Debit float=null,
    @Credit float=null,
    @CurrencyTypeId int,
    @Balance float,
    @ReferenceNumber varchar(75)=null,
    @EffectiveDate datetime=null,
    @StatusId int,
    @NewId int output
)
as
if exists(select 1 from transactions where Number=@Number)
update transactions set StatusId = @StatusId
where Number = @Number or Id = @Id
else
insert into transactions
(Number,PayerId,PayerAccount,PayerIdentification,PayeeId,PayeeAccount,PayeeIdentification,
PayeeBankId,TransactionTypeId,Concept,Debit,Credit,CurrencyTypeId,Balance,ReferenceNumber,EffectiveDate,StatusId)
values
(@Number,@PayerId,@PayerAccount,@PayerIdentification,@PayeeId,@PayeeAccount,@PayeeIdentification,
@PayeeBankId,@TransactionTypeId,@Concept,@Debit,@Credit,@CurrencyTypeId,@Balance,@ReferenceNumber,@EffectiveDate,@StatusId)
select @NewId = SCOPE_IDENTITY()


CREATE TABLE banks
(
    Id int identity(1,1) primary key,
    Name varchar(200),
    Identification varchar(11)
)


CREATE Table transactiontypes
(
	Id int identity(1,1) primary key,
	Name varchar(50),
	Description varchar(200),
    StatusId int
)
GO



CREATE Table transactiontypes
(
	Id int identity(1,1) primary key,
	Name varchar(50),
	Description varchar(200),
    StatusId int
)
GO

ALTER PROCEDURE gettransactiontypes
(
    @Id int=null,
    @Name varchar(50)=null
)
AS
select 
        [Id] = transtype.Id
       ,[Name] = max(transtype.Name)
       ,[Description] = max(transtype.Description)
       ,[StatusId] = max(transtype.StatusId)
from [dbo].[transactiontypes] transtype
where ((@Id is null) or (transtype.Id= @Id)) 
group by transtype.Id
go


create TABLE [dbo].[users]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(100) NULL, 
    [UserName] varchar(50),
    [Password] VARCHAR(250) NULL, 
    [ClientId] INT NOT NULL,
    [StatusId] int
)
Go
create TABLE [dbo].[empusers]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(100) NULL, 
    [UserName] varchar(50),
    [Password] VARCHAR(250) NULL, 
    [EmployeeId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [StatusId] int
)
CREATE TABLE [dbo].[roles]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Description] VARCHAR(200) NULL,
    [StatusId] int
)
GO

ALTER PROCEDURE InsertOrUpdateUser
(
	@Id int,
	@UserName varchar(50),
	@Email varchar(100),
    @Password varchar(250),
    @ClientId int,
    @StatusId int,
	@NewId int output
)
AS 
if exists(select 1 from users where Id = @Id or ClientId = @ClientId)
update users set Email = @Email,Password = @Password, UserName = @UserName,StatusId = @StatusId
where Id = @Id or ClientId = @ClientId
else
insert into users
(UserName,Email,Password,ClientId,StatusId)
values
(@UserName,@Email,@Password,@ClientId,@StatusId)
select @NewId = SCOPE_IDENTITY()
GO

alter PROCEDURE getusers
(
    @Id int=null,
    @UserName varchar(50) =null,
    @Email varchar(100)=null,
    @ClientId int=null
)
AS
select 
        [Id] = usr.Id
       ,[UserName] = max(usr.UserName)
       ,[Email] = max(usr.Email)
       ,[Password] = max(usr.Password)
       ,[ClientId] = max(usr.ClientId)
       ,[StatusId] = max(usr.StatusId)
from [dbo].[users] usr
where ((@Id is null) or (usr.Id= @Id)) and
((@UserName is null) or (usr.UserName= @UserName)) and
((@Email is null) or (usr.Email= @Email)) and
((@ClientId is null) or (usr.ClientId= @ClientId))
group by usr.Id
go

alter PROCEDURE ValidateUserCredentials
(
    @UserName varchar(50),
    @Password varchar(250)
)
AS
if exists(select 1 from users where UserName = @UserName and Password = @Password and StatusId =1 )
select 
        [Id] = usr.Id
       ,[UserName] = max(usr.UserName)
       ,[Email] = max(usr.Email)
       ,[Password] = max(usr.Password)
       ,[ClientId] = max(usr.ClientId)
       ,[StatusId] = max(usr.StatusId)
from [dbo].[users] usr
where (usr.UserName= @UserName) and Password = @Password and StatusId = 1
group by usr.Id
go

alter PROCEDURE InsertOrUpdateEmpUser
(
	@Id int,
	@UserName varchar(50),
	@Email varchar(100),
    @Password varchar(250),
    @EmployeeId int,
    @RoleId int,
    @StatusId int,
	@NewId int output
)
AS 
if exists(select 1 from empusers where Id = @Id or EmployeeId = @EmployeeId)
update empusers set Email = @Email,Password = @Password, UserName = @UserName,RoleId = @RoleId,StatusId = @StatusId
where Id = @Id or EmployeeId = @EmployeeId
else
insert into empusers
(UserName,Email,Password,EmployeeId,RoleId,StatusId)
values
(@UserName,@Email,@Password,@EmployeeId,@RoleId,@StatusId)
select @NewId = SCOPE_IDENTITY()
GO

alter PROCEDURE GetEmpUsers
(
    @Id int=null,
    @UserName varchar(50) =null,
    @Email varchar(100)=null,
    @EmployeeId int=null,
    @RoleId int = null
)
AS
select 
        [Id] = usr.Id
       ,[UserName] = max(usr.UserName)
       ,[Email] = max(usr.Email)
       ,[Password] = max(usr.Password)
       ,[EmployeeId] = max(usr.EmployeeId)
       ,[RoleId] = max(usr.RoleId)
       ,[StatusId] = max(usr.StatusId)
from [dbo].[empusers] usr
where ((@Id is null) or (usr.Id= @Id)) and
((@UserName is null) or (usr.UserName= @UserName)) and
((@Email is null) or (usr.Email= @Email)) and
((@EmployeeId is null) or (usr.EmployeeId= @EmployeeId)) and
((@RoleId is null) or (usr.RoleId = @RoleId))
group by usr.Id
go

alter PROCEDURE ValidateEmpUserCredentials
(
    @UserName varchar(50),
    @Password varchar(250)
)
AS
if exists(select 1 from empusers where UserName = @UserName and Password = @Password and StatusId = 1 )
select 
        [Id] = usr.Id
       ,[UserName] = max(usr.UserName)
       ,[Email] = max(usr.Email)
       ,[Password] = max(usr.Password)
       ,[EmployeeId] = max(usr.EmployeeId)
       ,[RoleId] = max(usr.RoleId)
       ,[StatusId] = max(usr.StatusId)
from [dbo].[empusers] usr
where (usr.UserName= @UserName and usr.Password = @Password and StatusId = 1)
group by usr.Id
go

