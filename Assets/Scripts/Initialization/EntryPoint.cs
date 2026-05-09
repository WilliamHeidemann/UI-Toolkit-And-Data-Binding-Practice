using System;
using System.Linq;
using Model;
using Model.Configs;
using Presentation;
using UnityEngine;
using View;

namespace ViewModel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private BagConfig[] _bagConfigs;
        [SerializeField] private ItemConfig[] _itemConfigs;
        private InventoryPresenter _inventoryPresenter;

        private void Start()
        {
            Inventory inventory = new();
            
            foreach (Bag bag in _bagConfigs.Select(config => new Bag(config)))
            {
                inventory.Add(bag);
            }
            
            foreach (Item item in _itemConfigs.Select(itemConfig => new Item(itemConfig)))
            {
                inventory.TryAdd(item);
            }

            _inventoryPresenter = new InventoryPresenter();

            _inventoryPresenter.Bind(_inventoryView, inventory);
        }

        private void OnDestroy()
        {
            _inventoryPresenter.Dispose();
        }
    }
}