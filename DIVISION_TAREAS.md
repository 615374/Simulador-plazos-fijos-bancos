# División del parcial

Cada archivo tiene un dueño para no pisarnos en Git. Cuando alguien termina su parte, manda un mensaje al grupo contando qué hizo, así los demás estamos al tanto.

Integrantes: Marcelo, Gisela, Carmela y Melanie.

- Integración y Dominio: Marcelo
- Negocio: Gisela
- Carga: Carmela
- Resultados: Melanie

Gisela, Carmela y Melanie pueden cambiarse Negocio, Carga y Resultados entre ellas si les cierra mejor. Dominio va con Marcelo porque son las clases que ya están en el repo y hay que completarlas.

## Lo que los cuatro tenemos que poder decir

- Entran 10 datos: el capital y 9 tasas (3 bancos por 3 años). La tasa va en porcentaje.
- El promedio de cada banco es la tasa que se usa.
- Anual es un período. Trimestral reinvierte 4 veces con tasa/4. Mensual reinvierte 12 veces con tasa/12.
- El rendimiento es el capital final menos el capital ingresado.
- Gana el mayor capital final. Caso con los tres bancos y capital 850000: Provincia 40, 42 y 38 (promedio 40%); Nación 35, 36 y 37 (promedio 36%); Hipotecario 45, 30 y 33 (promedio 36%). Gana Provincia mensual: 1.259.807,52. Nación e Hipotecario dan lo mismo en cada modalidad y quedan por debajo.
- La pantalla no hace la cuenta. Llama a `SimuladorNegocio`.

Eso está desarrollado en el README. Léanlo antes de codear. La organización del día a día va por WhatsApp.

## Quién escribe qué

Una rama por persona. El dueño es el único que edita esos archivos.

- **Integración**, rama `feature/menu`. `Program.cs`, el formulario principal y el botón que une la carga con el simulador y abre los resultados. También hace los merges a `main`.
- **Dominio**, rama `feature/dominio`. `Banco`, `Modalidad`, `TasaAnual`, `SolicitudInversion`, `ResultadoInversion`, `Recomendacion`. Hoy están vacías y el proyecto de .NET Framework no las muestra: Show All Files, Include in Project, y pasarlas a `public`. Completar propiedades y nombres para mostrar (Banco Provincia, Banco Nación, Banco Hipotecario).
- **Negocio**, rama `feature/negocio`. `SimuladorNegocio`: promedio, las tres modalidades y la recomendación, para los tres bancos. Valida capital mayor que 0 y las 9 tasas. Este archivo lo escribe una sola persona. Cuando está, manda al grupo el resultado del caso (Provincia, Nación e Hipotecario).
- **Carga**, rama `feature/ui-carga`. Formulario de los 10 casilleros. El capital arranca en `850000`. Arma una `SolicitudInversion`. No calcula intereses.
- **Resultados**, rama `feature/ui-resultados`. Grilla con banco, modalidad, tasa promedio, capital final y rendimiento, más el texto de la recomendación. Recibe la lista ya calculada.

Si hay que cambiar la firma de `Simular` o de `Recomendar`, se avisa en el grupo antes de pushear.

## Cómo nos mantenemos al tanto

Cuando terminás tu parte, mandá un mensaje corto: qué archivo tocaste y qué hace ahora. Con eso el resto sabe en qué está el proyecto.

Los videos del programa y del código los graban dos personas distintas. Lo coordinamos por el grupo cuando la app ya calcule el caso de los tres bancos.

## Orden

1. Dominio incluye las clases, las completa y mergea. Sin eso los demás no compilan contra los tipos. Avisa por mensaje.
2. En paralelo: Negocio implementa las cuentas de los tres bancos. Carga y Resultados dibujan las pantallas. Cada uno avisa al terminar.
3. Integración une el botón Calcular. Se carga el caso y tiene que salir Provincia mensual, con Nación e Hipotecario empatados por debajo.
