use WrightWayRestaurant
go


create table SystemUser
( 	
    UserId uniqueidentifier primary key default newid(),
	UserName varchar(50),
	[Password] varchar(100),
	PhoneNo varchar(50),
	Email varchar(50),
	CreateTime datetime default getdate()
)
go

create table Customer
(
	CustomerId uniqueidentifier primary key default newid(),
	CustomerName varchar(50),
	[Password] varchar(100),
	PhoneNo varchar(50),
	Email varchar(50),
	CreateTime datetime default getdate()
)

create table FoodType
( 	
    TypeId int identity(1,1) primary key,
	TypeName varchar(50),
	UserId uniqueidentifier Constraint FK_FoodType_UserId Foreign Key(UserId) References SystemUser(UserId)
)	
go


create table Food
(
    FoodId int identity(1,1) primary key,
	FoodName varchar(50),
	TypeId int Constraint FK_TypeId Foreign Key(TypeId) References FoodType(TypeId),
	Price money,
	Stock int,
	Foodimg varchar(100),
	FoodState varchar(50),
	CreateTime datetime default getdate(),
	UserId uniqueidentifier Constraint FK_Food_UserId Foreign Key(UserId) References SystemUser(UserId)
)
go


create table OrderState	
(
    StateId int identity(1,1) primary key,
	StateName varchar(50),
	UserId uniqueidentifier Constraint FK_OrderState_UserId Foreign Key(UserId) References SystemUser(UserId)
)
go
	
create table [Order]
(
    OrderId int identity(1,1) primary key,
	CustomerId uniqueidentifier Constraint FK_CustomerId Foreign Key(CustomerId) References Customer(CustomerId),
	ReserveTime datetime default getdate(),
	CreateTime datetime default getdate(),
	ModifyTime datetime default getdate(),
	OrderState varchar(50),
	StateId int Constraint FK_StateId Foreign Key(StateId) References OrderState(StateId),
	UserId uniqueidentifier Constraint FK_Order_UserId Foreign Key(UserId) References SystemUser(UserId)
)
go

	
create table OrderDetail
(	
    DetailId int identity(1,1) primary key,
	OrderId int Constraint FK_OrderId Foreign Key(OrderId) References [Order](OrderId),
	FoodId int Constraint FK_FoodId Foreign Key(FoodId) References [Food](FoodId),
	Number int	
)	
go

create table EmailConfig
(
	 ConfigId int identity(1,1) primary key,
	 EmailType varchar(50),
	 EmailAcount varchar(100),
     EmailPassword varchar(100),
     EmailHost varchar(100),
     EmailPort int,
	 EmailState bit default 1,
	 Sort int,
	 UserId uniqueidentifier Constraint FK_EmailConfig_UserId Foreign Key(UserId) References SystemUser(UserId)
)
go
