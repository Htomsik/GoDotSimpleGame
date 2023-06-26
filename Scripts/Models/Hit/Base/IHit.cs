using Godot;

namespace SimpleGame.Scripts.Models.Hit;

public interface IHit
{
    public void ChangeLifeTime(float lifeTime);

    public void ChangeStartDirection(Vector2 direction);

    public void ChangePower(float power);

}