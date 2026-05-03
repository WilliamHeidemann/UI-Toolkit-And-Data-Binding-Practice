using System.Linq;
using Configs;
using Models;
using UnityEngine;
using Views;

namespace Presenters
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private BagConfig[] _bagConfigs;
        [SerializeField] private ItemConfig[] _itemConfigs;

        private void Start()
        {
            Inventory inventory = new Inventory(_bagConfigs);
            InventoryPresenter presenter = new(_inventoryView, inventory);
            _inventoryView.Bind(inventory);
            
            _itemConfigs
                .Select(itemConfig => new Item(itemConfig))
                .ToList()
                .ForEach(item => inventory.TryAdd(item));
        }
    }
}