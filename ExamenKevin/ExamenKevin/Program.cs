using System;

class Program
{
    static void Main()
    {
        string nombreEmpleado = "";
        DateTime fecha = DateTime.MinValue, horaEntrada = DateTime.MinValue, horaSalida = DateTime.MinValue;
        string actividad = "";
        double salarioBaseHora = 10.0;
        double bonoFaltas = 50.0;
        double recargoHoraExtra = 1.5;
        double recargoHoraNocturna = 2.0;
        double limiteHorasDiarias = 8;
        double limiteHorasSemanales = 40;
        double deduccionesObligatorias = 0.1;
        double penalizacionRetraso = 20.0;
        double horasTrabajadas = 0, horasExtras = 0, horasNocturnas = 0, totalHorasSemanales = 0;
        bool tieneFaltas = false, esRetraso = false;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(" Sistema de Registro de Empleados de Kevin");
            Console.WriteLine("1. Registrar Jornada Laboral");
            Console.WriteLine("2. Calcular Salario");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opcion: ");

            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int opcion))
            {
                Console.WriteLine("Entrada no valida. Presione una tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            if (opcion == 1)
            {
                Console.Clear();
                Console.Write("Ingrese el nombre del empleado: ");
                nombreEmpleado = Console.ReadLine() ?? "";

                Console.Write("Ingrese la fecha de la jornada (YYYY-MM-DD): ");
                if (!DateTime.TryParse(Console.ReadLine(), out fecha))
                {
                    Console.WriteLine("Fecha invalida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Ingrese la hora de entrada (HH:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out horaEntrada))
                {
                    Console.WriteLine("Hora de entrada invalida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Ingrese la hora de salida (HH:mm): ");
                if (!DateTime.TryParse(Console.ReadLine(), out horaSalida))
                {
                    Console.WriteLine("Hora de salida invalida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                if (horaSalida < horaEntrada)
                {
                    Console.WriteLine("Error: La hora de salida no puede ser anterior a la hora de entrada.");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Ingrese la actividad realizada: ");
                actividad = Console.ReadLine() ?? "";

                Console.Write("¿El empleado tuvo faltas esta semana? (s/n): ");
                tieneFaltas = (Console.ReadLine()?.ToLower() == "s");

                horasTrabajadas = (horaSalida - horaEntrada).TotalHours;
                horasExtras = Math.Max(0, horasTrabajadas - limiteHorasDiarias);
                horasNocturnas = (horaEntrada.Hour < 6 || horaSalida.Hour > 22) ? horasTrabajadas : 0;
                esRetraso = horaEntrada.Hour > 9;
                totalHorasSemanales += horasTrabajadas;

                Console.WriteLine("Resumen de jornada registrada:");
                Console.WriteLine($"Horas trabajadas: {horasTrabajadas} horas");
                Console.WriteLine($"Horas extras: {horasExtras} horas");
                Console.WriteLine($"Horas nocturnas: {horasNocturnas} horas");
                Console.WriteLine($"Total horas semanales acumuladas: {totalHorasSemanales} horas");
                Console.WriteLine("Jornada registrada con exito.");
                Console.ReadKey();
            }
            else if (opcion == 2)
            {
                double salarioBruto = (horasTrabajadas * salarioBaseHora) + (horasExtras * salarioBaseHora * recargoHoraExtra) + (horasNocturnas * salarioBaseHora * recargoHoraNocturna);
                double bonificacion = tieneFaltas ? 0 : bonoFaltas;
                double deducciones = (salarioBruto * deduccionesObligatorias) + (esRetraso ? penalizacionRetraso : 0);
                double salarioNeto = salarioBruto + bonificacion - deducciones;

                Console.Clear();
                Console.WriteLine($"Empleado: {nombreEmpleado}");
                Console.WriteLine($"Fecha: {fecha.ToShortDateString()}");
                Console.WriteLine($"Horas trabajadas en la semana: {totalHorasSemanales} horas");
                Console.WriteLine($"Horas extras: {horasExtras} horas");
                Console.WriteLine($"Horas nocturnas: {horasNocturnas} horas");
                Console.WriteLine($"Salario bruto: {salarioBruto:C}");
                Console.WriteLine($"Bonificacion: {bonificacion:C}");
                Console.WriteLine($"Deducciones: {deducciones:C}");
                Console.WriteLine($"Salario neto: {salarioNeto:C}");
                Console.ReadKey();
            }
            else if (opcion == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Opcion no valida. Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
