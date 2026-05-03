using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [UxmlElement]
    public partial class BagElement : VisualElement
    {
        private Sprite _bagIcon;

        [UxmlAttribute]
        public Sprite BagIcon
        {
            get => _bagIcon;
            set
            {
                _bagIcon = value;
                ApplyBagIcon(_bagIcon);
            }
        }

        public BagElement()
        {
            RegisterCallback<AttachToPanelEvent>(_ => ApplyBagIcon(_bagIcon));
        }

        private void ApplyBagIcon(Sprite bagIcon)
        {
            if (bagIcon == null) return;
            VisualElement iconElement = this.Q<VisualElement>("Bag-icon");
            if (iconElement != null)
                iconElement.style.backgroundImage = new StyleBackground(bagIcon);
        }
    }
}