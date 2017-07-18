using Microsoft.Xna.Framework.Graphics;

namespace StardewValleyMods.CategorizeChests.Interface.Widgets
{
    class Stamp : Widget
    {
        private readonly TextureRegion TextureRegion;

        public Stamp(TextureRegion textureRegion)
        {
            TextureRegion = textureRegion;
            Width = TextureRegion.Width;
            Height = TextureRegion.Height;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(TextureRegion.Texture, TextureRegion.Region, GlobalPosition.X, GlobalPosition.Y,
                TextureRegion.Width, TextureRegion.Height);
        }
    }
}