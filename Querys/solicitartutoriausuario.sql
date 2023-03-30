--declare @ERROR varchar(100) 
--exec stpSolicitar 'er19-4504','MINERIA DE DATOS', @ERROR OUTPUT

alter procedure stpSolicitar(@matricula varchar(15), @asignatura varchar(70), @ERROR varchar(100) OUTPUT)
as
begin

declare @codigo varchar(15) = (select codigo_asignatura from asignaturas where nombre_asignatura = @asignatura);

if (select max(id) from tutoria where id_usuario = @matricula and asignatura = @codigo and estatus = 'A') is null
begin

insert into tutoria (id_usuario, asignatura, id_profesor, horario, estatus) values (@matricula, @codigo,'vacio','vacio','Espera')
set @ERROR = 'Solicitud realizada correctamente, estas en proceso de espera';
print @ERROR

end

else
begin
set @ERROR = 'Ya has solicitado una tutoria con esta asignatura'
print @ERROR

end
end
