using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Tutorias_Unphu.Models;

namespace Tutorias_Unphu.Controllers
{
    public class TutoriasUnphuController : Controller
    {
        private readonly TutoriasUnphuContext _context;
        public TutoriasUnphuController(TutoriasUnphuContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["Fecha"] = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            ViewBag.MisTutorias = mistutorias();
            ViewBag.Asignaturas = asignaturas();

            return View();
        }
        public Usuario usuario()
        {
            Usuario usuarioDatos = _context.Usuarios.FromSqlRaw("Select * from usuarios where matricula = '" + User.Identity.Name + "'").FirstOrDefault();
            return usuarioDatos;
        }

        public List<Asignatura> asignaturas()
        {
            var usuarioActual = usuario();
            List<Asignatura> asignaturaDatos = _context.Asignaturas.FromSqlRaw("select * from asignaturas where pemsum = '"+usuarioActual.Pemsum+"'").ToList();
            return asignaturaDatos;
        }

        public List<Tutorias> mistutorias()
        {
            SqlConnection conexion = Conexion.GetConexion();
            conexion.Open();
            string sql = "select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura, a.nombre_asignatura, concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, e.estatus\r\n  from asignaturas a, profesores b, tutoria e, usuarios f\r\n  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura and e.id_usuario = '" + User.Identity.Name + "' and e.estatus = 'A'";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader reader = comando.ExecuteReader();

            List<Tutorias> tutoria = new List<Tutorias>();

            while (reader.Read())
            {
                Tutorias t = new Tutorias();
                t.Id = reader.GetInt32(0);
                t.Estudiante = reader.IsDBNull(1) ? null : reader.GetString(1);
                t.Codigo = reader.IsDBNull(2) ? null : reader.GetString(2);
                t.Asignatura = reader.IsDBNull(3) ? null : reader.GetString(3);
                t.Profesor = reader.IsDBNull(4) ? null : reader.GetString(4);
                t.Horario = reader.IsDBNull(5) ? null : reader.GetString(5);
                t.Estatus = reader.IsDBNull(6) ? null : reader.GetString(6);

                tutoria.Add(t);
            }

            conexion.Close();

            return tutoria;
        }

        public IActionResult Solicitar(string asignatura)
        {
            SqlConnection con = Conexion.GetConexion();
            con.Open();
            SqlCommand cmd = new SqlCommand("stpSolicitar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@matricula", User.Identity.Name);
            cmd.Parameters.AddWithValue("@asignatura", asignatura);
            cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 500);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            var message = (string)cmd.Parameters["@ERROR"].Value;
            con.Close();

            return RedirectToAction("Index", "TutoriasUnphu");

        }
    }
}
