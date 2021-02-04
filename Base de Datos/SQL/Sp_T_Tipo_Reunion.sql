use Gestor_Reuniones2
/******************************Insert Tipo_Reunion******************************/
create procedure Sp_Insert_Tipo_Reunion
    @TC_Nombre_Tipo_Reunion varchar(50)

as begin
begin try
	begin tran Insert_Tipo_Reunion
	if not exists(select*from T_Tipo_Reunion where TC_Nombre_Tipo_Reunion =@TC_Nombre_Tipo_Reunion)begin
		insert into T_Tipo_Reunion(TC_Nombre_Tipo_Reunion) values(@TC_Nombre_Tipo_Reunion);
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Insert_Tipo_Reunion
end try
begin catch
	rollback tran Insert_Tipo_Reunion
    select 0 as valido;
end catch
end
go
/******************************Delete Tipo_Reunion******************************/
create procedure Sp_Delete_Tipo_Reunion
    @TN_Id_Tipo_Reunion int

as begin
begin try
	begin tran Delete_Tipo_Reunion
	if exists(select*from T_Tipo_Reunion where TN_Id_Tipo_Reunion = @TN_Id_Tipo_Reunion)begin
		delete from T_Tipo_Reunion where TN_Id_Tipo_Reunion = @TN_Id_Tipo_Reunion
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Tipo_Reunion
end try
begin catch
	rollback tran Delete_Tipo_Reunion
    select 0 as valido;
end catch
end
go

/******************************Update Tipo_Reunion******************************/
create procedure Sp_Update_Tipo_Reunion
    @TN_Id_Tipo_Reunion int,
	@TC_Nombre_Tipo_Reunion varchar(50)

as begin
begin try
	begin tran Update_Tipo_Reunion
	if exists(select*from T_Tipo_Reunion where TN_Id_Tipo_Reunion = @TN_Id_Tipo_Reunion)begin
		update T_Tipo_Reunion set TC_Nombre_Tipo_Reunion= @TC_Nombre_Tipo_Reunion  where TN_Id_Tipo_Reunion = @TN_Id_Tipo_Reunion
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Update_Tipo_Reunion
end try
begin catch
	rollback tran Update_Tipo_Reunion
    select 0 as valido;
end catch
end
go
/******************************Listar Tipo_Reunion******************************/
create procedure Sp_Listar_Tipo_Reunion
as begin
begin try
	begin tran Listar_Tipo_Reunion
	select TN_Id_Tipo_Reunion,TC_Nombre_Tipo_Reunion from T_Tipo_Reunion order by TN_Id_Tipo_Reunion
	commit tran Listar_Tipo_Reunion
end try
begin catch
	rollback tran Listar_Tipo_Reunion
	select 0 as valido;
end catch
end
go

/******************************Listar Unico Tipo_Reunion******************************/
create procedure Sp_Listar_Unico_Tipo_Reunion
@TN_Id_Tipo_Reunion int
as begin
begin try
	begin tran Listar_Unico_Tipo_Reunion
	if exists(select*from T_Tipo_Reunion where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion)begin
		select 1 as valido,TN_Id_Tipo_Reunion,TC_Nombre_Tipo_Reunion from T_Tipo_Reunion where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion
	end else begin
		select 0 as valido, '' as TN_Id_Tipo_Reunion, '' as TC_Nombre_Tipo_Reunion;
	end
	commit tran Listar_Unico_Tipo_Reunion
end try
begin catch
	rollback tran Listar_Unico_Tipo_Reunion
	select 0 as valido, '' as TN_Id_Tipo_Reunion, '' as TC_Nombre_Tipo_Reunion;
end catch
end
go