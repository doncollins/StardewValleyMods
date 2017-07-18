using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewValley.Characters;
using StardewValleyMods.Common;

namespace StardewValleyMods.LoveBubbles
{
    public class LoveBubblesMod : Mod
    {
        private bool HideWhenMilkable => Helper.ModRegistry.IsLoaded("BetterRanching");

        private TextureRegion Bubble;
        private TextureRegion Heart;

        private Config Config;

        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<Config>();

            if (Config.CheckForUpdates)
                new UpdateNotifier(Monitor).Check(ModManifest);

            Bubble = new TextureRegion(Game1.mouseCursors, new Rectangle(141, 465, 20, 24), zoom: true);
            Heart = new TextureRegion(Game1.mouseCursors, new Rectangle(226, 1811, 13, 12), zoom: true);
            
            GraphicsEvents.OnPreRenderHudEvent += (sender, e) => OnPreRenderHud();
        }

        private void OnPreRenderHud()
        {
            if (Game1.hasLoadedGame && Game1.currentLocation.isFarm)
                DrawAllBubbles(HideWhenMilkable);
        }

        private void DrawAllBubbles(bool hideWhenMilkable)
        {
            foreach (FarmAnimal animal in GetNearbyLivestock())
            {
                var suppress = HasProduct(animal) && hideWhenMilkable;

                if (!animal.wasPet && !suppress)
                    DrawBubble(Game1.spriteBatch, animal);
            }

            if (Config.IncludePets)
            {
                foreach (var pet in GetNearbyPets())
                {
                    if (!PetChecker.WasPetToday(pet))
                        DrawBubble(Game1.spriteBatch, pet);
                }
            }
        }

        private IEnumerable<Pet> GetNearbyPets()
        {
            return Game1.currentLocation.characters
                .Select(character => character as Pet)
                .Where(pet => pet != null);
        }

        private bool HasProduct(FarmAnimal animal)
        {
            if (animal.toolUsedForHarvest == "Milk Pail" || animal.toolUsedForHarvest == "Shears")
                return animal.currentProduce > 0;

            return false;
        }

        private IEnumerable<FarmAnimal> GetNearbyLivestock()
        {
            var location = Game1.currentLocation;

            if (location is AnimalHouse house)
                return house.animals.Values;
            else if (location is Farm farm)
                return farm.animals.Values;
            else
                return Enumerable.Empty<FarmAnimal>();
        }

        private void DrawBubble(SpriteBatch spriteBatch, FarmAnimal animal)
        {
            DrawBubbleAt(spriteBatch, new Vector2(
                animal.Position.X + animal.Sprite.getWidth() / 2,
                animal.Position.Y - (Game1.tileSize * 4) / 3 + GetBubbleOffset()
            ));
        }

        private void DrawBubble(SpriteBatch spriteBatch, Pet pet)
        {
            DrawBubbleAt(spriteBatch, new Vector2(
                pet.Position.X + pet.Sprite.getWidth() / 2,
                pet.Position.Y - (Game1.tileSize * 5) / 3 + GetBubbleOffset()
            ));
        }

        private void DrawBubbleAt(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Bubble,
                Game1.GlobalToLocal(Game1.viewport, position),
                (Color)(Color.White * 0.75f));

            var heartPosition = position
                                + new Vector2(Bubble.Width / 2, Bubble.Height / 2)
                                - new Vector2(Heart.Width / 2, Heart.Height / 2)
                                - new Vector2(0, 4);

            spriteBatch.Draw(Heart,
                Game1.GlobalToLocal(Game1.viewport, heartPosition),
                new Color(255, 128, 128, 192));
        }

        private float GetBubbleOffset()
        {
            return (float)(4.0 * Math.Round(Math.Sin(DateTime.Now.TimeOfDay.TotalMilliseconds / 250.0), 2));
        }
    }
}
