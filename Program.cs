using System;
using System.Collections.Generic;

namespace Proyecto_Unidad_3_Patrones
{
    // Componente base 
    public abstract class ComponenteEquipo
    {
        public string Nombre { get; protected set; }
        public abstract void Mostrar();
    }

    // Clase patrón Composite 
    public class Jugador : ComponenteEquipo
    {
        public Jugador(string nombre)
        {
            Nombre = nombre;
        }

        public override void Mostrar()
        {
            Console.WriteLine($"Jugador: {Nombre}");
        }
    }

    // Clase compuesta del patrón Composite 
    public class Equipo : ComponenteEquipo
    {
        private List<ComponenteEquipo> _miembros = new List<ComponenteEquipo>();

        public Equipo(string nombre)
        {
            Nombre = nombre;
        }

        public void Agregar(ComponenteEquipo componente)
        {
            _miembros.Add(componente);
        }


        // Método para obtener los miembros del grupo
        public List<ComponenteEquipo> ObtenerMiembros()
        {
            return _miembros;
        }

        public override void Mostrar()
        {
            Console.WriteLine($"\nGrupo: {Nombre}");
            foreach (var miembro in _miembros)
            {
                miembro.Mostrar();
            }
        }
    }
    public abstract class JugadorDecorator : ComponenteEquipo
    {
        protected ComponenteEquipo _jugador;

        public JugadorDecorator(ComponenteEquipo jugador)
        {
            _jugador = jugador;
            Nombre = jugador.Nombre;
        }

        public override void Mostrar()
        {
            _jugador.Mostrar();
        }
    }

    // Decorador Capitán
    public class Capitan : JugadorDecorator
    {
        public Capitan(ComponenteEquipo jugador) : base(jugador)
        {
        }

        public override void Mostrar()
        {
            _jugador.Mostrar();
            Console.WriteLine($"-> {Nombre} es el Capitán del equipo");
        }
    }

    // Decorador Tirador de Penales
    public class TiradorPenales : JugadorDecorator
    {
        public TiradorPenales(ComponenteEquipo jugador) : base(jugador)
        {
        }

        public override void Mostrar()
        {
            _jugador.Mostrar();
            Console.WriteLine($"-> {Nombre} es el Tirador de Penales");
        }
    }

    // Decorador Especialista en Tiros Libres
    public class EspecialistaTirosLibres : JugadorDecorator
    {
        public EspecialistaTirosLibres(ComponenteEquipo jugador) : base(jugador)
        {
        }

        public override void Mostrar()
        {
            _jugador.Mostrar();
            Console.WriteLine($"-> {Nombre} es Especialista en Tiros Libres");
        }
    }
    public class Program
    {
        public static void Main()
        {
            var equipoA = new Equipo("Equipo A");
            var equipoB = new Equipo("Equipo B");

            while (true)
            {
                Console.WriteLine("\n1. Agregar jugador");
                Console.WriteLine("2. Asignar rol especial a un jugador");
                Console.WriteLine("3. Mostrar equipos");
                Console.WriteLine("4. Salir");
                Console.Write("Selecciona una opción: ");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingresa el nombre del jugador: ");
                        var nombreJugador = Console.ReadLine();
                        var jugador = new Jugador(nombreJugador);

                        Console.WriteLine("Selecciona el equipo para agregar al jugador:");
                        Console.WriteLine("1. Equipo A");
                        Console.WriteLine("2. Equipo B");
                        var equipoSeleccionado = Console.ReadLine();

                        if (equipoSeleccionado == "1")
                        {
                            equipoA.Agregar(jugador);
                            Console.WriteLine($"Jugador {nombreJugador} agregado al Equipo A.");
                        }
                        else if (equipoSeleccionado == "2")
                        {
                            equipoB.Agregar(jugador);
                            Console.WriteLine($"Jugador {nombreJugador} agregado al Equipo B.");
                        }
                        else
                        {
                            Console.WriteLine("Opción de equipo no válida.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Selecciona el equipo del jugador al que deseas asignar un rol:");
                        Console.WriteLine("1. Equipo A");
                        Console.WriteLine("2. Equipo B");
                        equipoSeleccionado = Console.ReadLine();

                        Equipo equipo = equipoSeleccionado == "1" ? equipoA : equipoSeleccionado == "2" ? equipoB : null;

                        if (equipo == null)
                        {
                            Console.WriteLine("Opción de equipo no válida.");
                            break;
                        }

                        Console.Write("Ingresa el nombre del jugador al que deseas asignar un rol: ");
                        var nombreParaRol = Console.ReadLine();
                        var jugadorConRol = BuscarJugador(nombreParaRol, equipo);

                        if (jugadorConRol == null)
                        {
                            Console.WriteLine("Jugador no encontrado en el equipo.");
                            break;
                        }

                        Console.WriteLine("Selecciona el rol especial:");
                        Console.WriteLine("1. Capitán");
                        Console.WriteLine("2. Tirador de Penales");
                        Console.WriteLine("3. Especialista en Tiros Libres");
                        var rol = Console.ReadLine();

                        switch (rol)
                        {
                            case "1":
                                jugadorConRol = new Capitan(jugadorConRol);
                                break;
                            case "2":
                                jugadorConRol = new TiradorPenales(jugadorConRol);
                                break;
                            case "3":
                                jugadorConRol = new EspecialistaTirosLibres(jugadorConRol);
                                break;
                            default:
                                Console.WriteLine("Opción de rol no válida.");
                                continue;
                        }

                        
                        equipo.Agregar(jugadorConRol); // Agregar jugador con el rol seleccionado
                        Console.WriteLine($"Rol asignado a {nombreParaRol} en {equipo.Nombre}.");
                        break;

                    case "3":
                        equipoA.Mostrar();
                        equipoB.Mostrar();
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        // Función auxiliar para buscar un jugador por nombre en el equipo
        private static ComponenteEquipo BuscarJugador(string nombre, Equipo equipo)
        {
            foreach (var miembro in equipo.ObtenerMiembros())
            {
                if (miembro.Nombre == nombre)
                {
                    return miembro;
                }
            }
            return null;
        }
    }
}
