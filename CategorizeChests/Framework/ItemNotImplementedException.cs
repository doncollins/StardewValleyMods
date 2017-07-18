using StardewValley;
using System;

namespace StardewValleyMods.CategorizeChests.Framework
{
    class ItemNotImplementedException : Exception
    {
        public ItemNotImplementedException(Item item)
            : base($"Chest categorization for item named {item.Name} is not implemented")
        {
        }
    }
}