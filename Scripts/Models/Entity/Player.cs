using Godot;

namespace SimpleGame.Scripts.Models.Entity
{
    public class Player : Entity
    {
        #region Fields

        private readonly Camera2D _camera = new Camera2D();

        #endregion

        #region Constructors

        public Player()
        {
            // Иницилизация 
            Data.InitBody( new Vector2(0,0));
            Data.InitCollider(6f, 18);

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
            
            Body.Jump(Godot.Input.GetActionStrength("Jump"), false);
        }

        #endregion
        
    }
}