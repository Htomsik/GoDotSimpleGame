using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleGame.Scripts.Models.Item;

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
    
    public bool Remove(int id)
    {
        if (id >= Count || id < 0)
            return false;
        
        this[id] = default;
        
        BoxChanged?.Invoke();

        return true;
    }
    
    public bool Change(int id, TValue item)
    {
        if (id < 0 || id >= MaxItemsCount)
        {
            Console.WriteLine($"{nameof(Box)}: invalid id");
            return false;
        }

        if (id >= Count)
        {
            Resize(id+1);
        }
        
        this[id] = item;
        
        BoxChanged?.Invoke();

        return true;
    }

    public void Resize(int newCount)
    {
        if (Count < MaxItemsCount && newCount > 0)
        {
           AddRange( Enumerable.Repeat<TValue>(default, newCount - Count));
        }
    }
}
