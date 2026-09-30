using System;
using System.Collections.Generic;

//1
class Jugador
{
    private int vida;
    private int dano;

    public Jugador(int vida, int dano)
    {
        this.vida = vida;
        this.dano = dano;
    }

    public void RecibirDano(int cantidad)
    {
        vida -= cantidad;
        if (vida < 0) vida = 0;
    }

    public int ObtenerDano()
    {
        return dano;
    }

    public bool EstaVivo()
    {
        return vida > 0;
    }

    public int VidaActual()
    {
        return vida;
    }
}

//2
class Enemigo
{
    private int vida;
    private int dano;

    public Enemigo(int vida, int dano)
    {
        this.vida = vida;
        this.dano = dano;
    }

    public void RecibirDano(int cantidad)
    {
        vida -= cantidad;
        if (vida < 0) vida = 0;
    }

    public int ObtenerDano()
    {
        return dano;
    }

    public bool EstaVivo()
    {
        return vida > 0;
    }

    public int VidaActual()
    {
        return vida;
    }
}

//3
class Juego
{
    private Jugador jugador;
    private List<Enemigo> enemigos;

    public void Iniciar()
    {
        CrearJugador();
        CrearEnemigos();
        BucleDeJuego();
    }

    private void CrearJugador()
    {
        int vida = 0, dano = 0;

        while (true)
        {
            Console.Write("Vida de tu personaje (1-100): ");
            vida = int.Parse(Console.ReadLine());
            Console.Write("Daño de tu personaje (1-100): ");
            dano = int.Parse(Console.ReadLine());

            if (vida >= 1 && vida <= 100 && dano >= 1 && dano <= 100)
                break;
            else
                Console.WriteLine("Vida y daño deben estar entre 1 y 100. Intenta de nuevo.\n");
        }

        jugador = new Jugador(vida, dano);
        Console.WriteLine($"¡Personaje creado! Vida: {vida}, Daño: {dano}\n");
    }

    private void CrearEnemigos()
    {
        enemigos = new List<Enemigo>();
        enemigos.Add(new Enemigo(30, 10));
        enemigos.Add(new Enemigo(40, 15));
        enemigos.Add(new Enemigo(50, 20));
        Console.WriteLine($"Hay {enemigos.Count} enemigos en el campo de batalla.\n");
    }

    private void BucleDeJuego()
    {
        while (jugador.EstaVivo() && HayEnemigosVivos())
        {
            MostrarEstado();

            int indice = ElegirEnemigo();
            if (indice == -1) continue;

            Enemigo objetivo = enemigos[indice];
            objetivo.RecibirDano(jugador.ObtenerDano());
            Console.WriteLine($"Atacas al enemigo {indice + 1} y le causas {jugador.ObtenerDano()} de daño.");

            if (!objetivo.EstaVivo())
                Console.WriteLine($"¡Enemigo {indice + 1} ha muerto!\n");
            else
                Console.WriteLine($"Enemigo {indice + 1} tiene {objetivo.VidaActual()} de vida restante.\n");

            if (HayEnemigosVivos())
            {
                Enemigo atacante = enemigos[ObtenerEnemigoVivoAleatorio()];
                jugador.RecibirDano(atacante.ObtenerDano());
                Console.WriteLine($"Un enemigo te ataca y te causa {atacante.ObtenerDano()} de daño.");
                Console.WriteLine($"Tu vida ahora es {jugador.VidaActual()}.\n");
            }

            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
        }

        if (!jugador.EstaVivo())
            Console.WriteLine("=== DERROTA: Has caído en batalla. ===");
        else
            Console.WriteLine("=== VICTORIA: Has derrotado a todos los enemigos. ===");
    }

    private bool HayEnemigosVivos()
    {
        foreach (Enemigo e in enemigos)
            if (e.EstaVivo()) return true;
        return false;
    }

    private int ElegirEnemigo()
    {
        Console.WriteLine("Elige un enemigo para atacar:");
        for (int i = 0; i < enemigos.Count; i++)
        {
            string estado = enemigos[i].EstaVivo() ? $"Vida: {enemigos[i].VidaActual()}" : "MUERTO";
            Console.WriteLine($"{i + 1}. Enemigo {i + 1} ({estado})");
        }

        int opcion = int.Parse(Console.ReadLine()) - 1;

        if (opcion < 0 || opcion >= enemigos.Count || !enemigos[opcion].EstaVivo())
        {
            Console.WriteLine("Ese enemigo está muerto o no existe. Elige otro.\n");
            return -1;
        }
        return opcion;
    }

    private int ObtenerEnemigoVivoAleatorio()
    {
        List<int> vivos = new List<int>();
        for (int i = 0; i < enemigos.Count; i++)
            if (enemigos[i].EstaVivo()) vivos.Add(i);
        return vivos[new Random().Next(vivos.Count)];
    }

    private void MostrarEstado()
    {
        Console.WriteLine("=== ESTADO DE LA BATALLA ===");
        Console.WriteLine($"Jugador - Vida: {jugador.VidaActual()}, Daño: {jugador.ObtenerDano()}");
        for (int i = 0; i < enemigos.Count; i++)
        {
            string estado = enemigos[i].EstaVivo() ? $"Vida: {enemigos[i].VidaActual()}" : "MUERTO";
            Console.WriteLine($"Enemigo {i + 1} - {estado}, Daño: {enemigos[i].ObtenerDano()}");
        }
        Console.WriteLine();
    }
}

//4
class Program
{
    static void Main(string[] args)
    {
        Juego juego = new Juego();
        juego.Iniciar();
    }
}
