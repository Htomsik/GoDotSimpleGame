using SimpleGame.Scripts.Models.Entity;

namespace SimpleGame.Scripts.Models.Hit
{
    public class HitData : EntityData
    {
        public HitHitBox HitBox { get; set; } = new HitHitBox();

    }
}