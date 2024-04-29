using CourseWork.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Controllers
{
    public static class MapController
    {
        public const int mapHeight = 20;
        public const int mapWidth = 20;
        public static int cellSize = 32;
        public static int[,] mapFloor = new int[mapHeight, mapWidth];
        public static int[,] mapWalls = new int[mapHeight, mapWidth];
        public static Image spriteSheetFloor;
        public static Image spriteSheetWalls;
        public static List<MapEntity> mapObjects = new List<MapEntity>();
        public static List<Entity> PlayersAndBots = new List<Entity>();
        public static readonly Dictionary<int, Action<Graphics, MapEntity>> DrawEntity = new Dictionary<int, Action<Graphics, MapEntity>>()
            {
            {1, (Graphics g, MapEntity mapEntity) =>
            {
                g.DrawImage(spriteSheetWalls, new Rectangle(new Point(mapEntity.j * cellSize, mapEntity.i * cellSize), new Size(cellSize, cellSize)), 0, 0,
                            cellSize, cellSize, GraphicsUnit.Pixel);
            } },
            {2, (Graphics g, MapEntity mapEntity) =>
            {
                g.DrawImage(spriteSheetWalls, new Rectangle(new Point(mapEntity.j * cellSize, mapEntity.i * cellSize), new Size(cellSize, cellSize)), cellSize, 0,
                            cellSize, cellSize, GraphicsUnit.Pixel);
            } },
            {3, (Graphics g, MapEntity mapEntity) =>
            {
                g.DrawImage(spriteSheetWalls, new Rectangle(new Point(mapEntity.j * cellSize, mapEntity.i * cellSize), new Size(cellSize, cellSize)), 2*cellSize, 0,
                            cellSize, cellSize, GraphicsUnit.Pixel);
            } }
        };


        public static void Init()
        {
            //mapFloor = GetMapFloor();
            mapWalls = GetMapWalls();
            SeedMap();
            spriteSheetFloor = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\Floor.png"));
            spriteSheetWalls = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "Sprites\\Walls.png"));
        }

        public static int[,] GetMapFloor()
        {
            return new int[,]{
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };
        }
        public static int[,] GetMapWalls()
        {
            return new int[,]{
                { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,1,1,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,1,1,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,3 },
                { 3,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,1,1,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,1,1,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3 },
                { 3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3 }
            };
        }

        public static void DrawMap(Graphics g)
        {
            DrawFloor(g);
            DrawMapObjects(g);
        }

        public static void DrawFloor(Graphics g)
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    g.DrawImage(spriteSheetFloor, new Rectangle(new Point(j * cellSize, i * cellSize), new Size(cellSize, cellSize)), 0, 0,
                        cellSize, cellSize, GraphicsUnit.Pixel);
                }
            }
        }

        public static void SeedMap()
        {
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (mapWalls[i, j] == 1)
                    {
                        MapEntity mapEntity = new MapEntity(new PointF(j * cellSize + cellSize / 2, i * cellSize + cellSize /2), new Size(cellSize, cellSize), i, j, 1);
                        mapObjects.Add(mapEntity);
                    }
                    else if (mapWalls[i, j] == 2)
                    {
                        MapEntity mapEntity = new MapEntity(new PointF(j * cellSize + cellSize / 2, i * cellSize + cellSize / 2), new Size(cellSize, cellSize), i, j, 2);
                        mapObjects.Add(mapEntity);
                    }
                    else if (mapWalls[i, j] == 3)
                    {
                        MapEntity mapEntity = new MapEntity(new PointF(j * cellSize + cellSize / 2, i * cellSize + cellSize / 2), new Size(cellSize, cellSize), i, j, 3);
                        mapObjects.Add(mapEntity);
                    }
                }
            }
        }

        public static void DrawMapObjects(Graphics g)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                MapEntity mapEntity = mapObjects[i];
                DrawEntity[mapEntity.type](g, mapEntity);
            }
        }

        public static int GetWidth()
        {
            return cellSize * mapWidth + 300;
        }

        public static int GetHeight()
        {
            return cellSize * mapHeight + 38;
        }
    }
}
