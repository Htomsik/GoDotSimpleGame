using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Hit;

/// <summary>
///     Удар
/// </summary>
public interface IHit : INode
{
    /// <summary>
    ///     Изменение времени жизни объекта
    /// </summary>
    public void ChangeLifeTime(float lifeTime);

    /// <summary>
    ///     Изменение направления объекта
    /// </summary>
    /// <param name="direction">вектор направления
    /// <remarks>Задается 1 или -1</remarks></param>
    public void ChangeStartDirection(Vector2 direction);

    /// <summary>
    ///     Сила удара
    /// </summary>
    public void ChangePower(float power);
}