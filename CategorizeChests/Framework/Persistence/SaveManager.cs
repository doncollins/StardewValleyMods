using StardewModdingAPI;
using Newtonsoft.Json.Linq;
using System.IO;
using StardewValleyMods.CategorizeChests.Framework.Persistence.Legacy;

namespace StardewValleyMods.CategorizeChests.Framework.Persistence
{
    class SaveManager : ISaveManager
    {
        private readonly ISemanticVersion Version;
        private readonly IChestDataManager ChestDataManager;
        private readonly IChestFinder ChestFinder;
        private readonly IItemDataManager ItemDataManager;

        public SaveManager(ISemanticVersion version, IChestDataManager chestDataManager, IChestFinder chestFinder, IItemDataManager itemDataManager)
        {
            Version = version;
            ChestDataManager = chestDataManager;
            ChestFinder = chestFinder;
            ItemDataManager = itemDataManager;
        }

        public void Save(string path)
        {
            var saver = new Saver(Version, ChestDataManager);
            var json = saver.DumpData();

            File.WriteAllText(path, json); // TODO: (use SMAPI classes if possible!)
        }

        public void Load(string path)
        {
            var json = File.ReadAllText(path);
            var token = JToken.Parse(json);

            token = ConvertVersion(token);

            var loader = new Loader(ChestDataManager, ChestFinder, ItemDataManager);
            loader.LoadData(token);
        }

        private JToken ConvertVersion(JToken data)
        {
            var version = ReadVersionNumber(data);

            if (version.IsOlderThan("1.1.0"))
                data = new Version102Converter().Convert(data);

            return data;
        }

        private ISemanticVersion ReadVersionNumber(JToken token)
        {
            if (token is JObject jObject)
            {
                var versionString = jObject.Value<string>("Version");
                return new SemanticVersion(versionString);
            }
            else if (token is JArray)
            {
                return new SemanticVersion("1.0.2");
            }
            else
            {
                throw new InvalidSaveDataException("Cannot detect save data version");
            }
        }
    }
}