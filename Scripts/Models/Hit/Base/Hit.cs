using System;
using Godot;


namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Базовый удар
    /// </summary>
    public abstract class Hit<THitData, THitBody> : IHit
        where THitData : HitData
        where THitBody : HitBody<THitData>
    {
        protected  THitData Data { get; set; } 

        protected  THitBody Body { get; set; }

        public Hit(THitData data, THitBody body)
        {
            Data = data;
            Body = body;
            Body.Data = Data;
            
            Body.Ready += Ready;
            
            AddChild(Data.HitBox);
            AddChild(Data.LifeTimer);
            AddChild(Data.AnimatedSprite);

            Data.Collider.ChangeSize(2f, 1);
            Data.HitBox.Collider.ChangeSize(2f,1);
            
            Data.HitBox.SetDamage += action =>
            {
                action?.Invoke(Data.DamagePower, Data.Direction);
                data.LifeTimer.Start(0);
            };
            Body.PhysicsProcess += PhysicsProcess;
        }
        
        #region Обработчики тиков

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

        #endregion

        #region Другое

        public Type GenerateHit()
        {
            throw new NotImplementedException();
        }

        public void ChangeLifeTime(float lifeTime)
        {
            Data.LifeTime = lifeTime;
        }

        public void ChangeStartDirection(Vector2 direction)
        {
            Data.Direction = direction;
            Data.Velocity *= direction;
        }
        
        public void ChangePower(float power)
        {
            Data.DamagePower = power;
        }

        #endregion

        #region INode

        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
        
        public void ConnectToNode(Node parent) => parent.AddChild(Body);
        
        public void DisconnectFromNode(Node parent) => parent.RemoveChild(Body);
        
        public void AddChild(Node child) =>  Body.AddChild(child);
        
        public void RemoveChild(Node child) =>  Body.RemoveChild(child);

        #endregion
        
    }
}