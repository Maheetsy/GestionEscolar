using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionEscuela.Maestros
{
    public partial class ReportesAlumnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMaterias();
            }
        }

        public string GetConnectionString()
        {
            string sqlServerConnectionString = "Data Source=MAHE\\SQLEXPRESS01; Initial Catalog=GestionEscuela; User ID=sa; Password=aaa; TrustServerCertificate=True";
            return sqlServerConnectionString;
        }

        private void CargarMaterias()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idMateria, nombre FROM materia", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlMaterias.DataSource = reader;
                    ddlMaterias.DataTextField = "nombre";
                    ddlMaterias.DataValueField = "idMateria";
                    ddlMaterias.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"Error: {ex.Message}");
                }
            }

            ddlMaterias.Items.Insert(0, new ListItem("--Seleccione Materia--", "0"));
        }

        protected void ddlMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT estudiante.idEstudiante, AspNetUsers.nombre FROM auxiliar JOIN estudiante ON auxiliar.idEstudiante = estudiante.idEstudiante JOIN AspNetUsers ON estudiante.idUsuario = AspNetUsers.Id WHERE auxiliar.idMateria = @idMateria", con);
                    cmd.Parameters.AddWithValue("@idMateria", ddlMaterias.SelectedValue);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlEstudiantes.DataSource = reader;
                    ddlEstudiantes.DataTextField = "nombre";
                    ddlEstudiantes.DataValueField = "idEstudiante";
                    ddlEstudiantes.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"Error: {ex.Message}");
                }
            }

            ddlEstudiantes.Items.Insert(0, new ListItem("--Seleccione Estudiante--", "0"));
        }

        protected void ddlEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarReportes();
        }

        private void CargarReportes()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idReporte, detalle, fecha FROM reporte WHERE idEstudiante = @idEstudiante", con);
                    cmd.Parameters.AddWithValue("@idEstudiante", ddlEstudiantes.SelectedValue);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvReportes.DataSource = reader;
                    gvReportes.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log or handle el error as needed
                    Response.Write($"Error: {ex.Message}");
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO reporte (idReporte, detalle, fecha, idEstudiante) VALUES (@idReporte, @detalle, @fecha, @idEstudiante)", con);
                    cmd.Parameters.AddWithValue("@idReporte", txtIdReporte.Text);
                    cmd.Parameters.AddWithValue("@detalle", txtDetalle.Text);
                    cmd.Parameters.AddWithValue("@fecha", txtFecha.Text);
                    cmd.Parameters.AddWithValue("@idEstudiante", ddlEstudiantes.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    Response.Write($"Error: {ex.Message}");
                }
            }

            CargarReportes();
        }

        protected void gvReportes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle edit logic here
            }
            else if (e.CommandName == "Delete")
            {
                int idReporte = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM reporte WHERE idReporte = @idReporte", con);
                        cmd.Parameters.AddWithValue("@idReporte", idReporte);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        // Log or handle the error as needed
                        Response.Write($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the error as needed
                        Response.Write($"Error: {ex.Message}");
                    }
                }

                CargarReportes();
            }
        }
    }
}
