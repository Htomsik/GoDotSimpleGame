using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Удар
    /// <remarks>   Принимает информацию из данных, обрабатывает и передает телу и данным</remarks>
    /// </summary>
    public class Hit : INode
    {
         protected  HitData Data { get; set; } = new HitData();

         protected  HitBody Body { get; set; }

        public Hit()
        {
            Body = new HitBody(Data);
            
            AddChild(Data.HitBox);
            AddChild(Data.LifeTimer);

            Data.Collider.ChangeSize(2f, 1);
            Data.HitBox.Collider.ChangeSize(2f,1);
            
            Data.HitBox.SetDamage += action => action?.Invoke(Data.DamagePower, Body.GlobalPosition);

            Body.Ready += Ready;
            Body.PhysicsProcess += PhysicsProcess;
        }
        
        protected virtual void Ready()
        {
            // Запуск таймра жизни объекта
            Data.LifeTimer.Start(Data.LifeTime);
            Data.LifeTimer.OneShot = true;
        }

        protected virtual void PhysicsProcess()
        {
            if (Data.LifeTimer.TimeLeft <= 0)
            {
                Body.QueueFree();
            }
        }

        public void ChangeLifeTime(float lifeTime)
        {
            Data.LifeTime = lifeTime;
        }
        
        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
        
        public void ConnectToNode(Node parent) => parent.AddChild(Body);
        
        public void DisconnectFromNode(Node parent) => parent.RemoveChild(Body);
        
        public void AddChild(Node child) =>  Body.AddChild(child);
        
        public void RemoveChild(Node child) =>  Body.RemoveChild(child);
    }
}