using System;
using System.Collections.Generic;
using System.Linq;
using Attributes;
using Models;
using UI;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Views
{
    public class InventoryView : MonoBehaviour
    {
        [GetComponent] [SerializeField] private UIDocument _inventory;
        private List<BagElement> _bags;

        public event Action<SlotIdentifier, SlotIdentifier> OnRequestMove;
        
        private void OnEnable()
        {
            _bags = _inventory.rootVisualElement.Query<BagElement>().Build().ToList();

            foreach (BagElement bag in _bags)
            {
                List<SlotElement> slots = bag.Query<SlotElement>().Build().ToList();

                foreach (SlotElement slot in slots)
                {
                    int bagIndex = _bags.IndexOf(bag);
                    
                    int slotIndex = slots.IndexOf(slot);
                    
                    slot.Id = new SlotIdentifier(bagIndex, slotIndex);;
                    
                    DragManipulator dragManipulator = new(slot, _inventory.rootVisualElement);
                    
                    dragManipulator.OnDropPerformed += (source, target) => 
                    {
                        OnRequestMove?.Invoke(source.Id, target.Id);
                    };
                    
                    slot.AddManipulator(dragManipulator);
                }
            }
        }

        public void Bind(Inventory inventory)
        {
            List<SlotElement> slots = _inventory.rootVisualElement.Query<SlotElement>().Build().ToList();
            
            foreach (SlotElement slotElement in slots)
            {
                slotElement.SetBinding("style.backgroundImage", new DataBinding
                {
                    dataSource = inventory.GetSlot(slotElement.Id),
                    dataSourcePath = new PropertyPath("Icon"),
                });
            }
        }
    }
}