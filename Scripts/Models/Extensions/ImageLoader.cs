using Godot;


namespace SimpleGame.Scripts.Models.Extensions
{
    public static class ImageLoader
    {
        private static readonly Image Image = new Image();

        private static readonly ImageTexture ImageTexture = new ImageTexture();

        public static ImageTexture LoadTexture(string path, bool isPixel = false)
        {
            Image.Load(path);

            ImageTexture.CreateFromImage(Image, isPixel ? (uint)0 : (uint)7);

            return ImageTexture;
        }
    }
}