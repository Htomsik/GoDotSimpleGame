using Godot;

namespace SimpleGame.Scripts.Models.Dungeon
{
    public class Level
    {
        #region Properties

        public TileMap Walls { get; private set; } = new TileMap();

        #endregion

        #region Fields

        private const int TileSize = 16;

        private readonly Vector2 _size;
        
        public Vector2 Center { get; private set; }
        
        public Vector2 Start { get; private set; }
        
        public Vector2 End { get; private set; }
        
        #endregion

        #region Constructors

        public Level(Vector2 size)
        {
            _size = size;
            
            Center = new Vector2(_size.x * TileSize/2, _size.y * TileSize /2);
            
            Start = new Vector2(_size.x + TileSize, _size.y * TileSize - TileSize * 2);
            
            End = new Vector2(_size.x * TileSize - TileSize * 2, _size.y * TileSize - TileSize * 2) ;
            
            Walls.CellSize = new Vector2(TileSize, TileSize);
            
            Walls.TileSet = (TileSet)ResourceLoader.Load("res://Resources/TileSets/Wall.tres");
            GenerateLevel();
        }

        #endregion

        #region Methods
        
        private void GenerateLevel()
        {
            FillArea(Walls, Vector2.Zero, _size, 0);
        }
        
        private void FillArea(TileMap tileMap, Vector2 pos, Vector2 size, int idTile)
        {
            var tilePos = new Vector2();

            for (var row = 0; row < size.y; row++)
            {
                for (var col = 0; col < size.x; col++)
                {
                    tilePos.x = pos.x + col;
                    tilePos.y = pos.y + row;
                    
                    tileMap.SetCellv(tilePos, idTile);
                }
            }
            
            tileMap.UpdateBitmaskRegion();
        }

        public void ConnectLevel(Node parent)
        {
            parent.AddChild(Walls);
        }
        
        #endregion
    }
}