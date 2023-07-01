using System.Collections.Generic;
using Godot;
using SimpleGame.Scripts.Models.Item;
using Array = Godot.Collections.Array;

namespace SimpleGame.Scripts.Models.Inventory;

public class InventoryHelper : Node2D
{
    #region Instance
    
    public static InventoryHelper Instance => _helper ??= new InventoryHelper();

    private static InventoryHelper _helper;
    
    #endregion

    #region Properties
    
    public IItem HandItem
    { 
        get => _item;
        set
        {
            _item = value;

            _item?.ConnectToNode(this);
        }
    }

    private IItem _item;
    
    
    #endregion
    
    private InventoryHelper() { }


    #region Обработчики сигналов

    public override void _Input(InputEvent ev = default)
    {
        if (HandItem is Item.Item item)
        {
            item.GlobalPosition = GetGlobalMousePosition() - item.TextureRect.RectSize/2;
        }
    }
    
    private void GuiEntered(InputEvent ev, InventorySlot slot)
    {
        if (ev is not InputEventMouseButton evMouse) return;
        
        if (evMouse.ButtonIndex != 1 ) return;

        if (evMouse.Pressed)
        {
            MeshItems(slot);
        }
    }

    #endregion

    #region Действия

    public void AddSlots(IEnumerable<InventorySlot> slots)
    {
        foreach (var slot in slots)
        {
            slot.Connect("gui_input", this, nameof(GuiEntered), new Array(slot));
        }
    }

    /// <summary>
    ///     Поменять местами предмет в руке и слоте
    /// </summary>
    private void MeshItems(InventorySlot slot)
    {
        HandItem?.DisconnectFromNode(this);
        slot.Item?.DisconnectFromNode(slot.CenterContainer);
        (HandItem, slot.Item) = (slot.Item, HandItem);
        _Input();
    }

    #endregion
}