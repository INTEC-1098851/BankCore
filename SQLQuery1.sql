CREATE PROCEDURE getaccounts
(
    @AccountId int = null,
    @AccountNumber varchar(50),
    @ClientId int = null,
    @CurrencyTypeId int=null
)
AS
select 
        [Id] = acc.Id
       ,[Alias] =  max(acc.Alias)
       ,[Number] = max(acc.Number)
       ,[OwnerId] = max(acc.OwnerId)
       ,[AccountTypeId] = max(acc.AccountTypeId)
       ,[CreationDate] = max(acc.CreationDate)
       ,[LastUpdate] = max(acc.LastUpdate)
       ,[LastTransaction] = max(acc.LastTransation)
       ,[AccountManagerId] = max(acc.AccountManagerId)
       ,[CurrencyTypeId] = max(acc.CurrencyTypeId)
       ,[StatusId] = max(acc.StatusId)
       ,[Balance] = max(acc.Balance)
from [dbo].[accounts] acc 
where ((@AccountId is null) or (acc.Id= @AccountId)) and
((@ClientId is null) or (acc.OwnerId= @ClientId)) and
((@CurrencyTypeId is null) or (acc.CurrencyTypeId= @CurrencyTypeId)) and
((@AccountNumber is null) or (acc.Number= @AccountNumber))
group by acc.Id
go

CREATE PROCEDURE getbanks
(
    @Id int = null,
    @Name varchar(200)
)
AS
select 
        [Id] = bank.Id
       ,[Name] =  max(bank.Name)
       ,[Identification] =  max(bank.Identification)
from [dbo].[banks] bank 
where ((@Id is null) or (bank.Id= @Id)) and
((@Name is null) or (bank.Name= @Name))
group by bank.Id
go


CREATE PROCEDURE getcards
(
    @Id int = null,
    @ClientId int,
    @CardNumber varchar(50)
)
AS
select 
        [Id] = cards.Id
       ,[OwnerId] =  max(cards.OwnerId)
       ,[Number] =  max(cards.Number)
       ,[CreationDate] =  max(cards.CreationDate)
       ,[CutOffDate] =  max(cards.CutOffDate)
       ,[PaymentLimitDate] =  max(cards.PaymentLimitDate)
       ,[StatusId] =  max(cards.StatusId)
       ,[Limit] =  max(cards.Limit)
       ,[CurrencyTypes] =  max(cards.CurrencyTypes)
       ,[Balance] =  max(cards.Balance)
from [dbo].[cards] cards 
where ((@Id is null) or (cards.Id= @Id)) and
((@ClientId is null) or (cards.OwnerId= @ClientId)) and
((@CardNumber is null) or (cards.Number= @CardNumber))
group by cards.Id
go

CREATE PROCEDURE getclients
(
    @Id int,
    @Identification varchar(13)
)
AS     
select 
        [Id] = client.Id
       ,[Name] =  max(client.Name)
       ,[LastName] =  max(client.LastName)
       ,[IdentificationTypeId] =  max(client.IdentificationTypeId)
       ,[Identification] =  max(client.Identification)
       ,[Telephone] =  max(client.Telephone)
       ,[Address] =  max(client.Address)
       ,[GenderId] =  max(client.GenderId)
       ,[CurrencyTypeId] =  max(client.CurrencyTypeId)
       ,[StatusId] =  max(client.StatusId)
from [dbo].[clients] client 
where ((@Id is null) or (client.Id= @Id)) and
((@Identification is null) or (client.Identification= @Identification)) 
group by client.Id
go



CREATE PROCEDURE gettransactions
(
    @Id int,
    @PayerId int,
    @PayerAccount varchar(75),
    @PayerIdentification varchar(13),
    @PayeeId int,
    @PayeeAccount varchar(75),
    @PayeeIdentification varchar(13),
    @TransactionNumber varchar(75),
    @ReferenceNumber varchar(75),
    @CreationDateFrom datetime,
    @CreationDateTo datetime,
    @EffectiveDateFrom datetime,
    @EffectiveDateTo datetime
)
AS     

select 
        [Id] = transactions.Id
       ,[PayerId] =  max(transactions.PayerId)
       ,[PayerAccount] =  max(transactions.PayerAccount)
       ,[PayerIdentification] =  max(transactions.PayerIdentification)
       ,[PayeeId] =  max(transactions.PayeeId)
       ,[PayeeAccount] =  max(transactions.PayeeAccount)
       ,[PayeeIdentification] =  max(transactions.PayeeIdentification)
       ,[PayeeBankId] =  max(transactions.PayeeBankId)
       ,[TransactionTypeId] =  max(transactions.TransactionTypeId)
       ,[CreationDate] =  max(transactions.CreationDate)
       ,[Number] =  max(transactions.Number)
       ,[Concept] =  max(transactions.Concept)
       ,[Debit] =  max(transactions.Debit)
       ,[Credit] =  max(transactions.Credit)
       ,[CurrencyTypeId] =  max(transactions.CurrencyTypeId)
       ,[Balance] =  max(transactions.Balance)
       ,[ReferenceNumber] =  max(transactions.ReferenceNumber)
       ,[EffectiveDate] =  max(transactions.EffectiveDate)
       ,[StatusId] =  max(transactions.StatusId)
from [dbo].[transactions] transactions 
where ((@Id is null) or (transactions.Id= @Id)) and
((@PayerId is null) or (transactions.PayerId= @PayerId)) and 
((@PayerAccount is null) or (transactions.PayerAccount= @PayerAccount)) and 
((@PayerIdentification is null) or (transactions.PayerIdentification= @PayerIdentification)) and 
((@PayeeId is null) or (transactions.PayeeId= @PayeeId)) and 
((@PayeeAccount is null) or (transactions.PayeeAccount= @PayeeAccount)) and 
((@PayeeIdentification is null) or (transactions.PayeeIdentification= @PayeeIdentification)) and 
((@TransactionNumber is null) or (transactions.Number= @TransactionNumber)) and 
((@ReferenceNumber is null) or (transactions.ReferenceNumber= @ReferenceNumber)) and
((@CreationDateFrom is null or @CreationDateTo is null) or (transactions.CreationDate between @CreationDateFrom and @CreationDateTo)) and
((@EffectiveDateFrom is null or @EffectiveDateTo is null) or (transactions.EffectiveDate between @EffectiveDateFrom and @EffectiveDateTo))
group by transactions.Id