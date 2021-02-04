use Gestor_Reuniones2
/******************************Insert Usuario******************************/
create procedure Sp_Insert_Usuario 
	@TC_Usuario varchar(100),
	@TC_Contrasenna varchar(50),
	@TC_Identificacion varchar(20),
	@TC_Nombre_Usuario varchar(100), 
	@TC_Primer_Apellido varchar(100),
	@TC_Segundo_Apellido varchar(100),
	@TC_Correo varchar(100),
	@TN_Id_Puesto int,
	@TN_Id_Oficina int,
	@TN_Id_Rol int
as begin
begin try
begin tran Insert_Usuario
	if not exists(select*from T_Usuario where TC_Usuario =@TC_Usuario or TC_Identificacion=@TC_Identificacion or TC_Correo = @TC_Correo)begin
		if exists(select*from T_Rol where TN_Id_Rol=@TN_Id_Rol)begin
			if exists(select*from T_Puesto where TN_Id_Puesto=@TN_Id_Puesto)begin
				if exists(select*from T_Oficina where TN_Id_Oficina=@TN_Id_Oficina)begin
					insert into T_Usuario(TC_Usuario,TC_Contrasenna,TC_Identificacion,TC_Nombre_Usuario,TC_Primer_Apellido,TC_Segundo_Apellido,TC_Correo,TN_Id_Puesto,TN_Id_Oficina)
					values(@TC_Usuario,dbo.ENCRIPTA_PASS(@TC_Contrasenna),@TC_Identificacion,@TC_Nombre_Usuario,@TC_Primer_Apellido,@TC_Segundo_Apellido,@TC_Correo,@TN_Id_Puesto,@TN_Id_Oficina);

					Declare @TN_Id_Usuario int
					select @TN_Id_Usuario=T_Usuario.TN_Id_Usuario from T_Usuario where T_Usuario.TC_Usuario=@TC_Usuario
					execute Sp_Insert_Usuario_Rol @TN_Id_Rol, @TN_Id_Usuario

					select 1 as valido;
				end else begin
					select 0 as valido;
				end
			end else begin
				select 0 as valido;
			end
		end else begin
			select 0 as valido;
		end
	end else begin
		select 0 as valido;
	end
commit tran Insert_Usuario
end try
begin catch
	rollback tran Insert_Usuario
	select 0 as valido;
end catch
end 
go

/******************************Delete Usuario******************************/
create procedure Sp_Delete_Usuario 
@TN_Id_Usuario int
as begin
begin try
	begin tran Delete_Usuario
	if exists(select*from T_Usuario where TN_Id_Usuario=@TN_Id_Usuario and TB_Eliminado=0)begin
		update T_Usuario set TB_Eliminado=1, TB_Activo=0 where TN_Id_Usuario=@TN_Id_Usuario;
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Usuario
end try
begin catch
	rollback tran Delete_Usuario
	select 0 as valido;
end catch
end 
go
/******************************Update Usuario******************************/
create procedure Sp_Update_Usuario 
	@TN_Id_Usuario int,
	@TC_Usuario varchar(100),
	@TC_Contrasenna varchar(50),
	@TC_Identificacion varchar(20),
	@TC_Nombre_Usuario varchar(100), 
	@TC_Primer_Apellido varchar(100),
	@TC_Segundo_Apellido varchar(100),
	@TC_Correo varchar(100),
	@TN_Id_Puesto int,
	@TN_Id_Oficina int,
	@TN_Id_Rol int
as begin
begin try
	begin tran Update_Usuario
	if not exists(select*from T_Usuario where (TC_Usuario =@TC_Usuario or TC_Identificacion=@TC_Identificacion or TC_Correo = @TC_Correo) and TN_Id_Usuario != @TN_Id_Usuario)begin
		if exists(select*from T_Rol where TN_Id_Rol=@TN_Id_Rol)begin
			if exists(select*from T_Puesto where TN_Id_Puesto=@TN_Id_Puesto)begin
				if exists(select*from T_Oficina where TN_Id_Oficina=@TN_Id_Oficina)begin

					execute Sp_Update_Usuario_Rol @TN_Id_Rol, @TN_Id_Usuario;
					update T_Usuario set TC_Usuario=@TC_Usuario,TC_Contrasenna=dbo.ENCRIPTA_PASS(@TC_Contrasenna),TC_Identificacion=@TC_Identificacion,TC_Nombre_Usuario=@TC_Nombre_Usuario,TC_Primer_Apellido=@TC_Primer_Apellido,TC_Segundo_Apellido=@TC_Segundo_Apellido,TC_Correo=@TC_Correo,TN_Id_Puesto=@TN_Id_Puesto,TN_Id_Oficina=@TN_Id_Oficina 
					where TN_Id_Usuario=@TN_Id_Usuario;
					
					select 1 as valido;
				end else begin
					select 0 as valido;
				end
			end else begin
				select 0 as valido;
			end
		end else begin
			select 0 as valido;
		end
	end else begin
		select 0 as valido;
	end
	commit tran Update_Usuario
end try
begin catch
	rollback tran Update_Usuario
	select 0 as valido;
end catch
end 
go
/******************************Listar Usuario******************************/
create procedure Sp_Listar_Usuario
as begin
begin try
	begin tran Listar_Usuario
	select T_Usuario.TN_Id_Usuario,T_Usuario.TC_Nombre_Usuario,T_Usuario.TC_Primer_Apellido,T_Usuario.TC_Segundo_Apellido,T_Puesto.TC_Nombre_Puesto,T_Oficina.TC_Nombre as TC_Nombre_Oficina from T_Usuario
	inner join T_Oficina on T_Oficina.TN_Id_Oficina=T_Usuario.TN_Id_Oficina
	inner join T_Puesto on T_Puesto.TN_Id_Puesto=T_Usuario.TN_Id_Puesto where TB_Eliminado=0 and TB_Activo=1;
	commit tran Listar_Usuario
end try
begin catch
	rollback tran Listar_Usuario
	select 0 as valido;
end catch
end
go
/******************************Listar Usuario Unico******************************/
create procedure Sp_Listar_Usuario_Unico 
	@TN_Id_Usuario int
as begin
begin try
	begin tran Listar_Usuario_Unico
	if exists(select*from T_Usuario where TN_Id_Usuario =@TN_Id_Usuario)begin
		select 1 as valido,T_Usuario.TN_Id_Usuario,T_Usuario.TC_Usuario, dbo.DESENCRIPTA_PASS(T_Usuario.TC_Contrasenna) as TC_Contrasenna, T_Usuario.TC_Identificacion, T_Usuario.TC_Nombre_Usuario, T_Usuario.TC_Primer_Apellido, T_Usuario.TC_Segundo_Apellido, T_Usuario.TC_Correo , T_Puesto.TN_Id_Puesto, T_Puesto.TC_Nombre_Puesto, T_Oficina.TN_Id_Oficina,T_Oficina.TC_Nombre, T_Rol.TN_Id_Rol,T_Rol.TC_Nombre_Rol  from T_Usuario 
		inner join T_Usuario_Rol on T_Usuario_Rol.TN_Id_Usuario=T_Usuario.TN_Id_Usuario
		inner join T_Rol on T_Usuario_Rol.TN_Id_Rol=T_Rol.TN_Id_Rol
		inner join T_Puesto on T_Puesto.TN_Id_Puesto=T_Usuario.TN_Id_Puesto
		inner join T_Oficina on T_Oficina.TN_Id_Oficina=T_Usuario.TN_Id_Oficina
		where T_Usuario.TN_Id_Usuario=@TN_Id_Usuario
	end else begin
		select 0 as valido,0 as TN_Id_Usuario,'' as TC_Usuario,'' as TC_Contrasenna, '' as TC_Identificacion, '' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido, '' as TC_Correo , 0 as TN_Id_Puesto, '' as TC_Nombre_Puesto, 0 as TN_Id_Oficina,'' as TC_Nombre, 0 as TN_Id_Rol ,'' as TC_Nombre_Rol;
	end
	commit tran Listar_Usuario_Unico
end try
begin catch
	rollback tran Listar_Usuario_Unico
	select 0 as valido,0 as TN_Id_Usuario,'' as TC_Usuario,'' as TC_Contrasenna, '' as TC_Identificacion, '' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido, '' as TC_Correo , 0 as TN_Id_Puesto, '' as TC_Nombre_Puesto, 0 as TN_Id_Oficina,'' as TC_Nombre, 0 as TN_Id_Rol ,'' as TC_Nombre_Rol;
end catch
end 
go