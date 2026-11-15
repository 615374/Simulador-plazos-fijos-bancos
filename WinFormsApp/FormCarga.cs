using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FormCarga : Form
    {
        
        private Label lblCapital;
        private TextBox txtCapital;

        private Label lblProvincia;
        private Label lblNacion;
        private Label lblHipotecario;

        private Label lblAnio1;
        private Label lblAnio2;
        private Label lblAnio3;

        private TextBox[] txtProvincia = new TextBox[3];
        private TextBox[] txtNacion = new TextBox[3];
        private TextBox[] txtHipotecario = new TextBox[3];

        private Button btnCalcular;

        public FormCarga()
        {
           
            this.Text = "FormCarga";
            this.Size = new Size(650, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {

            lblCapital = new Label() { Text = "Capital Inicial ($):", Location = new Point(40, 40), AutoSize = true };
            txtCapital = new TextBox() { Text = "850000", Location = new Point(40, 65), Width = 120 };

            lblProvincia = new Label() { Text = "Banco Provincia:", Location = new Point(200, 100), AutoSize = true };
            lblNacion = new Label() { Text = "Banco Nación:", Location = new Point(320, 100), AutoSize = true };
            lblHipotecario = new Label() { Text = "Banco Hipotecario:", Location = new Point(440, 100), AutoSize = true };

            lblAnio1 = new Label() { Text = "Año 1", Location = new Point(140, 130), AutoSize = true };
            lblAnio2 = new Label() { Text = "Año 2", Location = new Point(140, 165), AutoSize = true };
            lblAnio3 = new Label() { Text = "Año 3", Location = new Point(140, 200), AutoSize = true };


            this.Controls.Add(lblCapital);
            this.Controls.Add(txtCapital);
            this.Controls.Add(lblProvincia);
            this.Controls.Add(lblNacion);
            this.Controls.Add(lblHipotecario);
            this.Controls.Add(lblAnio1);
            this.Controls.Add(lblAnio2);
            this.Controls.Add(lblAnio3);


            for (int i = 0; i < 3; i++)
            {
                int posY = 125 + (i * 35);

                txtProvincia[i] = new TextBox() { Location = new Point(200, posY), Width = 90 };
                txtNacion[i] = new TextBox() { Location = new Point(320, posY), Width = 90 };
                txtHipotecario[i] = new TextBox() { Location = new Point(440, posY), Width = 90 };

                this.Controls.Add(txtProvincia[i]);
                this.Controls.Add(txtNacion[i]);
                this.Controls.Add(txtHipotecario[i]);
            }


            btnCalcular = new Button() { Text = "Calcular", Location = new Point(280, 270), Width = 100, Height = 30 };
            btnCalcular.Click += BtnCalcular_Click;
            this.Controls.Add(btnCalcular);
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {

            if (!decimal.TryParse(txtCapital.Text, out decimal capitalInicial) || capitalInicial <= 0)
            {
                MessageBox.Show("Por favor, ingrese un capital inicial válido mayor a 0.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCapital.Focus();
                return;
            }

            TextBox[][] matrizInputs = { txtProvincia, txtNacion, txtHipotecario };
            string[] nombresBancos = { "Provincia", "Nación", "Hipotecario" };

            for (int b = 0; b < matrizInputs.Length; b++)
            {
                for (int a = 0; a < 3; a++)
                {
                    if (!decimal.TryParse(matrizInputs[b][a].Text, out decimal tasa) || tasa < 0)
                    {
                        MessageBox.Show($"Ingrese un valor numérico válido para {nombresBancos[b]} (Año {a + 1}).", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        matrizInputs[b][a].Focus();
                        return;
                    }
                }
            }

            MessageBox.Show("¡Datos cargados y validados correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            List<TasaAnual> listaTasas = new List<TasaAnual>();

         
            for (int i = 0; i < 3; i++)
            {
                decimal.TryParse(txtProvincia[i].Text, out decimal tasaVal);
                listaTasas.Add(new TasaAnual { Banco = Banco.Provincia, Anio = i + 1, TasaPorcentaje = tasaVal });
            }
          
            for (int i = 0; i < 3; i++)
            {
                decimal.TryParse(txtNacion[i].Text, out decimal tasaVal);
                listaTasas.Add(new TasaAnual { Banco = Banco.Nacion, Anio = i + 1, TasaPorcentaje = tasaVal });
            }
           
            for (int i = 0; i < 3; i++)
            {
                decimal.TryParse(txtHipotecario[i].Text, out decimal tasaVal);
                listaTasas.Add(new TasaAnual { Banco = Banco.Hipotecario, Anio = i + 1, TasaPorcentaje = tasaVal });
            }

            
            SolicitudInversion solicitud = new SolicitudInversion
            {
                Capital = capitalInicial,
                Tasas = listaTasas
            };

            try
            {
                
                SimuladorNegocio simulador = new SimuladorNegocio();
                List<ResultadoInversion> resultados = simulador.Simular(solicitud);
                List<Recomendacion> recomendaciones = simulador.Recomendar(resultados);

                
                FormResultados formRes = new FormResultados(resultados, recomendaciones);
                formRes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al simular: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}