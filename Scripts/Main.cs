using Godot;
using SimpleGame.Scripts.Models.Dungeon;
using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts
{
    public class Main : Node
    {
        private Player _player;

        private Level _level;
        
        public override void _Ready()
        {
            _level = new Level(new Vector2(100,100));
            _level.ConnectLevel(this);
                
            _player = new Player();
            _player.ConnectToNode(_level.Walls);
        }
    }
}
