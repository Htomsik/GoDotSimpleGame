using Godot;

namespace SimpleGame.Scripts.Models.Entity
{
    public class Entity
    {
        #region Fields

        protected readonly EntityBody Body;

        protected readonly EntityData Data;

        #endregion
        
        #region Constructors

        public Entity()
        {
            // Инициализируем основу
            Data = new EntityData();
            Body = new EntityBody(Data);
            
            // Инициализируем дочерние ноды
            AddChild(Data.Sprite);
            AddChild(Data.Collider);
            
            Body.PhysicsProcess = PhysicsProcess;
            Body.Process = Process;
            Body.Input = Input;
        }

        #endregion

        #region Methods

        public void PhysicsProcess(float delta)
        {
            
        }
        
        public void Process(float delta)
        {
            
        }
        
        public void Input(InputEvent ev)
        {
            
        }

        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
        
        #region Node extensions
        
        public void ConnectToNode(Node parent) => parent.AddChild(Body);
        
        public void DisconnectFromNode(Node parent) => parent.RemoveChild(Body);
        
        public void AddChild(Node child) =>  Body.AddChild(child);
        
        public void RemoveChild(Node child) =>  Body.RemoveChild(child);
        
        #endregion
        
        #endregion
    }
}