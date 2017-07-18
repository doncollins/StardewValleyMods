using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace StardewValleyMods.CategorizeChests.Framework.Persistence
{
    class ChestAddress
    {
        public ChestLocationType LocationType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LocationName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BuildingName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Vector2 Tile { get; set; }
    }
}