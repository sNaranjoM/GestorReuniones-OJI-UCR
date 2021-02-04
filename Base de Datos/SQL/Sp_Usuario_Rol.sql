use Gestor_Reuniones2
/******************************Insert Usuario_Rol******************************/
create procedure Sp_Insert_Usuario_Rol 
	@TN_Id_Rol int,
	@TN_Id_Usuario int
as begin
begin try
	begin tran Insert_Usuario_Rol
	if not exists(select*from T_Usuario_Rol where TN_Id_Usuario =@TN_Id_Usuario)begin
		insert into T_Usuario_Rol(TN_Id_Rol,TN_Id_Usuario) values(@TN_Id_Rol,@TN_Id_Usuario);
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Insert_Usuario_Rol
end try
begin catch
	rollback tran Insert_Usuario_Rol
	select 0 as valido;
end catch
end 
go

/******************************Delete Usuario_Rol******************************/
create procedure Sp_Delete_Usuario_Rol 
	@TN_Id_Rol int,
	@TN_Id_Usuario int
as begin
begin try
	begin tran Delete_Usuario_Rol
	if exists(select*from T_Usuario_Rol where TN_Id_Rol = @TN_Id_Rol and TN_Id_Usuario = @TN_Id_Usuario)begin
		delete from T_Usuario_Rol where TN_Id_Rol = @TN_Id_Rol and TN_Id_Usuario = @TN_Id_Usuario
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Usuario_Rol
end try
begin catch
	rollback tran Delete_Usuario_Rol
	select 0 as valido;
end catch
end 
go

/******************************Update Usuario_Rol******************************/
create procedure Sp_Update_Usuario_Rol 
	@TN_Id_Rol int,
	@TN_Id_Usuario int
as begin
begin try
	begin tran Update_Usuario_Rol
	if  exists(select*from T_Usuario_Rol where TN_Id_Usuario = @TN_Id_Usuario)begin
		update T_Usuario_Rol set TN_Id_Rol=@TN_Id_Rol where TN_Id_Usuario=@TN_Id_Usuario;
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Update_Usuario_Rol
end try
begin catch
	rollback tran Update_Usuario_Rol
	select 0 as valido;
end catch
end 
go