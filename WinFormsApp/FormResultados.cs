using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FormResultados : Form
    {
        public FormResultados(List<ResultadoInversion> resultados, List<Recomendacion> recomendaciones)
        {
            InitializeComponent();
            CargarResultados(resultados, recomendaciones);
        }
    


private void CargarResultados(List<ResultadoInversion> resultados, List<Recomendacion> recomendaciones)
        {
            
            dgvResultados.DataSource = null;
            dgvResultados.DataSource = resultados;

            
            if (recomendaciones != null && recomendaciones.Count > 0)
            {
                string textoRecomendacion = "--- RECOMENDACIÓN DEL SISTEMA ---\n\n";
                foreach (var rec in recomendaciones)
                {
                    textoRecomendacion += $"•Banco : {rec.Banco} | Modalidad: {rec.Modalidad} | Capital Final: {rec.CapitalFinal:C2}\n";
                }
                lblRecomendacion.Text = textoRecomendacion;
            }
            else
            {
                lblRecomendacion.Text = "No hay recomendaciones disponibles.";
            }
        }
    }


}
