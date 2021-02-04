use Gestor_Reuniones2
/******************************Insert Agenda******************************/
create procedure Sp_Insert_Agenda
    @TN_Id_Reunion int,
    @TC_Lista_Usuarios varchar(200)
as begin
begin try
	begin tran Insert_Agenda
	Declare @T_TN_Id_Usuario TABLE(TN_Id_Usuario int);
	insert into @T_TN_Id_Usuario select value from string_split(@TC_Lista_Usuarios,',');
	Declare @TN_Id_Usuario int
	while exists(select*from @T_TN_Id_Usuario)begin
		set rowcount 1
		select @TN_Id_Usuario=TN_Id_Usuario from @T_TN_Id_Usuario
		set rowcount 0
		if not exists(select * from T_Agenda where TN_Id_Usuario=@TN_Id_Usuario and TN_Id_Reunion=@TN_Id_Reunion)begin
			insert into T_Agenda (TN_Id_Usuario,TN_Id_Reunion)values(@TN_Id_Usuario,@TN_Id_Reunion);
		end
		delete @T_TN_Id_Usuario where TN_Id_Usuario=@TN_Id_Usuario;
	end
	commit tran Insert_Agenda
end try
begin catch
	rollback tran Insert_Agenda
	print('Error insertando en la tabla T_Agenda')
end catch
end
go

/******************************Delete Agenda******************************/
create procedure Sp_Delete_Agenda
    @TN_Id_Reunion int
as begin
begin try
	begin tran Delete_Agenda
	if exists(select*from T_Agenda where TN_Id_Reunion=@TN_Id_Reunion) begin
		delete T_Agenda where TN_Id_Reunion=@TN_Id_Reunion
	end
	commit tran Delete_Agenda
end try
begin catch
	rollback tran Delete_Agenda
	print('Error borrando en la tabla T_Agenda')
end catch
end
go

/******************************Listar Agenda******************************/
create procedure Sp_Listar_Agenda
    @TC_Usuario varchar(100)
as begin
begin try
	begin tran Sp_Listar_Agenda
	Declare @TN_Id_Usuario int
	select @TN_Id_Usuario=TN_Id_Usuario from T_Usuario where TC_Usuario=@TC_Usuario;
	if exists(select*from T_Agenda where TN_Id_Usuario=@TN_Id_Usuario) begin
		select 1 as valido,T_Reunion.TN_Id_Reunion,T_Reunion.TC_Nombre_Reunion,T_Reunion.TC_Descripcion,T_Reunion.TC_Comentario,T_Reunion.TC_Lugar,T_Reunion.TC_Fecha_Inicio from T_Agenda
		inner join T_Reunion on T_Reunion.TN_Id_Reunion=T_Agenda.TN_Id_Reunion
		where @TN_Id_Usuario=T_Agenda.TN_Id_Usuario
	end else begin
		select 0 as valido,0 as TN_Id_Reunion,'' as TC_Nombre_Reunion,'' as TC_Descripcion,'' as TC_Comentario,'' as TC_Lugar,'' as TC_Fecha_Inicio
	end
	commit tran Sp_Listar_Agenda
end try
begin catch
	rollback tran Sp_Listar_Agenda
	select 0 as valido,0 as TN_Id_Reunion,'' as TC_Nombre_Reunion,'' as TC_Descripcion,'' as TC_Comentario,'' as TC_Lugar,'' as TC_Fecha_Inicio
	print('Error listando agenda del usuario')
end catch
end
go