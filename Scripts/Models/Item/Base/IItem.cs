using Godot;

namespace SimpleGame.Scripts.Models.Item;

/// <summary>
///     Предмет 
/// </summary>
public interface IItem
{
    public string Name { get;  }
    
    public TextureRect InventorySprite { get; }
    
    public ItemType ItemType { get; }
}