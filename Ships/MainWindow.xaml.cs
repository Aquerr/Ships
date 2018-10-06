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
        private Dictionary<string, Button> _playerGridFields;
        private Dictionary<string, Button> _enemyGridFields;
        private GameState _gamePhase = GameState.OFF;

        private Player _player;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _player = new Player();
            _playerGridFields = new Dictionary<string, Button>();
            _enemyGridFields = new Dictionary<string, Button>();
            _gamePhase = GameState.PREPARATION;

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
                            _playerGridFields.Add(GameConfiguration.GridCharacters[column - 1] + row, button);
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
                            _enemyGridFields.Add(GameConfiguration.GridCharacters[column - 1] + row, button);
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
            switch (_gamePhase)
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
            int shipFlagNumber = 1;
            bool diagonalShip = false;
            bool verticalShip = false;
            bool horizontalShip = false;

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

            //Check if there are any already marked tiles around this one.
            if(id > 1 && id < 10)
            {
                int upperId = id - 1;
                int lowerId = id + 1;
           
                if(characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                {
                    //Check upperLeft
                    String upperLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + upperId;
                    if(ExistShipAt(upperLeftTileId))
                    {
                        diagonalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + upperLeftTileId);
                        shipFlagNumber += CheckNextTiles(upperLeftTileId, Direction.UpLeft);
                    }

                    //Check upperRight
                    String upperRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + upperId;
                    if(ExistShipAt(upperRightTileId))
                    {
                        diagonalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + upperRightTileId);
                        shipFlagNumber += CheckNextTiles(upperRightTileId, Direction.UpRight);
                    }

                    //Check lowerLeft
                    String lowerLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + lowerId;
                    if (ExistShipAt(lowerLeftTileId))
                    {
                        diagonalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + lowerLeftTileId);
                        shipFlagNumber += CheckNextTiles(lowerLeftTileId, Direction.DownLeft);
                    }

                    //Check lowerRight
                    String lowerRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + lowerId;
                    if (ExistShipAt(lowerRightTileId))
                    {
                        diagonalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + lowerRightTileId);
                        shipFlagNumber += CheckNextTiles(lowerRightTileId, Direction.DownRight);
                    }

                    //Check left
                    String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                    if (ExistShipAt(leftTileId))
                    {
                        horizontalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + leftTileId);
                        shipFlagNumber += CheckNextTiles(leftTileId, Direction.Left);
                    }

                    //Check right
                    String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                    if (ExistShipAt(rightTileId))
                    {
                        horizontalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + rightTileId);
                        shipFlagNumber += CheckNextTiles(rightTileId, Direction.Right);
                    }
                }

                //Check upper
                String upperTileId = GameConfiguration.GridCharacters[characterIndex] + upperId;
                if (ExistShipAt(upperTileId))
                {
                    verticalShip = true;
                    shipFlagNumber++;
                    MessageBox.Show("Ship exist at: " + upperTileId);
                    shipFlagNumber += CheckNextTiles(upperTileId, Direction.Up);
                }

                //Check lower
                String lowerTileId = GameConfiguration.GridCharacters[characterIndex] + lowerId;
                if(ExistShipAt(lowerTileId))
                {
                    verticalShip = true;
                    shipFlagNumber++;
                    MessageBox.Show("Ship exist at: " + lowerTileId);
                    shipFlagNumber += CheckNextTiles(lowerTileId, Direction.Down);
                }
            }
            else
            {
                //Id == 1 or Id == 10
                if (id == 1)
                {
                    //Check lower
                    String lowerTileId = GameConfiguration.GridCharacters[characterIndex] + (id + 1);
                    if (ExistShipAt(lowerTileId))
                    {
                        verticalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + lowerTileId);
                        shipFlagNumber += CheckNextTiles(lowerTileId, Direction.Down);
                    }

                    if(characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                    {
                        //Check lowerLeft
                        String lowerLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + (id + 1);
                        if (ExistShipAt(lowerLeftTileId))
                        {
                            diagonalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + lowerLeftTileId);
                            shipFlagNumber += CheckNextTiles(lowerLeftTileId, Direction.DownLeft);
                        }

                        //Check lowerRight
                        String lowerRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + (id + 1);
                        if (ExistShipAt(lowerRightTileId))
                        {
                            diagonalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + lowerRightTileId);
                            shipFlagNumber += CheckNextTiles(lowerRightTileId, Direction.DownRight);
                        }

                        //Check left
                        String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                        if (ExistShipAt(leftTileId))
                        {
                            horizontalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + leftTileId);
                            shipFlagNumber += CheckNextTiles(leftTileId, Direction.Left);
                        }

                        //Check right
                        String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                        if (ExistShipAt(rightTileId))
                        {
                            horizontalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + rightTileId);
                            shipFlagNumber += CheckNextTiles(rightTileId, Direction.Right);
                        }
                    }
                }
                else
                {
                    //Check upper
                    String upperTileId = GameConfiguration.GridCharacters[characterIndex] + (id - 1);
                    if (ExistShipAt(upperTileId))
                    {
                        verticalShip = true;
                        shipFlagNumber++;
                        MessageBox.Show("Ship exist at: " + upperTileId);
                        shipFlagNumber += CheckNextTiles(upperTileId, Direction.Up);
                    }

                    if (characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                    {
                        //Check upperLeft
                        String upperLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + (id - 1);
                        if (ExistShipAt(upperLeftTileId))
                        {
                            diagonalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + upperLeftTileId);
                            shipFlagNumber += CheckNextTiles(upperLeftTileId, Direction.UpLeft);
                        }

                        //Check upperRight
                        String upperRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + (id - 1);
                        if (ExistShipAt(upperRightTileId))
                        {
                            diagonalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + upperRightTileId);
                            shipFlagNumber += CheckNextTiles(upperRightTileId, Direction.UpRight);
                        }

                        //Check left
                        String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                        if (ExistShipAt(leftTileId))
                        {
                            horizontalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + leftTileId);
                            shipFlagNumber += CheckNextTiles(leftTileId, Direction.Left);
                        }

                        //Check right
                        String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                        if (ExistShipAt(rightTileId))
                        {
                            horizontalShip = true;
                            shipFlagNumber++;
                            MessageBox.Show("Ship exist at: " + rightTileId);
                            shipFlagNumber += CheckNextTiles(rightTileId, Direction.Right);
                        }
                    }
                }
            }

            //Validate ship orientation
            if (horizontalShip && diagonalShip && verticalShip)
                return;
            else if (horizontalShip && (diagonalShip || verticalShip))
                return;
            else if (verticalShip && (diagonalShip || horizontalShip))
                return;
            else if (diagonalShip && (horizontalShip || verticalShip))
                return;


            //Validate ship's flag number
            bool successValidation = ValidateShipFlagNumber(shipFlagNumber);
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

            MessageBox.Show("Your ship has " + shipFlagNumber + " flags.", "Debug");

            //Assign ship to the player
            _player.AddShip(shipFlagNumber);
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

        //Returns the number of marked tiles in the given direction from specified tile.
        private int CheckNextTiles(String tileId, Direction direction)
        {
            String character = tileId.Substring(0, 1);
            int characterIndex = Array.IndexOf(GameConfiguration.GridCharacters, character);
            int id = int.Parse(tileId.Substring(1));

            //Check if there are any already marked tiles around this one.
            if (id > 1 && id < 10)
            {
                int upperId = id - 1;
                int lowerId = id + 1;

                if (characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                {
                    if(direction == Direction.UpLeft)
                    {
                        //Check upperLeft
                        String upperLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + upperId;
                        if (ExistShipAt(upperLeftTileId))
                        {
                            MessageBox.Show("Ship exist at: " + upperLeftTileId);
                            return 1 + CheckNextTiles(upperLeftTileId, Direction.UpLeft);
                        }
                    }
                    else if(direction == Direction.UpRight)
                    {
                        //Check upperRight
                        String upperRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + upperId;
                        if (ExistShipAt(upperRightTileId))
                        {
                            MessageBox.Show("Ship exist at: " + upperRightTileId);
                            return 1 + CheckNextTiles(upperRightTileId, Direction.UpRight);
                        }
                    }
                    else if(direction == Direction.DownLeft)
                    {
                        //Check lowerLeft
                        String lowerLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + lowerId;
                        if (ExistShipAt(lowerLeftTileId))
                        {
                            MessageBox.Show("Ship exist at: " + lowerLeftTileId);
                            return 1 + CheckNextTiles(lowerLeftTileId, Direction.DownLeft);
                        }
                    }
                    else if(direction == Direction.DownRight)
                    {
                        //Check lowerRight
                        String lowerRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + lowerId;
                        if (ExistShipAt(lowerRightTileId))
                        {
                            MessageBox.Show("Ship exist at: " + lowerRightTileId);
                            return 1 + CheckNextTiles(lowerRightTileId, Direction.DownRight);
                        }
                    }
                    else if(direction == Direction.Left)
                    {
                        //Check left
                        String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                        if (ExistShipAt(leftTileId))
                        {
                            MessageBox.Show("Ship exist at: " + leftTileId);
                            return 1 + CheckNextTiles(leftTileId, Direction.Left);
                        }
                    }
                    else if(direction == Direction.Right)
                    {
                        //Check right
                        String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                        if (ExistShipAt(rightTileId))
                        {
                            MessageBox.Show("Ship exist at: " + rightTileId);
                            return 1 + CheckNextTiles(rightTileId, Direction.Right);
                        }
                    }
                }

                if(direction == Direction.Up)
                {
                    //Check upper
                    String upperTileId = GameConfiguration.GridCharacters[characterIndex] + upperId;
                    if (ExistShipAt(upperTileId))
                    {
                        MessageBox.Show("Ship exist at: " + upperTileId);
                        return 1 + CheckNextTiles(upperTileId, Direction.Up);
                    }
                }
                else if(direction == Direction.Down)
                {
                    //Check lower
                    String lowerTileId = GameConfiguration.GridCharacters[characterIndex] + lowerId;
                    if (ExistShipAt(lowerTileId))
                    {
                        MessageBox.Show("Ship exist at: " + lowerTileId);
                        return 1 + CheckNextTiles(lowerTileId, Direction.Down);
                    }
                }
            }
            else
            {
                //Id == 1 or Id == 10
                if (id == 1)
                {
                    if(direction == Direction.Down)
                    {
                        //Check lower
                        String lowerTileId = GameConfiguration.GridCharacters[characterIndex] + (id + 1);
                        if (ExistShipAt(lowerTileId))
                        {
                            MessageBox.Show("Ship exist at: " + lowerTileId);
                            return 1 + CheckNextTiles(lowerTileId, Direction.Down);
                        }
                    }

                    if (characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                    {
                        if(direction == Direction.DownLeft)
                        {
                            //Check lowerLeft
                            String lowerLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + (id + 1);
                            if (ExistShipAt(lowerLeftTileId))
                            {
                                MessageBox.Show("Ship exist at: " + lowerLeftTileId);
                                return 1 + CheckNextTiles(lowerLeftTileId, Direction.DownLeft);
                            }
                        }
                        else if(direction == Direction.DownRight)
                        {
                            //Check lowerRight
                            String lowerRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + (id + 1);
                            if (ExistShipAt(lowerRightTileId))
                            {
                                MessageBox.Show("Ship exist at: " + lowerRightTileId);
                                return 1 + CheckNextTiles(lowerRightTileId, Direction.DownRight);
                            }
                        }
                        else if(direction == Direction.Left)
                        {
                            //Check left
                            String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                            if (ExistShipAt(leftTileId))
                            {
                                MessageBox.Show("Ship exist at: " + leftTileId);
                                return 1 + CheckNextTiles(leftTileId, Direction.Left);
                            }
                        }
                        else if(direction == Direction.Right)
                        {
                            //Check right
                            String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                            if (ExistShipAt(rightTileId))
                            {
                                MessageBox.Show("Ship exist at: " + rightTileId);
                                return 1 + CheckNextTiles(rightTileId, Direction.Right);
                            }
                        }
                    }
                }
                else
                {
                    if(direction == Direction.Up)
                    {
                        //Check upper
                        String upperTileId = GameConfiguration.GridCharacters[characterIndex] + (id - 1);
                        if (ExistShipAt(upperTileId))
                        {
                            MessageBox.Show("Ship exist at: " + upperTileId);
                            return 1 + CheckNextTiles(upperTileId, Direction.Up);
                        }
                    }

                    if (characterIndex > 0 && characterIndex < GameConfiguration.GridCharacters.Length - 1)
                    {
                        if(direction == Direction.UpLeft)
                        {
                            //Check upperLeft
                            String upperLeftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + (id - 1);
                            if (ExistShipAt(upperLeftTileId))
                            {
                                MessageBox.Show("Ship exist at: " + upperLeftTileId);
                                return 1 + CheckNextTiles(upperLeftTileId, Direction.UpLeft);
                            }
                        }
                        else if(direction == Direction.UpRight)
                        {
                            //Check upperRight
                            String upperRightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + (id - 1);
                            if (ExistShipAt(upperRightTileId))
                            {
                                MessageBox.Show("Ship exist at: " + upperRightTileId);
                                return 1 + CheckNextTiles(upperRightTileId, Direction.UpRight);
                            }
                        }
                        else if(direction == Direction.Left)
                        {
                            //Check left
                            String leftTileId = GameConfiguration.GridCharacters[characterIndex - 1] + id;
                            if (ExistShipAt(leftTileId))
                            {
                                MessageBox.Show("Ship exist at: " + leftTileId);
                                return 1 + CheckNextTiles(leftTileId, Direction.Left);
                            }
                        }
                        else if(direction == Direction.Right)
                        {
                            //Check right
                            String rightTileId = GameConfiguration.GridCharacters[characterIndex + 1] + id;
                            if (ExistShipAt(rightTileId))
                            {
                                MessageBox.Show("Ship exist at: " + rightTileId);
                                return 1 + CheckNextTiles(rightTileId, Direction.Right);
                            }
                        }
                    }
                }
            }

            return 0;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _gamePhase = GameState.RUNNING;
            Button button = (Button)sender;
            button.IsEnabled = false;
        }

        public bool ExistShipAt(String tileId)
        {
            if (_playerGridFields.TryGetValue(tileId, out Button tile))
            {
                return tile.Background == Brushes.Gray;
            }
            return false;
        }
    }
}
