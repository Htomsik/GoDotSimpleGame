using System;
using System.Collections.Generic;

namespace SimpleGame.Scripts.Models.Inventory.Box;

/// <summary>
///     Контейнер с уведомлением об изменени
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class Box<TValue> : List<TValue>
{
    public int MaxItemsCount { get; } = 9;
    
    public Action BoxChanged { get; set; }
    
    public new void Add(TValue value)
    {
        if (Count >= MaxItemsCount) return;
        
        base.Add(value);
        
        BoxChanged?.Invoke();
    }
    
    public new bool Remove(TValue value)
    {
        var isRemove = base.Remove(value);

        if (!isRemove) return false;
        
        BoxChanged?.Invoke();

        return true;
    }
}