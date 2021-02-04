use Gestor_Reuniones2
/******************************Insert T_Rol******************************/
create procedure Sp_Insert_Rol
@TC_Nombre_Rol varchar(50),
@TN_Id_Permiso int
as begin
begin try
	begin tran Insert_Rol
	if not exists(select*from T_Rol where TC_Nombre_Rol=@TC_Nombre_Rol)begin
		if exists(select*from T_Permiso where TN_Id_Permiso=@TN_Id_Permiso)begin
			insert into T_Rol(TC_Nombre_Rol,TN_Id_Permiso) values (@TC_Nombre_Rol,@TN_Id_Permiso);
			select 1 as valido;
		end else begin
			select 0 as valido;
		end
		select 0 as valido;
	end else begin
		select 0 as valido
	end
	commit tran Insert_Rol
end try
begin catch
	rollback tran Insert_Rol
	select 0 as valido;
end catch
end
go

/******************************Delete T_Rol******************************/
create procedure Sp_Delete_Rol
	@TN_Id_Rol int
as begin
begin try
	begin tran Delete_Rol
	if  exists(select*from T_Rol where TN_Id_Rol=@TN_Id_Rol)begin
		delete from T_Rol where TN_Id_Rol=@TN_Id_Rol;
		select 1 as valido;
	end else begin
		select 0 as valido
	end
	commit tran Delete_Rol
end try
begin catch
	rollback tran Delete_Rol
	select 0 as valido;
end catch
end
go

/******************************Update T_Rol******************************/
create procedure Sp_Update_Rol
	@TN_Id_Rol int,
	@TC_Nombre_Rol varchar(50),
	@TN_Id_Permiso int
as begin
begin try
	begin tran Update_Rol
	if  exists(select*from T_Rol where TN_Id_Rol=@TN_Id_Rol)begin
		if exists(select*from T_Permiso where TN_Id_Permiso=@TN_Id_Permiso)begin
			update T_Rol set TC_Nombre_Rol=@TC_Nombre_Rol, TN_Id_Permiso=@TN_Id_Permiso where TN_Id_Rol=@TN_Id_Rol;
			select 1 as valido;
		end else begin
			select 0 as valido
		end
	end else begin
		select 0 as valido
	end
	commit tran Update_Rol
end try
begin catch
	rollback tran Update_Rol
	select 0 as valido;
end catch
end
go

/******************************Listar Unico T_Rol******************************/
create procedure Sp_Listar_Unico_Rol
	@TN_Id_Rol int
as begin
begin try
	begin tran Listar_Unico_Rol
	if exists(select*from T_Rol where TN_Id_Rol=@TN_Id_Rol)begin
		select 1 as valido, TN_Id_Rol,TC_Nombre_Rol from T_Rol where TN_Id_Rol=@TN_Id_Rol
	end else begin
		select 0 as valido, '' as TN_Id_Rol,'' as TC_Nombre_Rol
	end
	commit tran Listar_Unico_Rol
end try
begin catch
	rollback tran Listar_Unico_Rol
	select 0 as valido, '' as TN_Id_Rol,'' as TC_Nombre_Rol
end catch
end
go

/******************************Listar T_Rol******************************/
create procedure Sp_Listar_Rol
as begin
begin try
	begin tran Listar_Rol
	select T_Rol.TN_Id_Rol,T_Rol.TC_Nombre_Rol,T_Rol.TN_Id_Permiso,T_Permiso.TC_Nombre_Permiso from T_Rol
	inner join T_Permiso on T_Rol.TN_Id_Permiso=T_Permiso.TN_Id_Permiso
	commit tran Listar_Rol
end try
begin catch
	rollback tran Listar_Rol
	select 0 as valido;
end catch
end
go