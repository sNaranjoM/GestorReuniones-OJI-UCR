use Gestor_Reuniones2
/******************************Insert Archivo_Reunion******************************/
create procedure Sp_Insert_Archivo_Reunion
    @TN_Id_Reunion int,
    @TC_Lista_Link varchar(1000)
as begin
begin try
	begin tran Insert_Links
	Declare @T_TC_Link TABLE(TC_Link varchar(100));
	insert into @T_TC_Link select value from string_split(@TC_Lista_Link,'?');
	Declare @TC_Link varchar(100)
	while exists(select*from @T_TC_Link)begin
		set rowcount 1
		select @TC_Link=TC_Link from @T_TC_Link
		set rowcount 0
		if not exists(select*from T_Archivo_Reunion where TN_Id_Reunion=@TN_Id_Reunion and TC_Link=@TC_Link)begin
			insert into T_Archivo_Reunion (TN_Id_Reunion,TC_Link)values(@TN_Id_Reunion,@TC_Link);
		end
		set rowcount 1
		delete @T_TC_Link;
		set rowcount 0
	end
	commit tran Insert_Links
end try
begin catch
	rollback tran Insert_Links
	print('Error insertando en la tabla Archivo_Reunion')
end catch
end
go

/******************************Delete Archivo_Reunion******************************/
create procedure Sp_Delete_Archivo_Reunion
    @TN_Id_Reunion int
as begin
begin try
	begin tran Delete_Archivo_Reunion
	if exists(select*from T_Archivo_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
		delete T_Archivo_Reunion where TN_Id_Reunion=@TN_Id_Reunion
	end
	commit tran Delete_Archivo_Reunion
end try
begin catch
	rollback tran Delete_Archivo_Reunion
	print('Error borrando en la tabla Archivo_Reunion')
end catch
end
go

/******************************Listar Archivo_Reunion******************************/
create procedure Sp_Listar_Archivo_Reunion
    @TN_Id_Reunion int
as begin
begin try
	begin tran Listar_Archivo_Reunion
	if exists(select*from T_Archivo_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
		select 1 as valido,TN_Id_Archivo, TC_Link from T_Archivo_Reunion where TN_Id_Reunion=@TN_Id_Reunion
	end else begin
		select 0 as valido,'' as TN_Id_Archivo,'' as TC_Link
	end
	commit tran Listar_Archivo_Reunion
end try
begin catch
	rollback tran Listar_Archivo_Reunion
	select 0 as valido,'' as TN_Id_Archivo,'' as TC_Link
	print('Error borrando en la tabla Archivo_Reunion')
end catch
end
go


/******************************Delete Archivo_Reunion_Unico******************************/
create procedure Sp_Delete_Archivo_Reunion_Unico
    @TN_Id_Archivo int
as begin
begin try
	begin tran Delete_Archivo_Reunion_Unico
	if exists(select*from T_Archivo_Reunion where TN_Id_Archivo=@TN_Id_Archivo)begin
		delete T_Archivo_Reunion where TN_Id_Archivo=@TN_Id_Archivo
		select 1 as valido
	end else begin 
		select 0 as valido
	end
	commit tran Delete_Archivo_Reunion_Unico
end try
begin catch
	rollback tran Delete_Archivo_Reunion_Unico
	select 0 as valido
	print('Error borrando en la tabla Archivo_Reunion')
end catch
end
go