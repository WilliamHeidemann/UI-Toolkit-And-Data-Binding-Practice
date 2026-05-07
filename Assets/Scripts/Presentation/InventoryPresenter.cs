using System.Collections.Generic;
using Core;
using Model;
using Unity.Properties;
using UnityEngine.UIElements;
using View;
using View.UXMLElements;

namespace Presentation
{
    public class InventoryPresenter
    {
        private Inventory _inventory;
        private readonly Dictionary<VisualElement, Slot> _slots = new();

        public void Bind(InventoryView view, Inventory inventory)
        {
            _inventory = inventory;
            
            foreach (Bag bag in inventory.Bags)
            {
                BagElement bagElement = view.AddBag();
                foreach (Slot slot in bag.Slots)
                {
                    Button slotElement = view.AddSlot(bagElement);
                    _slots[slotElement] = slot;
                    
                    DragManipulator dragManipulator = new(slotElement, view.Root);
                    dragManipulator.OnDropPerformed += OnDragPerformed;
                    slotElement.AddManipulator(dragManipulator);
                    
                    slotElement.SetBinding("style.backgroundImage", new DataBinding
                    {
                        dataSource = slot,
                        dataSourcePath = new PropertyPath(nameof(Slot.Icon))
                    });
                }
            }
        }

        private void OnDragPerformed(VisualElement source, VisualElement target)
        {
            Slot sourceSlot = _slots[source];
            Slot targetSlot = _slots[target];
            _inventory.Move(sourceSlot, targetSlot);
        }
    }
}