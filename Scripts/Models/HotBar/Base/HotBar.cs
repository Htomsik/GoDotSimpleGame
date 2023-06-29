using System;
using SimpleGame.Scripts.Models.Inventory.Box;
using SimpleGame.Scripts.Models.Item;
using SimpleGame.Scripts.Models.Weapon;

namespace SimpleGame.Scripts.Models.HotBar;

public class HotBar : IHotBar
{
    #region Properties
    
    public int CurrentIndex { get; protected set; } = 0;

   
    public Action<int> SelectionChanged { get; set; }
    
    #endregion

    #region Физические свойства

    public Box<IItem> Box { get; protected set; } = new Box<IItem>();
    
    public IItem Current { get; protected set; }
    
    #endregion

    #region Methods

    public void Select(int number)
    {
        var nextHotItem = CurrentIndex + number;

        if (Box.MaxItemsCount - 1  < nextHotItem || nextHotItem < 0) return;

        if (Box.Count -1 >= nextHotItem)
        {
            Current = Box[nextHotItem];
        }
      
        CurrentIndex = nextHotItem;
        
        SelectionChanged?.Invoke(CurrentIndex);
    }

    public bool CanSwitch()
    {
        if (Current?.ItemType == ItemType.Weapon && Current is IWeapon weapon)
        {
            return weapon.AttackTimer?.TimeLeft == 0;
        }

        return true;
    }
    
    #endregion
}