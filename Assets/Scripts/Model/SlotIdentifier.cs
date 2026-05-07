namespace Model
{
    public class SlotIdentifier
    {
        public SlotIdentifier(int bagIndex, int slotIndex)
        {
            BagIndex = bagIndex;
            SlotIndex = slotIndex;
        }

        public int BagIndex { get; }
        public int SlotIndex { get; }
    }
}