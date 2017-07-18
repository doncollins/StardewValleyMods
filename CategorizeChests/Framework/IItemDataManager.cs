using StardewValley;
using System.Collections.Generic;

namespace StardewValleyMods.CategorizeChests.Framework
{
    interface IItemDataManager
    {
        IDictionary<string, IEnumerable<ItemKey>> Categories { get; }

        ItemKey GetKey(Item item);
        Item    GetItem(ItemKey itemKey);
        bool    HasItem(ItemKey itemKey);
    }
}