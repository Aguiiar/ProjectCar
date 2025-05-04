using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectCar.Models;
using ProjectCar.Repositories;

namespace ProjectCar.Pages
{
    public partial class CarManagement : System.Web.UI.Page
    {
        private int? CarEditId
        {
            get => ViewState["CarEditId"] as int?;
            set => ViewState["CarEditId"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var dbContext = new OracleDbContext())
                {
                    ddlBrands.DataSource = dbContext.Brands.ToList();
                    ddlBrands.DataTextField = "Name";
                    ddlBrands.DataValueField = "Id";
                    ddlBrands.DataBind();
                    ddlBrands.Items.Insert(0, new ListItem("Selecione...", ""));
                }
                UpdateTable();
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtManufacture.Text) ||
                string.IsNullOrWhiteSpace(txtPower.Text) ||
                ddlBrands.SelectedIndex <= 0)
            {
                lblMensagem.Text = "Preencha todos os campos, obrigatório!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (!DateTime.TryParse(txtManufacture.Text, out _) || !int.TryParse(txtPower.Text, out _))
            {
                lblMensagem.Text = "Dados no formato inválido!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }

        protected void RegisterEditCar(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            using (var dbContext = new OracleDbContext())
            {
                if (CarEditId.HasValue)
                {
                    var car = dbContext.Cars.FirstOrDefault(c => c.Id == CarEditId.Value);
                    if (car != null)
                    {
                        car.Model = txtModel.Text;
                        car.Manufacture = DateTime.Parse(txtManufacture.Text);
                        car.Power = int.Parse(txtPower.Text);
                        car.Turbo = rbtnYes.Checked;
                        car.BrandId = int.Parse(ddlBrands.SelectedValue);
                        // car.Brand será carregado automaticamente pelo EF se necessário
                        dbContext.SaveChanges();
                        lblMensagem.Text = "Veículo atualizado!";
                        lblMensagem.ForeColor = System.Drawing.Color.Green;
                        CarEditId = null;
                    }
                }
                else
                {
                    var carro = new Car
                    {
                        Model = txtModel.Text,
                        Manufacture = DateTime.Parse(txtManufacture.Text),
                        Power = int.Parse(txtPower.Text),
                        Turbo = rbtnYes.Checked,
                        BrandId = int.Parse(ddlBrands.SelectedValue)
                    };
                    dbContext.Cars.Add(carro);
                    dbContext.SaveChanges();
                    lblMensagem.Text = "Veículo cadastrado!";
                    lblMensagem.ForeColor = System.Drawing.Color.Green;
                }
                ClearFields();
                UpdateTable();
            }
        }

        private void UpdateTable()
        {
            using (var dbContext = new OracleDbContext())
            {
                var lista = dbContext.Cars.Select(c => new
                {
                    c.Id,
                    c.Model,
                    c.Manufacture,
                    c.Power,
                    TurboTexto = c.Turbo ? "Sim" : "Não",
                    MarcaNome = c.Brand.Name // Garante que a marca seja carregada (eager loading)
                }).ToList();

                repeaterCars.DataSource = lista;
                repeaterCars.DataBind();
                tableCars.Visible = lista.Any();
            }
        }

        private void ClearFields()
        {
            txtModel.Text = "";
            txtManufacture.Text = "";
            txtPower.Text = "";
            rbtnYes.Checked = false;
            rbtnNo.Checked = false;
            ddlBrands.SelectedIndex = 0;
        }

        protected void EditDelete(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                using (var dbContext = new OracleDbContext())
                {
                    var carro = dbContext.Cars.FirstOrDefault(c => c.Id == id);
                    if (carro != null)
                    {
                        txtModel.Text = carro.Model;
                        txtManufacture.Text = carro.Manufacture.ToString("yyyy-MM-dd");
                        txtPower.Text = carro.Power.ToString();
                        rbtnYes.Checked = carro.Turbo;
                        rbtnNo.Checked = !carro.Turbo;
                        ddlBrands.SelectedValue = carro.BrandId.ToString();
                        CarEditId = carro.Id;
                        lblMensagem.Text = "Atualizando dados - Modifique os campos";
                        lblMensagem.ForeColor = System.Drawing.Color.GreenYellow;

                    }
                }
            }
            else if (e.CommandName == "Deletar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                using (var dbContext = new OracleDbContext())
                {
                    var carro = dbContext.Cars.FirstOrDefault(c => c.Id == id);
                    if (carro != null)
                    {
                        dbContext.Cars.Remove(carro);
                        dbContext.SaveChanges();
                        lblMensagem.Text = "Veículo deletado!";
                        lblMensagem.ForeColor = System.Drawing.Color.Red;

                    }
                    UpdateTable();
                }
            }
        }
    }
}