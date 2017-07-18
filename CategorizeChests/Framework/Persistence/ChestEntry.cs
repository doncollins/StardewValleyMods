using System.Collections.Generic;

namespace StardewValleyMods.CategorizeChests.Framework.Persistence
{
    class ChestEntry
    {
        public ChestAddress Address;
        public IEnumerable<ItemKey> AcceptedItemKinds;
    }
}