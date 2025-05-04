using System;
using System.Linq;
using ProjectCar.Models;
using ProjectCar.Repositories;

namespace ProjectCar.Pages
{
    public partial class RegisterBrand : System.Web.UI.Page
    {
        protected void Register(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                lblMensagem.Text = "Campo obrigatório, informe uma Marca!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                using (var context = new OracleDbContext())
                {
                    // Verifica se a marca já existe
                    if (context.Brands.Any(m => m.Name.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        lblMensagem.Text = "Marca já cadastrada, informe outra!";
                        lblMensagem.ForeColor = System.Drawing.Color.DarkRed;
                        return;
                    }

                    var brand = new Brand
                    {
                        Name = txtName.Text
                        // O ID será gerado automaticamente pelo banco
                    };

                    context.Brands.Add(brand);
                    context.SaveChanges();

                    lblMensagem.Text = "Marca cadastrada com sucesso!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                    txtName.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = $"Erro ao cadastrar: {ex.Message}";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}