using Godot;
using SimpleGame.Scripts.Models.Extensions;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory.HotBar;

public class HotBarUiPanel : Panel
{
    #region Свойства
    

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

    public IItem Item { get; set; }

    protected CenterContainer CenterContainer { get; set; } = new CenterContainer();
    
    #endregion

    
    public HotBarUiPanel(IItem item)
    {
        Item = item;
        
        RectSize = new Vector2(20, 20);
        CenterContainer.RectSize = RectSize;
        Selected = false;
        
        CenterContainer.AddChild(item.InventorySprite);
        AddChild(CenterContainer);
    }

    protected virtual void SetStyleBoxes()
    {
        BaseStyle.Texture = ImageLoader.LoadTexture("res://Sprites/HotBar/HotBar.png", true);
        
        SelectedStyle.Texture = ImageLoader.LoadTexture("res://Sprites/HotBar/SelectedHotbarItem.png", true);
    }
}