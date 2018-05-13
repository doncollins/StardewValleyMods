﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

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
            
            Bubble = new TextureRegion(Game1.mouseCursors, new Rectangle(141, 465, 20, 24), zoom: true);
            Heart = new TextureRegion(Game1.mouseCursors, new Rectangle(226, 1811, 13, 12), zoom: true);
            
            GraphicsEvents.OnPreRenderHudEvent += (sender, e) => OnPreRenderHud();
        }

        private void OnPreRenderHud()
        {
            if (Game1.hasLoadedGame && Game1.currentLocation.IsFarm)
                DrawAllBubbles(HideWhenMilkable);
        }

        private void DrawAllBubbles(bool hideWhenMilkable)
        {
            foreach (FarmAnimal animal in GetNearbyAnimals())
            {
                var suppress = HasProduct(animal) && hideWhenMilkable;

                if (!animal.wasPet.Value && !suppress)
                    DrawBubble(Game1.spriteBatch, animal);
            }
        }

        private bool HasProduct(FarmAnimal animal)
        {
            
            if (animal.toolUsedForHarvest.Value == "Milk Pail" || animal.toolUsedForHarvest.Value == "Shears")
                return animal.currentProduce.Value > 0;

            return false;
        }

        private IEnumerable<FarmAnimal> GetNearbyAnimals()
        {
            switch (Game1.currentLocation)
            {
                case AnimalHouse house:
                    return house.animals.Values;
         
                case Farm farm:
                    return farm.animals.Values;
                
                default:
                    return Enumerable.Empty<FarmAnimal>();
            }
        }

        private void DrawBubble(SpriteBatch spriteBatch, FarmAnimal animal)
        {
            var bubblePosition = new Vector2(
                animal.Position.X + animal.Sprite.getWidth() / 2,
                animal.Position.Y - (Game1.tileSize * 4) / 3 + GetBubbleOffset());

            spriteBatch.Draw(Bubble,
                Game1.GlobalToLocal(Game1.viewport, bubblePosition),
                (Color)(Color.White * 0.75f));

            var heartPosition = bubblePosition
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
