use Gestor_Reuniones2
/******************************Insert Tarea******************************/
create procedure Sp_Insert_Tarea
    @TC_Nombre_Tarea varchar(100),
    @TC_Descripcion varchar(200),
	@TC_Lista_Usuarios varchar(200)

as begin
begin try
	begin tran Insert_Tarea
    if not exists(select*from T_Tarea where TC_Nombre_Tarea=@TC_Nombre_Tarea)begin
			insert into T_Tarea(TC_Nombre_Tarea, TC_Descripcion,TC_Acuerdo) values(@TC_Nombre_Tarea,@TC_Descripcion,'');
			declare @TN_Id_Tarea int;
			select @TN_Id_Tarea=TN_Id_Tarea from T_Tarea where TC_Nombre_Tarea=@TC_Nombre_Tarea
			execute Sp_Insert_Tarea_Usuario @TN_Id_Tarea,@TC_Lista_Usuarios	
		select 1 as valido;
    end else begin
		select 0 as valido
	end
	commit tran Insert_Tarea
end try
begin catch
	rollback tran Insert_Tarea
    select 0 as valido;
end catch
end
go

/******************************Delete Tarea******************************/
create procedure Sp_Delete_Tarea
@TN_Id_Tarea int
as begin
begin try
	begin tran Delete_Tarea
	if exists(select*from T_Tarea where TN_Id_Tarea=@TN_Id_Tarea)begin
			execute Sp_Delete_Tarea_Usuario @TN_Id_Tarea
			delete T_Tarea where TN_Id_Tarea=@TN_Id_Tarea
		select	1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Tarea
end try
begin catch
	rollback tran Delete_Tarea
    select 0 as valido;
end catch
end
go

/******************************Update Tarea******************************/
create procedure Sp_Update_Tarea
	@TN_Id_Tarea int,
    @TC_Nombre_Tarea varchar(100),
    @TC_Descripcion varchar(200),
	@TC_Lista_Usuarios varchar(200)
as begin
begin try
	begin tran Update_Tarea
    if exists(select*from T_Tarea where TN_Id_Tarea=@TN_Id_Tarea)begin
		if not exists(select*from T_Tarea where TC_Nombre_Tarea=@TC_Nombre_Tarea and TN_Id_Tarea!=@TN_Id_Tarea)begin
			execute Sp_Delete_Tarea_Usuario @TN_Id_Tarea
			update T_Tarea set TC_Nombre_Tarea=@TC_Nombre_Tarea , TC_Descripcion=@TC_Descripcion where TN_Id_Tarea = @TN_Id_Tarea
			execute Sp_Insert_Tarea_Usuario @TN_Id_Tarea,@TC_Lista_Usuarios
			select 1 as valido;
		end else begin
			select 0 as valido
		end
    end else begin
		select 0 as valido
	end
	commit tran Update_Tarea
end try
begin catch
	rollback tran Update_Tarea
    select 0 as valido;
end catch
end
go

/******************************Listar Tarea******************************/
create procedure Sp_Listar_Tarea
as begin
begin try
	begin tran Listar_Tarea
	if exists(select*from T_Tarea)begin
		select	1 as valido, TN_Id_Tarea, TC_Nombre_Tarea, TC_Descripcion from T_Tarea
	end else begin
		select 0 as valido,'' as TN_Id_Tarea, '' as TC_Nombre_Tarea, '' as TC_Descripcion;
	end
	commit tran Listar_Tarea
end try
begin catch
	rollback tran Listar_Tarea
    select 0 as valido,'' as TN_Id_Tarea, '' as TC_Nombre_Tarea, '' as TC_Descripcion;
end catch
end
go

/******************************Listar Tarea Unico******************************/
create procedure Sp_Listar_Tarea_Unico
@TN_Id_Tarea int
as begin
begin try
	begin tran Listar_Tarea_Unico
	if exists(select*from T_Tarea where TN_Id_Tarea=@TN_Id_Tarea)begin
		Declare @TN_Id_Usuario int,@TC_Lista_Usuarios varchar(max);
		Declare @T_TN_Id_Usuario TABLE(TN_Id_Usuario int);
		insert into @T_TN_Id_Usuario select T_Tarea_Usuario.TN_Id_Usuario from T_Tarea_Usuario where T_Tarea_Usuario.TN_Id_Tarea=@TN_Id_Tarea
			set @TC_Lista_Usuarios='';
			while exists(select*from @T_TN_Id_Usuario)begin
				set rowcount 1
				select @TN_Id_Usuario=TN_Id_Usuario from @T_TN_Id_Usuario
				set rowcount 0
				set @TC_Lista_Usuarios+=CAST(@TN_Id_Usuario as varchar(max))
				set @TC_Lista_Usuarios+=','
				delete @T_TN_Id_Usuario where TN_Id_Usuario=@TN_Id_Usuario;
			end
		select	1 as valido, TN_Id_Tarea, TC_Nombre_Tarea, TC_Descripcion,SUBSTRING (@TC_Lista_Usuarios, 1, Len(@TC_Lista_Usuarios) - 1 ) as TC_Lista_Usuarios from T_Tarea where TN_Id_Tarea=@TN_Id_Tarea
	end else begin
		select 0 as valido,'' as TN_Id_Tarea, '' as TC_Nombre_Tarea, '' as TC_Descripcion, ''as TC_Lista_Usuarios;
	end	
	commit tran Listar_Tarea_Unico
end try
begin catch
	rollback tran Listar_Tarea_Unico
    select 0 as valido,'' as TN_Id_Tarea, '' as TC_Nombre_Tarea, '' as TC_Descripcion, '' as TC_Lista_Usuarios;
end catch
end
go

/******************************Insert Tarea_Acuerdo******************************/
create procedure Sp_Insert_Tarea_Acuerdo
    @TN_Id_Tarea int,
    @TC_Acuerdo varchar(max)
as begin
begin try
	begin tran Tarea_Acuerdo
		update  T_Tarea set TC_Acuerdo=@TC_Acuerdo where TN_Id_Tarea=@TN_Id_Tarea;
		select 1 as valido
	commit tran Tarea_Acuerdo
end try
begin catch
	select 0 as valido
	rollback tran Tarea_Acuerdo
	print('Error insertando en la tabla Tarea_Acuerdo')
end catch
end
go