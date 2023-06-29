using System.Collections.Generic;
using SimpleGame.Scripts.Models.Inventory.HotBar;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory;

public interface IInventory
{
    public List<IItem> Box { get; }
    
    public IHotBar HotBar { get; }
}