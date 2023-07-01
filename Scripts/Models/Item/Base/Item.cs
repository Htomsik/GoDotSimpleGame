using Godot;
using SimpleGame.Scripts.Models.Extensions;


namespace SimpleGame.Scripts.Models.Item;

/// <summary>
///     Базовый предмет
/// </summary>
public abstract class Item : Node2D, IItem
{
    #region Свойства

    public string Name { get; }
    
    public ItemType ItemType { get; protected set; } = ItemType.Quest;

    public readonly TextureRect TextureRect = new TextureRect();
    
    public bool IsPicked { get; set; }
    
    #endregion
    
    #region Constructors

    public Item()
    {
        // TODO Переделать по нормальному
        Name =  GetType().Name;
        TextureRect.Texture = ImageLoader.LoadTexture("res://Sprites/Item/ItemPlaceHolder.png", true);
        TextureRect.MouseFilter = Control.MouseFilterEnum.Ignore;
        TextureRect.RectPosition = new Vector2(2, 2);
        
        AddChild(TextureRect);
    }
    
    #endregion

    #region INode extensions
    
    public void ConnectToNode(Node parent) =>  parent.AddChild(this);
   

    public void DisconnectFromNode(Node parent) =>  parent.RemoveChild(this);
   

    public void AddChild(Node child) => base.AddChild(child);
        
    public new void RemoveChild(Node child) =>  base.RemoveChild(child);
    

    #endregion
}