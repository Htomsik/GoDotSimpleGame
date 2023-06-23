using Godot;
using SimpleGame.Scripts.Models.Extensions;

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
            // Загрузка текстуры в пресонажа
            Data.Sprite.Texture = ImageLoader.LoadTexture("res://Sprites/Entity/Character Idle 48x48.png", true);
            
            // Иницилизация 
            Data.InitBody(1,10, new Vector2(0,-16));
            Data.InitCollider(3.5f, 3);

            // Делаем камеру текущей для игры
            _camera.Current = true;
            _camera.Zoom = new Vector2(5, 5);// test

            // Добавляем камеру дочерним нодом
            AddChild(_camera);

            Body.PhysicsProcess += Control;
        }

        #endregion

        #region Methods

        private void Control(float delta) => GetInputDirection();
        
        private void GetInputDirection()
        {
            Data.Velocity.x = Godot.Input.GetActionStrength("Right") - Godot.Input.GetActionStrength("Left");
            Data.Velocity.y = Godot.Input.GetActionStrength("Down") - Godot.Input.GetActionStrength("Up");
        }

        #endregion
        
    }
}