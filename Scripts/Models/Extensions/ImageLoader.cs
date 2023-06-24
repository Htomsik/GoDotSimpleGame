using Godot;


namespace SimpleGame.Scripts.Models.Extensions
{
    public static class ImageLoader
    {
        public static ImageTexture LoadTexture(string path, bool isPixel = false)
        {
            var image = new Image();

            var imageTexture = new ImageTexture();
            
            image.Load(path);

            imageTexture.CreateFromImage(image, isPixel ? (uint)0 : (uint)7);

            return imageTexture;
        }


        public static void LoadAnimationFrames(this SpriteFrames spriteFrames, string name, string path,
            bool isLoop = false, bool isPixel = false, int frameStart = -1, int frameEnd = -1)

        {
            spriteFrames.AddAnimation(name);
            spriteFrames.SetAnimationLoop(name, isLoop);

            var image = LoadTexture(path, isPixel);

            var textureSize = new Vector2(image.GetWidth(), image.GetHeight());
            var spriteSize = new Vector2(image.GetHeight(), image.GetHeight());

            for (var column = 0; column < textureSize.x / spriteSize.x; column++)
            {
                for (var row = 0; row < textureSize.y / textureSize.y; row++)
                {
                    if (frameStart > 0 && frameEnd > 0)
                    {
                        var currentFrameId = column+1 * row+1;
                        
                        if (currentFrameId < frameStart || currentFrameId > frameEnd)
                        {
                            continue;
                        }
                    }
                    
                    var frame = new AtlasTexture();

                    frame.Atlas = image;
                    frame.Region = new Rect2(new Vector2(column, row) * spriteSize, spriteSize);

                    spriteFrames.AddFrame(name, frame, (int)(row * textureSize.x / spriteSize.x + column));
                }
            }
        }
    }
}