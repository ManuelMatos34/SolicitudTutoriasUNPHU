--exec stpProfHorario 11,'omar kiero','lo mielcole'

alter procedure stpProfHorario(@ID int, @Prof varchar(25), @Horario varchar(100), @ERROR varchar(100) OUTPUT)
as
begin

	DECLARE @nombre varchar(25)
	DECLARE @spaceIndex int = CHARINDEX(' ', @Prof)
	DECLARE @estatus varchar(25) = (select estatus from tutoria where id = @ID)

	if(@estatus) = 'A'
		begin
			set @ERROR = 'D'
			print @ERROR
		end
	else if(@estatus) = 'I'
		begin
			set @ERROR = 'E'
			print @ERROR
		end
	else if(@estatus) = 'R'
		begin
			set @ERROR = 'F'
			print @ERROR
		end
	else if(@estatus) = 'Espera'
		begin
			IF @spaceIndex > 0
				BEGIN
					if(select id_profesor from tutoria where id = @ID) <> 'vacio'
						begin
							set @ERROR = 'A'
							print @ERROR
						end
					else if(select horario from tutoria where id = @ID) <> 'vacio'
						begin
							set @ERROR = 'B'
							print @ERROR
						end
					else
						begin
							-- Si hay un espacio, tomamos la cadena hasta ese punto
							SET @nombre = LEFT(@Prof, @spaceIndex - 1)
							DECLARE @idprof varchar(25) = (select matricula from profesores where nombre = @nombre)
							UPDATE Tutoria SET id_profesor = @idprof, horario = @Horario WHERE id = @ID
							set @ERROR = 'C'
							print @ERROR
						end
				END
		end
end

