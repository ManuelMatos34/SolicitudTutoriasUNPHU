/****** Script for SelectTopNRows command from SSMS  ******/


  select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura, a.nombre_asignatura, concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, e.estatus
  from asignaturas a, profesores b, tutoria e, usuarios f
  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura

  select * from tutoria

  SELECT t.id AS ID,
       CONCAT(u.nombre, ' ', u.apellido) AS estudiante,
       t.asignatura AS codigo_asignatura,
       a.nombre_asignatura AS nombre_asignatura,
       CONCAT(p.nombre, ' ', p.apellido) AS profesor,
       t.horario AS horario,
       t.estatus AS estatus
FROM tutoria t
JOIN usuarios u ON t.id_usuario = u.matricula
JOIN asignaturas a ON t.asignatura = a.codigo_asignatura
JOIN profesores p ON t.id_profesor = p.matricula;

select prof from prof_asig where asig = 'INF-158'

select CONCAT(p.nombre, ' ', p.apellido) AS profesor from prof_asig a, profesores p, asignaturas f
where a.prof = p.matricula and a.asig = f.codigo_asignatura and a.asig = 'INF-158'


  select 
  concat(f.nombre,' ',f.apellido) as nombre_estudiante, 
  concat(b.nombre,' ',b.apellido) as nombre_profesor 
  from profesores b, tutoria e, usuarios f
  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.id = 10

 