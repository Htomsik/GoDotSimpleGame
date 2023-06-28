using SimpleGame.Scripts.Models.Weapon;

namespace SimpleGame.Scripts.Models.Extensions;

public static class WeaponExtensions
{
    public static string GetSpriteName(this IWeapon weapon)
    {
        return weapon.Type switch
        {
            WeaponType.Punch => EntitySpriteNames.PunchSprite,
            WeaponType.Pistol => EntitySpriteNames.PistolShootSprite,
            _ => ""
        };
    }
}