using StardewValley;
using StardewValley.Objects;
using System.Collections.Generic;

namespace StardewValleyMods.CategorizeChests.Framework
{
    class ChestData
    {
        private readonly IItemDataManager ItemDataManager;
        private readonly Chest Chest;

        private HashSet<ItemKey> ItemsEnabled = new HashSet<ItemKey>();

        public IEnumerable<ItemKey> AcceptedItemKinds
        {
            get { return ItemsEnabled; }

            set
            {
                ItemsEnabled.Clear();

                foreach (var itemKey in value)
                {
                    ItemsEnabled.Add(itemKey);
                }
            }
        }

        public ChestData(Chest chest, IItemDataManager itemDataManager)
        {
            Chest = chest;
            ItemDataManager = itemDataManager;
        }

        public void Accept(ItemKey itemKey)
        {
            if (!ItemsEnabled.Contains(itemKey))
                ItemsEnabled.Add(itemKey);
        }

        public void Reject(ItemKey itemKey)
        {
            if (ItemsEnabled.Contains(itemKey))
                ItemsEnabled.Remove(itemKey);
        }

        public void Toggle(ItemKey itemKey)
        {
            if (Accepts(itemKey))
                Reject(itemKey);
            else
                Accept(itemKey);
        }

        public bool Accepts(Item item)
        {
            return Accepts(ItemDataManager.GetKey(item));
        }

        public bool Accepts(ItemKey itemKey)
        {
            return ItemsEnabled.Contains(itemKey);
        }
    }
}