/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [matricula]
      ,[password]
      ,[nombre]
      ,[apellido]
      ,[email]
      ,[edad]
      ,[pemsum]
      ,[rol]
      ,[estatus]
  FROM [tutoriasUnphu].[dbo].[usuarios]


  SELECT t.id AS ID, 
  CONCAT(u.nombre, ' ', u.apellido) AS estudiante, 
  t.asignatura AS codigo_asignatura, 
  a.nombre_asignatura AS nombre_asignatura, 
  CONCAT(p.nombre, ' ', p.apellido) AS profesor, 
  t.horario AS horario, t.estatus AS estatus 
  FROM tutoria t
  JOIN usuarios u ON t.id_usuario = u.matricula 
  JOIN asignaturas a ON t.asignatura = a.codigo_asignatura 
  JOIN profesores p ON t.id_profesor = p.matricula;