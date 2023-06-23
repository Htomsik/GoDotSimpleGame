using Godot;
using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts
{
    public class Main : Node
    {
        private Player _player;

        private TileMap _dungeon;
        
        public override void _Ready()
        {
            _player = new Player();
            
            _dungeon = (TileMap)GetNode("TileMap");
            _player.ConnectToNode(_dungeon);
        }
    }
}
