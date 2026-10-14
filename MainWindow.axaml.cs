
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace TicTacToeGUI;

public partial class MainWindow : Window
{
    private string[] spelplan = new string[9];
    private string spelare = "X";
    private bool speletSlut = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    // När spelaren klickar på en ruta
    private void Spela(object? sender, RoutedEventArgs e)
    {
        if (speletSlut || sender is not Button knapp)
            return;

        int index = int.Parse(knapp.Tag!.ToString()!);

        // Kontrollera att rutan är ledig
        if (spelplan[index] != null)
            return;

        spelplan[index] = spelare;
        knapp.Content = spelare;

        // Kontrollera om någon har vunnit
        if (HarVunnit())
        {
            StatusText.Text = $"Spelare {spelare} vann!";
            speletSlut = true;
        }
        else if (spelplan.All(ruta => ruta != null))
        {
            StatusText.Text = "Oavgjort!";
            speletSlut = true;
        }
        else
        {
            // Byt spelare
            spelare = spelare == "X" ? "O" : "X";
            StatusText.Text = $"Spelare {spelare}:s tur";
        }
    }

    // Kontrollera tre i rad
    private bool HarVunnit()
    {
        int[,] kombinationer =
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < 8; i++)
        {
            int a = kombinationer[i, 0];
            int b = kombinationer[i, 1];
            int c = kombinationer[i, 2];

            if (spelplan[a] == spelare &&
                spelplan[b] == spelare &&
                spelplan[c] == spelare)
            {
                return true;
            }
        }

        return false;
    }

    // Börja ett nytt spel
    private void StartaOm(object? sender, RoutedEventArgs e)
    {
        spelplan = new string[9];
        spelare = "X";
        speletSlut = false;

        StatusText.Text = "Spelare X:s tur";

        foreach (var knapp in this.GetVisualDescendants().OfType<Button>())
        {
            if (knapp.Tag != null)
            {
                knapp.Content = null;
            }
        }
    }
}
