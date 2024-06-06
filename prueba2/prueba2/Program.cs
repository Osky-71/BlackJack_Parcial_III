using System;
using Spectre.Console;

class Program
{
    private static AudioPlayer menuMusicPlayer;

    static void Main(string[] args)
    {
        menuMusicPlayer = new AudioPlayer("C:\\Users\\Alessandro\\source\\repos\\prueba2\\prueba2\\sounds\\background.wav");
        bool exit = false;

        while (!exit)
        {
            ShowMenu();
        }
    }

    static void ShowMenu()
    {
        menuMusicPlayer.PlaySound();
        Console.Clear();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold blue]¡Bienvenido al Black Jack![/]")
                .PageSize(10)
                .AddChoices(new[] {
                    "Jugar", "Ayuda", "Salir"
                }));

        menuMusicPlayer.StopSound();

        switch (choice)
        {
            case "Jugar":
                var game = new Game();
                game.StartGame();
                break;
            case "Ayuda":
                ShowHelp();
                break;
            case "Salir":
                Environment.Exit(0);
                break;
        }
    }

    static void ShowHelp()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold green]Instrucciones de Black Jack:[/]");
        AnsiConsole.MarkupLine("[bold green]1.[/] El objetivo es acercarse lo más posible a 21 sin pasarse.");
        AnsiConsole.MarkupLine("[bold green]2.[/] Las cartas con figuras (Rey, Reina, Jota) valen 10 puntos.");
        AnsiConsole.MarkupLine("[bold green]3.[/] Los ases valen 1 o 11 puntos, lo que sea más favorable.");
        AnsiConsole.MarkupLine("[bold green]4.[/] Cada jugador comienza con dos cartas y puede elegir 'Hit' (pedir otra carta) o 'Stand' (quedarse con su mano actual).");
        AnsiConsole.MarkupLine("[bold green]5.[/] El crupier debe pedir cartas hasta tener al menos 17 puntos.");
        AnsiConsole.MarkupLine("[bold green]6.[/] El jugador con la puntuación más alta por debajo de 21 gana.");
        AnsiConsole.MarkupLine("[bold green]7.[/] El jugador con las Flechas (Arriba y Abajo) se mueve en la interfas.");
        AnsiConsole.MarkupLine("[bold green]Presiona cualquier tecla para regresar al menú...[/]");
        Console.ReadKey();
        ShowMenu();
    }
}
