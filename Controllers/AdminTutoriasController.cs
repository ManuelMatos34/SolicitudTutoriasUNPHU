using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using Rotativa.AspNetCore;
using System.Data;
using System.Net.Mail;
using System.Net.NetworkInformation;
using Tutorias_Unphu.Models;

namespace Tutorias_Unphu.Controllers
{
    public class AdminTutoriasController : Controller
    {
        private readonly TutoriasUnphuContext _context;

        public AdminTutoriasController(TutoriasUnphuContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "administrador")]
        public IActionResult Index(string filtro, string buscar)
        {
            TempData["Fecha"] = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            ViewBag.Tutorias = tutorias(filtro, buscar);

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
            List<Asignatura> asignaturaDatos = _context.Asignaturas.FromSqlRaw("select * from asignaturas where pemsum = '" + usuarioActual.Pemsum + "'").ToList();
            return asignaturaDatos;
        }

        public string profesores(string codigo)
        {
            SqlConnection conexion = Conexion.GetConexion();
            conexion.Open();
            string sql = "select CONCAT(p.nombre, ' ', p.apellido) AS profesor from prof_asig a, profesores p, asignaturas f\r\nwhere a.prof = p.matricula and a.asig = f.codigo_asignatura and a.asig = @codigo";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@codigo",codigo );
            SqlDataReader reader = comando.ExecuteReader();

            List<string> values = new List<string>();

            while (reader.Read())
            {
                values.Add(reader.GetValue(0).ToString());
            }

            conexion.Close();

            string jsonProfesores = new JavaScriptSerializer().Serialize(values);
            return jsonProfesores;
        }

        public List<Tutorias> tutorias(string filtro, string buscar)
        {
            if (filtro != null)
            {
                SqlConnection conexion = Conexion.GetConexion();
                conexion.Open();
                string sql = "select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura, a.nombre_asignatura, " +
                    "concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, e.estatus\r\n  from asignaturas a, profesores b, tutoria e, " +
                    "usuarios f\r\n  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura and e.estatus = '" + filtro + "'";
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
            else if (buscar != null)
            {
                SqlConnection conexion = Conexion.GetConexion();
                conexion.Open();
                string sql = "  select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura, " +
                    "a.nombre_asignatura, concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, e.estatus\r\n  from asignaturas a, " +
                    "profesores b, tutoria e, usuarios f\r\n  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura " +
                    "and a.nombre_asignatura like '%"+buscar+"%'";
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
            else 
            {
                SqlConnection conexion = Conexion.GetConexion();
                conexion.Open();
                string sql = "select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura," +
                    " a.nombre_asignatura, concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, " +
                    "e.estatus\r\n  from asignaturas a, profesores b, tutoria e, usuarios f\r\n  where e.id_profesor = " +
                    "b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura";
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
        }

        public IActionResult estatus(int ID)
        {
            SqlConnection con = Conexion.GetConexion();
            con.Open();
            SqlCommand cmd = new SqlCommand("stpEstatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 500);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            var message = (string)cmd.Parameters["@ERROR"].Value;
            con.Close();

            switch (message)
            {
                case "A":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede cambiar estado porque la tutoria esta en estado de espera";
                    TempData["Tipo"] = "error";

                    break;

                case "B":

                    TempData["Titulo"] = "Confirmacion";
                    TempData["Mensaje"] = "Estado Actualizado Correctamente";
                    TempData["Tipo"] = "success";

                    break;

                case "C":

                    TempData["Titulo"] = "Confirmacion";
                    TempData["Mensaje"] = "Estado Actualizado Correctamente";
                    TempData["Tipo"] = "success";

                    break;

                case "D":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede cambiar estado porque la tutoria esta en estado de espera";
                    TempData["Tipo"] = "error";

                    break;
            }

            return RedirectToAction("Index", "AdminTutorias");
        }

        public async Task<IActionResult> confirmar(int ID)
        {
            SqlConnection con = Conexion.GetConexion();
            con.Open();
            SqlCommand cmd = new SqlCommand("stpConfirmar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 500);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            var message = (string)cmd.Parameters["@ERROR"].Value;

            con.Close();

            switch (message)
            {
                case "A":
                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede confirmar porque esta tutoria no tiene un maestro asignado";
                    TempData["Tipo"] = "error";

                    break;

                case "B":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede confirmar porque esta tutoria no tiene un horario asignado";
                    TempData["Tipo"] = "error";

                    break;

                case "C":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede confirmar porque esta tutoria tiene el estatus de inactivo";
                    TempData["Tipo"] = "error";

                    break;

                case "E":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede confirmar porque esta tutoria tiene el estatus de rechazado";
                    TempData["Tipo"] = "error";

                    break;

                case "F":

                    SqlConnection conexion = Conexion.GetConexion();
                    conexion.Open();
                    string sql = "select p.email, u.email \r\nfrom profesores p, usuarios u, " +
                        "tutoria t \r\nwhere t.id_profesor = p.matricula and t.id_usuario = u.matricula and" +
                        " t.id = @ID;";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = comando.ExecuteReader();

                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        values.Add(reader.GetValue(0).ToString());
                        values.Add(reader.GetValue(1).ToString());
                    }

                    conexion.Close();

                    MailMessage mail = new MailMessage();

                    foreach (string destinos in values)
                    {
                        mail.To.Add(new MailAddress(destinos, ""));
                    }

                    mail.From = new MailAddress("cuposUNPHU@hotmail.com");
                    mail.Subject = "Estado de la solicidud de tutoria";
                    mail.Body = "Su solicitud de tutoria ha sido confirmada, " +
                        "podra ver los detalles en la plataforma unphu virtual";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("cuposUNPHU@hotmail.com", "1234HOLA");
                    smtp.Send(mail);

                    TempData["Titulo"] = "Confirmacion";
                    TempData["Mensaje"] = "Operacion realizada correctamente";
                    TempData["Tipo"] = "success";

                    break;
            }

            return RedirectToAction("Index", "AdminTutorias");
        }

        public async Task<IActionResult> rechazar(int ID)
        {
            SqlConnection con = Conexion.GetConexion();
            con.Open();
            SqlCommand cmd = new SqlCommand("stpRechazar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 500);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            var message = (string)cmd.Parameters["@ERROR"].Value;

            con.Close();

            switch (message)
            {
                case "A":

                    SqlConnection conexion = Conexion.GetConexion();
                    conexion.Open();
                    string sql = "select u.email \r\nfrom usuarios u, tutoria t \r\nwhere t.id_usuario = " +
                        "u.matricula and t.id = @ID;";
                    SqlCommand comando = new SqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = comando.ExecuteReader();

                    List<string> values = new List<string>();

                    while (reader.Read())
                    {
                        values.Add(reader.GetValue(0).ToString());
                    }

                    conexion.Close();

                    MailMessage mail = new MailMessage();

                    foreach (string destinos in values)
                    {
                        mail.To.Add(new MailAddress(destinos, ""));
                    }

                    mail.From = new MailAddress("cuposUNPHU@hotmail.com");
                    mail.Subject = "Estado de la solicidud de tutoria";
                    mail.Body = "Su solicitud de tutoria ha sido rechazada debido " +
                        "a que no se alcanzaron los requisitos necesarios";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("cuposUNPHU@hotmail.com", "1234HOLA");
                    smtp.Send(mail);

                    TempData["Titulo"] = "Confirmacion";
                    TempData["Mensaje"] = "Operacion realizada correctamente";
                    TempData["Tipo"] = "success";

                    break;

                case "B":
                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede rechazar porque esta tutoria tiene el estatus de inactivo";
                    TempData["Tipo"] = "error";

                    break;

                case "C":
                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede rechazar porque esta tutoria ya se ha rechazado";
                    TempData["Tipo"] = "error";
                    break;

                case "D":
                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede rechazar porque esta tutoria esta en curso";
                    TempData["Tipo"] = "error";
                    break;
            }

            return RedirectToAction("Index", "AdminTutorias");
        }

        public async Task<IActionResult> ProfHorario(int ID, string prof, string dia, string hora1, string hora2)
        {
            string horario = dia + " " + hora1 + " " + hora2;
            SqlConnection con = Conexion.GetConexion();
            con.Open();
            SqlCommand cmd = new SqlCommand("stpProfHorario", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Prof", prof);
            cmd.Parameters.AddWithValue("@Horario", horario);
            cmd.Parameters.Add("@ERROR", SqlDbType.VarChar, 500);
            cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            var message = (string)cmd.Parameters["@ERROR"].Value;
            con.Close();

            switch (message)
            {
                case "D":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede asignar porque esta tutoria ya esta en curso";
                    TempData["Tipo"] = "error";

                    break;

                case "E":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede asignar porque esta tutoria esta inactiva";
                    TempData["Tipo"] = "error";

                    break;

                case "F":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede asignar porque esta tutoria se ha rechazado";
                    TempData["Tipo"] = "error";

                    break;

                case "A":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede asignar profesor porque ya tiene uno";
                    TempData["Tipo"] = "error";

                    break;

                case "B":

                    TempData["Titulo"] = "Ha ocurrido un error";
                    TempData["Mensaje"] = "No se puede asignar horario porque ya tiene uno";
                    TempData["Tipo"] = "error";

                    break;

                case "C":

                    TempData["Titulo"] = "Confirmacion";
                    TempData["Mensaje"] = "Operacion realizada correctamente";
                    TempData["Tipo"] = "success";

                    break;
            }

            return RedirectToAction("Index", "AdminTutorias");
        }

        public async Task<IActionResult> GenerarPDF()
        {
            var reloj = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            SqlConnection conexion = Conexion.GetConexion();
            conexion.Open();
            string sql = "select e.id, concat(f.nombre,' ',f.apellido) as nombre_estudiante, e.asignatura, a.nombre_asignatura, " +
                "concat(b.nombre,' ',b.apellido) as nombre_profesor, e.horario, e.estatus\r\n  from asignaturas a, profesores b, tutoria e, " +
                "usuarios f\r\n  where e.id_profesor = b.matricula and e.id_usuario = f.matricula and e.asignatura = a.codigo_asignatura ";
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

            return new ViewAsPdf("GenerarPDF", tutoria)
            {

                FileName = $"TUTORIAS SOLICITADAS" + "-" + reloj + ".pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
