using System.Collections.Generic;
using SimpleGame.Scripts.Models.Inventory.HotBar;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory;

public class Inventory : IInventory
{
    #region Properties

    protected int BoxCount = 18;
    
    #endregion
    
    #region Физические свойства
    
    public List<IItem> Box { get; protected set; } = new List<IItem>();
    
    public IHotBar HotBar { get; }

    #endregion

    public Inventory()
    {
        HotBar = new HotBar.HotBar();
    }
    
}