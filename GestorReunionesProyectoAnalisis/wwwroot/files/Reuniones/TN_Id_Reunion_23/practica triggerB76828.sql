create trigger tr_upd_ins_delete
on RESERVAS
instead of insert,delete,update

as 
begin
declare
	@NumReserva int, 
	@ID_Cliente int, 
	@FechaInicio date,
	@FechaFinal date,
	@NumTuristaAdultos int,
	@NumTuristasNiños int,
	@IDHab int,
	@Estado char(12),
	@Total_Personas int,
	@FechaConf int
begin try
	--inserted
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @IDHab=IDHab,@ID_Cliente=ID_Cliente, @FechaInicio=FechaInicio,@NumTuristaAdultos=NumTuristaAdultos,@NumTuristasNiños=NumTuristasNiños from inserted;
		if exists(select * from CLIENTE where ID_Cliente=@ID_Cliente)begin

		set @Total_Personas=@NumTuristaAdultos+ isnull(@NumTuristasNiños,0)

			if exists (select* from HABITACIONES where capacidad>=@Total_Personas and Estado='L'and IDHab=@IDHab)begin
			
				if @FechaInicio=convert(date, getdate()begin
					set @Estado='A'--indica que se encuentra activada
					insert RESERVAS values(@NumReserva,@ID_Cliente,@FechaInicio,null,@FechaFinal,null,@NumTuristaAdultos,@NumTuristasNiños,@IDHab,@Estado)
				end
				set @FechaConf=@FechaInicio

				while @FechaConf<=@FechaFinal begin -- aquí se va a asumir que siempre se registran en el hotel antes del medio día
					insert RESTAURANTE values (@Fecha_Con, @ID_Cliente,  'A', @Total_Personas);
					insert RESTAURANTE values (@Fecha_Con, @ID_Cliente, 'C', @Total_Personas);
					set @FechaConf = dateadd (dd, 1,  @FechaConf)
				end
				update HABITACIONES set Estado='O' where IDHab=@IDHab;
				print 'La habitación cambió a estado Ocupada'
			end else begin
				print 'La habitación solicitada tiene menor capacidad o no está disponible' 
			end
		end
	end 
	--deleted
	if exists (select * from deleted) and not exists (select * from inserted)begin
		print 'La reserva cambiará de estado a Borrada'
		select @NumReserva = NumReserva from deleted
		update RESERVAS set Estado = 'B' where NumReserva = @NumReserva
	end
	--update
	if exists (select * from inserted) and  exists (select * from deleted)begin

	end
end try
begin catch

end catch
end

--Primero
create trigger tr_ins_manutencion
on MANUTENCION
instead of insert
as
begin
declare
	@cod_pais int,
	@fecha_i datetime,
	@fecha_f datetime,
	@n_dias int,
	@cod_viaje int,
	@num_gasto int,
	@dietadiario int
begin try
	select @cod_viaje=cod_viaje,@num_gasto=num_gasto,@n_dias=n_dias from inserted;
	select @cod_pais=cod_pais,@fecha_i=fecha_i,@fecha_f=fecha_f from VIAJES where cod_viaje=@cod_viaje;
	if @num_gasto=1 begin
		select @dietadiario=dietadiario_alimentacion from Dietas_Diarios where fecha_i>=@fecha_i and fecha_f<=@fecha_f;
	end
	if @num_gasto =2 begin
		select @dietadiario=dietadiario_viaje from Dietas_Diarios where fecha_i>=@fecha_i and fecha_f<=@fecha_f;
	end
	insert MANUTENCIONES (cod_viaje, num_gasto, n_dias, importe_M)
		values(@cod_viaje,@num_gasto,@n_dias,@dietadiario*@n_dias)
end try
begin catch
	 
end catch
end

--Primero Examen 
create trigger tr_pagos
on Pago
instead of insert
as
begin
declare
	@numero_pago int,
	@numero_c int,
	@fecha_pago datetime,
	@monto_pago int,
	@saldo_inicial int,
	@saldo_pendiente int
begin try
	
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @numero_pago=numero_pago, @numero_c=numero_c,@fecha_pago=fecha_pago, @monto_pago=monto_pago from inserted;
		if exists(select * from Cliente where numero_c=@numero_c)begin
			insert Pago (@numero_pago,@numero_c,@fecha_pago,@monto_pago);

			if exists(select*from Deuda where numero_c=@numero_c)begin
			select @saldo_pendiente=saldo_pendiente from Deuda where numero_c=@numero_c;
			set @saldo_pendiente=@saldo_pendiente-@monto_pago;
			if @saldo_pendiente<=0 begin 
				update Deuda set saldo_pendiente=0 where numero_c=@numero_c;
				end
			end else begin
				select @saldo_inicial=saldo_inicial from Cliente where numero_c=@numero_c;
				insert Deuda(@numero_c,@saldo_inicial);
			end
		end
	end

	if exists (select * from inserted) and  exists (select * from deleted)begin

	end
end try
begin catch
	 raiserror ('Se produjo un error', 16, 1)
end catch
end

--Segundo Examen
create trigger tr_compra
on Compra
instead of insert
as
begin
declare
	@numero_com int,
	@numero_c int,
	@fecha datetime,
	@monto_c int,
	@saldo_inicial int,
	@monto_d int
begin try
	
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @numero_com=numero_com, @numero_c=numero_c,@fecha=fecha, @monto_c=monto_c from inserted;
		if exists(select * from Cliente where numero_c=@numero_c and tipo=1)begin
			insert Compra (@numero_pago,@numero_c,@fecha_pago,@monto_pago);

			if exists(select*from Deuda where numero_c=@numero_c)begin
				select @monto_d=monto_d from Deuda where numero_c=@numero_c;
				set @monto_d=@monto_d+@monto_pago+@monto_pago*0.1;
			
				update Deuda set monto_d=@monto_d where numero_c=@numero_c;
			
			end else begin
				set @monto_d=@monto_c+@monto_c*0.1;
				insert Deuda(@numero_c,@monto_d,0.1);
			end
		end
	end
end try
begin catch
	 raiserror ('Se produjo un error', 16, 1)
end catch
end

--Tercero Examen
create trigger tr_upd_ins_delete_citas
on Citas
instead of insert,delete,update

as 
begin
declare
	@fecha date,
	@hora time,
	@idM int,
	@idP int,
	@estadoC char(2),
	@tarjeta int,
	@idMed int,
	@fechaM date,
	@idMNuevo int
begin try
	--inserted 5pts
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @fecha=fecha,@hora=hora,@idM=idM,@idP=idP,@estadoC=estadoC,@tarjeta=tarjeta from inserted;
		if exists(select*from AgendaMedico where fecha=@fecha and hora=@hora and estadoA='L')begin
			select @idMed=idM, @fechaM=fecha from AgendaMedico where  fecha=@fecha and hora=@hora and estadoA='L';
			update AgendaMedico set estadoA='O' where fecha=@fecha and hora=@hora and estadoA='L';
			update ResumenCitas_diarias_medico set numpacientes=numpacientes+1 where idM=@idMed and fecha=@fecha;
			set @estadoC='PE';
			insert Citas values(@fecha,@hora,@idMed,@idP,@estadoC,@tarjeta);
		end
	end 
	--deleted 5pts
	if exists (select * from deleted) and not exists (select * from inserted)begin
		select @fecha=fecha,@hora=hora,@idM=idM,@idP=idP from deleted;
		--numfactura que es pk debe ser auto incrementado
		if exists(select *from Citas where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP)begin
			select @estadoC=estadoC, @tarjeta=tarjeta from Citas where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP;
			if @fecha=convert(date, getdate()begin
				insert Caja values(@idP,2000,@tarjeta);
				update Citas set estadoC='CC' where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP;
			end else begin
				update Citas set estadoC='CA' where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP;
			end
			update AgendaMedico set estadoA='L' where fecha=@fecha and hora=@hora and idM=@idM;
			update ResumenCitas_diarias_medico set numpacientes=numpacientes-1 where idM=@idM and fecha=@fecha;
		end
	end

	--update 5pts
	if exists (select * from inserted) and  exists (select * from deleted)begin
		select @fecha=fecha,@hora=hora,@idM=idM,@idP=idP, @idMNuevo=idMNuevo from inserted;
		if exists(select *from Citas where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP)begin

			if exists(select* from AgendaMedico where fecha=@fecha and hora=@hora and idM=@idMNuevo and estadoA='L' )begin
				update AgendaMedico set estadoA='L' where fecha=@fecha and hora=@hora and idM=@idM;
				update ResumenCitas_diarias_medico set numpacientes=numpacientes-1 where idM=@idM and fecha=@fecha;

				update AgendaMedico set estadoA='O' where fecha=@fecha and hora=@hora and idM=@idMNuevo;
				update ResumenCitas_diarias_medico set numpacientes=numpacientes+1 where idM=@idMNuevo and fecha=@fecha;

				update Citas set idM=@idMNuevo where fecha=@fecha and hora=@hora and idM=@idM and idP=@idP;
			end 
		end
	end
end try
begin catch
	 raiserror ('Se produjo un error', 16, 1)
end catch
end













--Primero Examen 
create trigger tr_Admision
on Pago
instead of insert
as
begin
declare
	@idS int,
	@DNIP int,
	@fecha_Ádmision datetime,
	@consecutivo_tratam int,
	@DNIM int,
	@num_Sala int,
	@monto_trat int,
	@especialidad varchar(50)
begin try
	
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @numero_pago=numero_pago, @numero_c=numero_c,@fecha_pago=fecha_pago, @monto_pago=monto_pago from inserted;
		if exists(select * from Paciente where DNIP=@DNIP)begin
			//insert Pago (@numero_pago,@numero_c,@fecha_pago,@monto_pago);

			if exists(select*from Sala where num_Camas>num_ocupados and especialidad = @especialiad )begin
			select @saldo_pendiente=saldo_pendiente from Deuda where numero_c=@numero_c;
			set @saldo_pendiente=@saldo_pendiente-@monto_pago;
			update Sala set num_ocupados = num_ocupados+1;
			update Sala set %_ocupado = (num_ocupados*100/num_camas);


			if @saldo_pendiente<=0 begin 
				update Deuda set saldo_pendiente=0 where numero_c=@numero_c;
				end
			end else begin
				select @saldo_inicial=saldo_inicial from Cliente where numero_c=@numero_c;
				insert Deuda(@numero_c,@saldo_inicial);
			end
		end
	end

	if exists (select * from inserted) and  exists (select * from deleted)begin

	end
end try
begin catch
	 raiserror ('Se produjo un error', 16, 1)
end catch
end


create trigger tr_Admision
on Admision
instead of insert
as
begin
declare
	@idS int,
	@DNIP int,
	@fecha_Ádmision datetime,
	@consecutivo_tratam int,
	@DNIM int,
	@num_Sala int,
	@monto_trat int,
	@especialidad varchar(50)
begin try
	
	if exists (select * from inserted) and not exists (select * from deleted)begin
		select @numero_pago=numero_pago, @numero_c=numero_c,@fecha_pago=fecha_pago, @monto_pago=monto_pago from inserted;
		if exists(select * from Paciente where DNIP=@DNIP)begin
			//insert Pago (@numero_pago,@numero_c,@fecha_pago,@monto_pago);

			if exists(select*from Sala where num_Camas>num_ocupados and especialidad = @especialiad )begin
			select @saldo_pendiente=saldo_pendiente from Deuda where numero_c=@numero_c;
			set @saldo_pendiente=@saldo_pendiente-@monto_pago;
			update Sala set num_ocupados = num_ocupados+1;
			update Sala set %_ocupado = (num_ocupados*100/num_camas);

		end
	end

end try
begin catch
	 raiserror ('Se produjo un error', 16, 1)
end catch
end