using Godot;
using SimpleGame.Scripts.Models.HotBar;
using SimpleGame.Scripts.Models.Weapon;
using SimpleGame.Scripts.Models.World;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Player : Enemy
    {
        #region Fields

        private readonly Camera2D _camera = new Camera2D();
        
        protected HotBarUi HotBarUi { get; set; }

        #endregion

        #region Constructors

        public Player()
        {
            // Делаем камеру текущей для игры
            _camera.Current = true;
            //_camera.Zoom = new Vector2(10, 10);// test

            // Добавляем камеру дочерним нодом
            AddChild(_camera);

            Body.PhysicsProcess += Control;
            
            Data.HotBar.Box.Add(new PistolWeapon());
            Data.HotBar.Box.Add(new PunchWeapon());
        }

        #endregion

        #region Methods

        private void Control(float delta) => GetInputDirection();
        
        private void GetInputDirection()
        {
            Body.Run(Godot.Input.GetActionStrength("Right") - Godot.Input.GetActionStrength("Left"));
            
            if (Godot.Input.IsActionPressed("Jump"))
            {
                Body.Jump(Godot.Input.GetActionStrength("Jump"));
            }

            if (Godot.Input.IsActionPressed("Attack"))
            {
                Attack();
                return;
            }
            
            if (Godot.Input.IsActionJustReleased("HotBarSwitchUp"))
            {
                ChangeHotBarItem(1);
                return; 
            }
            
            if (Godot.Input.IsActionJustReleased("HotBarSwitchDown"))
            {
                ChangeHotBarItem(-1);
            }
        }

        protected override void InitializeCollisionLayers()
        {
            base.InitializeCollisionLayers();
            
            Body.SetCollisionLayerBit((int)WorldLayers.Player, true);
        }

        #endregion
    }
}