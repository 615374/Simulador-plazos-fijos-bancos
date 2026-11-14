using System;
using System.Collections.Generic;
using System.Linq;
using Dominio;

namespace Negocio
{
    public class SimuladorNegocio
    {
        public List<ResultadoInversion> Simular(SolicitudInversion solicitud)
        {
            ValidarSolicitud(solicitud);

            var resultados = new List<ResultadoInversion>();
            var bancos = new[] { Banco.Provincia, Banco.Nacion, Banco.Hipotecario };

            foreach (var banco in bancos)
            {
                var tasasBanco = solicitud.Tasas
                    .Where(t => t.Banco == banco)
                    .ToList();

                decimal tasaPromedioPorcentaje = tasasBanco.Average(t => t.TasaPorcentaje);
                decimal r = tasaPromedioPorcentaje / 100m;

                resultados.Add(CalcularModalidad(banco, Modalidad.Anual, tasaPromedioPorcentaje, solicitud.Capital, r));
                resultados.Add(CalcularModalidad(banco, Modalidad.Trimestral, tasaPromedioPorcentaje, solicitud.Capital, r));
                resultados.Add(CalcularModalidad(banco, Modalidad.Mensual, tasaPromedioPorcentaje, solicitud.Capital, r));
            }

            return resultados;
        }

        public List<Recomendacion> Recomendar(List<ResultadoInversion> resultados)
        {
            if (resultados == null || resultados.Count == 0)
            {
                throw new ArgumentException("No hay resultados para evaluar una recomendación.");
            }

            decimal maxCapitalFinal = resultados.Max(res => Math.Round(res.CapitalFinal, 2));

            return resultados
                .Where(res => Math.Round(res.CapitalFinal, 2) == maxCapitalFinal)
                .Select(res => new Recomendacion
                {
                    Banco = res.Banco,
                    Modalidad = res.Modalidad,
                    CapitalFinal = res.CapitalFinal,
                    Rendimiento = res.Rendimiento
                })
                .ToList();
        }

        private void ValidarSolicitud(SolicitudInversion solicitud)
        {
            if (solicitud == null)
            {
                throw new ArgumentNullException(nameof(solicitud), "La solicitud de inversión no puede ser nula.");
            }

            if (solicitud.Capital <= 0)
            {
                throw new ArgumentException("El capital ingresado debe ser mayor a 0.");
            }

            if (solicitud.Tasas == null || solicitud.Tasas.Count != 9)
            {
                throw new ArgumentException("Se deben ingresar exactamente 9 tasas (3 años por cada uno de los 3 bancos).");
            }

            var bancos = new[] { Banco.Provincia, Banco.Nacion, Banco.Hipotecario };
            foreach (var banco in bancos)
            {
                var tasasBanco = solicitud.Tasas.Where(t => t.Banco == banco).ToList();
                if (tasasBanco.Count != 3)
                {
                    throw new ArgumentException($"Faltan tasas para {NombresBanco.De(banco)}. Se requieren 3 tasas anuales.");
                }

                if (tasasBanco.Any(t => t.TasaPorcentaje <= 0))
                {
                    throw new ArgumentException($"Todas las tasas de {NombresBanco.De(banco)} deben ser mayores a 0%.");
                }
            }
        }

        private ResultadoInversion CalcularModalidad(
            Banco banco,
            Modalidad modalidad,
            decimal tasaPromedioPorcentaje,
            decimal capitalInicial,
            decimal r)
        {
            decimal capitalFinal = capitalInicial;

            switch (modalidad)
            {
                case Modalidad.Anual:
                    capitalFinal = capitalInicial * (1m + r);
                    break;

                case Modalidad.Trimestral:
                    for (int i = 0; i < 4; i++)
                    {
                        capitalFinal = capitalFinal * (1m + (r / 4m));
                    }
                    break;

                case Modalidad.Mensual:
                    for (int i = 0; i < 12; i++)
                    {
                        capitalFinal = capitalFinal * (1m + (r / 12m));
                    }
                    break;
            }

            capitalFinal = Math.Round(capitalFinal, 2);
            decimal rendimiento = Math.Round(capitalFinal - capitalInicial, 2);

            return new ResultadoInversion
            {
                Banco = banco,
                Modalidad = modalidad,
                TasaPromedioPorcentaje = Math.Round(tasaPromedioPorcentaje, 2),
                CapitalInicial = capitalInicial,
                CapitalFinal = capitalFinal,
                Rendimiento = rendimiento
            };
        }
    }
}