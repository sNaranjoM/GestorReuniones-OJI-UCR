use Gestor_Reuniones2
/******************************Insert Temas******************************/
create procedure Sp_Insert_Temas
    @TN_Id_Reunion int,
    @TC_Lista_Temas varchar(1000)
as begin
begin try
	begin tran Insert_Temas
	Declare @T_TC_Nombre_Tema TABLE(TC_Nombre_Tema varchar(100));
	insert into @T_TC_Nombre_Tema select value from string_split(@TC_Lista_Temas,'&');
	Declare @TC_Nombre_Tema varchar(100)
	while exists(select*from @T_TC_Nombre_Tema)begin
		set rowcount 1
		select @TC_Nombre_Tema=TC_Nombre_Tema from @T_TC_Nombre_Tema
		set rowcount 0
		insert into T_Temas (TN_Id_Reunion,TC_Nombre_Tema,TC_Acuerdo)values(@TN_Id_Reunion,@TC_Nombre_Tema,'');
		set rowcount 1
		delete @T_TC_Nombre_Tema;
		set rowcount 0
	end
	commit tran Insert_Temas
end try
begin catch
	rollback tran Insert_Temas
	print('Error insertando en la tabla T_Temas')
end catch
end
go

/******************************Delete Temas******************************/
create procedure Sp_Delete_Temas
    @TN_Id_Reunion int
as begin
begin try
	begin tran Delete_Temas
	if exists(select*from T_Temas where TN_Id_Reunion=@TN_Id_Reunion)begin
		delete T_Temas where TN_Id_Reunion=@TN_Id_Reunion
	end
	commit tran Delete_Temas
end try
begin catch
	rollback tran Delete_Temas
	print('Error borrando en la tabla T_Temas')
end catch
end
go
/******************************Update Temas******************************/

/******************************Listar Temas******************************/

/******************************Insert Temas_Acuerdo******************************/
create procedure Sp_Insert_Temas_Acuerdo
    @TN_Id_Temas int,
    @TC_Acuerdo varchar(max)
as begin
begin try
	begin tran Temas_Acuerdo
		update  T_Temas set TC_Acuerdo=@TC_Acuerdo where TN_Id_Temas=@TN_Id_Temas;
		select 1 as valido
	commit tran Temas_Acuerdo
end try
begin catch
	select 0 as valido
	rollback tran Temas_Acuerdo
	print('Error insertando en la tabla Temas_Acuerdo')
end catch
end
go