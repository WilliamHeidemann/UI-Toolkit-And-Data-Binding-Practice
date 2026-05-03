using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Core;

namespace Models
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

        public void Move(SlotIdentifier source, SlotIdentifier target)
        {
            Bag sourceBag = Bags[source.BagIndex];
            Bag targetBag = Bags[target.BagIndex];
            
            Option<Item> sourceItem = sourceBag.GetItem(source.SlotIndex);
            Option<Item> targetItem = targetBag.GetItem(target.SlotIndex);
            
            Slot sourceSlot = sourceBag.Slots[source.SlotIndex];
            Slot targetSlot = targetBag.Slots[target.SlotIndex];
            
            sourceSlot.SetItem(targetItem);
            targetSlot.SetItem(sourceItem);
        }

        public Slot GetSlot(SlotIdentifier slotElementId)
        {
            return Bags[slotElementId.BagIndex].Slots[slotElementId.SlotIndex];
        }
    }
}