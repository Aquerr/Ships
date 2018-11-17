using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ships
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player _player;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _player = new Player();
            //_playerGridFields = new Dictionary<string, Button>();
            //_enemyGridFields = new Dictionary<string, Button>();
            Game.GAME_STATE = GameState.PREPARATION;

            //Load player grid
            for (int row = 0; row <= 10; row++)
            {
                for (int column = 0; column <= 10; column++)
                {
                    //If first row, print labels with characters
                    //Else print buttons
                    if (row == 0)
                    {
                        if(column != 0)
                        {
                            Label label = new Label();
                            label.Name = "playerLbl_" + GameConfiguration.GridCharacters[column - 1];
                            label.Content = GameConfiguration.GridCharacters[column - 1];
                            label.SetValue(Grid.ColumnProperty, column);
                            label.SetValue(Grid.RowProperty, row);
                            PlayerGrid.Children.Add(label);
                        }
                    }
                    else
                    {
                        //If first column (column == 0) then print label with number
                        //Else print button
                        if (column == 0)
                        {
                            Label label = new Label();
                            label.Name = "playerLbl_" + row;
                            label.Content = row;
                            label.SetValue(Grid.ColumnProperty, column);
                            label.SetValue(Grid.RowProperty, row);
                            PlayerGrid.Children.Add(label);
                        }
                        else
                        {
                            Button button = new Button();
                            button.Name = "playerBtn_" + GameConfiguration.GridCharacters[column - 1] + row;
                            button.Background = Brushes.White;
                            button.FontSize = 18;
                            button.SetValue(Grid.ColumnProperty, column);
                            button.SetValue(Grid.RowProperty, row);
                            button.Click += new RoutedEventHandler(PlayerGridClick);
                            PlayerGrid.Children.Add(button);
                            Game.PLAYER_GRID_FIELDS.Add(GameConfiguration.GridCharacters[column - 1] + row, button);
                        }
                    }
                }
            }

            //Load enemy grid
            for (int row = 0; row <= 10; row++)
            {
                for (int column = 0; column <= 10; column++)
                {
                    //If first row, print labels with characters
                    //Else print buttons
                    if (row == 0)
                    {
                        if (column != 0)
                        {
                            Label label = new Label();
                            label.Name = "enemyLbl_" + GameConfiguration.GridCharacters[column - 1];
                            label.Content = GameConfiguration.GridCharacters[column - 1];
                            label.SetValue(Grid.ColumnProperty, column);
                            label.SetValue(Grid.RowProperty, row);
                            PlayerShootingGrid.Children.Add(label);
                        }
                    }
                    else
                    {
                        //If first column (column == 0) then print label with number
                        //Else print button
                        if (column == 0)
                        {
                            Label label = new Label();
                            label.Name = "enemyLbl_" + row;
                            label.Content = row;
                            label.SetValue(Grid.ColumnProperty, column);
                            label.SetValue(Grid.RowProperty, row);
                            PlayerShootingGrid.Children.Add(label);
                        }
                        else
                        {
                            Button button = new Button();
                            button.Name = "enemyBtn_" + GameConfiguration.GridCharacters[column - 1] + row;
                            button.Background = Brushes.White;
                            button.FontSize = 18;
                            button.SetValue(Grid.ColumnProperty, column);
                            button.SetValue(Grid.RowProperty, row);
                            button.Click += new RoutedEventHandler(EnemyGridClick);
                            PlayerShootingGrid.Children.Add(button);
                            Game._enemyGridFields.Add(GameConfiguration.GridCharacters[column - 1] + row, button);
                        }
                    }
                }
            }
        }

        private void EnemyGridClick(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerGridClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (Game.GAME_STATE)
            {
                case GameState.PREPARATION:
                    HandlePrepareGridClick(button);
                    break;
                case GameState.RUNNING:
                    HandleRunningGridClick(button);
                    break;
                case GameState.OFF:
                    //handleOFFGridClick(button);
                    break;

                default:
                    MessageBox.Show("Invalid game state! Please restart your game!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void HandleRunningGridClick(Button button)
        {
            //Check if button is a ship
            if (button.Background == Brushes.Gray)
            {
                if (button.Content == null || button.Content.Equals(""))
                {
                    button.Content = "X";
                }
                else
                {
                    button.Content = "";
                }
            }
        }

        private void HandlePrepareGridClick(Button button)
        {
            bool diagonalShip = false;
            bool verticalShip = false;
            bool horizontalShip = false;
            bool wrongShip = false;

            //Check available ships
            if (!_player.HasAvailableShips())
            {
                MessageBox.Show("You can't place more ships!", "Error");
                return;
            }

            String tileId = button.Name.Split(new char[] { '_' })[1];
            String character = tileId.Substring(0, 1);
            int characterIndex = Array.IndexOf(GameConfiguration.GridCharacters, character);
            int id = int.Parse(tileId.Substring(1));

            List<string> tilesAboveLeft = Utils.GetAboveLeftTile(characterIndex, id, Direction.DownRight);
            List<string> tilesAboveUp = Utils.GetAboveTile(characterIndex, id, Direction.Down);
            List<string> tilesAboveRight = Utils.GetAboveRightTile(characterIndex, id, Direction.DownLeft);
            List<string> tilesLeft = Utils.GetLeftTile(characterIndex, id, Direction.Right);
            List<string> tilesRight = Utils.GetRightTile(characterIndex, id, Direction.Left);
            List<string> tilesBelowLeft = Utils.GetBelowLeftTile(characterIndex, id, Direction.UpRight);
            List<string> tilesBelowDown = Utils.GetBelowTile(characterIndex, id, Direction.Up);
            List<string> tilesBelowRight = Utils.GetBelowRightTile(characterIndex, id, Direction.UpLeft);

            if (tilesAboveLeft == null || tilesAboveUp == null || tilesAboveRight == null || tilesLeft == null
                || tilesRight == null || tilesBelowLeft == null || tilesBelowDown == null || tilesBelowRight == null)
                wrongShip = true;

            if((tilesAboveLeft != null && tilesAboveLeft.Count > 0) 
                || (tilesAboveRight != null && tilesAboveRight.Count > 0)
                || (tilesBelowLeft != null && tilesBelowLeft.Count > 0) 
                || (tilesBelowRight != null && tilesBelowRight.Count > 0))
            {
                diagonalShip = true;
                if ((tilesAboveLeft != null && tilesAboveLeft.Count > 0) 
                    && ((tilesAboveRight != null && tilesAboveRight.Count > 0) 
                    || (tilesBelowLeft != null && tilesBelowLeft.Count > 0)))
                {
                    wrongShip = true;
                }
                else if ((tilesAboveRight != null && tilesAboveRight.Count > 0) 
                    && ((tilesAboveLeft != null && tilesAboveLeft.Count > 0) 
                    || (tilesBelowRight != null && tilesBelowRight.Count > 0)))
                {
                    wrongShip = true;
                }
                else if ((tilesBelowLeft != null && tilesBelowLeft.Count > 0) 
                    && ((tilesAboveLeft != null && tilesAboveLeft.Count > 0) 
                    || (tilesBelowRight != null && tilesBelowRight.Count > 0)))
                {
                    wrongShip = true;
                }
                else if ((tilesBelowRight != null && tilesBelowRight.Count > 0) 
                    && ((tilesBelowLeft != null && tilesBelowLeft.Count > 0) 
                    || (tilesAboveRight != null && tilesAboveRight.Count > 0)))
                {
                    wrongShip = true;
                }
            }

            if((tilesAboveUp != null && tilesAboveUp.Count > 0) || (tilesBelowDown != null && tilesBelowDown.Count > 0))
            {
                verticalShip = true;
            }

            if((tilesLeft != null && tilesLeft.Count > 0) || (tilesRight != null && tilesRight.Count > 0))
            {
                horizontalShip = true;
            }

            //Validate ship orientation
            if (wrongShip)
            {
                MessageBox.Show("You can't place ship here!");
                return;
            }

            if (horizontalShip && diagonalShip && verticalShip)
                return;
            else if (horizontalShip && (diagonalShip || verticalShip))
                return;
            else if (verticalShip && (diagonalShip || horizontalShip))
                return;
            else if (diagonalShip && (horizontalShip || verticalShip))
                return;

            List<string> shipTiles = new List<string>();
            if (tilesAboveLeft != null)
                shipTiles.AddRange(tilesAboveLeft);
            if (tilesAboveUp != null)
                shipTiles.AddRange(tilesAboveUp);
            if (tilesAboveRight != null)
                shipTiles.AddRange(tilesAboveRight);
            if (tilesLeft != null)
                shipTiles.AddRange(tilesLeft);
            if (tilesRight != null)
                shipTiles.AddRange(tilesRight);
            if (tilesBelowLeft != null)
                shipTiles.AddRange(tilesBelowLeft);
            if (tilesBelowDown != null)
                shipTiles.AddRange(tilesBelowDown);
            if (tilesBelowRight != null)
                shipTiles.AddRange(tilesBelowRight);

            //Check player ships
            Ship ship = _player.GetShip(shipTiles);
            if(ship != null)
            {
                _player.RemoveShip(ship);
            }

            //Add current tile
            shipTiles.Add(tileId);

            //Validate ship's flag number
            bool successValidation = ValidateShipFlagNumber(shipTiles.Count);
            if (!successValidation) return;

            if (button.Background == Brushes.White)
            {
                //_player.addShip();
                button.Background = Brushes.Gray;
            }
            else
            {
                button.Background = Brushes.White;
            }

            //MessageBox.Show("Your ship has " + shipTiles.Count + " flags.", "Debug");

            //Assign ship to the player
            _player.AddShip(new Ship(shipTiles));
        }

        private bool ValidateShipFlagNumber(int shipFlagNumber)
        {
            if (shipFlagNumber > 4)
            {
                MessageBox.Show("This ship has too many flags/tiles!", "Error");
                return false;
            }
            else if(shipFlagNumber == 4 && _player.GetAvailableShips(ShipType.FourFlag) == 0)
            {
                MessageBox.Show("You can't place more four flag ships!", "Error");
                return false;
            }
            else if(shipFlagNumber == 3 && _player.GetAvailableShips(ShipType.ThreeFlag) == 0 &&
                _player.GetAvailableShips(ShipType.FourFlag) == 0)
            {
                MessageBox.Show("You can't place more three or four flag ships!", "Error");
                return false;
            }
            else if (shipFlagNumber == 2 && _player.GetAvailableShips(ShipType.TwoFlag) == 0 && _player.GetAvailableShips(ShipType.ThreeFlag) == 0 &&
    _player.GetAvailableShips(ShipType.FourFlag) == 0)
            {
                MessageBox.Show("You can't place more two, three or four flag ships!", "Error");
                return false;
            }
            else if (shipFlagNumber == 1 && _player.GetAvailableShips(ShipType.OneFlag) == 0 && _player.GetAvailableShips(ShipType.TwoFlag) == 0 && _player.GetAvailableShips(ShipType.ThreeFlag) == 0 &&
    _player.GetAvailableShips(ShipType.FourFlag) == 0)
            {
                MessageBox.Show("You can't place more ships!", "Error");
                return false;
            }

            return true;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Game.GAME_STATE = GameState.RUNNING;
            Button button = (Button)sender;
            button.IsEnabled = false;
        }
    }
}
