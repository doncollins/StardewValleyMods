using StardewValley.Objects;

namespace StardewValleyMods.CategorizeChests.Framework
{
    public interface IChestFiller
    {
        void DumpItemsToChest(Chest chest);
    }
}