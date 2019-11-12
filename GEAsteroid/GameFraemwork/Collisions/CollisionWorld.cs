using System;
using System.Collections.Generic;

namespace GEAsteroid.GameFraemwork.Collisions
{
    public class CollisionWorld
    {
        private bool isStarted;
        public void Start()
        {
            isStarted = true;
        }
        public void Stop()
        {
            isStarted = false;
        }
        private List<PolygonColliderObject>[] collidersLayered;
        public int CollidersLayers
        {
            get;
        }
        public CollisionWorld()
        {
            CollidersLayers = 2;
            collidersLayered = new List<PolygonColliderObject>[CollidersLayers];
            for (int i = 0; i < 2; i++)
            {
                collidersLayered[i] = new List<PolygonColliderObject>();
            }
        }
        public int AddCollider(PolygonColliderObject collider, int layer)
        {
            collidersLayered[layer].Add(collider);
            return collidersLayered[layer].Count - 1;
        }
        public void RemoveCollider(int index, int layer)
        {
            collidersLayered[layer].RemoveAt(index);
        }
        public void RemoveCollider(PolygonColliderObject collider, int layer)
        {
            int index = collidersLayered[layer].FindIndex(x => x == collider);
            RemoveCollider(index, layer);
        }
        public void UpdateTick()
        {
            if (isStarted)
            {
                for (int i = 0; i < collidersLayered.Length; i++)
                {
                    for (int j = i + 1; j < collidersLayered.Length; j++)
                    {
                        var colliders1 = collidersLayered[i];
                        var colliders2 = collidersLayered[j];
                        for (int k = 0; k < colliders1.Count; k++)
                        {
                            if (colliders1[k].IsActive())
                            {
                                for (int l = 0; l < colliders2.Count; l++)
                                {
                                    if (colliders2[l].IsActive())
                                    {
                                        colliders1[k].PolygonCollision(colliders2[l]);
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
    }
}
