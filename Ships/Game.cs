using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ships
{
    public class Game
    {
        public static readonly Dictionary<string, Button> PLAYER_GRID_FIELDS = new Dictionary<string, Button>();
        public static readonly Dictionary<string, Button> _enemyGridFields = new Dictionary<string, Button>();
        public static GameState GAME_STATE = GameState.OFF;
    }
}
