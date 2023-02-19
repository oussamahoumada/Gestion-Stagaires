create database [Gestion Stagaires]
go 
use [Gestion Stagaires]
go
create table [Services]
(
Numéro int primary key,
Nom varchar(100)
)
go
insert into [Services] values (0,null)
go
go
create table Responsables
(
CIN varchar(100) primary key,
Nom varchar(100),
Prenom varchar(100),
Tel varchar(100),
Email varchar(100),
[R.Service] int,
constraint fk1 foreign key ([R.Service]) references [Services](Numéro) on update cascade on delete cascade
)
go
create table Villes
(
Id_V int primary key identity,
Nom varchar(100),
CodPostale varchar(100)
)
go
create table Etablissements
(
Id_E int primary key identity,
Nom varchar(100),
Adresse varchar(100),
ville int foreign key references Villes(Id_V) on update cascade on delete cascade
)
go
create table Spécialité 
(
Id_S int primary key identity,
Nom varchar(100),
Etab int foreign key references Etablissements(Id_E) on update cascade on delete cascade
)
go
create table Profiles
(
Id_P int primary key identity,
 Libelle varchar(100),
CV Varchar (200),
Experience varchar(200),
Compétence varchar(200),
ProjetRealisé varchar(200),
Spés int foreign key references Spécialité(Id_S) on update cascade on delete cascade
)
go
create table Stagaires
(
CIN varchar(100) primary key,
Nom varchar(100),
Prenom varchar(100),
Email varchar(100),
Tel varchar(100),
[Date Debut] date,
Durée varchar(50),
Sujet  varchar(100),
Rapport varchar(100),
Depos_Rappoert bit,
Note float,
[S.Service] int,
constraint fk2 foreign key ([S.Service]) references [Services](Numéro) on update cascade on delete cascade,
Profil int foreign key references Profiles(Id_P) on update cascade on delete cascade
)
go
create table Administrateur
(
Email varchar(100) primary key,
MotdePass varchar(100)
)

select *from Stagaires


/* n9adro nstaghnaw aliha hna fhad projet yamkan 3la hsab mabanli ???
create table liaison
(
id int primary key identity,
 Etab int foreign key references Etablissements(Id_E),
 Spés int foreign key references Spécialité(Id_S)
)
go
*/