namespace SimpleGame.Scripts.Models.World;

/// <summary>
///     Слои мира
/// </summary>
public enum WorldLayers : int
{
    /// <summary>
    ///     Персонаж
    /// </summary>
    Player,
    
    /// <summary>
    ///     Мир
    /// </summary>
    World,
    
    /// <summary>
    ///     Противники
    /// </summary>
    Enemy,
    
    /// <summary>
    ///     Предметы
    /// </summary>
    Items,
    
    /// <summary>
    ///     Сущности 
    /// </summary>
    Entity,
    
    /// <summary>
    ///     Удары
    /// </summary>
    Hit
}