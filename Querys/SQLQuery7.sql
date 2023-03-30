alter PROCEDURE stpConfirmar(@ID int, @ERROR varchar(100) OUTPUT)
AS
BEGIN
    DECLARE @maestro_obtenido varchar(10)
    SELECT @maestro_obtenido = (select p.matricula from profesores p, tutoria t where t.id_profesor = p.matricula and t.id = @ID);

	DECLARE @horario_obtenido varchar(10)
    SELECT @horario_obtenido = (select horario from tutoria where id = @ID);

    DECLARE @estatus_obtenido varchar(10)
    SELECT @estatus_obtenido = (select estatus from tutoria where id = @ID );

    IF (@maestro_obtenido) = 'vacio'
	begin
        set @ERROR = 'A';
		print @ERROR;
	end

    ELSE IF (@horario_obtenido) = 'vacio'
	begin
        set @ERROR = 'B';
		print @ERROR;
	end

    ELSE IF (@estatus_obtenido) = 'I'
	begin
	    set @ERROR = 'C';
		print @ERROR;
	end

	ELSE IF (@estatus_obtenido) = 'R'
	begin
	    set @ERROR = 'E';
		print @ERROR;
	end

   ELSE
	begin
	    UPDATE tutoria SET estatus = 'A' WHERE id = @ID
	    set @ERROR = 'F';
		print @ERROR;
	end
END