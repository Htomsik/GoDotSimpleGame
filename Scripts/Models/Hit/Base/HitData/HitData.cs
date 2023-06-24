using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitData : EntityData
    {
        #region Свйоства сущности

        public float DamagePower { get; set; } = 10;

        public float LifeTime { get; set; } = 0.1f;

        #endregion
        
        #region Физические объекты

        public HitHitBox HitBox { get; set; } = new HitHitBox();

        #endregion

        
    }
}