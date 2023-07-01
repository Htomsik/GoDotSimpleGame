using System;
using Godot;
using SimpleGame.Scripts.Models.Extensions;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory;

public class InventorySlot : Panel
{
    #region Свойства

    public int Id { get;}
    
    /// <summary>
    ///     ID и вещь для замены в боксе
    /// </summary>
    public Action<int, IItem> ItemChanged { get; set; }

    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            
            if (BaseStyle.Texture == null || SelectedStyle.Texture == null)
                SetStyleBoxes();
            
            AddStyleboxOverride("panel", _selected ? SelectedStyle : BaseStyle );
        }
    }

    private bool _selected;

    #endregion
    
    #region Фзические свойства

    protected virtual StyleBoxTexture BaseStyle { get; } = new ();
    protected virtual StyleBoxTexture SelectedStyle { get; } = new ();

    public IItem Item
    { 
        get => _item;
        set
        {
            var oldItem = _item;
            
            _item = value;
            
            if (oldItem != _item)
                ItemChanged?.Invoke(Id, _item);

            if (_item == null)
                return;
            
            _item.IsPicked = false;
            _item.SetPosition(Vector2.Zero);
            _item.ConnectToNode(CenterContainer);
        }
    }

    private IItem _item;

    public CenterContainer CenterContainer { get; set; } = new CenterContainer();
    
    #endregion

    #region Constructors
    
    public InventorySlot(IItem item,int position) : this(position)
    {
        Item = item;
    }

    public InventorySlot(int id)
    {
        Id = id;
        RectSize = new Vector2(20, 20);
        Selected = false;
        AddChild(CenterContainer);
        CenterContainer.RectSize = RectSize;
        CenterContainer.MouseFilter = MouseFilterEnum.Ignore;
    }

    #endregion

    #region Methods

    protected virtual void SetStyleBoxes()
    {
        BaseStyle.Texture = ImageLoader.LoadTexture("res://Sprites/HotBar/HotBar.png", true);
        
        SelectedStyle.Texture = ImageLoader.LoadTexture("res://Sprites/HotBar/SelectedHotbarItem.png", true);
    }

    #endregion
}