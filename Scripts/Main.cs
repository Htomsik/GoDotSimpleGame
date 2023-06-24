using Godot;
using SimpleGame.Scripts.Models.Dungeon;
using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts
{
    public class Main : Node
    {
        private Player _player;

        private Enemy _enemy;

        private Level _level;
        
        public override void _Ready()
        {
            _level = new Level(new Vector2(100,10));
            _level.ConnectLevel(this);

            _enemy = new Enemy();
            _enemy.ConnectToNode(_level.Walls);
            _enemy.SetPosition(new Vector2(20, -200));
            
            
            _player = new Player();
            _player.ConnectToNode(_level.Walls);
            _player.SetPosition(new Vector2(10,-100));
        }
    }
}
