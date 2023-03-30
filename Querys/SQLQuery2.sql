alter PROCEDURE stpRechazar(@ID int, @ERROR varchar(100) OUTPUT)
AS
BEGIN

    DECLARE @estatus_obtenido varchar(10)
    SELECT @estatus_obtenido = (select estatus from tutoria where id = @ID );

    IF (@estatus_obtenido) = 'Espera'
	begin
        UPDATE tutoria SET estatus = 'R' WHERE id = @ID
		set @ERROR = 'A';
		print @ERROR;
	end
	ELSE IF (@estatus_obtenido) = 'I'
	begin
	    set @ERROR = 'B';
		print @ERROR;
	end

	ELSE IF (@estatus_obtenido) = 'R'
	begin
	    set @ERROR = 'C';
		print @ERROR;
	end
	ELSE IF (@estatus_obtenido) = 'A'
	begin
	    set @ERROR = 'D';
		print @ERROR;
	end
END