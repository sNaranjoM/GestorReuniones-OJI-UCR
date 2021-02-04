use Gestor_Reuniones2
/******************************Insert T_Puesto******************************/
create procedure Sp_Insert_Puesto
	@TC_Nombre_Puesto varchar(100),
	@TN_Salario float
as begin
begin try
	begin tran Insert_Puesto
	if not exists(select*from T_Puesto where TC_Nombre_Puesto=@TC_Nombre_Puesto)begin
		insert into T_Puesto(TC_Nombre_Puesto,TN_Salario) values(@TC_Nombre_Puesto,@TN_Salario);
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Insert_Puesto
end try
begin catch
	rollback tran Insert_Puesto
	select 0 as valido;
end catch
end
go

/******************************Listar T_Puesto******************************/
create procedure Sp_Listar_Puesto
as begin
begin try
	begin tran Listar_Puesto
		select TN_Id_Puesto,TC_Nombre_Puesto,TN_Salario from T_Puesto
	commit tran Listar_Puesto
end try
begin catch
	rollback tran Listar_Puesto
	select 0 as valido;
end catch
end
go

/******************************Delete T_Puesto******************************/
create procedure Sp_Delete_Puesto
	@TN_Id_Puesto int
as begin
begin try
	begin tran Delete_Puesto
	if exists(select*from T_Puesto where TN_Id_Puesto=@TN_Id_Puesto)begin
		delete from T_Puesto where TN_Id_Puesto=@TN_Id_Puesto
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Puesto
end try
begin catch
	rollback tran Delete_Puesto
	select 0 as valido;
end catch
end
go

/******************************Update T_Puesto******************************/
create procedure Sp_Update_Puesto
	@TN_Id_Puesto int,
	@TC_Nombre_Puesto varchar(100),
	@TN_Salario float
as begin
begin try
	begin tran Update_Puesto
	if exists(select*from T_Puesto where TN_Id_Puesto=@TN_Id_Puesto)begin
		if not exists(select*from T_Puesto where TC_Nombre_Puesto=@TC_Nombre_Puesto and TN_Id_Puesto!=@TN_Id_Puesto)begin
			update T_Puesto set TC_Nombre_Puesto=@TC_Nombre_Puesto, TN_Salario=@TN_Salario where TN_Id_Puesto=@TN_Id_Puesto;
			select 1 as valido;
		end else begin
			select 0 as valido;
		end
	end else begin
		select 0 as valido;
	end
	commit tran Update_Puesto
end try
begin catch
	rollback tran Update_Puesto
	select 0 as valido;
end catch
end
go