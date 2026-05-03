using Core;
using Models;
using Views;

namespace Presenters
{
    public class InventoryPresenter
    {
        private readonly InventoryView _view;
        private readonly Inventory _inventory;

        private Option<SlotIdentifier> _slotClicked;

        public InventoryPresenter(InventoryView view, Inventory inventory)
        {
            _view = view;
            
            _inventory = inventory;
            
            _view.OnRequestMove += HandleMoveRequest;
        }

        private void HandleMoveRequest(SlotIdentifier from, SlotIdentifier to)
        {
            _inventory.Move(from, to);
        }
    }
}