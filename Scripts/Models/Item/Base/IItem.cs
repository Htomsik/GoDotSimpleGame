using Godot;

namespace SimpleGame.Scripts.Models.Item;

/// <summary>
///     Предмет 
/// </summary>
public interface IItem
{
    public string Name { get;  }
    
    public Sprite Sprite { get; }
    
    public ItemType ItemType { get; }
}