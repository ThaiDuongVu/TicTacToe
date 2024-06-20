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
        private bool _isGameEnded;

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
        /// Handle player cell placement when a grid button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private void OnGridButtonClicked(object sender, RoutedEventArgs e)
        {
            if (_isGameEnded) return;

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

            var winner = CheckWinner();
            // If no winner yet then switch turn
            if (winner == 0) SwitchTurn();
            // Else end the game
            else
            {
                _isGameEnded = true;
                UpdateWinnerLabel(winner);
                restartButton.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Handle game restart when the restart button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {
            // Reset game state
            _grid = new int[GridSize, GridSize];
            _currentPlayerTurn = 1;
            _isGameEnded = false;

            // Reset cell buttons
            button1x1.Content = "";
            button1x2.Content = "";
            button1x3.Content = "";
            button2x1.Content = "";
            button2x2.Content = "";
            button2x3.Content = "";
            button3x1.Content = "";
            button3x2.Content = "";
            button3x3.Content = "";

            // Reset end game components
            winnerLabel.Visibility = Visibility.Hidden;
            restartButton.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Check the grid to find if there's a winner yet
        /// </summary>
        /// <returns>0 if no winner yet, 1 if player1 wins, 2 if player2 wins, 3 if it's a tie</returns>
        private int CheckWinner()
        {
            var isGridFilled = true;
            var checkStartIndex = GridSize - 3;
            var checkEndIndex = GridSize - 1;
            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    var cell = _grid[x, y];
                    if (cell == 0)
                    {
                        isGridFilled = false;
                        continue;
                    }

                    // Check horizontal match-3
                    if (x <= checkStartIndex)
                    {
                        if (cell == _grid[x + 1, y] && cell == _grid[x + 2, y]) return cell;
                    }

                    // Check vertical match-3
                    if (y <= checkStartIndex)
                    {
                        if (cell == _grid[x, y + 1] && cell == _grid[x, y + 2]) return cell;
                    }

                    // Check diagonal match
                    if (x <= checkStartIndex && y <= checkStartIndex)
                    {
                        if (cell == _grid[x + 1, y + 1] && cell == _grid[x + 2, y + 2]) return cell;
                    }
                    if (x <= checkStartIndex && y >= checkEndIndex)
                    {
                        if (cell == _grid[x + 1, y - 1] && cell == _grid[x + 2, y - 2]) return cell;
                    }
                }
            }

            // If can't find a winner and
            //      grid is filled: tie
            //      grid is not filled: game is still going
            return isGridFilled ? 3 : 0;
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

        /// <summary>
        /// Update text to show who won the game
        /// </summary>
        /// <param name="winner">1 if player1 wins, 2 if player2 wins, 3 if it's a tie</param>
        private void UpdateWinnerLabel(int winner)
        {
            switch (winner)
            {
                case 1:
                    winnerLabel.Content = "Player 1 wins!";
                    winnerLabel.Foreground = Player1Color;
                    break;

                case 2:
                    winnerLabel.Content = "Player 2 wins!";
                    winnerLabel.Foreground = Player2Color;
                    break;

                case 3:
                    winnerLabel.Content = "It's a tie!";
                    break;
            }
            winnerLabel.Visibility = Visibility.Visible;
        }
    }
}