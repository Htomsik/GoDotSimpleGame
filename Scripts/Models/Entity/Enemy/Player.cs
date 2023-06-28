using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Player : Enemy
    {
        #region Fields

        private readonly Camera2D _camera = new Camera2D();

        #endregion

        #region Constructors

        public Player()
        {
            // Делаем камеру текущей для игры
            _camera.Current = true;
            //_camera.Zoom = new Vector2(5, 5);// test

            // Добавляем камеру дочерним нодом
            AddChild(_camera);

            Body.PhysicsProcess += Control;
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
        }

        #endregion
        
    }
}