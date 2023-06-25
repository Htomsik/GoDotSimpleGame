using Godot;
using SimpleGame.Scripts.Models.Dungeon;
using SimpleGame.Scripts.Models.Entity;
using SimpleGame.Scripts.Models.Entity.Enemy;
using SimpleGame.Scripts.Models.Hit;

namespace SimpleGame.Scripts
{
    public class Main : Node
    {
        private Player _player;

        private Enemy _enemy;

        private Hit _hit;

        private Level _level;
        
        public override void _Ready()
        {
            _level = new Level(new Vector2(20,20));
            _level.ConnectLevel(this);

            _enemy = new Enemy();
            _enemy.ConnectToNode(_level.Walls);
            _enemy.SetPosition(_level.End);
            
            
            _player = new Player();
            _player.ConnectToNode(_level.Walls);
            _player.SetPosition(_level.Start);
            
        }
    }
}
