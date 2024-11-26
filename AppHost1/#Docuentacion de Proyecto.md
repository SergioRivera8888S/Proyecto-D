# Docuentacion de Proyecto

## Codigo

```php
public abstract class ComponenteEquipo
    {
        public string Nombre { get; protected set; }
        public abstract void Mostrar();
    }
```
```php
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
```
```php
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
```
```php
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
```
```php
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
```
```php
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
```
```php
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
```
```php
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
```
# Pruebas Unitarias

```php
 [TestMethod]
        public void AgregarJugador()
        {
            var equipo = new Equipo("Equipo A");
            var jugador = new Jugador("Juan");
            equipo.Agregar(jugador);
            Assert.IsTrue(equipo.ObtenerMiembros().Contains(jugador));
        }
```

```php
 public void AgregarCapitan()
        {
            var jugador = new Jugador("Juan");
            var capitan = new Capitan(jugador);

            Assert.AreEqual("Juan", capitan.Nombre);
        }
```

```php
 public void BuscarJugadorCorrecto()
        {
            var equipo = new Equipo("Equipo A");
            var jugador = new Jugador("Juan");

            equipo.Agregar(jugador);

            var encontrado = BuscarJugador("Juan", equipo);

            Assert.AreEqual(jugador, encontrado);
        }
```

```php
public void BuscarJugadorIncorrecto()
        {
            var equipo = new Equipo("Equipo A");
            var jugador = new Jugador("Juan");

            equipo.Agregar(jugador);

            var encontrado = BuscarJugador("Pedro", equipo);

            Assert.IsNull(encontrado);
        }
```

```php
  public void EspecialistaTirosLibresCorrectamente()
        {
            var jugador = new Jugador("Carlos");
            var especialista = new EspecialistaTirosLibres(jugador);

            Assert.AreEqual("Carlos", especialista.Nombre);
        }
```

```php
 public void EquipoContieneJugadores()
   {
       {
            var equipo = new Equipo("Equipo A");
            var jugador1 = new Jugador("Juan");
            var jugador2 = new Jugador("Pedro");

            equipo.Agregar(jugador1);
            equipo.Agregar(jugador2);

            var miembros = equipo.ObtenerMiembros();
            Assert.IsTrue(miembros.Contains(jugador1) && miembros.Contains(jugador2));
        }

        // Auxiliar
        private ComponenteEquipo BuscarJugador(string nombre, Equipo equipo)
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
```

# Guia

## Explicacion del codigo

El codigo funciona para la asignacion de roles en teoricamente varios equipos de futbol, partiendo desde la asignacion de un nombre a los equipos y un metodo que los muestra en el componente de equipo

Ahora, seguimos con los componentes de juego, que seran el equipo y el jugador, con sus respectivos nombres y acciones, particularmente el equipo, pues en el se agregaran jugadores y se mostraran, asi como subdividiendolos en el.

Luego tenemos una clase aparte que sera la de los jugadores y sus roles, con la clase de jugadorDecorador, clase base para las clases derivadas que son Capitan, EspecialistaDeTirosLibres y TiradorPenales.

Terminando con un Main o clase principal que recopila toda la informacion anterior y la usa en diferentes switches repartidos para buscar y añadir jugadores a listas, empleando los using iniciales agregados.

Teniendo como un Auxiliar un metodo dentro del principal que ayuda a buscar jugadores en situaciones especificas (mas que nada para buscar jugadores en roles asignados)

## Especificaciones Tecnicas

El codigo fue implementado en VisualStudio 2019, en el lenguaje de C#, la instalacion del mismo puede ser directa en este documento o en el repositorio visto en GitHub con el usuario de Sergio Rivera, pues este lo manifesto para el publico el dia 20/11/24.

No se utilizan lenguajes o adaptadores aparte del visto, por lo que con el usa de la consola clasica de la aplicacion mencionada es suficiente para probar este Programa.

## Diagrama UML

![UML](D:/Sergio_Aaron_Rivera/imagenes/UMLMD.png)
