using CourseWork.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Controllers
{
    public static class PhysicsController
    {
        public static readonly Dictionary<int, Action<MapEntity>> ChangeMapEntity = new Dictionary<int, Action<MapEntity>>()
            {
            {1, (MapEntity mapEntity) =>
            {
                mapEntity.type = 2;
            } },
            {2, (MapEntity mapEntity) =>
            {
                MapController.mapWalls[mapEntity.i, mapEntity.j] = 0;
                MapController.mapObjects.Remove(mapEntity);
                mapEntity = null;
            } },
            {3, (MapEntity mapEntity) =>
            {

            } }
        };
        public static bool IsCollide(Entity entity)
        {
            for (int i = 0; i < MapController.mapObjects.Count; i++)
            {
                var curObject = MapController.mapObjects[i];
                PointF delta = new PointF();
                delta.X = (entity.posX - curObject.position.X);
                delta.Y = (entity.posY - curObject.position.Y);

                if (Math.Abs(delta.X) < entity.size / 2 + curObject.size.Width / 2 && Math.Abs(delta.Y) < entity.size / 2 + curObject.size.Height / 2)
                {
                    if (delta.X < 0 && entity.dirXstatic == 1)
                    {
                        entity.posX -= entity.speedD;
                        entity.speedD = 0;
                        entity.speedA = 1;
                        entity.speedS = 1;
                        entity.speedW = 1;
                        return true;
                    }
                    else if (delta.Y < 0 && entity.dirYstatic == 1)
                    {
                        entity.posY -= entity.speedW;
                        entity.speedW = 0;
                        entity.speedD = 1;
                        entity.speedA = 1;
                        entity.speedS = 1;
                        return true;
                    }
                    else if (delta.X > 0 && entity.dirXstatic == -1)
                    {
                        entity.posX += entity.speedA;
                        entity.speedA = 0;
                        entity.speedS = 1;
                        entity.speedW = 1;
                        entity.speedD = 1;
                        return true;
                    }
                    else if (delta.Y > 0 && entity.dirYstatic == -1)
                    {
                        entity.posY += entity.speedS;
                        entity.speedS = 0;
                        entity.speedW = 1;
                        entity.speedD = 1;
                        entity.speedA = 1;
                        return true;
                    }
                }
                else
                {
                    entity.speedA = 1;
                    entity.speedS = 1;
                    entity.speedW = 1;
                    entity.speedD = 1;
                }
            }
            for (int i = 0; i < MapController.PlayersAndBots.Count; i++)
            {
                
                Entity curObject = MapController.PlayersAndBots[i];
                
                if (entity == curObject)
                {
                    entity.speedA = 1;
                    entity.speedS = 1;
                    entity.speedW = 1;
                    entity.speedD = 1;
                    continue;
                }
                PointF delta = new PointF();
                delta.X = (entity.posX - curObject.posX);
                delta.Y = (entity.posY - curObject.posY);

                if (Math.Abs(delta.X) < entity.size / 2 + curObject.size / 2 && Math.Abs(delta.Y) < entity.size / 2 + curObject.size / 2)
                {

                    if (delta.X < 0 && entity.dirXstatic == 1)
                    {
                        entity.speedD = 0;
                        entity.speedA = 1;
                        entity.speedS = 1;
                        entity.speedW = 1;
                        return true;
                    }
                    else if (delta.Y < 0 && entity.dirYstatic == 1)
                    {
                        entity.speedW = 0;
                        entity.speedD = 1;
                        entity.speedA = 1;
                        entity.speedS = 1;
                        return true;
                    }
                    else if (delta.X > 0 && entity.dirXstatic == -1)
                    {
                        entity.speedA = 0;
                        entity.speedS = 1;
                        entity.speedW = 1;
                        entity.speedD = 1;
                        return true;
                    }
                    else if (delta.Y > 0 && entity.dirYstatic == -1)
                    {
                        entity.speedS = 0;
                        entity.speedW = 1;
                        entity.speedD = 1;
                        entity.speedA = 1;
                        return true;
                    }
                }
                
            }
            return false;
        }

        public static bool BulletIsCollide(Bullet bullet)
        {
            // Здесь и не только я решил оптимизировать циклы, распараллелив их
            bool isConditionMet = false;
            MapEntity mapEntity = null;

            Parallel.For(0, MapController.mapObjects.Count, i =>
            {
                try
                {
                    MapEntity curObject = MapController.mapObjects[i];
                    PointF delta = new PointF();

                    delta.X = (bullet.posX - curObject.position.X);
                    delta.Y = (bullet.posY - curObject.position.Y);

                    if (Math.Abs(delta.X) < bullet.size / 2 + curObject.size.Width / 2 && Math.Abs(delta.Y) < bullet.size / 2 + curObject.size.Height / 2)
                    {
                        mapEntity = curObject;
                        isConditionMet = true;
                    }
                }
                catch
                {
                    isConditionMet = false;
                }
            });
            if (isConditionMet)
            {
                ChangeMapEntity[mapEntity.type](mapEntity);
            }

            Parallel.For(0, MapController.PlayersAndBots.Count, i =>
            {
                try
                {
                    Entity curObject = MapController.PlayersAndBots[i];
                    PointF delta = new PointF();
                    if (bullet.team != curObject.team)
                    {
                        delta.X = (bullet.posX - curObject.posX);
                        delta.Y = (bullet.posY - curObject.posY);

                        if (Math.Abs(delta.X) < bullet.size / 2 + curObject.size / 2 && Math.Abs(delta.Y) < bullet.size / 2 + curObject.size / 2)
                        {
                            if (curObject.type == 1)
                            {
                                curObject.IsAlive = false;
                                if(curObject.team == "Enemy")
                                {
                                    MapController.PlayersAndBots.Remove(curObject);
                                    Account.AccountTank--;
                                }
                                else
                                {
                                    Account.AccountLife--;
                                }
                            }

                            isConditionMet = true;
                        }
                    }
                }
                catch
                {
                    isConditionMet = true;
                }
            });

            return isConditionMet;
        }
    }
}
