using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionEscuela.Maestros
{
    public partial class RegistroAlumno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarTutores();
                CargarGrupos();
            }
        }

        public string GetConnectionString()
        {
            string sqlServerConnectionString = "Data Source=MAHE\\SQLEXPRESS01; Initial Catalog=GestionEscuela; User ID=sa; Password=aaa; TrustServerCertificate=True";
            return sqlServerConnectionString;
        }

        private void CargarUsuarios()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT u.Id, u.UserName FROM AspNetUsers u " +
                        "JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
                        "JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "WHERE r.Name = 'Estudiante'", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlUsuarios.DataSource = reader;
                    ddlUsuarios.DataTextField = "UserName";
                    ddlUsuarios.DataValueField = "Id";
                    ddlUsuarios.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }

            ddlUsuarios.Items.Insert(0, new ListItem("--Seleccione Usuario--", "0"));
        }

        private void CargarTutores()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idTutor, idUsuario, relacion FROM tutor", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlTutores.DataSource = reader;
                    ddlTutores.DataTextField = "idUsuario";
                    ddlTutores.DataValueField = "idTutor";
                    ddlTutores.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }

            ddlTutores.Items.Insert(0, new ListItem("--Seleccione Tutor--", "0"));
        }

        private void CargarGrupos()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idGrupo, nombre FROM grupo", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlGrupos.DataSource = reader;
                    ddlGrupos.DataTextField = "nombre";
                    ddlGrupos.DataValueField = "idGrupo";
                    ddlGrupos.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }

            ddlGrupos.Items.Insert(0, new ListItem("--Seleccione Grupo--", "0"));
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosEstudiante();
        }

        private void CargarDatosEstudiante()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idEstudiante, matricula, curp, promedioGeneral, idGrupo, calif1ero, calif2do, calif3ero FROM estudiante WHERE idUsuario = @idUsuario", con);
                    cmd.Parameters.AddWithValue("@idUsuario", ddlUsuarios.SelectedValue);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtIdEstudiante.Text = reader["idEstudiante"].ToString();
                        txtMatricula.Text = reader["matricula"].ToString();
                        txtCurp.Text = reader["curp"].ToString();
                        ddlGrupos.SelectedValue = reader["idGrupo"].ToString();
                        gvEstudiante.Visible = true;

                        gvEstudiante.DataSource = new[] {
                            new {
                                idEstudiante = reader["idEstudiante"].ToString(),
                                matricula = reader["matricula"].ToString(),
                                curp = reader["curp"].ToString(),
                                idGrupo = reader["idGrupo"].ToString(),
                                calif1ero = reader["calif1ero"].ToString(),
                                calif2do = reader["calif2do"].ToString(),
                                calif3ero = reader["calif3ero"].ToString(),
                                promedioGeneral = reader["promedioGeneral"].ToString()
                            }
                        };
                        gvEstudiante.DataBind();
                    }
                    else
                    {
                        txtIdEstudiante.Text = string.Empty;
                        txtMatricula.Text = string.Empty;
                        txtCurp.Text = string.Empty;
                        ddlGrupos.SelectedIndex = 0;
                        gvEstudiante.Visible = false;
                    }
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO estudiante (idEstudiante, matricula, curp, promedioGeneral, idGrupo, idUsuario, calif1ero, calif2do, calif3ero) " +
                        "VALUES (@idEstudiante, @matricula, @curp, 0, @idGrupo, @idUsuario, 0, 0, 0)", con);

                    cmd.Parameters.AddWithValue("@idEstudiante", txtIdEstudiante.Text);
                    cmd.Parameters.AddWithValue("@matricula", txtMatricula.Text);
                    cmd.Parameters.AddWithValue("@curp", txtCurp.Text);
                    cmd.Parameters.AddWithValue("@idGrupo", ddlGrupos.SelectedValue);
                    cmd.Parameters.AddWithValue("@idUsuario", ddlUsuarios.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    CargarDatosEstudiante();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        protected void btnAsignarTutor_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO tutor (idTutor, idEstudiante, idUsuario, relacion) " +
                        "VALUES (@idTutor, @idEstudiante, @idUsuario, @relacion)", con);

                    cmd.Parameters.AddWithValue("@idTutor", ddlTutores.SelectedValue);
                    cmd.Parameters.AddWithValue("@idEstudiante", txtIdEstudiante.Text);
                    cmd.Parameters.AddWithValue("@idUsuario", ddlUsuarios.SelectedValue);
                    cmd.Parameters.AddWithValue("@relacion", "Tutor Estudiante");

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    CargarTutoresAsignados();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        private void CargarTutoresAsignados()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT t.idTutor, t.relacion, u.UserName as nombre FROM tutor t " +
                        "JOIN AspNetUsers u ON t.idUsuario = u.Id " +
                        "WHERE t.idEstudiante = @idEstudiante", con);
                    cmd.Parameters.AddWithValue("@idEstudiante", txtIdEstudiante.Text);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvTutores.DataSource = reader;
                    gvTutores.DataBind();
                    gvTutores.Visible = true;
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        protected void gvEstudiante_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle edit logic here
            }
            else if (e.CommandName == "Delete")
            {
                int idEstudiante = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM estudiante WHERE idEstudiante = @idEstudiante", con);
                        cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        CargarDatosEstudiante();
                    }
                    catch (SqlException ex)
                    {
                        Response.Write($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }
        }

        protected void gvTutores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle edit logic here
            }
            else if (e.CommandName == "Delete")
            {
                int idTutor = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM tutor WHERE idTutor = @idTutor", con);
                        cmd.Parameters.AddWithValue("@idTutor", idTutor);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        CargarTutoresAsignados();
                    }
                    catch (SqlException ex)
                    {
                        Response.Write($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Response.Write($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
