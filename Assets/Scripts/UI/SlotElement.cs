using Models;
using UnityEngine.UIElements;

namespace UI
{
    [UxmlElement]
    public partial class SlotElement : Button
    {
        public SlotIdentifier Id { get; set; }
    }
}