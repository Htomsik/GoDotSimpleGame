

using Godot;

namespace SimpleGame.Scripts.Models.Entity.Enemy
{
    public class Enemy : Entity
    {
        private EnemyData EnemyData { get; set; }
        
        
        protected override void InitializeData()
        {
            EnemyData = new EnemyData();
            
            Data = EnemyData;
            Body = new EntityBody(Data);
            
            // Иницилизация 
            Data.InitBody( new Vector2(0,0));
            Data.Collider.ChangeSize(8f, 18);
            EnemyData.HitBox.Collider.ChangeSize(8f, 18);


            EnemyData.Dead += Dead;
        }

        protected override void InitializeChild()
        {
            AddChild(EnemyData.HpBar);
            AddChild(EnemyData.HitBox);
            base.InitializeChild();
        }

        protected void Dead()
        {
            Body.QueueFree();
        }
    }
}