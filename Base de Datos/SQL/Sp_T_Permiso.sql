use Gestor_Reuniones2
/******************************Listar T_Permiso******************************/
create procedure Sp_Listar_Permiso
as begin
begin try
	begin tran Listar_Permiso
	select TN_Id_Permiso,TC_Nombre_Permiso from T_Permiso
	commit tran Listar_Permiso
end try
begin catch
	rollback tran Listar_Permiso
	select 0 as valido;
end catch
end
go