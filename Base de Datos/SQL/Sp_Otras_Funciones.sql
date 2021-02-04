use Gestor_Reuniones2
/************************************************************Funciones/Procedures************************************************************/
/**************Encriptar**************/
create function ENCRIPTA_PASS
(@clave varchar(50))
	returns VarBinary(500)
	as 
	begin
		declare @TC_Contrasenna as VarBinary(500)
		set @TC_Contrasenna=ENCRYPTBYPASSPHRASE('clave',@clave)
		return @TC_Contrasenna
end
go
/**************Desncriptar**************/
create function DESENCRIPTA_PASS
(@clave varbinary(500))
	returns varchar(50)
	as
	begin
		declare @TC_Contrasenna as varchar(50)
		set @TC_Contrasenna=DECRYPTBYPASSPHRASE('clave',@clave)
		return @TC_Contrasenna
end
go
/**************Login**************/
create procedure Sp_Login_Usuario
@TC_Usuario varchar(100),
@TC_Contrasenna varchar(50)
as begin
begin try
	begin tran Login_Usuario
	if exists(select*from T_Usuario where T_Usuario.TC_Usuario=@TC_Usuario and dbo.DESENCRIPTA_PASS(T_Usuario.TC_Contrasenna)=@TC_Contrasenna and T_Usuario.TB_Eliminado=0 and T_Usuario.TB_Activo=1)begin
		select T_Usuario.TC_Usuario, T_Rol.TC_Nombre_Rol,T_Permiso.TN_Id_Permiso,1 as valido from T_Usuario 
		inner join T_Usuario_Rol on T_Usuario_Rol.TN_Id_Usuario=T_Usuario.TN_Id_Usuario
		inner join T_Rol on T_Usuario_Rol.TN_Id_Rol=T_Rol.TN_Id_Rol
		inner join T_Permiso on T_Rol.TN_Id_Permiso=T_Permiso.TN_Id_Permiso
		where T_Usuario.TC_Usuario=@TC_Usuario and dbo.DESENCRIPTA_PASS(T_Usuario.TC_Contrasenna)=@TC_Contrasenna and T_Usuario.TB_Eliminado=0 and T_Usuario.TB_Activo=1;
	end else begin
		select '' as TC_Usuario,'' as TC_Nombre_Rol, '' as TN_Id_Permiso,0 as valido
	end
	commit tran Login_Usuario
end try
begin catch
	rollback tran Login_Usuario
	select '' as TC_Usuario,'' as TC_Nombre_Rol, '' as TN_Id_Permiso,0 as valido
end catch

end
go

/**************Validar creador reunion**************/
create procedure Sp_Validar_Host
@TN_Id_Reunion int
as begin
begin try
	begin tran Validar_Host
	if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
		declare @TN_Id_Usuario int
		select @TN_Id_Usuario=TN_Id_Usuario from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion
		select T_Usuario.TC_Usuario from T_Usuario where TN_Id_Usuario=@TN_Id_Usuario
	end else begin
		select '' as TC_Usuario
	end
	commit tran Validar_Host
end try
begin catch
	rollback tran Validar_Host
	select '' as TC_Usuario
end catch

end
go