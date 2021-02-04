use Gestor_Reuniones2
/******************************Insert Lista_Tareas******************************/
create procedure Sp_Insert_Lista_Tareas
    @TN_Id_Reunion int,
    @TC_Lista_Tareas varchar(200)
as begin
begin try
	begin tran Insert_Lista_Tareas
	Declare @T_TN_Id_Tarea TABLE(TN_Id_Tarea int);
	insert into @T_TN_Id_Tarea select value from string_split(@TC_Lista_Tareas,',');
	Declare @TN_Id_Tarea int
	while exists(select*from @T_TN_Id_Tarea)begin
		set rowcount 1
		select @TN_Id_Tarea=TN_Id_Tarea from @T_TN_Id_Tarea
		set rowcount 0
		if not exists(select * from T_Lista_Tareas where TN_Id_Tarea=@TN_Id_Tarea and TN_Id_Reunion=@TN_Id_Reunion)begin
			insert into T_Lista_Tareas (TN_Id_Tarea,TN_Id_Reunion)values(@TN_Id_Tarea,@TN_Id_Reunion);
		end
		delete @T_TN_Id_Tarea where TN_Id_Tarea=@TN_Id_Tarea;
	end
	commit tran Insert_Lista_Tareas
end try
begin catch
	rollback tran Insert_Lista_Tareas
	print('Error insertando en la tabla Lista_Tareas')
end catch
end
go

/******************************Delete Lista_Tareas******************************/
create procedure Sp_Delete_Lista_Tareas
    @TN_Id_Reunion int
as begin
begin try
	begin tran Delete_Lista_Tareas
	if exists(select*from T_Lista_Tareas where TN_Id_Reunion=@TN_Id_Reunion) begin
		delete T_Lista_Tareas where TN_Id_Reunion=@TN_Id_Reunion
	end
	commit tran Delete_Lista_Tareas
end try
begin catch
	rollback tran Delete_Lista_Tareas
	print('Error borrando en la tabla Lista_Tareas')
end catch
end
go