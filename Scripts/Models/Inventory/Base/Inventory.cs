using System.Collections.Generic;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory;

public class Inventory : IInventory
{
    #region Properties

    protected int BoxCount = 18;
    
    #endregion
    
    #region Физические свойства
    
    public List<IItem> Box { get; protected set; } = new List<IItem>();
    
    #endregion

    public Inventory()
    {
       
    }
}