use Gestor_Reuniones2
/******************************Listar T_Circuito******************************/
create procedure Sp_Listar_Circuito
as begin
begin try
	begin tran Listar_Circuito
		select TN_Id_Circuito,TC_Des_Circuito from T_Circuito
	commit tran Listar_Circuito
end try
begin catch
	rollback tran Listar_Circuito
	select 0 as valido;
end catch
end
go