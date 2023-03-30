--exec stpEstatus 10
--select * from tutoria

Alter PROCEDURE stpEstatus(@ID int, @ERROR varchar(100) OUTPUT)
AS
BEGIN
    DECLARE @status_obtenido varchar(10)
    SELECT @status_obtenido = estatus FROM tutoria WHERE id = @ID

    IF @status_obtenido = 'Espera'
	begin
        set @ERROR = 'No se puede cambiar estado porque la tutoria esta en estado de espera';
		print @ERROR;
	end
    ELSE IF @status_obtenido = 'A'
	begin
        UPDATE tutoria SET estatus = 'I' WHERE id = @ID
		set @ERROR = 'Estado Actualizado Correctamente';
		print @ERROR;
	end
    ELSE IF @status_obtenido = 'I'
	begin
        UPDATE tutoria SET estatus = 'A' WHERE id = @ID
		set @ERROR = 'Estado Actualizado Correctamente';
		print @ERROR;
	end
	ELSE IF @status_obtenido = 'R'
	begin
		set @ERROR = 'No se puede cambiar estado porque la tutoria ya ha sido rechazada';
		print @ERROR;
	end
END



