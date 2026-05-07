using Core;
using Unity.Properties;
using UnityEngine;

namespace Model
{
    public class Slot
    {
        [CreateProperty] public Option<Item> Item { get; private set; }
        public bool HasItem => Item.IsSome(out _);
        [CreateProperty] public Sprite Icon => Item.Match(item => item.Config.Icon, null);
        
        public void SetItem(Option<Item> newItem)
        {
            Item = newItem;
        }
    }
}