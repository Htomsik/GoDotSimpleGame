using System.Collections.Generic;
using Godot;
using SimpleGame.Scripts.Models.Inventory;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.HotBar;

public class HotBarUi : Container
{
    #region Properties

    protected int OldSelected { get; set; }

    private bool _noCycle = false;

    #endregion

    #region Физические свойства

    protected List<InventorySlot> Slots { get; } = new ();

    protected IHotBar Bar
    {
        get => _bar;
        set
        {
            _bar = value;
            
            CreateSlots();
            SetHotBarSubscriptions();
        }
    }

    private IHotBar _bar;

    #endregion

    #region Constructors

    public HotBarUi(IHotBar bar)
    {
        Bar = bar;
    }

    #endregion
    
    #region Methods

    protected virtual void SetHotBarSubscriptions()
    {
        Bar.SelectionChanged += id =>
        {
            Slots[OldSelected].Selected = false;
            Slots[id].Selected = true;
            OldSelected = id;
        };

        Bar.Box.BoxChanged += InitializePanels;
        
        InitializePanels();
        Slots[0].Selected = true;
    }

    /// <summary>
    ///     Пересборка панелей пр ининцалзаци нового хотбара
    /// </summary>
    public virtual void CreateSlots()
    {
        var count = 0;
        
        for (var id = 0; id < Bar.Box.MaxItemsCount; id++)
        {
            var slot = new InventorySlot(id);
            
            slot.SetPosition(new Vector2(count, 0));
            
            count += 20;
            
            AddChild(slot);
            Slots.Add(slot);
        }
        
        if (Slots.Count == 0)
        {
            return; 
        }
        
        SetSlotsSubscriptions();
    }

    public virtual void InitializePanels()
    {
        if (_noCycle)
        {
            _noCycle = false;
            return;
        }
        
        for (var id = 0; id < Bar.Box.Count; id++)
        {
            Slots[id].Item = Bar.Box[id];
        }
    }


    /// <summary>
    ///     Инициализация подписок из слотов
    /// </summary>
    protected virtual void SetSlotsSubscriptions()
    {
        InventoryHelper.Instance.AddSlots(Slots);
        
        foreach (var slot in Slots)
        {
            slot.ItemChanged += ChangeItemInSlot;
        }
    }
    
    /// <summary>
    ///     Удалене вещи из бокса
    /// </summary>
    protected virtual void ChangeItemInSlot(int id, IItem item)
    {
        _noCycle = true;
        Bar.Box.Change(id, item);
    }
    
    #endregion
}