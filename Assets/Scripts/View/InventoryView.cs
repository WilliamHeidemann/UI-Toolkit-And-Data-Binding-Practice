using System;
using Core.Attributes;
using UnityEngine;
using UnityEngine.UIElements;
using View.UXMLElements;

namespace View
{
    public class InventoryView : MonoBehaviour
    {
        [GetComponent] [SerializeField] private UIDocument _inventory;
        [SerializeField] private VisualTreeAsset _bag;
        
        public VisualElement Root => _inventory.rootVisualElement;

        public BagElement AddBag()
        {
            VisualElement inventory = Root.Q<VisualElement>("Inventory");
            TemplateContainer template = _bag.CloneTree();
            inventory.Add(template);
            return template.Q<BagElement>();
        }

        public Button AddSlot(BagElement bag)
        {
            Button slot = new Button();
            slot.AddToClassList("slot");
            
            VisualElement container = bag.Q<VisualElement>("Collection");
            container.Add(slot);
            
            return slot;
        }
    }
}