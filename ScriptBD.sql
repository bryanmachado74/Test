use LenguajesLab2_B43917


create table tb_encuentro(
	encuentro_id int identity(1,1) primary key,
	encuentro_local varchar(30) not null,
	encuentro_visitante varchar(30) not null,
	encuentro_jugado bit,
	encuentro_marcador_local int not null,
	encuentro_marcador_visitante int not null,
	encuentro_probabilidad_local float not null,
	encuentro_probabilidad_empate float not null,
	encuentro_probabilidad_visitante float not null,
	encuentro_jornada int not null
);

/*Datos base*/
insert into tb_encuentro values('Aston Villa','QPR',0,0,0,63.64,25.06,15.97,1);
insert into tb_encuentro values('Sheffield Utd','Burton',0,0,0,66.67,24.81,13.11,1);



create table tb_cliente(
	cliente_id int identity(1,1) primary key,
	cliente_nombre varchar(50),
	cliente_fondos float
);

/*Dato base*/
insert into tb_cliente values('Bryan Machado',1000);
select * from tb_cliente


create table tb_apuesta(
	apuesta_id int identity(1,1) primary key,
	apuesta_cliente int,
	apuesta_encuentro int,
	apuesta_eleccion varchar(15),
	apuesta_monto int,
	CONSTRAINT chk_apuesta_eleccion CHECK (apuesta_eleccion IN ('Local', 'Empate', 'Visitante')),
	constraint fk_cliente foreign key (apuesta_cliente) references tb_cliente (cliente_id),
	constraint fk_encuentro foreign key (apuesta_encuentro) references tb_encuentro (encuentro_id)
);

/*Dato base*/
insert into tb_apuesta values(1,1,'Visitante',3);


/*Procedimientos almacenados para el CRUD de las tablas Encuentro y Apuesta. Para la tabla Cliente solo se hara el obtener una tupla y actualizar la columna saldo*/
GO
create procedure sp_listar_encuentro as
begin
	select encuentro_id, encuentro_local, encuentro_visitante, encuentro_jugado, encuentro_marcador_local, encuentro_marcador_visitante, encuentro_probabilidad_local, encuentro_probabilidad_empate, 
		   encuentro_probabilidad_visitante, encuentro_jornada from tb_encuentro; 	
end
exec sp_listar_encuentro

GO
create procedure sp_obtener_encuentro @id int as
begin
	select encuentro_id, encuentro_local, encuentro_visitante, encuentro_jugado, encuentro_marcador_local, encuentro_marcador_visitante, encuentro_probabilidad_local, encuentro_probabilidad_empate, 
		   encuentro_probabilidad_visitante, encuentro_jornada from tb_encuentro where encuentro_id = @id;  
end

GO
create procedure sp_insertar_encuentro @local varchar(30), @visitante varchar(30), @probabilidad_local float, @probabilidad_empate float, @probabilidad_visitante float, @jornada int as
begin
	insert into tb_encuentro values (@local,@visitante,0,0,0,@probabilidad_local,@probabilidad_empate,@probabilidad_visitante,@jornada);
end

GO
create procedure sp_actualizar_encuentro @id int, @local varchar(30), @visitante varchar(30), @jugado bit, @marcador_local int, @marcador_visitante int, @probabilidad_local float, @probabilidad_empate float, 
										 @probabilidad_visita float, @jornada int as
begin
	update tb_encuentro 
	set encuentro_local = @local, encuentro_visitante = @visitante, encuentro_jugado = @jugado, encuentro_marcador_local = @marcador_local,	encuentro_marcador_visitante = @marcador_visitante, 
		encuentro_probabilidad_local = @probabilidad_local, encuentro_probabilidad_empate = @probabilidad_empate, encuentro_probabilidad_visitante = @probabilidad_visita, encuentro_jornada = @jornada 
	where encuentro_id = @id;
end

GO
create procedure sp_borrar_encuentro @id int as
begin
	delete from tb_encuentro where encuentro_id = @id;
end

GO
create procedure sp_listar_apuesta as
begin
	select apuesta_id, apuesta_cliente, apuesta_encuentro, apuesta_eleccion, apuesta_monto from tb_apuesta; 	
end

GO
create procedure sp_obtener_apuesta @id int as
begin
	select apuesta_id, apuesta_cliente, apuesta_encuentro, apuesta_eleccion, apuesta_monto from tb_apuesta where apuesta_id = @id;  
end

GO
create procedure sp_insertar_apuesta @cliente int, @encuentro int, @eleccion varchar(15), @monto int as
begin
	insert into tb_apuesta values (@cliente,@encuentro,@eleccion,@monto);
end

GO
create procedure sp_actualizar_apuesta @id int, @cliente int, @encuentro int, @eleccion varchar(15), @monto int as
begin
	update tb_apuesta set apuesta_cliente = @cliente, apuesta_encuentro = @encuentro, apuesta_eleccion = @eleccion, apuesta_monto = @monto
	where apuesta_id = @id;
end

GO
create procedure sp_borrar_apuesta @id int as
begin
	delete from tb_apuesta where apuesta_id = @id;
end

GO
create procedure sp_obtener_cliente @id int as
begin
 select cliente_nombre, cliente_fondos from tb_cliente where cliente_id = @id;
end

GO
create procedure sp_actualizar_fondos_cliente @id int, @fondos float as
begin
	update tb_cliente set cliente_fondos = @fondos where cliente_id = @id;
end


