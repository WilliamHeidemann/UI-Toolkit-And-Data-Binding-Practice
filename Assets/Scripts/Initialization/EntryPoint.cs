using System.Linq;
using Model;
using Model.Configs;
using Reflex.Attributes;
using UnityEngine;

namespace ViewModel
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private BagConfig[] _bagConfigs;
        [SerializeField] private ItemConfig[] _itemConfigs;
        
        [Inject]
        private void FillInventory(Inventory inventory)
        {
            foreach (Bag bag in _bagConfigs.Select(config => new Bag(config)))
            {
                inventory.Add(bag);
            }
            
            foreach (Item item in _itemConfigs.Select(itemConfig => new Item(itemConfig)))
            {
                inventory.TryAdd(item);
            }
        }
    }
}