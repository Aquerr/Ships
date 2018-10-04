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
        private readonly string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};

        private Dictionary<string, Button> playerGridFields;
        private Dictionary<string, Button> enemyGridFields;
        private GameState gamePhase = GameState.OFF;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            playerGridFields = new Dictionary<string, Button>();
            gamePhase = GameState.PREPARATION;

            //Load player grid
            for (int i = 1; i <= 11; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    if (i == 1)
                    {
                        if(j != 1)
                        {
                            Label label = new Label();
                            label.Name = "playerLbl_" + letters[j - 2];
                            label.Content = letters[j - 2];
                            label.SetValue(Grid.ColumnProperty, j - 1);
                            label.SetValue(Grid.RowProperty, i - 1);
                            PlayerGrid.Children.Add(label);
                        }
                    }
                    else
                    {
                        if (j == 1)
                        {
                            Label label = new Label();
                            label.Name = "playerLbl_" + (i - 1);
                            label.Content = i - 1;
                            label.SetValue(Grid.ColumnProperty, j - 1);
                            label.SetValue(Grid.RowProperty, i - 1);
                            PlayerGrid.Children.Add(label);
                        }
                        else
                        {
                            Button button = new Button();
                            button.Name = "playerBtn_" + letters[j - 2] + i;
                            button.Background = Brushes.White;
                            button.FontSize = 18;
                            button.SetValue(Grid.ColumnProperty, j - 1);
                            button.SetValue(Grid.RowProperty, i - 1);
                            button.Click += new RoutedEventHandler(PlayerGridClick);
                            PlayerGrid.Children.Add(button);
                            playerGridFields.Add(letters[j - 2] + i, button);
                        }
                    }
                }
            }

            //Load enemy grid
            for (int i = 1; i <= 11; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    if (i == 1)
                    {
                        if (j != 1)
                        {
                            Label label = new Label();
                            label.Name = "enemyLbl_" + letters[j - 2];
                            label.Content = letters[j - 2];
                            label.SetValue(Grid.ColumnProperty, j - 1);
                            label.SetValue(Grid.RowProperty, i - 1);
                            PlayerShootingGrid.Children.Add(label);
                        }
                    }
                    else
                    {
                        if (j == 1)
                        {
                            Label label = new Label();
                            label.Name = "enemyLbl_" + (i - 1);
                            label.Content = i - 1;
                            label.SetValue(Grid.ColumnProperty, j - 1);
                            label.SetValue(Grid.RowProperty, i - 1);
                            PlayerShootingGrid.Children.Add(label);
                        }
                        else
                        {
                            Button button = new Button();
                            button.Name = "enemyBtn_" + letters[j - 2] + i;
                            button.Background = Brushes.White;
                            button.FontSize = 18;
                            button.SetValue(Grid.ColumnProperty, j - 1);
                            button.SetValue(Grid.RowProperty, i - 1);
                            button.Click += new RoutedEventHandler(EnemyGridClick);
                            PlayerShootingGrid.Children.Add(button);
                            enemyGridFields.Add(letters[j - 2] + i, button);
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
            switch (gamePhase)
            {
                case GameState.PREPARATION:
                    handlePrepareGridClick(button);
                    break;
                case GameState.RUNNING:
                    handleRunningGridClick(button);
                    break;
                case GameState.OFF:
                    //handleOFFGridClick(button);
                    break;

                default:
                    MessageBox.Show("Invalid game state! Please restart your game!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void handleRunningGridClick(Button button)
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

        private void handlePrepareGridClick(Button button)
        {
            if (button.Background == Brushes.White)
            {
                button.Background = Brushes.Gray;
            }
            else
            {
                button.Background = Brushes.White;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            gamePhase = GameState.RUNNING;
            Button button = (Button)sender;
            button.IsEnabled = false;
        }
    }
}
