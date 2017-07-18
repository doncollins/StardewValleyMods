using Newtonsoft.Json;

namespace StardewValleyMods.CategorizeChests.Framework
{
    [JsonObject(MemberSerialization.OptIn)]
    class ItemKey
    {
        [JsonProperty] public readonly ItemType ItemType;

        [JsonProperty] public readonly int ObjectIndex;

        [JsonConstructor]
        public ItemKey(ItemType itemType, int parentSheetIndex)
        {
            ItemType = itemType;
            ObjectIndex = parentSheetIndex;
        }

        public override int GetHashCode()
        {
            return ((int) ItemType) * 10000 + ObjectIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemKey itemKey
                   && itemKey.ItemType == ItemType
                   && itemKey.ObjectIndex == ObjectIndex;
        }
    }
}