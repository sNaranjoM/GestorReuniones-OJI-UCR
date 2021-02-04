use Gestor_Reuniones2
/******************************Insert Reunion******************************/
create procedure Sp_Insert_Reunion 
	@TC_Nombre_Reunion varchar(100),
	@TN_Id_Tipo_Reunion int,
	@TC_Descripcion varchar(200),
	@TC_Comentario varchar(200),
	@TC_Lugar varchar(100),
	@TC_Fecha_Inicio datetime,
	@TC_Lista_Usuarios varchar(200),
	@TC_Lista_Temas varchar(1000),
	@TC_Lista_Tareas varchar(200),
	@TC_Lista_Archivos varchar(1000),
	@TC_Usuario varchar(100)
as begin
begin try
	begin tran Insert_Reunion
		if not exists(select*from T_Reunion where TC_Lugar=@TC_Lugar and TC_Fecha_Inicio=@TC_Fecha_Inicio)begin

			Declare @TN_Id_Usuario int;
			select @TN_Id_Usuario=TN_Id_Usuario from T_Usuario where TC_Usuario=@TC_Usuario

			insert into T_Reunion(TC_Nombre_Reunion,TN_Id_Tipo_Reunion,TC_Descripcion,TC_Comentario,TC_Lugar,TC_Fecha_Inicio,TN_Id_Usuario)
			values(@TC_Nombre_Reunion,@TN_Id_Tipo_Reunion,@TC_Descripcion,@TC_Comentario,@TC_Lugar,@TC_Fecha_Inicio,@TN_Id_Usuario);

			declare @TN_Id_Reunion int;
			select @TN_Id_Reunion=TN_Id_Reunion from T_Reunion where TC_Lugar=@TC_Lugar and TC_Fecha_Inicio=@TC_Fecha_Inicio
			set @TC_Lista_Usuarios+=','
			set @TC_Lista_Usuarios+=CAST(@TN_Id_Usuario as varchar(max))

			Declare @T_TN_Id_Tarea TABLE(TN_Id_Tarea int);
			insert into @T_TN_Id_Tarea select value from string_split(@TC_Lista_Tareas,',');
			print(@TC_Lista_Tareas)
			Declare @TN_Id_Tarea int
			Declare @T_TN_Id_Usuario TABLE(TN_Id_Usuario int);
			while exists(select*from @T_TN_Id_Tarea)begin
				set rowcount 1
				select @TN_Id_Tarea=TN_Id_Tarea from @T_TN_Id_Tarea
				set rowcount 0
				insert into @T_TN_Id_Usuario select T_Tarea_Usuario.TN_Id_Usuario from T_Tarea_Usuario where T_Tarea_Usuario.TN_Id_Tarea=@TN_Id_Tarea
				delete @T_TN_Id_Tarea where TN_Id_Tarea=@TN_Id_Tarea;
			end

			while exists(select*from @T_TN_Id_Usuario)begin
				set rowcount 1
				select @TN_Id_Usuario=TN_Id_Usuario from @T_TN_Id_Usuario
				set rowcount 0
				set @TC_Lista_Usuarios+=','
				set @TC_Lista_Usuarios+=CAST(@TN_Id_Usuario as varchar(max))
				delete @T_TN_Id_Usuario where TN_Id_Usuario=@TN_Id_Usuario;
			end

			execute Sp_Insert_Temas @TN_Id_Reunion, @TC_Lista_Temas
			execute Sp_Insert_Lista_Tareas @TN_Id_Reunion, @TC_Lista_Tareas
			execute Sp_Insert_Agenda @TN_Id_Reunion, @TC_Lista_Usuarios
			execute Sp_Insert_Archivo_Reunion @TN_Id_Reunion, @TC_Lista_Archivos

			select 1 as valido, TN_Id_Reunion from T_Reunion where TC_Lugar=@TC_Lugar and TC_Fecha_Inicio=@TC_Fecha_Inicio;
		end	else begin
			select 0 as valido, 0 as TN_Id_Reunion;
		end
	commit tran Insert_Reunion
end try
begin catch
	rollback tran Insert_Reunion
	select 0 as valido, 0 as TN_Id_Reunion;
end catch
end 
go

/******************************Delete Reunion******************************/
create procedure Sp_Delete_Reunion 
	@TN_Id_Reunion int
as begin
begin try
	begin tran Delete_Reunion
		if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
			execute Sp_Delete_Temas @TN_Id_Reunion
			execute Sp_Delete_Lista_Tareas @TN_Id_Reunion
			execute Sp_Delete_Agenda @TN_Id_Reunion
			execute Sp_Delete_Archivo_Reunion @TN_Id_Reunion
			delete T_Reunion where TN_Id_Reunion=@TN_Id_Reunion
			
			select 1 as valido;
		end else begin
			select 0 as valido;
		end
	commit tran Delete_Reunion
end try
begin catch
	rollback tran Delete_Reunion
	select 0 as valido;
end catch
end 
go

/******************************Update Reunion******************************/
create procedure Sp_Update_Reunion 
	@TN_Id_Reunion int ,
	@TC_Nombre_Reunion varchar(100),
	@TN_Id_Tipo_Reunion int,
	@TC_Descripcion varchar(200),
	@TC_Comentario varchar(200),
	@TC_Lugar varchar(100),
	@TC_Fecha_Inicio datetime,
	@TC_Lista_Usuarios varchar(200),
	@TC_Lista_Temas varchar(1000),
	@TC_Lista_Tareas varchar(200),
	@TC_Lista_Archivos varchar(1000)
as begin
begin try
	begin tran Update_Reunion
		if not exists(select*from T_Reunion where TC_Nombre_Reunion=@TC_Nombre_Reunion and TC_Lugar=@TC_Lugar and TC_Fecha_Inicio=@TC_Fecha_Inicio and TN_Id_Reunion!=@TN_Id_Reunion)begin
			update T_Reunion set TC_Nombre_Reunion=@TC_Nombre_Reunion,TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion,TC_Descripcion=@TC_Descripcion,TC_Comentario=@TC_Comentario,TC_Lugar=@TC_Lugar,TC_Fecha_Inicio=@TC_Fecha_Inicio where TN_Id_Reunion=@TN_Id_Reunion
			if(@TC_Lista_Archivos='sin_archivos' and @TC_Lista_Temas='sin_temas')begin
				execute Sp_Delete_Lista_Tareas @TN_Id_Reunion
				execute Sp_Delete_Agenda @TN_Id_Reunion

				execute Sp_Insert_Lista_Tareas @TN_Id_Reunion, @TC_Lista_Tareas
				execute Sp_Insert_Agenda @TN_Id_Reunion, @TC_Lista_Usuarios
			end  
			if(@TC_Lista_Archivos='sin_archivos' and @TC_Lista_Temas!='sin_temas')begin
				execute Sp_Delete_Lista_Tareas @TN_Id_Reunion
				execute Sp_Delete_Agenda @TN_Id_Reunion

				execute Sp_Insert_Temas @TN_Id_Reunion, @TC_Lista_Temas
				execute Sp_Insert_Lista_Tareas @TN_Id_Reunion, @TC_Lista_Tareas
				execute Sp_Insert_Agenda @TN_Id_Reunion, @TC_Lista_Usuarios
			end
			if(@TC_Lista_Archivos!='sin_archivos' and @TC_Lista_Temas='sin_temas')begin
				execute Sp_Delete_Lista_Tareas @TN_Id_Reunion
				execute Sp_Delete_Agenda @TN_Id_Reunion

				execute Sp_Insert_Lista_Tareas @TN_Id_Reunion, @TC_Lista_Tareas
				execute Sp_Insert_Agenda @TN_Id_Reunion, @TC_Lista_Usuarios
				execute Sp_Insert_Archivo_Reunion @TN_Id_Reunion, @TC_Lista_Archivos
			end
			if(@TC_Lista_Archivos!='sin_archivos' and @TC_Lista_Temas!='sin_temas')begin
				execute Sp_Delete_Lista_Tareas @TN_Id_Reunion
				execute Sp_Delete_Agenda @TN_Id_Reunion

				execute Sp_Insert_Temas @TN_Id_Reunion, @TC_Lista_Temas
				execute Sp_Insert_Lista_Tareas @TN_Id_Reunion, @TC_Lista_Tareas
				execute Sp_Insert_Agenda @TN_Id_Reunion, @TC_Lista_Usuarios
				execute Sp_Insert_Archivo_Reunion @TN_Id_Reunion, @TC_Lista_Archivos
			end
			
			select 1 as valido;
		end else begin
			select 0 as valido;
		end
	commit tran Update_Reunion
end try
begin catch
	rollback tran Update_Reunion
	select 0 as valido;
end catch
end 
go

/******************************Listar Reunion******************************/
create procedure Sp_Listar_Reunion
as begin
begin try
	begin tran Listar_Reunion
		select	1 as valido, T_Reunion.TN_Id_Reunion, T_Reunion.TC_Nombre_Reunion, T_Reunion.TN_Id_Tipo_Reunion, T_Reunion.TC_Descripcion, T_Reunion.TC_Comentario, T_Reunion.TC_Lugar, T_Reunion.TC_Fecha_Inicio, T_Tipo_Reunion.TC_Nombre_Tipo_Reunion, T_Reunion.TN_Finalizada from T_Reunion
		inner join T_Tipo_Reunion on T_Reunion.TN_Id_Tipo_Reunion=T_Tipo_Reunion.TN_Id_Tipo_Reunion
	commit tran Listar_Reunion
end try
begin catch
	rollback tran Listar_Reunion
    select	0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Nombre_Tipo_Reunion, '' as TN_Finalizada
end catch
end
go
/******************************Finalizar Reunion******************************/
create procedure Sp_Finalizar_Reunion
	@TN_Id_Reunion int
as begin
begin try
	begin tran Finalizar_Reunion
		if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
			update T_Reunion set TC_Fecha_Final=GETDATE(), TN_Finalizada=1  where TN_Id_Reunion=@TN_Id_Reunion
			select 1 as valido
		end else begin
			select 0 as valido
		end
	commit tran Finalizar_Reunion
end try
begin catch
	rollback tran Finalizar_Reunion
	select 0 as valido
end catch
end
go

/******************************Listar Unico_Reunion******************************/
create procedure Sp_Listar_Unico_Reunion
	@TN_Id_Reunion int 
as begin
begin try
	begin tran Listar_Unico_Reunion
	if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
		Declare @TN_Id_Usuario int, @TN_Id_Tarea int,@TC_Lista_Usuarios varchar(max),@TC_Lista_Tareas varchar(max);
		Declare @T_TN_Id_Usuario TABLE(TN_Id_Usuario int);
		Declare @T_TN_Id_Tarea TABLE(TN_Id_Tarea int);
		insert into @T_TN_Id_Usuario select T_Agenda.TN_Id_Usuario from T_Agenda where T_Agenda.TN_Id_Reunion=@TN_Id_Reunion
		insert into @T_TN_Id_Tarea select T_Lista_Tareas.TN_Id_Tarea from T_Lista_Tareas where T_Lista_Tareas.TN_Id_Reunion=@TN_Id_Reunion

		set @TC_Lista_Usuarios='';
		while exists(select*from @T_TN_Id_Usuario)begin
			set rowcount 1
			select @TN_Id_Usuario=TN_Id_Usuario from @T_TN_Id_Usuario
			set rowcount 0
			set @TC_Lista_Usuarios+=CAST(@TN_Id_Usuario as varchar(max))
			set @TC_Lista_Usuarios+=','
			delete @T_TN_Id_Usuario where TN_Id_Usuario=@TN_Id_Usuario;
		end

		set @TC_Lista_Tareas='';
		while exists(select*from @T_TN_Id_Tarea)begin
			set rowcount 1
			select @TN_Id_Tarea=TN_Id_Tarea from @T_TN_Id_Tarea
			set rowcount 0
			set @TC_Lista_Tareas+=CAST(@TN_Id_Tarea as varchar(max))
			set @TC_Lista_Tareas+=','
			delete @T_TN_Id_Tarea where TN_Id_Tarea=@TN_Id_Tarea;
		end

		select	1 as valido, T_Reunion.TN_Id_Reunion, T_Reunion.TC_Nombre_Reunion, T_Reunion.TN_Id_Tipo_Reunion, T_Reunion.TC_Descripcion, T_Reunion.TC_Comentario, T_Reunion.TC_Lugar, T_Reunion.TC_Fecha_Inicio, T_Tipo_Reunion.TC_Nombre_Tipo_Reunion,SUBSTRING (@TC_Lista_Usuarios, 1, Len(@TC_Lista_Usuarios) - 1 ) as TC_Lista_Usuarios, SUBSTRING (@TC_Lista_Tareas, 1, Len(@TC_Lista_Tareas) - 1 ) as TC_Lista_Tareas from T_Reunion
		inner join T_Tipo_Reunion on T_Reunion.TN_Id_Tipo_Reunion=T_Tipo_Reunion.TN_Id_Tipo_Reunion where T_Reunion.TN_Id_Reunion=@TN_Id_Reunion
	end else begin
		select	0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Nombre_Tipo_Reunion, '' as TC_Lista_Usuarios, '' as TC_Lista_Tareas
	end	
	commit tran Listar_Unico_Reunion
end try
begin catch
	rollback tran Listar_Unico_Reunion
    select	0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Nombre_Tipo_Reunion, '' as TC_Lista_Usuarios, '' as TC_Lista_Tareas
end catch
end
go

/******************************Listar Reunion_Usuarios******************************/
create procedure Sp_Listar_Reunion_Usuarios
	@TN_Id_Reunion int 
as begin
begin try
	begin tran Listar_Reunion_Usuarios
		if exists(select*from T_Agenda where TN_Id_Reunion = @TN_Id_Reunion)begin
			select	1 as valido, T_Usuario.TN_Id_Usuario,T_Usuario.TC_Nombre_Usuario,T_Usuario.TC_Primer_Apellido,T_Puesto.TC_Nombre_Puesto,T_Oficina.TC_Nombre  from T_Agenda
			inner join T_Usuario on T_Usuario.TN_Id_Usuario =T_Agenda.TN_Id_Usuario
			inner join T_Puesto on T_Puesto.TN_Id_Puesto = T_Usuario.TN_Id_Puesto
			inner join T_Oficina on T_Oficina.TN_Id_Oficina = T_Usuario.TN_Id_Oficina
			where T_Agenda.TN_Id_Reunion = @TN_Id_Reunion
		end else begin
			select	0 as valido,'' as TN_Id_Usuario,'' as TC_Nombre_Usuario,'' as TC_Primer_Apellido,'' as TC_Nombre_Puesto,'' as TC_Nombre
		end
		
	commit tran Listar_Reunion_Usuarios
end try
begin catch
	rollback tran Listar_Reunion_Usuarios
	select	0 as valido,'' as TN_Id_Usuario,'' as TC_Nombre_Usuario,'' as TC_Primer_Apellido,'' as TC_Nombre_Puesto,'' as TC_Nombre
end catch
end
go

/******************************Listar Reunion_Tareas******************************/
create procedure Sp_Listar_Reunion_Tareas
	@TN_Id_Reunion int 
as begin
begin try
	begin tran Listar_Reunion_Tareas
	if exists (select*from T_Lista_Tareas where TN_Id_Reunion=@TN_Id_Reunion) begin
		Declare @T_Resultado_Final TABLE(valido int,TN_Id_Tarea int,TC_Nombre_Tarea varchar(100),TC_Descripcion varchar(200),TC_Usuarios varchar(MAX));
		Declare @T_Resultado_Temp TABLE(valido int,TN_Id_Tarea int,TC_Nombre_Tarea varchar(100),TC_Descripcion varchar(200));

		insert into @T_Resultado_Temp select 1 as valido, T_Tarea.TN_Id_Tarea,T_Tarea.TC_Nombre_Tarea,T_Tarea.TC_Descripcion from T_Lista_Tareas
		inner join T_Tarea on T_Tarea.TN_Id_Tarea = T_Lista_Tareas.TN_Id_Tarea
		where T_Lista_Tareas.TN_Id_Reunion = @TN_Id_Reunion

		Declare @Lista_Usuarios TABLE(TN_Id_Usuario int,TC_Nombre_Usuario varchar(100),TC_Primer_Apellido varchar(100));
		Declare @valido int, @TN_Id_Tarea int, @TC_Nombre_Tarea varchar(100), @TC_Descripcion varchar(200), @TC_Usuarios varchar(max),@TN_Id_Usuario int,@TC_Nombre_Usuario varchar(100), @TC_Primer_Apellido varchar(100)
		while exists(select*from @T_Resultado_Temp)begin
			set rowcount 1
			select @valido=valido, @TN_Id_Tarea=TN_Id_Tarea, @TC_Nombre_Tarea=TC_Nombre_Tarea, @TC_Descripcion=TC_Descripcion  from @T_Resultado_Temp
			set rowcount 0
			set @TC_Usuarios= '';
			insert into @Lista_Usuarios select T_Usuario.TN_Id_Usuario ,T_Usuario.TC_Nombre_Usuario, T_Usuario.TC_Primer_Apellido from T_Tarea_Usuario
			inner join T_Usuario on T_Tarea_Usuario.TN_Id_Usuario = T_Usuario.TN_Id_Usuario
			where T_Tarea_Usuario.TN_Id_Tarea = @TN_Id_Tarea

			while exists(select*from @Lista_Usuarios)begin
				set rowcount 1
				select @TN_Id_Usuario=TN_Id_Usuario, @TC_Nombre_Usuario=TC_Nombre_Usuario, @TC_Primer_Apellido=TC_Primer_Apellido  from @Lista_Usuarios
				set rowcount 0
				set @TC_Usuarios += @TC_Nombre_Usuario+' '+@TC_Primer_Apellido+', '
				
				delete @Lista_Usuarios where @TN_Id_Usuario=TN_Id_Usuario
			end
			
			insert into @T_Resultado_Final(valido ,TN_Id_Tarea ,TC_Nombre_Tarea ,TC_Descripcion ,TC_Usuarios)values(@valido, @TN_Id_Tarea, @TC_Nombre_Tarea, @TC_Descripcion, SUBSTRING (@TC_Usuarios, 1, Len(@TC_Usuarios) - 1 ) );
			delete @T_Resultado_Temp where @TN_Id_Tarea=TN_Id_Tarea;
		end
		select*from @T_Resultado_Final
	end else begin
		select	0 as valido,'' as TN_Id_Tarea,'' as TC_Nombre_Tarea, '' as TC_Descripcion, '' as TC_Usuarios
	end	
	commit tran Listar_Reunion_Tareas
end try
begin catch
	rollback tran Listar_Reunion_Tareas
	select	0 as valido,'' as TN_Id_Tarea,'' as TC_Nombre_Tarea, '' as TC_Descripcion, '' as TC_Usuarios
end catch
end
go

/******************************Listar Reunion_Temas******************************/
create procedure Sp_Listar_Reunion_Temas
	@TN_Id_Reunion int 
as begin
begin try
	begin tran Listar_Reunion_Temas
	if exists (select*from T_Temas where TN_Id_Reunion=@TN_Id_Reunion) begin
		select	1 as valido, T_Temas.TN_Id_Temas,T_Temas.TC_Nombre_Tema from T_Temas
		where T_Temas.TN_Id_Reunion = @TN_Id_Reunion
	end else begin
		select	0 as valido,'' as TN_Id_Temas,'' as TC_Nombre_Tema
	end	
	commit tran Listar_Reunion_Temas
end try
begin catch
	rollback tran Listar_Reunion_Temas
	select	0 as valido,'' as TN_Id_Temas,'' as TC_Nombre_Tema
end catch
end
go

/******************************Valida_Fecha_Reunion******************************/
create procedure Sp_Valida_Fecha_Reunion
    @TN_Id_Reunion int,
	@TC_Usuario varchar(100)
as begin
begin try
    begin tran Valida_Fecha_Reunion
    if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion AND  GETDATE()>= T_Reunion.TC_Fecha_Inicio and TN_Finalizada=0)begin
		declare @TN_Id_Usuario int
		select @TN_Id_Usuario=TN_Id_Usuario from T_Usuario where TC_Usuario=@TC_Usuario
		if exists(select*from T_Agenda where TN_Id_Reunion=@TN_Id_Reunion and TN_Id_Usuario=@TN_Id_Usuario)begin
			update T_Agenda set TN_Asistencia=1 where TN_Id_Reunion=@TN_Id_Reunion and TN_Id_Usuario=@TN_Id_Usuario
			select    1 as valido
		end else begin
			select    0 as valido
		end
    end else begin
        select    0 as valido
	end
    commit tran Valida_Fecha_Reunion
end try
begin catch
    rollback tran Valida_Fecha_Reunion
    select    0 as valido
end catch
end
go

/******************************Listar Reunion_Finalizada******************************/
create procedure Sp_Listar_Reunion_Finalizada
as begin
begin try
	begin tran Listar_Reunion_Finalizada
		select	1 as valido, T_Reunion.TN_Id_Reunion, T_Reunion.TC_Nombre_Reunion, T_Reunion.TN_Id_Tipo_Reunion, T_Reunion.TC_Descripcion, T_Reunion.TC_Comentario, T_Reunion.TC_Lugar, T_Reunion.TC_Fecha_Inicio, T_Tipo_Reunion.TC_Nombre_Tipo_Reunion from T_Reunion
		inner join T_Tipo_Reunion on T_Reunion.TN_Id_Tipo_Reunion=T_Tipo_Reunion.TN_Id_Tipo_Reunion
		where TN_Finalizada=1
	commit tran Listar_Reunion_Finalizada
end try
begin catch
	rollback tran Listar_Reunion_Finalizada
    select	0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Nombre_Tipo_Reunion
end catch
end
go