using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewValleyMods.CategorizeChests.Framework.Persistence
{
    class SaveData
    {
        public string Version;
        public IEnumerable<ChestEntry> ChestEntries;
    }
}