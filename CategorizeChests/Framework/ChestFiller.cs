using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;

namespace StardewValleyMods.CategorizeChests.Framework
{
    class ChestFiller : IChestFiller
    {
        private readonly IChestDataManager ChestDataManager;
        private readonly IMonitor Monitor;

        public ChestFiller(IChestDataManager chestDataManager, IMonitor monitor)
        {
            ChestDataManager = chestDataManager;
            Monitor = monitor;
        }

        public void DumpItemsToChest(Chest chest)
        {
            var chestData = ChestDataManager.GetChestData(chest);

            bool shouldPlaySound = false;
            foreach (var item in GetInventoryItems())
            {
                try
                {
                    if (chestData.Accepts(item))
                    {
                        bool movedSome = TryPutItemInChest(chest, item);
                        if (movedSome) shouldPlaySound = true;
                    }
                }
                catch (ItemNotImplementedException)
                {
                    // it's fine, just skip it
                }
            }

            if (shouldPlaySound)
                Game1.playSound("dwop");
        }

        /// <summary>
        /// Attempt to move as much as possible of the given item stack into the chest.
        /// </summary>
        /// <param name="chest">The chest to put the items in</param>
        /// <param name="item">The items to put in the chest</param>
        /// <returns>True if at least some of the stack was moved into the chest</returns>
        private bool TryPutItemInChest(Chest chest, Item item)
        {
            bool movedSome = false;

            var candidates = chest.items
                .Where(i => i != null)
                .Where(i => i.canStackWith(item));

            foreach (var recipient in candidates)
            {
                var spaceLeft = recipient.maximumStackSize() - recipient.Stack;

                if (spaceLeft >= item.Stack)
                {
                    chest.grabItemFromInventory(item, Game1.player);
                    return true;
                }
                else if (spaceLeft > 0)
                {
                    item.Stack -= spaceLeft; //TODO: is there a better way to do this?
                    recipient.addToStack(spaceLeft);
                    movedSome = true;
                }
            }

            // if we got here, we should still have some left to put in

            if (ChestHasEmptySpaces(chest))
            {
                chest.grabItemFromInventory(item, Game1.player);
                return true;
            }

            return movedSome;
        }

        private bool ChestHasEmptySpaces(Chest chest)
        {
            return chest.items.Count < Chest.capacity
                   || chest.items.Any(i => i == null);
        }

        private IEnumerable<Item> GetInventoryItems()
        {
            return Game1.player.Items.Where(i => i != null).ToList();
        }
    }
}