--declare @ERROR varchar(100) 
--exec stpSolicitar 'er19-4504','MINERIA DE DATOS', @ERROR OUTPUT

alter procedure stpSolicitar(@matricula varchar(15), @asignatura varchar(70), @ERROR varchar(100) OUTPUT)
as
begin

declare @codigo varchar(15) = (select codigo_asignatura from asignaturas where nombre_asignatura = @asignatura);

if (select max(id) from tutoria where id_usuario = @matricula and asignatura = @codigo and estatus = 'A') IS NOT NULL
	begin
		set @ERROR = 'A';
		print @ERROR
	end

else if (select max(id) from tutoria where id_usuario = @matricula and asignatura = @codigo and estatus = 'Espera') IS NOT NULL
	begin
		set @ERROR = 'B';
		print @ERROR
	end
else
	begin
		insert into tutoria (id_usuario, asignatura, id_profesor, horario, estatus) values (@matricula, @codigo,'vacio','vacio','Espera')
		set @ERROR = 'C';
		print @ERROR
	end
end
