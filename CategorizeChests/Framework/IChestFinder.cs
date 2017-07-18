using StardewValley.Objects;
using StardewValleyMods.CategorizeChests.Framework.Persistence;

namespace StardewValleyMods.CategorizeChests.Framework
{
    interface IChestFinder
    {
        Chest GetChestByAddress(ChestAddress address);
    }
}