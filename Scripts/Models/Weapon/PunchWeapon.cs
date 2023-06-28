using Godot;
using SimpleGame.Scripts.Models.Hit.Punch;

namespace SimpleGame.Scripts.Models.Weapon;

public class PunchWeapon : Weapon<Punch>
{ 
    public PunchWeapon() 
    {
        AttackDelay = 0.5f;
        Offset = new Vector2(20, -2);
    }
}