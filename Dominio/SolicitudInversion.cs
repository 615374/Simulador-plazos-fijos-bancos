using System.Collections.Generic;

namespace Dominio
{
    public class SolicitudInversion
    {
        public decimal Capital { get; set; }
        public List<TasaAnual> Tasas { get; set; }

        public SolicitudInversion()
        {
            Tasas = new List<TasaAnual>();
        }
    }
}
