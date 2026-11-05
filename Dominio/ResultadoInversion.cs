namespace Dominio
{
    public class ResultadoInversion
    {
        public Banco Banco { get; set; }
        public Modalidad Modalidad { get; set; }
        public decimal TasaPromedioPorcentaje { get; set; }
        public decimal CapitalInicial { get; set; }
        public decimal CapitalFinal { get; set; }
        public decimal Rendimiento { get; set; }
    }
}
