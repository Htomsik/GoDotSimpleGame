using System;
using System.Collections.Generic;
using SimpleGame.Scripts.Models.Item;
using SimpleGame.Scripts.Models.Weapon;

namespace SimpleGame.Scripts.Models.HotBar;

public class HotBar : IHotBar
{
    #region Properties
    
    public int CurrentIndex { get; protected set; } = 0;

    public int ItemsCount { get; } = 9;
    public Action<int> SelectionChanged { get; set; }

    #endregion

    #region Физические свойства

    public List<IItem> Box { get; protected set; } = new List<IItem>();
    
    public IItem Current { get; protected set; }
    
    #endregion

    public HotBar()
    {
        Box.Add(new PunchWeapon());
        Box.Add(new PistolWeapon());
    }
    
    public void Select(int number)
    {
        var nextHotItem = CurrentIndex + number;

        if (Box.Count - 1 < nextHotItem || nextHotItem < 0) return;
        
        Current = Box[nextHotItem];
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
}