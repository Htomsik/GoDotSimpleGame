using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Item;

/// <summary>
///     Предмет 
/// </summary>
public interface IItem : INode
{
    public string Name { get;  }
    
    public ItemType ItemType { get; }
    
    public bool IsPicked { get; set; }
}
