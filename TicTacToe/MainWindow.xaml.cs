using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GridSize = 3;
        // 2D array of integers, each represent the state of a cell
        // 0: Empty cell
        // 1: Player 1 (X) occupied
        // 2: Player 2 (O) occupied
        private int[,] _grid = new int[GridSize, GridSize];

        private int _currentPlayerTurn = 1;

        // Symbols of each player
        private const string Player1Symbol = "X";
        private const string Player2Symbol = "O";

        // Colors of each player
        private readonly SolidColorBrush Player1Color = Brushes.Red;
        private readonly SolidColorBrush Player2Color = Brushes.Blue;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle player cell placement when a button on the grid is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void OnGridButtonClicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var tag = button.Tag.ToString() ?? throw new InvalidOperationException("Button must contain a tag");
            int x = int.Parse(tag[0].ToString());
            int y = int.Parse(tag[1].ToString());

            // If cell already clicked
            if (_grid[x - 1, y - 1] != 0)
            {
                ShowErrorBox("Invalid cell placement!", "Error");
                return;
            }

            // Update grid array
            _grid[x - 1, y - 1] = _currentPlayerTurn;

            // Update button appearance
            button.Content = _currentPlayerTurn == 1 ? Player1Symbol : Player2Symbol;
            button.Foreground = _currentPlayerTurn == 1 ? Player1Color : Player2Color;

            SwitchTurn();
        }

        /// <summary>
        /// Switch player turn
        /// </summary>
        private void SwitchTurn()
        {
            _currentPlayerTurn = (_currentPlayerTurn == 1) ? 2 : 1;
            //Cursor = _currentPlayerTurn == 1 ? Cursors.Cross : Cursors.Wait;
        }

        /// <summary>
        /// Display a dialog with an error message
        /// </summary>
        /// <param name="text">Error message to show</param>
        /// <param name="caption">Dialog caption</param>
        private static void ShowErrorBox(string text, string caption)
        {
            var button = MessageBoxButton.OK;
            var icon = MessageBoxImage.Error;
            MessageBox.Show(text, caption, button, icon, MessageBoxResult.Yes);
        }
    }
}