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
    @PayerIdentification varchar(13),
    @PayeeId int ,
    @PayeeAccount varchar(75),
    @PayeeIdentification varchar(13) ,
    @PayeeBankId int ,
    @TransactionTypeId int,
    @CreationDate datetime,
    @Number varchar(75),
    @Concept varchar(700),
    @Debit float,
    @Credit float,
    @CurrencyTypeId int,
    @Balance float,
    @ReferenceNumber varchar(75),
    @EffectiveDate datetime,
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
