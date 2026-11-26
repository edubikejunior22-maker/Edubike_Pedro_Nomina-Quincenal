# Edubike_Pedro_Nómina Quincenal

## 1. Datos del Estudiante y Asignatura

* Nombre: Pedro Yunior Edubique Hernández
* Matrícula: [2025-1422]
* Asignatura: Programación Básica
* Evaluación: Actividad individual
* Profesor: Gamalier Reyes del Carmen

## 2. Descripción del Programa

Aplicación de escritorio desarrollada en C# (Windows Forms) que permite calcular el pago quincenal de empleados. El sistema permite registrar múltiples empleados en una lista, aplicar tarifas por cargo, pagar horas extra con recargo, sumar el incentivo de transporte, calcular las deducciones (AFP y SFS) para contratos fijos y mostrar un resumen con los totales de la nómina.

## 3. Datos de Entrada

Para operar el sistema, el usuario debe ingresar los siguientes datos a través de la interfaz gráfica:

* Datos del Empleado:
   * Nombre del empleado: Texto ingresado en `txtEmpleado`.
   * Tipo de contrato: Selección entre contrato fijo (`rdbFijo`) o temporal (`rdbTemporal`).
* Datos del Pago (por cada registro):
   * Cargo: Selección en la lista desplegable `cmbCargo` (Auxiliar, Técnico, Supervisor o Gerente).
   * Horas trabajadas: Número entero entre 1 y 120 ingresado en `txtHoras`.
   * Incentivo de transporte: Selección opcional mediante `chkTransporte`.

## 4. Proceso

El programa ejecuta las siguientes validaciones y cálculos matemáticos:

1. Validación de Campos: Verifica que el nombre no esté vacío, que se haya seleccionado el contrato y el cargo, que las horas sean un número entero entre 1 y 120 (`int.TryParse`) y que exista al menos un empleado registrado antes de mostrar el resumen.
2. Tarifa por Hora según el Cargo:
   * Auxiliar RD$ 180.00 · Técnico RD$ 250.00 · Supervisor RD$ 350.00 · Gerente RD$ 500.00.
3. Cálculo del Pago Bruto:
   * `Salario Regular = Horas (hasta 88) × Tarifa`
   * `Pago Horas Extra = Horas sobre 88 × Tarifa × 1.35`
   * `Incentivo = RD$ 1,500.00 si chkTransporte está marcado`
   * `Bruto = Salario Regular + Pago Horas Extra + Incentivo`
4. Cálculo de Deducciones y Neto:
   * Contrato Fijo: `Deducciones = (Salario Regular + Pago Horas Extra) × (0.0287 + 0.0304)` (AFP 2.87% + SFS 3.04%), sin incluir el incentivo.
   * Contrato Temporal: sin deducciones.
   * `Neto = Bruto - Deducciones`
5. Acumuladores: Por cada empleado registrado se actualizan la cantidad de empleados y los totales de bruto, deducciones y neto.

## 5. Salida

El sistema muestra los resultados en pantalla mediante los siguientes controles:

* Listado de Nómina (`lstNomina`): Muestra cada empleado registrado con su nombre, cargo y pago neto.
* Resumen (Etiquetas de texto formateadas en moneda `RD$` con dos decimales):
   * Empleados registrados (`lblEmpleados`): Cantidad de empleados en la lista.
   * Total bruto (`lblBruto`): Suma de los pagos brutos.
   * Total deducciones (`lblDeducciones`): Suma de AFP y SFS descontados.
   * Total neto (`lblNeto`): Monto total a pagar a los empleados.

## 6. Capturas de Pantalla del Programa en Ejecución

