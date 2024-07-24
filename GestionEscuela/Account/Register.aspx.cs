using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using GestionEscuela.Models;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace GestionEscuela.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        private void CargarRoles()
        {
            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM AspNetRoles", con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlRoles.DataSource = reader;
                    ddlRoles.DataTextField = "Name";
                    ddlRoles.DataValueField = "Id";
                    ddlRoles.DataBind();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    ErrorMessage.Text = $"Error de SQL: {ex.Message}";
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = $"Error: {ex.Message}";
                }
            }

            ddlRoles.Items.Insert(0, new ListItem("--Seleccione Rol--", "0"));
        }

        public string GetConnectionString()
        {
            return "Data Source=MAHE\\SQLEXPRESS01; Initial Catalog=GestionEscuela; User ID=sa; Password=aaa; TrustServerCertificate=True";
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                // Additional user info
                using (SqlConnection con = new SqlConnection(GetConnectionString()))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(
                            "INSERT INTO AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, contrasenaHASH, nombre, apPaterno, apMaterno, fechaNac, direccion, telefono) " +
                            "VALUES (@Id, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName, @contrasenaHASH, @nombre, @apPaterno, @apMaterno, @fechaNac, @direccion, @telefono)", con);

                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.Parameters.AddWithValue("@Email", Email.Text);
                        cmd.Parameters.AddWithValue("@EmailConfirmed", false);
                        cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        cmd.Parameters.AddWithValue("@SecurityStamp", user.SecurityStamp);
                        cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        cmd.Parameters.AddWithValue("@PhoneNumberConfirmed", false);
                        cmd.Parameters.AddWithValue("@TwoFactorEnabled", false);
                        cmd.Parameters.AddWithValue("@LockoutEndDateUtc", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LockoutEnabled", false);
                        cmd.Parameters.AddWithValue("@AccessFailedCount", 0);
                        cmd.Parameters.AddWithValue("@UserName", Email.Text);
                        cmd.Parameters.AddWithValue("@contrasenaHASH", Password.Text);
                        cmd.Parameters.AddWithValue("@nombre", Nombre.Text);
                        cmd.Parameters.AddWithValue("@apPaterno", ApPaterno.Text);
                        cmd.Parameters.AddWithValue("@apMaterno", ApMaterno.Text);
                        cmd.Parameters.AddWithValue("@fechaNac", FechaNac.Text);
                        cmd.Parameters.AddWithValue("@direccion", Direccion.Text);
                        cmd.Parameters.AddWithValue("@telefono", Telefono.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        ErrorMessage.Text = $"Error de SQL: {ex.Message}";
                        return;
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = $"Error: {ex.Message}";
                        return;
                    }
                }

                // Assign role to user
                try
                {
                    var roleId = ddlRoles.SelectedValue;
                    var roleName = ddlRoles.SelectedItem.Text;

                    if (roleId != "0")
                    {
                        var roleResult = manager.AddToRole(user.Id, roleName);
                        if (!roleResult.Succeeded)
                        {
                            ErrorMessage.Text = string.Join(", ", roleResult.Errors);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = $"Error asignando rol: {ex.Message}";
                    return;
                }

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}
