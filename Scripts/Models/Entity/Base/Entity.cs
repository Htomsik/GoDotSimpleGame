using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Entity
{
    public abstract class Entity<TEntityBody, TEntityData> : INode 
        where TEntityBody : EntityBody<TEntityData>
        where TEntityData : EntityData
    {
        #region Properties

        protected TEntityBody Body { get; private set; }
        
        protected TEntityData Data { get; private set; }
        
        #endregion
        
        #region Constructors
        
        protected Entity(TEntityBody body, TEntityData data)
        {
            Body = body;
            Data = data;
            Body.Data = data;
            
            Body.PhysicsProcess = PhysicsProcess;
            Body.Process = Process;
            Body.Input = Input;
            Body.Ready = Ready;
            
            AddChild(Data.AnimatedSprite);
            AddChild(Data.Collider);
            AddChild(Data.HurtTimer);
        }

        #endregion

        #region Methods

        #region Обработчики тиков

        protected virtual void PhysicsProcess(float delta)
        {
            
        }
        
        protected virtual void Process(float delta)
        {
            
        }
        
        protected virtual  void Input(InputEvent ev)
        {
            
        }
        
        protected virtual void Ready()
        {
          
        }

        #endregion
        
        #region INode extensions
        
        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
        
        public void ConnectToNode(Node parent) => parent.AddChild(Body);
        
        public void DisconnectFromNode(Node parent) => parent.RemoveChild(Body);
        
        public void AddChild(Node child) =>  Body.AddChild(child);
        
        public void RemoveChild(Node child) =>  Body.RemoveChild(child);
        
        #endregion
        
        #endregion
    }
}