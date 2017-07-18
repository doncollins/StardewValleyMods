using StardewValley.Characters;
using System;
using System.Reflection;

namespace StardewValleyMods.LoveBubbles
{
    static class PetChecker
    {
        public static bool WasPetToday(Pet pet)
        {
            try
            {
                // Yes, this is horrifying, but it's the only way we're getting at this information.
                var field = typeof(Pet).GetField("wasPetToday", BindingFlags.NonPublic | BindingFlags.Instance);
                return (bool) field.GetValue(pet);
            }
            catch (Exception ex)
            {
                // We're just going to assume true so that in the event that this madness goes wrong,
                // the player won't see a permanent heart showing up on their dog.
                return true;
            }
        }
    }
}
