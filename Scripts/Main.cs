using Godot;
using SimpleGame.Scripts.Models.Dungeon;
using SimpleGame.Scripts.Models.Entity.Enemy;
using SimpleGame.Scripts.Models.HotBar;
using SimpleGame.Scripts.Models.Inventory;


namespace SimpleGame.Scripts;

    public class Main : Node
    {
        private Player _player;

        private Enemy _enemy;
        
        private Level _level;

        private readonly CanvasLayer _layer = new ();

        private readonly InventoryHelper _helper = InventoryHelper.Instance;
        
        public override void _Ready()
        {
            _level = new Level(new Vector2(50,10));
            _level.ConnectLevel(this);

            _enemy = new Enemy();
            _enemy.ConnectToNode(_level.Walls);
            _enemy.SetPosition(_level.Center);
            
            _player = new Player();
            _player.ConnectToNode(_level.Walls);
            _player.SetPosition(_level.Center * 1.1f);
            
            var hotbar = new HotBarUi(_player.GetHotBar());
            
            hotbar.SetPosition(new Vector2(100,150));
            
            AddChild(_layer);
            
            _layer.AddChild(hotbar);
            _layer.AddChild(_helper);
        }
    }



