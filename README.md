# Parcial 2 — Plazos fijos

Aplicación de escritorio en C# WinForms. Compara un capital en plazos fijos de Banco Provincia, Banco Nación y Banco Hipotecario.

Repositorio: https://github.com/MarceloCarabajal/parcial2-analisis-datos

## Cómo abrirlo

- Visual Studio 2026 18.11, o Visual Studio 2022 17.14.
- Carga de trabajo: Desarrollo de escritorio de .NET.
- Abrir `parcial2-analisis-datos.slnx`.
- Los tres proyectos son **.NET Framework 4.8.1**.
- El proyecto de inicio es **WinFormsApp** (tiene que quedar en negrita). Si Visual Studio intenta ejecutar Dominio o Negocio: clic derecho en WinFormsApp, Set as Startup Project.

## Proyectos

- `Dominio`: entidades. No referencia a nadie.
- `Negocio`: cálculos. Referencia a Dominio.
- `WinFormsApp`: pantallas. Referencia a Dominio y a Negocio. Los formularios no calculan.

## Qué carga el usuario

10 datos: el capital y 9 tasas (3 bancos por 3 años). Las tasas se ingresan en porcentaje: 40 significa 40% anual. El ejemplo de la consigna es un capital de 850000.

## Fórmula

`r` es el promedio de las tres tasas del banco, dividido por 100. `C` es el capital ingresado. Se usa `decimal`.

- Anual: `C * (1 + r)`
- Trimestral: 4 veces `capital = capital * (1 + r/4)`
- Mensual: 12 veces `capital = capital * (1 + r/12)`
- Rendimiento = capital final − `C`

La recomendación es el mayor capital final. Si hay empate, se listan las opciones empatadas.

Caso de prueba, con los tres bancos y capital 850000:

- Provincia 40, 42 y 38 (promedio 40%): anual 1.190.000,00 / trimestral 1.244.485,00 / mensual 1.259.807,52
- Nación 35, 36 y 37 (promedio 36%): anual 1.156.000,00 / trimestral 1.199.844,37 / mensual 1.211.896,75
- Hipotecario 45, 30 y 33 (promedio 36%): los mismos montos que Nación
- Recomendación: Banco Provincia, modalidad mensual. Nación e Hipotecario empatan entre sí y quedan por debajo.

## Ramas

- `feature/dominio`
- `feature/negocio`
- `feature/ui-carga`
- `feature/ui-resultados`

`main` tiene que compilar antes de cada merge. No se toca `Program.cs` ni el formulario principal sin avisar en el grupo. El video del programa y el video del código los graban dos personas distintas.
