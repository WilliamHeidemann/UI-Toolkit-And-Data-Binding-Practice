using Model;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using View;
using Resolution = Reflex.Enums.Resolution;

namespace Initialization
{
    public class SceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private InventoryView _inventoryView;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(Inventory), Lifetime.Scoped, Resolution.Lazy);
            containerBuilder.RegisterValue(_inventoryView);
        }
    }
}
