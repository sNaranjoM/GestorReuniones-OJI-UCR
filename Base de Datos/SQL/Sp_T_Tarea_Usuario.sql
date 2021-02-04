use Gestor_Reuniones2
/******************************Insert Tarea_Usuario******************************/
create procedure Sp_Insert_Tarea_Usuario
    @TN_Id_Tarea int ,
    @TC_Lista_Usuarios varchar(200)/*Esta lista seria una lista de TN_Id_Usuario separados por ,*/

as begin
begin try
	begin tran Insert_Tarea_Usuario
	if exists(select*from T_Tarea where TN_Id_Tarea=@TN_Id_Tarea)begin
			Declare @T_TN_Id_Usuario TABLE(TN_Id_Usuario int);
			insert into @T_TN_Id_Usuario select value from string_split(@TC_Lista_Usuarios,',');
			Declare @TN_Id_Usuario int

			/*se declaro una tabla la cual se va a llenar con toda la lista de TN_Id_Usuario que se desean insertar
			para la cual se recorre la tabla con un while en la cual se ava seleccionando linea por linea y se valida que no exista ya 
			insertado, de ser que no existe se inserta y despues se borra esa tupla de la tabla para no volver a seleccionarla*/
			while exists(select*from @T_TN_Id_Usuario)begin
				set rowcount 1
				select @TN_Id_Usuario=TN_Id_Usuario from @T_TN_Id_Usuario
				set rowcount 0
				if not exists(select * from T_Tarea_Usuario where TN_Id_Tarea=@TN_Id_Tarea and TN_Id_Usuario=@TN_Id_Usuario)begin
					insert into T_Tarea_Usuario (TN_Id_Tarea,TN_Id_Usuario)values(@TN_Id_Tarea,@TN_Id_Usuario);
				end
				delete @T_TN_Id_Usuario where TN_Id_Usuario=@TN_Id_Usuario;
			end
	end
	commit tran Insert_Tarea_Usuario
end try
begin catch
	rollback tran Insert_Tarea_Usuario
    print('Error insertando en la tabla T_Tarea_Usuario')
end catch
end
go
/******************************Delete Tarea_Usuario******************************/
create procedure Sp_Delete_Tarea_Usuario
@TN_Id_Tarea int
as begin
begin try
	begin tran Delete_Tarea_Usuario
	if exists(select*from T_Tarea_Usuario where TN_Id_Tarea=@TN_Id_Tarea)begin
		delete T_Tarea_Usuario where TN_Id_Tarea=@TN_Id_Tarea
	end
	commit tran Delete_Tarea_Usuario
end try
begin catch
	rollback tran Delete_Tarea_Usuario;
end catch
end
go