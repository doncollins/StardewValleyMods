using Microsoft.Xna.Framework.Graphics;

namespace StardewValleyMods.CategorizeChests.Interface.Widgets
{
    class SpriteButton : Button
    {
        private readonly TextureRegion TextureRegion;

        public SpriteButton(TextureRegion textureRegion)
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