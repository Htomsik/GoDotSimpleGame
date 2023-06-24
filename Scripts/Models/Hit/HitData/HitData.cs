using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitData : EntityData
    {
        #region Свйоства сущности

        public float DamagePower { get; set; } = 10;

        #endregion
        
        #region Физические объекты

        public HitHitBox HitBox { get; set; } = new HitHitBox();

        #endregion

        public HitData()
        {
            HitBox.SetDamage += action => action?.Invoke(DamagePower);
        }
        
    }
}