using Core;
using Model.Configs;
using UnityEngine;

namespace Model
{
    public class Bag
    {
        public BagConfig BagConfig { get; }
        public Slot[] Slots { get; }
        public int SlotCount => Slots.Length;

        public Bag(BagConfig bagConfig)
        {
            BagConfig = bagConfig;
            Slots = new Slot[bagConfig.Capacity];
            for (int i = 0; i < bagConfig.Capacity; i++)
            {
                Slot slot = new();
                Slots[i] = slot;
            }
        }

        public bool TryAdd(Item item)
        {
            foreach (Slot s in Slots)
            {
                if (s.HasItem)
                {
                    continue;
                }

                s.SetItem(Option<Item>.Some(item));
                return true;
            }

            return false;
        }

        public Option<Item> GetItem(int slotIndex)
        {
            if (!slotIndex.IsInRange(0, SlotCount))
            {
                Debug.LogWarning($"Slot index {slotIndex} is out of range.");
                return Option<Item>.None;
            }

            return Slots[slotIndex].Item;
        }
    }
}