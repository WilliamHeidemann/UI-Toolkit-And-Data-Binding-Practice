using System;
using System.Collections.Generic;
using System.Linq;
using Core;
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

        private void Start()
        {
            ToggleAll();
        }

        public BagElement AddBag()
        {
            VisualElement inventory = Root.Q<VisualElement>("Inventory");
            TemplateContainer template = _bag.CloneTree();
            template.name = "Bag";
            inventory.Add(template);
            return template.Q<BagElement>();
        }

        public Button AddSlot(BagElement bag)
        {
            Button slot = new();
            slot.AddToClassList("slot");

            VisualElement container = bag.Q<VisualElement>("Collection");
            container.Add(slot);

            return slot;
        }

        public void Toggle(int bagIndex)
        {
            List<VisualElement> bags = Root.Query<VisualElement>("Bag").ToList();

            if (!bagIndex.IsInRange(bags.Count)) return;

            bagIndex = bags.Count - 1 - bagIndex;

            VisualElement bag = bags[bagIndex];
            bag.Toggle();

            List<Button> bagButtons = Root.Query<Button>("BagButton").ToList();

            if (!bagIndex.IsInRange(bagButtons.Count)) return;

            Button bagButton = bagButtons[bagIndex];

            if (bag.IsDisplayFlex())
            {
                bagButton.AddToClassList("yellow-border");
            }
            else
            {
                bagButton.RemoveFromClassList("yellow-border");
            }
        }

        public void SetHighlightAllBagButtons(bool active)
        {
            List<Button> bagButtons = Root.Query<Button>("BagButton").ToList();

            foreach (Button bagButton in bagButtons)
            {
                if (active)
                {
                    bagButton.AddToClassList("yellow-border");
                }
                else
                {
                    bagButton.RemoveFromClassList("yellow-border");
                }
            }
        }

        public void ToggleAll()
        {
            List<VisualElement> bags = Root.Query<VisualElement>("Bag").ToList();

            if (bags.All(bag => bag.IsDisplayFlex()))
            {
                foreach (VisualElement bag in bags)
                {
                    bag.Hide();
                }
                SetHighlightAllBagButtons(false);
            }
            else
            {
                foreach (VisualElement bag in bags)
                {
                    bag.Show();
                }
                SetHighlightAllBagButtons(true);
            }
        }
    }
}