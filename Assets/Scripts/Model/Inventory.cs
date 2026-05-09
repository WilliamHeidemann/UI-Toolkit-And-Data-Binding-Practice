using System.Collections.Generic;
using System.Linq;
using Core;
using Model.Configs;

namespace Model
{
    public class Inventory
    {
        public List<Bag> Bags { get; } = new();
        public int BagCount => Bags.Count;
        public int SlotCount => Bags.Sum(bag => bag.SlotCount);
        public int OccupiedSlotCount => Bags.Sum(bag => bag.Slots.Count(slot => slot.HasItem));
        public void Add(Bag bag) => Bags.Add(bag);
        public bool TryAdd(Item item) => Bags.Any(bag => bag.TryAdd(item));

        public Option<Item> GetItem(SlotIdentifier slotIdentifier)
        {
            return Bags[slotIdentifier.BagIndex].GetItem(slotIdentifier.SlotIndex);
        }

        public void Move(Slot sourceSlot, Slot targetSlot)
        {
            Option<Item> sourceItem = sourceSlot.Item;
            Option<Item> targetItem = targetSlot.Item;
            
            sourceSlot.SetItem(targetItem);
            targetSlot.SetItem(sourceItem);
        }

        public Slot GetSlot(SlotIdentifier slotElementId)
        {
            return Bags[slotElementId.BagIndex].Slots[slotElementId.SlotIndex];
        }
    }
}