create database dbConocimiento;

use dbConocimiento;

create table Animado (
	Id_Obj int not null,
	Estado varchar(20) null
);

create table Color (
	Id_Obj int not null,
	Color varchar(40) not null
);

create table Genero (
	Id_Obj int not null,
	Genero varchar(3) null
);

create table Objecto (
	Id_Obj int not null,
	Nombre varchar(50) not null,
	Definicion text not null
);