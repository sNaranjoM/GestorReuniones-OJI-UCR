use Gestor_Reuniones2
/***************************************************DATOS OBLIGATORIOS PARA EL FUNCIONAMIENTO BASE DE LA APLICACIÓN***************************************************/

/******Se debe crear el primer circuito al que pertenece la primer oficina del sistema a la cual va a pertenercer el primer empleado que se encargara de crear los catalogos*******/
insert into T_Circuito(TC_Des_Circuito) values('01');
/*******Se debe crear una oficina, los datos que se ingresan son el nombre, codigo, id del circuito, fecha de incio de vigencia*******/
exec Sp_Insert_Oficina 'Planes y Operaciones','PO',1,'2020-11-13'
/*******Estos son los permisos que se asignan a los deferentes roles en el sistema, estos permisos estan quemados en el sistema por lo cual si se cambian se debe de cambiar en el sistema*******/
insert into T_Permiso(TC_Nombre_Permiso) values ('Acceso total');
insert into T_Permiso(TC_Nombre_Permiso) values ('Acceso estandar');
insert into T_Permiso(TC_Nombre_Permiso) values ('Administardor catalogos');
/*******Se debe crear un rol, en este caso se recomienda la creacion de un usuario 'Administardor catalogos' para que este pueda ingrasar todos los datos que ocupa el sistema para funcionar*******/
exec Sp_Insert_Rol 'Jefe',1
exec Sp_Insert_Rol 'Usuarios estandar',2
select*from T_Rol
/*******Se debe crear un puesto al cual va a pertener el primer usuario del sistema, para el cual se especifica el nombre y el precio del salario por hora*******/
exec Sp_Insert_Puesto 'Ing. Sistemas',3500
exec Sp_Insert_Puesto 'Administrados DB',3500
select*from T_Puesto
/*******Se debe crear un Usuario, para esto se especifica los datos en el siguiente orden,
usuario,contraseña,identificacion,nombre, primer apellido, segundo apellido, correo, id del puesto,id de la oficina, id del rol*******/
exec Sp_Insert_Usuario 'Admin','admin123','301200164','Juan','Perez','Abarca','juan.perez@gmail.com',1,1,1
exec Sp_Insert_Usuario 'Usuario','usuario123','302450210','Maria','Romero','Sanchez','maria.romero@gmail.com',2,1,2
select*from T_Usuario
/***************************************************DATOS NO OBLIGATORIOS PARA EL FUNCIONAMIENTO BASE DE LA APLICACIÓN, SERIA SOLO COMO DATOS DE PRUEBA***************************************************/
/*******Se debe de crear un tipo de reunion para este caso se inserta el nombre del tipo nada mas*******/
exec Sp_Insert_Tipo_Reunion 'Control'
select*from T_Tipo_Reunion
/*******Se debe de crear la tarea, para esto se debe especificar el nombre de la tarea, la descripcion y la lista de los usuarios, para esta lista se pasan los id's de los usuarios separados por una ","*******/
exec Sp_Insert_Tarea 'Verificacion de avance en diagramas','Se va a verificar de los avances hechos sobre los diagramas','1,2'
exec Sp_Insert_Tarea 'Verificacion de trabajo','Se desea verificar el trabajo de cada inegrante','1,2'
select*from T_Tarea
select*from T_Tarea_Usuario
/*******Se debe crear la reunion para esto se debe especificar los siguientes datos: nombre de la reunion, id del tipo de reunion, descripcion, comentarios, lugar, fecha y hora de inicio en formato datetime,
lista de id's de usuarios agregardos a la reunion separados por ",", lista de temas de la reunion separados por "&",lista de id's de la tareas de la reunion separadas por",", lista de los nombres de los archivos
separadas por "?" y por ultimo el id del usuario que esta creando la reunion*******/
exec Sp_Insert_Reunion 'Reunion Uno',1,'Descripcion de la reunion uno','Comentario de la reunion uno','Lugar de la reunion uno','2020-10-15T13:00:00','1,2','Tema1&Tema2','1,2','archivoUno.pdf?archivo2.pdf','Admin'

/*******Se actualizan dichas tablas para poder semejar cuando una reunion se finalizado, en este caso se coloca un acuerdo generico sobre todas las tareas y los temas, aparte se coloca que todos asistieron a la reunion
y se define a que hora termino para poder calcular los diferentes valores en los dashboard y ver poder ver la minuta de esta reunion*******/
update T_Tarea set TC_Acuerdo='aqui van los acuerdos de las tareas'
update T_Temas set TC_Acuerdo='aqui van los acuerdos escritos'  where TN_Id_Reunion=1
update T_Agenda set TN_Asistencia=1 where TN_Id_Reunion=1
update T_Reunion set TN_Finalizada=1,TC_Fecha_Final='2020-10-15T16:00:00.000' where TN_Id_Reunion=1

