using System;
using System.Collections.Generic;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.Inventory.HotBar;

public interface IHotBar
{
    public List<IItem> Box { get; }
    
    public IItem Current { get; }
    
    
    public int ItemsCount { get; }
    
    public Action<int> SelectionChanged { get; set; }

    public int CurrentIndex { get; }

    public void Select(int number);
    
    public bool CanSwitch();
}