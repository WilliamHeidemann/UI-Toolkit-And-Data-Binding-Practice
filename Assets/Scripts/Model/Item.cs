using Model.Configs;

namespace Model
{
    public class Item
    {
        public ItemConfig Config { get; }

        public Item(ItemConfig config)
        {
            Config = config;
        }
    }
}