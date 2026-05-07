using System.Collections.Generic;
using System.Linq;
using Core;
using Model.Configs;

namespace Model
{
    public class Inventory
    {
        public Inventory(BagConfig[] bagConfig)
        {
            Bags = bagConfig.Select(config => new Bag(config)).ToList();
        }

        public List<Bag> Bags { get; }
        public int BagCount => Bags.Count;
        public int SlotCount => Bags.Sum(bag => bag.SlotCount);
        
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