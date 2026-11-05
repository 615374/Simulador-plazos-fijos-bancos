namespace Dominio
{
    public enum Modalidad
    {
        Anual,
        Trimestral,
        Mensual
    }

    public static class NombresModalidad
    {
        public static string De(Modalidad modalidad)
        {
            switch (modalidad)
            {
                case Modalidad.Anual:
                    return "Anual";
                case Modalidad.Trimestral:
                    return "Trimestral";
                case Modalidad.Mensual:
                    return "Mensual";
                default:
                    return modalidad.ToString();
            }
        }
    }
}
