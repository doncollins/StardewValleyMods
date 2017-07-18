using System;

namespace StardewValleyMods.CategorizeChests.Framework.Persistence
{
    class InvalidSaveDataException : Exception
    {
        public InvalidSaveDataException(string message) : base(message) {}
        public InvalidSaveDataException(string message, Exception inner) : base(message, inner) {}
    }
}