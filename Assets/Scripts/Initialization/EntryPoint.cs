using System;
using System.Linq;
using Model;
using Model.Configs;
using Presentation;
using Reflex.Attributes;
using UnityEngine;
using View;

namespace ViewModel
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private InventoryView _inventoryView;
        [Inject] private Inventory _inventory;
        
        [SerializeField] private BagConfig[] _bagConfigs;
        [SerializeField] private ItemConfig[] _itemConfigs;
        
        private InventoryPresenter _inventoryPresenter;
        
        private void Start()
        {
            foreach (Bag bag in _bagConfigs.Select(config => new Bag(config)))
            {
                _inventory.Add(bag);
            }
            
            foreach (Item item in _itemConfigs.Select(itemConfig => new Item(itemConfig)))
            {
                _inventory.TryAdd(item);
            }

            _inventoryPresenter = new InventoryPresenter();

            _inventoryPresenter.Bind(_inventoryView, _inventory);
        }

        private void OnDestroy()
        {
            _inventoryPresenter.Dispose();
        }
    }
}