using StardewValley.Objects;

namespace StardewValleyMods.CategorizeChests.Framework
{
    interface IChestDataManager
    {
        ChestData GetChestData(Chest chest);
    }
}