using System;
using System.Collections.Generic;
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

        private int _tileSize = 16;
        
        private int _wallTileSize = 2;
        
        private Vector2 _size;

        #endregion
        
        
        #region Chunk params

        private readonly int _chunkSize;

        public readonly List<Chunk> Chunks = new List<Chunk>();

        #endregion
        

        #region Room params

        private int _minRoomSize;
        
        private int _maxRoomSize;

        private int _numRooms;

        private List<Vector2> _roomsPos = new List<Vector2>();

        #endregion
        
        private Random _random = new Random();
        
        #endregion

        #region Constructors

        public Level(Vector2 size, int chunkSize = 20, int numRooms = 10, int minRoomSize = 5, int maxRoomSize = 16 )
        {
            _size = size;
            
            _minRoomSize = minRoomSize;
            _maxRoomSize = maxRoomSize;
            _numRooms = numRooms;

            _chunkSize = chunkSize;

            Floor.CellSize = new Vector2(_tileSize, _tileSize);
            Walls.CellSize = new Vector2(_tileSize, _tileSize);

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

        /// <summary>
        ///  Инициализация лвл-а
        /// </summary>
        private void GenerateLevel()
        {
            // Заливка стен и полов
            FillArea(Walls, Vector2.Zero, _size, 0);
            
            // Создание чанков
            CreateChunks();
            ShuffleChunks();

            GenerateRooms();
        }

        /// <summary>
        ///     Заливка стен и бекграунда
        /// </summary>
        /// <param name="tileMap"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        /// <param name="idTile"></param>
        private void FillArea(TileMap tileMap, Vector2 pos, Vector2 size, int idTile)
        {
            Vector2 tilePos = new Vector2();

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

        #region Chunks

        /// <summary>
        ///     Создание чанков
        /// </summary>
        private void CreateChunks()
        {
            Vector2 chunkPos = Vector2.Zero;
            
            for (var chunkY = 0; chunkY < _size.y / _chunkSize; chunkY++)
            {
                for (var chunkX = 0; chunkX < _size.x / _chunkSize; chunkX++)
                {
                    chunkPos.x = chunkX * chunkX;
                    chunkPos.y = chunkY * chunkY;
                    
                    Chunks.Add(new Chunk(chunkPos));
                }
            }
        }
        
        /// <summary>
        ///     Перетасовка чанков
        /// </summary>
        private void ShuffleChunks()
        {
            for (int chunkId = 0; chunkId < Chunks.Count - 1; chunkId++)
            {
                int randomChunkId = _random.Next(chunkId + 1);

                (Chunks[randomChunkId], Chunks[chunkId]) = (Chunks[chunkId], Chunks[randomChunkId]);
            }
        }

        private void GenerateRooms()
        {
            var limit = _numRooms <= Chunks.Count ? _numRooms : Chunks.Count;
            
            for (var count = 0; count < limit; count++)
            {
                var room = new Room(Walls, 
                    Chunks[count].Position, 
                    _chunkSize,
                    _minRoomSize,
                    _maxRoomSize, 
                    _tileSize,
                    _wallTileSize);
                
                Chunks[count].Room = room;
                
                FillArea(Floor, room.Position, new Vector2(room.Width, room.Height), 0);
                
                _roomsPos.Add(room.Center);
            }
        }

        #endregion
        
        #endregion
        
    }

    public class Room
    {
        #region Properties

        public Vector2 Position { get; private set; }
        
        public int Width { get; private set; }
        
        public int Height { get; private set; }
        
        public Vector2 Center { get; private set; }

        #endregion

        #region Fields

        private readonly Random _random = new Random();
        
        #endregion

        #region Constructors

        public Room(TileMap tileMap, Vector2 chunkPos, int chunkSize, int minSize, int maxSize, int tileSize, int wallSize)
        {
            Width = _random.Next(minSize, maxSize);
            Height = _random.Next(minSize, maxSize);
            
            Position = new Vector2(
                _random.Next((int)chunkPos.x + wallSize, (int)chunkPos.x + chunkSize - wallSize - Width), 
                _random.Next((int)chunkPos.y + wallSize, (int)chunkPos.y + chunkSize - wallSize - Width));

            Center = new Vector2(Position.x * tileSize + Width * tileSize /2, Position.y * tileSize * tileSize /2);

            GenerateRoom(tileMap);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Создание комнаты
        /// </summary>
        private void GenerateRoom(TileMap tileMap)
        {
            var tilePos = new Vector2();
            
            for (int cell = 0; cell < Height; cell++)
            {
                for (int row = 0; row < Width; row++)
                {
                    tilePos.x = Position.x + row;
                    tilePos.y = Position.x + cell;
                    
                    tileMap.SetCellv(tilePos, -1);
                }
            }
            
            tileMap.UpdateBitmaskRegion();
        }

        #endregion
    }

    public class Chunk
    {
        #region Properties

        public Vector2 Position { get; private set; }
        
        public Room Room { get; set; }

        #endregion

        #region Constructors

        public Chunk(Vector2 position)
        {
            Position = position;
        }

        #endregion
        
    }
}