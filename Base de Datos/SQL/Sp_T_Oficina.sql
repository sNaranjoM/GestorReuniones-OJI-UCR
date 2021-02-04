use Gestor_Reuniones2
/******************************Insert T_Oficina******************************/
create procedure Sp_Insert_Oficina
	@TC_Nombre varchar(100),
	@TC_Codigo varchar(100),
	@TN_Id_Circuito int,
	@TF_Inicio_Vigencia date
as begin
begin try
	begin tran Insert_Oficina
	if not exists(select*from T_Oficina where TC_Nombre=@TC_Nombre or TC_Codigo=@TC_Codigo)begin
		if exists(select*from T_Circuito where TN_Id_Circuito=@TN_Id_Circuito)begin
			insert into T_Oficina(TC_Nombre,TC_Codigo,TN_Id_Circuito,TF_Inicio_Vigencia,TF_Fin_Vigencia) values(@TC_Nombre,@TC_Codigo,@TN_Id_Circuito,@TF_Inicio_Vigencia,null);
			select 1 as valido;
		end else begin 
			select 0 as valido;
		end
	end else begin 
		select 0 as valido;
	end
	commit tran Insert_Oficina
end try
begin catch
	rollback tran Insert_Oficina
	select 0 as valido;
end catch
end
go

/******************************Delete T_Oficina******************************/
create procedure Sp_Delete_Oficina
	@TN_Id_Oficina int
as begin
begin try
	begin tran Delete_Oficina
	if exists(select*from T_Oficina where TN_Id_Oficina=@TN_Id_Oficina) begin
		update T_Oficina set TB_Activa=0, TF_Fin_Vigencia=GETDATE() where TN_Id_Oficina=@TN_Id_Oficina;
		select 1 as valido;
	end else begin
		select 0 as valido;
	end
	commit tran Delete_Oficina
end try
begin catch
	rollback tran Delete_Oficina
	select 0 as valido;
end catch
end
go

/******************************Update T_Oficina******************************/
create procedure Sp_Update_Oficina
	@TN_Id_Oficina int,
	@TC_Nombre varchar(100),
	@TC_Codigo varchar(100)
as begin
begin try
	begin tran Update_Oficina
	if exists(select*from T_Oficina where TN_Id_Oficina=@TN_Id_Oficina) begin
		if not exists(select*from T_Oficina where (TC_Nombre=@TC_Nombre or TC_Codigo=@TC_Codigo) and TN_Id_Oficina!=@TN_Id_Oficina )begin
			update T_Oficina set TC_Nombre=@TC_Nombre, TC_Codigo=@TC_Codigo where TN_Id_Oficina=@TN_Id_Oficina;
			select 1 as valido;
		end else begin
			select 0 as valido;
		end
	end else begin
		select 0 as valido;
	end
	commit tran Update_Oficina
end try
begin catch
	rollback tran Update_Oficina
	select 0 as valido;
end catch
end
go

/******************************Listar T_Oficina******************************/
create procedure Sp_Listar_Oficina
as begin
begin try
	begin tran Listar_Oficina
		select TN_Id_Oficina,TC_Nombre,TC_Codigo from T_Oficina where TB_Activa=1;
	commit tran Listar_Oficina
end try
begin catch
	rollback tran Listar_Oficina
	select 0 as valido;
end catch
end
go