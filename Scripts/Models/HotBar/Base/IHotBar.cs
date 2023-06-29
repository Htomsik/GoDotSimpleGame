using System;
using SimpleGame.Scripts.Models.Inventory.Box;
using SimpleGame.Scripts.Models.Item;

namespace SimpleGame.Scripts.Models.HotBar;

public interface IHotBar
{
    public Box<IItem> Box { get; }
    
    public IItem Current { get; }
    
    public Action<int> SelectionChanged { get; set; }

    public int CurrentIndex { get; }

    public void Select(int number);
    
    public bool CanSwitch();
}