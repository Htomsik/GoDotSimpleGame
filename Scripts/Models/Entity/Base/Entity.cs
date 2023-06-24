using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Entity
{
    public class Entity : INode
    {
        #region Fields

        protected EntityBody Body;

        protected EntityData Data;

        #endregion
        
        #region Constructors

        public Entity()
        {
            InitializeData();
            
            InitializeChild();
            
            Body.PhysicsProcess = PhysicsProcess;
            Body.Process = Process;
            Body.Input = Input;
            
        }

        #endregion

        #region Methods

        protected virtual void InitializeData()
        {
            Data = new EntityData();
            Body = new EntityBody(Data);
        }

        protected virtual void InitializeChild()
        {
            // Инициализируем дочерние ноды
            AddChild(Data.AnimatedSprite);
            AddChild(Data.Collider);
        }
        
        public virtual void PhysicsProcess(float delta)
        {
            
        }
        
        public virtual void Process(float delta)
        {
            
        }
        
        public  void Input(InputEvent ev)
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