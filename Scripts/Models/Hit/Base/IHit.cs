using System;
using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Hit;

public interface IHit : INode
{
    public void ChangeLifeTime(float lifeTime);

    public void ChangeStartDirection(Vector2 direction);

    public void ChangePower(float power);
}