use Gestor_Reuniones2
/************************************************************Dashboard General************************************************************/
/**************Cantidad de reuniones por mes **************/
create procedure Sp_Cantidad_Reuniones
    @TC_Fecha_Inicio date,
    @TC_Fecha_Fin date
as begin
begin try
	begin tran Sp_Cantidad_Reuniones
	
	Declare @TN_Mes_Inicio int, @TN_Anno_Inicio int, @TN_Anno_Fin int,  @TN_dif_fechas int,  @TN_Cantidades_Mes int,
	 @TN_Cantidades_Totales Varchar(500), @cont int;

	Set @TN_Mes_Inicio= MONTH(@TC_Fecha_Inicio)

	Set @TN_Anno_Inicio= YEAR(@TC_Fecha_Inicio)

	Set @TN_Anno_Fin= YEAR(@TC_Fecha_Fin)

	set @TN_dif_fechas = DATEDIFF(month, @TC_Fecha_Inicio, @TC_Fecha_Fin )+1

	Set @cont=  0

	if exists (Select * from T_Reunion where T_Reunion.TC_Fecha_Inicio BETWEEN @TC_Fecha_Inicio AND @TC_Fecha_Fin)
	begin

		while  @cont<= @TN_dif_fechas
		begin

			Select @TN_Cantidades_Mes= count(T_Reunion.TN_Id_Reunion) from T_Reunion where  MONTH(T_Reunion.TC_Fecha_Inicio)= @TN_Mes_Inicio AND YEAR(T_Reunion.TC_Fecha_Inicio) = @TN_Anno_Inicio
		
			if (@cont = 0)
			begin
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, '', @TN_Cantidades_Mes) 
			end
			else
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, ',', @TN_Cantidades_Mes)


			if (@TN_Mes_Inicio = 12)
				begin
				
					set @TN_Mes_Inicio= 1
					set @TN_Anno_Inicio= @TN_Anno_Inicio +1
				end

			else if (@TN_Mes_Inicio < 12)
		
				begin

					set @TN_Mes_Inicio= @TN_Mes_Inicio+1
			
				end
			
			set @cont+= 1
		
		end

		Select 1 as valido, @TN_Cantidades_Totales as TN_Cantidad_Reuniones
	end
	else
	  Select  0 as valido, @TN_Cantidades_Totales as TN_Cantidad_Reuniones

	commit tran Sp_Cantidad_Reuniones
end try
begin catch
	rollback tran Sp_Cantidad_Reuniones
	print('Error obteniendo cantidad de reuniones por mes')
end catch
end
go

/**************Cantidad de tiempo invertido reuniones por mes **************/
create procedure Sp_Cantidad_Tiempo_Reuniones
    @TC_Fecha_Inicio date,
    @TC_Fecha_Fin date
as begin
begin try
	begin tran Sp_Cantidad_Tiempo_Reuniones
	
	Declare @TN_Mes_Inicio int, @TN_Anno_Inicio int, @TN_Anno_Fin int,  @TN_dif_fechas int,  @TN_Cantidades_Tiempo int,
	 @TN_Cantidades_Totales Varchar(500), @cont int;

	Set @TN_Mes_Inicio= MONTH(@TC_Fecha_Inicio)

	Set @TN_Anno_Inicio= YEAR(@TC_Fecha_Inicio)

	Set @TN_Anno_Fin= YEAR(@TC_Fecha_Fin)

	set @TN_dif_fechas = DATEDIFF(month, @TC_Fecha_Inicio, @TC_Fecha_Fin )+1

	Set @cont=  0

	if exists (Select * from T_Reunion where T_Reunion.TC_Fecha_Inicio BETWEEN @TC_Fecha_Inicio AND @TC_Fecha_Fin)
	begin

		while  @cont<= @TN_dif_fechas
		begin

			if exists (Select * from T_Reunion where  MONTH(T_Reunion.TC_Fecha_Inicio)= @TN_Mes_Inicio AND YEAR(T_Reunion.TC_Fecha_Inicio) = @TN_Anno_Inicio)
			begin
				Select @TN_Cantidades_Tiempo= SUM(DATEDIFF (HOUR, T_Reunion.TC_Fecha_Inicio, T_Reunion.TC_Fecha_Final) ) 
						from T_Reunion where  MONTH(T_Reunion.TC_Fecha_Inicio)= @TN_Mes_Inicio AND YEAR(T_Reunion.TC_Fecha_Inicio) = @TN_Anno_Inicio
			end
			else
				Select @TN_Cantidades_Tiempo=0;

		
			if (@cont = 0)
			begin
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, '', @TN_Cantidades_Tiempo) 
			end
			else
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, ',', @TN_Cantidades_Tiempo)


			if (@TN_Mes_Inicio = 12)
				begin
				
					set @TN_Mes_Inicio= 1
					set @TN_Anno_Inicio= @TN_Anno_Inicio +1
				end

			else if (@TN_Mes_Inicio < 12)
		
				begin

					set @TN_Mes_Inicio= @TN_Mes_Inicio+1
			
				end
			
			set @cont+= 1
		
		end

		Select 1 as valido, @TN_Cantidades_Totales as TN_Cantidad_Tiempo_Reuniones
	end
	else
	  Select  0 as valido, @TN_Cantidades_Totales as TN_Cantidad_Tiempo_Reuniones

	commit tran Sp_Cantidad_Tiempo_Reuniones
end try
begin catch
	rollback tran Sp_Cantidad_Tiempo_Reuniones
	print('Error obteniendo tiempo total de reuniones por mes')
end catch
end
go

/**************Cantidad dinero invertido reuniones por mes **************/
create procedure Sp_Cantidad_Dinero_Reuniones
    @TC_Fecha_Inicio date,
    @TC_Fecha_Fin date
as begin
begin try
	begin tran Sp_Cantidad_Dinero_Reuniones
	
	Declare @TN_Mes_Inicio int, @TN_Anno_Inicio int, @TN_Anno_Fin int,  @TN_dif_fechas int,  @TN_Cantidades_Dinero int,
	 @TN_Cantidades_Totales Varchar(500), @cont int;

	Set @TN_Mes_Inicio= MONTH(@TC_Fecha_Inicio)

	Set @TN_Anno_Inicio= YEAR(@TC_Fecha_Inicio)

	Set @TN_Anno_Fin= YEAR(@TC_Fecha_Fin)

	set @TN_dif_fechas = DATEDIFF(month, @TC_Fecha_Inicio, @TC_Fecha_Fin )+1

	Set @cont=  0

	if exists (Select * from T_Reunion where T_Reunion.TC_Fecha_Inicio BETWEEN @TC_Fecha_Inicio AND @TC_Fecha_Fin)
	begin

		while  @cont< @TN_dif_fechas
		begin

			if exists (Select * from T_Reunion where  MONTH(T_Reunion.TC_Fecha_Inicio)= @TN_Mes_Inicio AND YEAR(T_Reunion.TC_Fecha_Inicio) = @TN_Anno_Inicio)
			begin
				Select @TN_Cantidades_Dinero= Sum(P.TN_Salario * DATEDIFF (HOUR, R.TC_Fecha_Inicio, R.TC_Fecha_Final) ) from T_Reunion R INNER JOIN T_Agenda A ON R.TN_Id_Reunion = A.TN_Id_Reunion
				INNER JOIN T_Usuario U ON A.TN_Id_Usuario = U.TN_Id_Usuario 
				INNER JOIN T_Puesto P ON U.TN_Id_Puesto =  P.TN_Id_Puesto
				Where MONTH(R.TC_Fecha_Inicio)= @TN_Mes_Inicio AND YEAR(R.TC_Fecha_Inicio) = @TN_Anno_Inicio AND A.TN_Asistencia = 1
			end
			else

				Select @TN_Cantidades_Dinero=0;
		
			if (@cont = 0)
			begin
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, '', @TN_Cantidades_Dinero) 
			end
			else
				Select @TN_Cantidades_Totales= CONCAT(@TN_Cantidades_Totales, ',', @TN_Cantidades_Dinero)


			if (@TN_Mes_Inicio = 12)
				begin
				
					set @TN_Mes_Inicio= 1
					set @TN_Anno_Inicio= @TN_Anno_Inicio +1
				end

			else if (@TN_Mes_Inicio < 12)
		
				begin

					set @TN_Mes_Inicio= @TN_Mes_Inicio+1
			
				end			
			set @cont+= 1	
		end

		Select 1 as valido, @TN_Cantidades_Totales as TN_Cantidad_Dinero_Reuniones
	end
	else
	  Select  0 as valido, @TN_Cantidades_Totales as TN_Cantidad_Dinero_Reuniones

	commit tran Sp_Cantidad_Dinero_Reuniones
end try
begin catch
	rollback tran Sp_Cantidad_Dinero_Reuniones
	print('Error obteniendo dinero total de reuniones por mes')
end catch
end
go

/**************Tipo de reuniones realizadas en esos meses **************/
/**************Cantidad tipos reuniones por mes**************/
create procedure Sp_Nombre_Tipo_Reunion_Mes
    @TC_Fecha_Inicio date,
    @TC_Fecha_Final date
as begin
begin try
	begin tran Sp_Nombre_Tipo_Reunion_Mes
	declare @Cantidad_Meses int, @Mes int, @Anno int, @TN_Id_Tipo_Reunion int, @TC_Nombre_Tipo_Reunion varchar(50), @cantidad int, @Mes_Final int, @Anno_Final int

	set @Cantidad_Meses= DATEDIFF(month, @TC_Fecha_Inicio, @TC_Fecha_Final) 
	set @Mes=MONTH(@TC_Fecha_Inicio)
	set @Anno=YEAR(@TC_Fecha_Inicio)

	set @Mes_Final=MONTH(@TC_Fecha_Final)
	set @Anno_Final=YEAR(@TC_Fecha_Final)

	Declare @Meses_Evaluar table (mes int, anno int)
	Declare @Resultado_Final table(valido int, TN_Id_Tipo_Reunion int, TC_Nombre_Tipo_Reunion varchar(50), cantidad int)
	Declare @Resultado_Final2 table(valido int, TC_Nombre_Tipo_Reunion varchar(50), cantidad int)
	Declare @Tabla_Temp table (fecha datetime, TN_Id_Tipo_Reunion int)
	Declare @Tabla_Tipos table (TN_Id_Tipo_Reunion int, TC_Nombre_Tipo_Reunion varchar(50))
	while(@Cantidad_Meses>=0)begin
		insert into @Meses_Evaluar(mes,anno)values(@Mes,@Anno)
		if(@Mes=12)begin
			set @Mes=1
			set @Anno+=1
		end else begin
			set @Mes+=1
		end
		set @Cantidad_Meses-=1
	end

	if exists(select*from T_Reunion 
	inner join @Meses_Evaluar on MONTH(T_Reunion.TC_Fecha_Inicio)=mes and YEAR(T_Reunion.TC_Fecha_Inicio)=anno
	where T_Reunion.TN_Finalizada =1)begin
		while exists(select*from @Meses_Evaluar)begin
			set rowcount 1
			select @Mes=mes, @Anno=anno from @Meses_Evaluar
			set rowcount 0

			insert into @Tabla_Temp select T_Reunion.TC_Fecha_Inicio, T_Reunion.TN_Id_Tipo_Reunion from T_Reunion
			where MONTH(T_Reunion.TC_Fecha_Inicio)=@Mes and YEAR(T_Reunion.TC_Fecha_Inicio)=@Anno and T_Reunion.TN_Finalizada =1
			
			insert into @Tabla_Tipos select TN_Id_Tipo_Reunion, TC_Nombre_Tipo_Reunion from T_Tipo_Reunion order by TN_Id_Tipo_Reunion

			while exists(select*from @Tabla_Tipos)begin
				set rowcount 1
					select @TN_Id_Tipo_Reunion=TN_Id_Tipo_Reunion, @TC_Nombre_Tipo_Reunion=TC_Nombre_Tipo_Reunion from @Tabla_Tipos
				set rowcount 0

				select @cantidad=COUNT(TN_Id_Tipo_Reunion) from @Tabla_Temp where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion

				insert into @Resultado_Final(valido, TN_Id_Tipo_Reunion, TC_Nombre_Tipo_Reunion, cantidad)values(1,@TN_Id_Tipo_Reunion,@TC_Nombre_Tipo_Reunion,@cantidad)
		
				delete @Tabla_Tipos where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion
			end
			delete @Meses_Evaluar where @Mes=mes and @Anno=anno
			delete @Tabla_Temp
		end
	--select*from @Resultado_Final
	insert into @Tabla_Tipos select TN_Id_Tipo_Reunion, TC_Nombre_Tipo_Reunion from T_Tipo_Reunion order by TN_Id_Tipo_Reunion
	
	while exists(select*from @Tabla_Tipos)begin
		set rowcount 1
			select @TN_Id_Tipo_Reunion=TN_Id_Tipo_Reunion, @TC_Nombre_Tipo_Reunion=TC_Nombre_Tipo_Reunion from @Tabla_Tipos
		set rowcount 0

			set @cantidad=0;
			set @TC_Nombre_Tipo_Reunion='';

			while exists(select*from @Resultado_Final where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion)begin
				set rowcount 1
				select @cantidad+=cantidad, @TC_Nombre_Tipo_Reunion=TC_Nombre_Tipo_Reunion from @Resultado_Final where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion
				delete @Resultado_Final where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion
				set rowcount 0
			end

			insert into @Resultado_Final2(valido,TC_Nombre_Tipo_Reunion,cantidad) values(1,@TC_Nombre_Tipo_Reunion,@cantidad)

		delete @Tabla_Tipos where TN_Id_Tipo_Reunion=@TN_Id_Tipo_Reunion
	end
	select*from @Resultado_Final2
	end
	commit tran Sp_Nombre_Tipo_Reunion_Mes
end try
begin catch
	rollback tran Sp_Nombre_Tipo_Reunion_Mes
	select 0 as valido, '' as mes, '' as anno, '' as TC_Nombre_Tipo_Reunion, '' as cantidad
end catch
end
go

/************************************************************Dashboard ESpecíficos Por Reunion ************************************************************/

/**************Cantidad dinero invertido por reunion  **************/

create procedure Sp_Cantidad_Dinero_Por_Reunion

    @TN_Id_Reunion int

as begin
begin try
    begin tran Sp_Cantidad_Dinero_Por_Reunion

    if exists (Select * from T_Reunion where T_Reunion.TN_Id_Reunion = @TN_Id_Reunion)
    begin

        Select 1 as valido, Sum(P.TN_Salario * DATEDIFF (HOUR, R.TC_Fecha_Inicio, R.TC_Fecha_Final) ) as TN_Cantidad_Dinero_Reuniones from T_Reunion R 
            INNER JOIN T_Agenda A ON R.TN_Id_Reunion = A.TN_Id_Reunion
                INNER JOIN T_Usuario U ON A.TN_Id_Usuario = U.TN_Id_Usuario 
                    INNER JOIN T_Puesto P ON U.TN_Id_Puesto =  P.TN_Id_Puesto
                        Where  R.TN_Id_Reunion = @TN_Id_Reunion AND A.TN_Asistencia = 1
    end
    else

      Select  0 as valido, NULL as TN_Cantidad_Dinero_Reuniones

    commit tran Sp_Cantidad_Dinero_Por_Reunion
end try
begin catch
    rollback tran Sp_Cantidad_Dinero_Por_Reunion
    print('Error obteniendo dinero invertido en la reunion')
end catch
end
go

/**************Cantidad Tareas Finalizadas de Reunion **************/
create procedure Sp_Cantidad_Tareas_Finalizadas

	@TN_Id_Reunion int

as begin
begin try
	begin tran Sp_Cantidad_Tareas_Finalizadas

	Declare @TN_CanT_Tareas_Finalizadas int, @TN_CanT_Tareas_Activas int

	if exists (Select * from T_Reunion where T_Reunion.TN_Id_Reunion = @TN_Id_Reunion)
	begin

		Select @TN_CanT_Tareas_Finalizadas=count(T.TN_Id_Tarea)  from T_Reunion R 
			INNER JOIN  T_Lista_Tareas LT on R.TN_Id_Reunion = LT.TN_Id_Reunion
			INNER JOIN T_Tarea T on LT.TN_Id_Tarea = T.TN_Id_Tarea where R.TN_Id_Reunion = @TN_Id_Reunion AND T.TB_Estado = 0

	 Select @TN_CanT_Tareas_Activas=count(T.TN_Id_Tarea)  from T_Reunion R 
			INNER JOIN  T_Lista_Tareas LT on R.TN_Id_Reunion = LT.TN_Id_Reunion
			INNER JOIN T_Tarea T on LT.TN_Id_Tarea = T.TN_Id_Tarea where R.TN_Id_Reunion = @TN_Id_Reunion AND T.TB_Estado = 1

	    Select 1 as valido, @TN_CanT_Tareas_Finalizadas as TN_CanT_Tareas_Finalizadas, @TN_CanT_Tareas_Activas as TN_CanT_Tareas_Activas

	end
	else
	
	  Select  0 as valido, NULL as TN_CanT_Tareas_Finalizadas, NULL as TN_CanT_Tareas_Activas

	commit tran Sp_Cantidad_Tareas_Finalizadas
end try
begin catch
	rollback tran Sp_Cantidad_Tareas_Finalizadas
	print('Error obteniendo las tareas finalizadas de la reunion')
end catch
end
go

/**************Cantidad Asistencia en una Reunion **************/
create procedure Sp_Asistencia_Reunion_Unica

	@TN_Id_Reunion int

as begin
begin try
	begin tran Sp_Asistencia_Reunion_Unica

	Declare @TN_Usuarios_Asistieron int, @TN_Usuarios_Faltaron int

	if exists (Select * from T_Reunion where T_Reunion.TN_Id_Reunion = @TN_Id_Reunion)
	begin

		Select @TN_Usuarios_Asistieron= count(A.TN_Id_Usuario)  from T_Reunion R 
			INNER JOIN  T_Agenda A on R.TN_Id_Reunion = A.TN_Id_Reunion
			where R.TN_Id_Reunion = @TN_Id_Reunion AND A.TN_Asistencia = 1

		Select  @TN_Usuarios_Faltaron= count(A.TN_Id_Usuario)  from T_Reunion R 
			INNER JOIN  T_Agenda A on R.TN_Id_Reunion = A.TN_Id_Reunion
			where R.TN_Id_Reunion = @TN_Id_Reunion AND A.TN_Asistencia = 0

	    Select 1 as valido, @TN_Usuarios_Asistieron as TN_Usuarios_Asistieron, @TN_Usuarios_Faltaron as TN_Usuarios_Faltaron

	end
	else
	
	  Select  0 as valido, NULL as TN_CanT_Tareas_Finalizadas, NULL as TN_CanT_Tareas_Activas

	commit tran Sp_Asistencia_Reunion_Unica
end try
begin catch
	rollback tran Sp_Asistencia_Reunion_Unica
	print('Error obteniendo la asistencia de la reunion')
end catch
end
go

/************************************************************Minuta Reunion ************************************************************/


/******************************Listar Info Reunion minuta******************************/
create procedure Sp_Listar_Unico_Reunion_Minuta
    @TN_Id_Reunion int 
as begin
begin try
    begin tran Sp_Listar_Unico_Reunion_Minuta

    if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
        select    1 as valido, T_Reunion.TN_Id_Reunion, T_Reunion.TC_Nombre_Reunion, T_Reunion.TN_Id_Tipo_Reunion, T_Reunion.TC_Descripcion, T_Reunion.TC_Comentario, T_Reunion.TC_Lugar, T_Reunion.TC_Fecha_Inicio, T_Reunion.TC_Fecha_Final,  T_Usuario.TC_Nombre_Usuario , T_Tipo_Reunion.TC_Nombre_Tipo_Reunion  from T_Reunion
        inner join T_Tipo_Reunion on T_Reunion.TN_Id_Tipo_Reunion=T_Tipo_Reunion.TN_Id_Tipo_Reunion 
		inner join T_Usuario on T_Reunion.TN_Id_Usuario = T_Usuario.TN_Id_Usuario where T_Reunion.TN_Id_Reunion=@TN_Id_Reunion
    end else begin
        select    0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Fecha_Final, '' as TC_Nombre_Usuario ,'' as TC_Nombre_Tipo_Reunion
    end
    commit tran Sp_Listar_Unico_Reunion_Minuta
end try
begin catch
    rollback tran Sp_Listar_Unico_Reunion_Minuta
    select    0 as valido,'' as TN_Id_Reunion, '' as TC_Nombre_Reunion, '' as TN_Id_Tipo_Reunion, '' as TC_Descripcion, '' as TC_Comentario, '' as TC_Lugar, '' as TC_Fecha_Inicio, '' as TC_Fecha_Final, '' as TC_Nombre_Usuario ,'' as TC_Nombre_Tipo_Reunion
end catch
end
go

 /******************************Listar Usuarios minuta******************************/
create procedure Sp_Listar_Usuarios_Minuta
 @TN_Id_Reunion int 
as begin
begin try
	begin tran Sp_Listar_Usuarios_Minuta
		if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin
			Select 1 as valido, U.TC_Nombre_Usuario, U.TC_Primer_Apellido, U.TC_Segundo_Apellido, P.TC_Nombre_Puesto, CAST(A.TN_Asistencia AS INT) AS TN_Asistencia from T_Reunion R 
				INNER JOIN  T_Agenda A on R.TN_Id_Reunion = A.TN_Id_Reunion
				INNER JOIN T_Usuario U on A.TN_Id_Usuario= U.TN_Id_Usuario 
				INNER JOIN T_Puesto P on U.TN_Id_Puesto= P.TN_Id_Puesto
				where R.TN_Id_Reunion = @TN_Id_Reunion 
		end
		else
			select    0 as valido,'' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido, '' as TC_Nombre_Puesto, 0 as TN_Asistencia

	commit tran Sp_Listar_Usuarios_Minuta
end try
begin catch
	rollback tran Sp_Listar_Usuarios_Minuta
	select    0 as valido,'' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido, '' as TC_Nombre_Puesto, 0 as TN_Asistencia
end catch
end
go

/******************************Listar Tareas minuta******************************/
create procedure Sp_Listar_Tareas_Minuta
 @TN_Id_Reunion int 
as begin
begin try
	begin tran Sp_Listar_Tareas_Minuta


		if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin

			Select 1 as valido, T.TC_Nombre_Tarea, T.TC_Descripcion, CAST(T.TB_Estado AS INT) TB_Estado, T.TC_Acuerdo, U.TC_Nombre_Usuario, U.TC_Primer_Apellido, U.TC_Segundo_Apellido from T_Reunion R 
				INNER JOIN  T_Lista_Tareas LT on R.TN_Id_Reunion = LT.TN_Id_Reunion
				INNER JOIN T_Tarea T on LT.TN_Id_Tarea= T.TN_Id_Tarea 
				INNER JOIN T_Tarea_Usuario TU on T.TN_Id_Tarea= TU.TN_Id_Tarea
				INNER JOIN T_Usuario U on Tu.TN_Id_Usuario = U.TN_Id_Usuario
				where R.TN_Id_Reunion = @TN_Id_Reunion 
		end
		else
			select    0 as valido,'' as TC_Nombre_Tarea, '' as TC_Descripcion, '' as TB_Estado, '' as TC_Acuerdo, '' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido


	commit tran Sp_Listar_Tareas_Minuta
end try
begin catch
	rollback tran Sp_Listar_Tareas_Minuta
	select 0 as valido,'' as TC_Nombre_Tarea, '' as TC_Descripcion, '' as TB_Estado, '' as TC_Acuerdo, '' as TC_Nombre_Usuario, '' as TC_Primer_Apellido, '' as TC_Segundo_Apellido
end catch
end
go

/******************************Listar  temas minuta******************************/
create procedure Sp_Listar_Temas_Minuta
 @TN_Id_Reunion int 
as begin
begin try
	begin tran Sp_Listar_Temas_Minuta


		if exists(select*from T_Reunion where TN_Id_Reunion=@TN_Id_Reunion)begin

			Select 1 as valido, T.TC_Nombre_Tema, T.TC_Acuerdo from T_Reunion R 
				INNER JOIN  T_Temas T on R.TN_Id_Reunion = T.TN_Id_Reunion
				where R.TN_Id_Reunion = @TN_Id_Reunion 
		end
		else
			select 0 as valido,'' as TC_Nombre_Tema, '' as TC_Acuerdo


	commit tran Sp_Listar_Temas_Minuta
end try
begin catch
	rollback tran Sp_Listar_Temas_Minuta
	select 0 as valido,'' as TC_Nombre_Tema, '' as TC_Acuerdo
end catch
end
go

/**************Asistencia a reuniones**************/
create procedure Sp_Asistencia_Reuniones
@TC_Fecha_Inicio date,
@TC_Fecha_Final date
as begin
begin try
	begin tran Asistencia_Reunion
	declare @Cantidad_Meses int, @Mes int, @Anno int, @asistencia int, @invitados int, @Mes_Final int, @Anno_Final int

	set @Cantidad_Meses= DATEDIFF(month, @TC_Fecha_Inicio, @TC_Fecha_Final) 
	set @Mes=MONTH(@TC_Fecha_Inicio)
	set @Anno=YEAR(@TC_Fecha_Inicio)

	set @Mes_Final=MONTH(@TC_Fecha_Final)
	set @Anno_Final=YEAR(@TC_Fecha_Final)

	Declare @Meses_Evaluar table (mes int, anno int)
	Declare @Resultado_Final table(valido int,mes int, anno int, asistencia int, invitados int)
	Declare @Tabla_Temp table (fecha datetime, TN_Asistencia bit)
	while(@Cantidad_Meses>=0)begin
		insert into @Meses_Evaluar(mes,anno)values(@Mes,@Anno)
		if(@Mes=12)begin
			set @Mes=1
			set @Anno+=1
		end else begin
			set @Mes+=1
		end
		set @Cantidad_Meses-=1
	end

	if exists(select*from T_Reunion 
	inner join @Meses_Evaluar on MONTH(T_Reunion.TC_Fecha_Inicio)=mes and YEAR(T_Reunion.TC_Fecha_Inicio)=anno
	where T_Reunion.TN_Finalizada =1)begin
		while exists(select*from @Meses_Evaluar)begin
			set rowcount 1
			select @Mes=mes, @Anno=anno from @Meses_Evaluar
			set rowcount 0

			insert into @Tabla_Temp select T_Reunion.TC_Fecha_Inicio, T_Agenda.TN_Asistencia from T_Reunion
			inner join T_Agenda on T_Reunion.TN_Id_Reunion=T_Agenda.TN_Id_Reunion
			where MONTH(T_Reunion.TC_Fecha_Inicio)=@Mes and YEAR(T_Reunion.TC_Fecha_Inicio)=@Anno and T_Reunion.TN_Finalizada =1
			delete @Meses_Evaluar where @Mes=mes and @Anno=anno

			if exists(select*from @Tabla_Temp)begin
				select @asistencia=COUNT(TN_Asistencia) from @Tabla_Temp where TN_Asistencia=1
				select @invitados=COUNT(TN_Asistencia) from @Tabla_Temp
				insert into @Resultado_Final(valido, mes, anno, asistencia, invitados) values(1,@Mes,@Anno,@asistencia,@invitados)
			end else begin
				insert into @Resultado_Final(valido, mes, anno, asistencia, invitados) values(1,@Mes,@Anno,0,0)
			end
			delete @Tabla_Temp
		end
		select*from @Resultado_Final
	end else begin
		select 0 as valido, '' as mes, '' as anno, '' as asistencia, '' as invitados
	end
	commit tran Asistencia_Reunion
end try
begin catch
	rollback tran Asistencia_Reunion
	select 0 as valido, '' as mes, '' as anno, '' as asistencia, '' as invitados
end catch

end
go

/**Cantidad dinero invertido por reunion  **/
create procedure Sp_Tiempo_Por_Reunion

    @TN_Id_Reunion int

as begin
begin try
    begin tran Sp_Tiempo_Por_Reunion

    if exists (Select * from T_Reunion where T_Reunion.TN_Id_Reunion = @TN_Id_Reunion and TN_Finalizada = 1)
    begin
        select 1 as valido, DATEDIFF(MINUTE,T_Reunion.TC_Fecha_Inicio,T_Reunion.TC_Fecha_Final) as duracion from T_Reunion where T_Reunion.TN_Id_Reunion = @TN_Id_Reunion and TN_Finalizada = 1 
    end else begin
		Select  0 as valido, '' as duracion
	end
    commit tran Sp_Tiempo_Por_Reunion
end try
begin catch
    rollback tran Sp_Tiempo_Por_Reunion
	Select  0 as valido, '' as duracion
    print('Error obteniendo dinero invertido en la reunion')
end catch
end
go