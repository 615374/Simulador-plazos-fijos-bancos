namespace Dominio
{
    public enum Banco
    {
        Provincia,
        Nacion,
        Hipotecario
    }

    public static class NombresBanco
    {
        public static string De(Banco banco)
        {
            switch (banco)
            {
                case Banco.Provincia:
                    return "Banco Provincia";
                case Banco.Nacion:
                    return "Banco Nación";
                case Banco.Hipotecario:
                    return "Banco Hipotecario";
                default:
                    return banco.ToString();
            }
        }
    }
}
