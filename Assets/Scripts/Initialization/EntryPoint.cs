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

        private void Start()
        {
            Inventory inventory = new Inventory(_bagConfigs);
            InventoryPresenter presenter = new();
            presenter.Bind(_inventoryView, inventory);
            
            _itemConfigs
                .Select(itemConfig => new Item(itemConfig))
                .ToList()
                .ForEach(item => inventory.TryAdd(item));
        }
    }
}