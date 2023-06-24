using Godot;

namespace SimpleGame.Scripts.Models.Dungeon
{
    public class Level
    {
        #region Properties

        public TileMap Walls { get; private set; } = new TileMap();

        public TileMap Floor { get; private set; } = new TileMap();
        
        #endregion

        #region Fields

        #region tileParams

        private const int TileSize = 16;

        private readonly Vector2 _size;

        #endregion

        public Vector2 Center { get; private set; }
        
        #endregion

        #region Constructors

        public Level(Vector2 size)
        {
            _size = size;
            
            Center = new Vector2(_size.x * TileSize/2, _size.y * TileSize /2);
            
            Floor.CellSize = new Vector2(TileSize, TileSize);
            Walls.CellSize = new Vector2(TileSize, TileSize);

            // Центрировние чтобы персонаж перекрывался тайлами уходя за их стенки
            Walls.CellTileOrigin = TileMap.TileOrigin.Center;
            Walls.CellYSort = true;

            Walls.TileSet = (TileSet)ResourceLoader.Load("res://Resources/TileSets/Wall.tres");
            Floor.TileSet = (TileSet)ResourceLoader.Load("res://Resources/TileSets/Floor.tres");
            
            GenerateLevel();
        }

        #endregion

        #region Methods

        #region Level
        
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

        
        #endregion
        
    }
}