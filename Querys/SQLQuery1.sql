--exec stpEstatus 10
--select * from tutoria

Alter PROCEDURE stpEstatus(@ID int, @ERROR varchar(100) OUTPUT)
AS
BEGIN
    DECLARE @status_obtenido varchar(10)
    SELECT @status_obtenido = estatus FROM tutoria WHERE id = @ID

    IF @status_obtenido = 'Espera'
	begin
        set @ERROR = 'A';
		print @ERROR;
	end
    ELSE IF @status_obtenido = 'A'
	begin
        UPDATE tutoria SET estatus = 'I' WHERE id = @ID
		set @ERROR = 'B';
		print @ERROR;
	end
    ELSE IF @status_obtenido = 'I'
	begin
        UPDATE tutoria SET estatus = 'A' WHERE id = @ID
		set @ERROR = 'C';
		print @ERROR;
	end
	ELSE IF @status_obtenido = 'R'
	begin
		set @ERROR = 'D';
		print @ERROR;
	end
END



