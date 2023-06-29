using Godot;
using SimpleGame.Scripts.Models.Extensions;

namespace SimpleGame.Scripts.Models.Item;

/// <summary>
///     Базовый предмет
/// </summary>
public abstract class Item : IItem
{
    #region Свойства

    public string Name { get; }

    public ItemType ItemType { get; } = ItemType.Quest;

    #endregion

    #region Физические свойства

    public Sprite Sprite { get; }

    #endregion


    #region Constructors

    public Item()
    {
        // TODO Переделать по нормальному
        Name =  GetType().Name;
        Sprite = new Sprite();
        Sprite.Texture = ImageLoader.LoadTexture("res://Sprites/Item/ItemPlaceHolder.png", true);
    }

    #endregion
    
}