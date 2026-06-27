using System;
using System.Collections.Generic;
using Core;
using Model;
using Reflex.Attributes;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using View;
using View.UXMLElements;

namespace Presentation
{
    public class InventoryPresenter : IDisposable
    {
        private InventoryView _view;
        private Inventory _inventory;
        private readonly Dictionary<VisualElement, Slot> _slots = new();

        public InventoryPresenter(InventoryView view, Inventory inventory)
        {
            InputSystem.actions.FindAction("ToggleBag").performed += ToggleBag;
            InputSystem.actions.FindAction("ToggleAllBags").performed += ToggleAllBags;
            Bind(view, inventory);
        }
        
        
        // problem: the inventory has not yet been filled, so the foreach loops dont start and nothing gets bound
        public void Bind(InventoryView view, Inventory inventory)
        {
            _view = view;
            _inventory = inventory;
            
            foreach (Bag bag in inventory.Bags)
            {
                BagElement bagElement = view.AddBag();
                bagElement.BagIcon = bag.BagConfig.Icon;
                
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

        private void ToggleBag(InputAction.CallbackContext context)
        {
            if (!context.ReadValueAsButton()) return;
            
            string controlName = context.control.name;
            
            if (!int.TryParse(controlName, out int bagIndex)) return;

            bagIndex -= 1;
            
            if (!bagIndex.IsInRange(_inventory.BagCount)) return;
            
            _view.Toggle(bagIndex);
        }
        
        private void ToggleAllBags(InputAction.CallbackContext context) => _view.ToggleAll();

        private void OnDragPerformed(VisualElement source, VisualElement target)
        {
            Slot sourceSlot = _slots[source];
            Slot targetSlot = _slots[target];
            _inventory.Move(sourceSlot, targetSlot);
        }

        public void Dispose()
        {
            InputSystem.actions.FindAction("ToggleBag").performed -= ToggleBag;
            InputSystem.actions.FindAction("ToggleAllBags").performed -= ToggleAllBags;
        }
    }
}