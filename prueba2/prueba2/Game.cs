using System;
using Spectre.Console;

public class Game
{
    private Deck deck;
    private Player player;
    private Player dealer;
    private int playerWins;
    private int dealerWins;
    private int ties;
    private AudioPlayer winSoundPlayer;
    private AudioPlayer dealerWinSoundPlayer;

    public Game()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        deck = new Deck();
        player = new Player("Jugador");
        dealer = new Player("Crupier");
        playerWins = 0;
        dealerWins = 0;
        ties = 0;
        winSoundPlayer = new AudioPlayer("C:\\Users\\Alessandro\\source\\repos\\prueba2\\prueba2\\sounds\\win.wav");//En caso de error actualizar la ruta
        dealerWinSoundPlayer = new AudioPlayer("C:\\Users\\Alessandro\\source\\repos\\prueba2\\prueba2\\sounds\\lose.wav");//En caso de error actualizar la ruta
    }

    public void StartGame()
    {
        deck.Shuffle();
        while (deck.CardsRemaining() > 0)
        {
            DealInitialCards();
            PlayerTurn();
            DealerTurn();
            DetermineWinner();
            ShowRoundResults();
            AnsiConsole.MarkupLine("[bold cyan]Presiona cualquier tecla para continuar...[/]");
            Console.ReadKey();
            winSoundPlayer.StopSound();
            dealerWinSoundPlayer.StopSound();
        }
        ShowFinalResults();
        AskReplay();
    }

    private void DealInitialCards()
    {
        player.ResetHand();
        dealer.ResetHand();
        player.AddCardToHand(deck.DrawCard());
        player.AddCardToHand(deck.DrawCard());
        dealer.AddCardToHand(deck.DrawCard());
        dealer.AddCardToHand(deck.DrawCard());
        DisplayHands();
    }

    private void PlayerTurn()
    {
        while (player.Score < 21 && deck.CardsRemaining() > 0)
        {
            DisplayHands();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]¿Quieres (H)it o (S)tand?[/]")
                    .AddChoices(new[] { "Hit", "Stand" }));

            if (choice == "Hit")
            {
                player.AddCardToHand(deck.DrawCard());
                DisplayHands();
            }
            else if (choice == "Stand")
            {
                break;
            }
        }
    }

    private void DealerTurn()
    {
        while (dealer.Score < 17 && deck.CardsRemaining() > 0)
        {
            dealer.AddCardToHand(deck.DrawCard());
            DisplayHands();
        }
    }

    private void DisplayHands()
    {
        Console.Clear();
        var playerTable = new Table();
        playerTable.AddColumn(new TableColumn($"[bold yellow]{player.Name} - Puntuación: {player.Score}[/]"));

        foreach (var card in player.Hand)
        {
            playerTable.AddRow(card.ToString());
        }

        var dealerTable = new Table();
        dealerTable.AddColumn(new TableColumn($"[bold red]{dealer.Name} - Puntuación: {dealer.Score}[/]"));

        foreach (var card in dealer.Hand)
        {
            dealerTable.AddRow(card.ToString());
        }

        AnsiConsole.Write(playerTable);
        AnsiConsole.Write(dealerTable);
        ShowScore();
    }

    private void DetermineWinner()
    {
        if (player.Score > 21)
        {
            dealerWins++;
            dealerWinSoundPlayer.PlaySound();
        }
        else if (dealer.Score > 21)
        {
            playerWins++;
            winSoundPlayer.PlaySound();
        }
        else if (player.Score > dealer.Score)
        {
            playerWins++;
            winSoundPlayer.PlaySound();
        }
        else if (dealer.Score > player.Score)
        {
            dealerWins++;
            dealerWinSoundPlayer.PlaySound();
        }
        else
        {
            ties++;
        }
    }

    private void ShowRoundResults()
    {
        if (player.Score > 21)
        {
            AnsiConsole.MarkupLine("[bold red]¡El jugador se pasa! Gana el crupier.[/]");
        }
        else if (dealer.Score > 21)
        {
            AnsiConsole.MarkupLine("[bold yellow]¡El crupier se pasa! Gana el jugador.[/]");
        }
        else if (player.Score > dealer.Score)
        {
            AnsiConsole.MarkupLine("[bold yellow]¡Gana el jugador![/]");
        }
        else if (dealer.Score > player.Score)
        {
            AnsiConsole.MarkupLine("[bold red]¡Gana el crupier![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold blue]¡Empate![/]");
        }
    }

    private void ShowScore()
    {
        var scoreTable = new Table();
        scoreTable.AddColumn(new TableColumn("[bold yellow]Victorias del jugador[/]"));
        scoreTable.AddColumn(new TableColumn("[bold red]Victorias del crupier[/]"));
        scoreTable.AddColumn(new TableColumn("[bold blue]Empates[/]"));

        scoreTable.AddRow(playerWins.ToString(), dealerWins.ToString(), ties.ToString());
        AnsiConsole.Write(scoreTable);
    }

    private void ShowFinalResults()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold underline green]Resultados finales:[/]");
        ShowScore();
        string overallWinner;
        if (playerWins > dealerWins)
        {
            overallWinner = "Jugador";
        }
        else if (dealerWins > playerWins)
        {
            overallWinner = "Crupier";
        }
        else
        {
            overallWinner = "Empate";
        }
        AnsiConsole.MarkupLine($"[bold green]El ganador general es: {overallWinner}[/]");
    }

    private void AskReplay()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("¿Quieres jugar de nuevo?")
                .AddChoices(new[] { "Sí", "No" }));

        if (choice == "Sí")
        {
            InitializeGame();
            StartGame();
        }
    }
}
