using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionEscuela.Directivo
{
    public partial class RegistrarDocente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarDepartamentos();
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
                        "WHERE r.Name = 'Docente'", con);

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

        private void CargarDepartamentos()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idDepar, nombre FROM departamento", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlDepartamentos.DataSource = reader;
                    ddlDepartamentos.DataTextField = "nombre";
                    ddlDepartamentos.DataValueField = "idDepar";
                    ddlDepartamentos.DataBind();
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

            ddlDepartamentos.Items.Insert(0, new ListItem("--Seleccione Departamento--", "0"));
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosDocente();
        }

        private void CargarDatosDocente()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT idDocente, idUsuario, nombre FROM docente WHERE idUsuario = @idUsuario", con);
                    cmd.Parameters.AddWithValue("@idUsuario", ddlUsuarios.SelectedValue);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        gvDocentes.Visible = true;

                        gvDocentes.DataSource = new[] {
                            new {
                                idUsuario = reader["idUsuario"].ToString(),
                                idDocente = reader["idDocente"].ToString(),
                                nombre = reader["nombre"].ToString()
                            }
                        };
                        gvDocentes.DataBind();
                    }
                    else
                    {
                        gvDocentes.Visible = false;
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
                        "INSERT INTO docente (idDocente, idUsuario, nombre) " +
                        "VALUES (NEWID(), @idUsuario, (SELECT UserName FROM AspNetUsers WHERE Id = @idUsuario))", con);

                    cmd.Parameters.AddWithValue("@idUsuario", ddlUsuarios.SelectedValue);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    CargarDatosDocente();
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

        protected void btnAsignarDepartamento_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE docente SET idDepar = @idDepar WHERE idDocente = @idDocente", con);

                    cmd.Parameters.AddWithValue("@idDepar", ddlDepartamentos.SelectedValue);
                    cmd.Parameters.AddWithValue("@idDocente", (gvDocentes.Rows[0].FindControl("lblIdDocente") as Label).Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    CargarDepartamentosAsignados();
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

        private void CargarDepartamentosAsignados()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT d.idDocente, d.idDepar, dept.nombre as nombreDepartamento FROM docente d " +
                        "JOIN departamento dept ON d.idDepar = dept.idDepar " +
                        "WHERE d.idDocente = @idDocente", con);
                    cmd.Parameters.AddWithValue("@idDocente", (gvDocentes.Rows[0].FindControl("lblIdDocente") as Label).Text);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    gvDepartamentos.DataSource = reader;
                    gvDepartamentos.DataBind();
                    gvDepartamentos.Visible = true;
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

        protected void gvDocentes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle edit logic here
            }
            else if (e.CommandName == "Delete")
            {
                int idDocente = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM docente WHERE idDocente = @idDocente", con);
                        cmd.Parameters.AddWithValue("@idDocente", idDocente);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        CargarDatosDocente();
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

        protected void gvDepartamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Handle edit logic here
            }
            else if (e.CommandName == "Delete")
            {
                int idDocente = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE docente SET idDepar = NULL WHERE idDocente = @idDocente", con);
                        cmd.Parameters.AddWithValue("@idDocente", idDocente);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        CargarDepartamentosAsignados();
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
