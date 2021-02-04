create database Gestor_Reuniones2
use Gestor_Reuniones2
/************************************************************T_Puesto************************************************************/
create table T_Puesto(
	TN_Id_Puesto int identity(1,1) primary key,
	TC_Nombre_Puesto varchar(100) not null unique,
	TN_Salario float not null
)
/************************************************************T_Circuito************************************************************/
create table T_Circuito(
	TN_Id_Circuito int identity(1,1) primary key,
	TC_Des_Circuito varchar(100) not null
)

/************************************************************T_Oficina************************************************************/
create table T_Oficina(
	TN_Id_Oficina int identity(1,1) primary key,
	TC_Nombre varchar(100) not null unique,
	TC_Codigo varchar(100) not null unique,
	TN_Id_Circuito int not null,
	TB_Activa bit not null default 1,
	TF_Inicio_Vigencia date not null,
	TF_Fin_Vigencia date,
	foreign key (TN_Id_Circuito) references T_Circuito (TN_Id_Circuito)
)

/************************************************************T_Permiso************************************************************/
create table T_Permiso(
	TN_Id_Permiso int identity(1,1) primary key,
	TC_Nombre_Permiso varchar(50) not null unique
)

/************************************************************T_Rol************************************************************/
create table T_Rol(
	TN_Id_Rol int identity(1,1) primary key,
	TC_Nombre_Rol varchar(50) not null unique,
	TN_Id_Permiso int,
	foreign key (TN_Id_Permiso) references T_Permiso (TN_Id_Permiso)
)

/************************************************************T_Usuario************************************************************/
create table T_Usuario(
	TN_Id_Usuario int identity(1,1) primary key,
	TC_Usuario varchar(100) not null unique,
	TC_Contrasenna varbinary(500) not null,
	TC_Identificacion varchar(20) not null unique,
	TC_Nombre_Usuario varchar(100) not null,
	TC_Primer_Apellido varchar(100) not null,
	TC_Segundo_Apellido varchar(100) not null,
	TC_Correo varchar(100) not null unique,
	TB_Activo bit not null default 1,
	TN_Id_Puesto int not null,
	TN_Id_Oficina int not null,
	TB_Eliminado bit not null default 0,
	foreign key (TN_Id_Puesto) references T_Puesto (TN_Id_Puesto),
	foreign key (TN_Id_Oficina) references T_Oficina (TN_Id_Oficina)
)

/************************************************************T_Usuario_Rol************************************************************/
create table T_Usuario_Rol(
	TN_Id_Usuario_Rol int identity(1,1) primary key,
	TN_Id_Rol int not null,
	TN_Id_Usuario int not null,
	foreign key (TN_Id_Rol) references T_Rol (TN_Id_Rol),
	foreign key (TN_Id_Usuario) references T_Usuario (TN_Id_Usuario)
)

/************************************************************T_Tipo_Reunio************************************************************/
create table T_Tipo_Reunion(
	TN_Id_Tipo_Reunion int identity(1,1) primary key,
	TC_Nombre_Tipo_Reunion varchar(50) not null unique
)

/************************************************************T_Reunion************************************************************/
create table T_Reunion(
	TN_Id_Reunion int identity(1,1) primary key,
	TC_Nombre_Reunion varchar(100) not null,
	TN_Id_Tipo_Reunion int not null,
	TC_Descripcion varchar(200) not null,
	TC_Comentario varchar(200) not null,
	TC_Lugar varchar(100) not null,
	TC_Fecha_Inicio datetime not null,
	TC_Fecha_Final datetime  default null,
	TN_Id_Usuario int not null,
	TN_Finalizada bit not null default 0
	foreign key (TN_Id_Tipo_Reunion) references T_Tipo_Reunion (TN_Id_Tipo_Reunion),
	foreign key (TN_Id_Usuario) references T_Usuario(TN_Id_Usuario)
)

/************************************************************T_Temas************************************************************/
create table T_Temas(
	TN_Id_Temas int identity(1,1) primary key,
	TN_Id_Reunion int not null,
	TC_Nombre_Tema varchar(100) not null,
	TC_Acuerdo varchar(max) not null,
	foreign key (TN_Id_Reunion) references T_Reunion (TN_Id_Reunion)
)

/************************************************************T_Archivo_Reunion************************************************************/
create table T_Archivo_Reunion(
	TN_Id_Archivo int identity(1,1) primary key,
	TN_Id_Reunion int not null,
	TC_Link varchar(100) not null,
	foreign key (TN_Id_Reunion) references T_Reunion (TN_Id_Reunion)
)

/************************************************************T_Tarea************************************************************/
create table T_Tarea(
	TN_Id_Tarea int identity(1,1) primary key,
	TC_Nombre_Tarea varchar(100) not null unique,
	TC_Descripcion varchar(200) not null,
	TB_Estado bit not null default 1,
	TC_Acuerdo varchar(max) not null
)

/************************************************************T_Tarea_Usuario************************************************************/
create table T_Tarea_Usuario(
	TN_Id_Tarea_Usuario int identity(1,1) primary key,
	TN_Id_Tarea int not null,
	TN_Id_Usuario int not null,
	foreign key (TN_Id_Usuario) references T_Usuario (TN_Id_Usuario),
	foreign key (TN_Id_Tarea) references T_Tarea (TN_Id_Tarea)
)

/************************************************************T_Lista_Tareas************************************************************/
create table T_Lista_Tareas(
	TN_Id_Lista_Tareas int identity(1,1) primary key,
	TN_Id_Tarea int not null,
	TN_Id_Reunion int not null,
	foreign key (TN_Id_Tarea) references T_Tarea (TN_Id_Tarea),
	foreign key (TN_Id_Reunion) references T_Reunion (TN_Id_Reunion)
)

/************************************************************T_Agenda************************************************************/
create table T_Agenda(
	TN_Id_Agenda int identity(1,1) primary key,
	TN_Id_Usuario int not null,
	TN_Id_Reunion int not null,
	TN_Asistencia bit not null default 0,
	foreign key (TN_Id_Usuario) references T_Usuario (TN_Id_Usuario),
	foreign key (TN_Id_Reunion) references T_Reunion (TN_Id_Reunion)
)