using GEAsteroid.Core.Resources;
using GEAsteroid.Engine.Extensions;
using GEAsteroid.Engine.Data;
using GEAsteroid.GameFraemwork.Actors;
using System;
using System.Numerics;

namespace GEAsteroid.Asteroids
{
    public class AsteroidsSpawner : Actor
    {
        private struct AsteroidSpawnInfo
        {
            public Asteroid[] pull;
            public Polygon polygon;
            public float minForce;
            public float maxForce;
            public float minAngularForce;
            public float maxAngularForce;
            public Vector2 scaled;
            public SpriteInfo spriteInfo;
        }
       

        private Random random;
        private MainAsteroidsScene scene;

        private AsteroidSpawnInfo small;
        private AsteroidSpawnInfo mid;
        private AsteroidSpawnInfo big;
        public AsteroidsSpawner(MainAsteroidsScene scene) : base(scene)
        {

            this.scene = scene;
            random = new Random();

            big.polygon = Resources.Polygones.Asteroid1;
            big.scaled = new Vector2(1, 1);
            big.minForce = 60;
            big.maxForce = 80;
            big.minAngularForce = 150;
            big.maxAngularForce = 200;
            big.spriteInfo = Resources.Sprites.Asteroid1;

            mid.minForce = 135;
            mid.maxForce = 200;
            mid.minAngularForce = 200;
            mid.maxAngularForce = 250;
            mid.polygon = new Polygon(big.polygon);
            mid.scaled = new Vector2(0.5f, 0.5f);
            mid.polygon.Scale(mid.scaled);

            small.minForce = 250;
            small.maxForce = 300;
            small.minAngularForce = 300;
            small.maxAngularForce = 400;
            small.polygon = new Polygon(big.polygon);
            small.scaled = new Vector2(0.3f, 0.3f);
            small.polygon.Scale(small.scaled);


            big.pull = new Asteroid[15];
            for (int i = 0; i < big.pull.Length; i++)
            {
                big.pull[i] = new Asteroid(scene, Resources.Sprites.Asteroid1, big.polygon);
                var aster = big.pull[i];
                aster.Name = "BigAsteroid";
                big.pull[i].OnCollideBullet += () => CollidedBullt(aster);
            }
            mid.pull = new Asteroid[20];
            for (int i = 0; i < mid.pull.Length; i++)
            {
                mid.pull[i] = new Asteroid(scene, Resources.Sprites.Asteroid1, mid.polygon);
                var aster = mid.pull[i];
                aster.Name = "MidAsteroid";
                aster.SpriteRender.Scale = mid.scaled;
                var trans = aster.Transform;
                mid.pull[i].OnCollideBullet += () => CollidedBullt(aster);
            }
            small.pull = new Asteroid[25];
            for (int i = 0; i < small.pull.Length; i++)
            {
                small.pull[i] = new Asteroid(scene, Resources.Sprites.Asteroid1, small.polygon);
                var aster = small.pull[i];
                aster.SpriteRender.Scale = small.scaled;
                aster.Name = "SmallAsteroid";
                aster.OnCollideBullet += () => CollidedBullt(aster);
            }
            Clear();
        }
        private void CollidedBullt(Asteroid asteroid)
        {
            if (asteroid.Name == "BigAsteroid")
            {
                scene.Score += 1;
                SpawnMidlleAsteroid(asteroid.Transform.Position);
                SpawnMidlleAsteroid(asteroid.Transform.Position);
            }
            if (asteroid.Name == "MidAsteroid")
            {
                scene.Score += 2;
                SpawnSmallAsteroid(asteroid.Transform.Position);
                SpawnSmallAsteroid(asteroid.Transform.Position);
            }
            if (asteroid.Name == "SmallAsteroid")
            {
                scene.Score += 3;
            }
            asteroid.SetActive(false);
        }
        public void Clear()
        {
            foreach (var x in small.pull)
            {
                x.SetActive(false);
            }
            foreach (var x in mid.pull)
            {
                x.SetActive(false);
            }
            foreach (var x in big.pull)
            {
                x.SetActive(false);
            }
        }
        public void SpawnSmallAsteroid(Vector2 position)
        {
            var dir = RandomDirections();
            SpawnAsteroid(small, position, dir);
        }
        public void SpawnMidlleAsteroid(Vector2 position)
        {
            var dir = RandomDirections();
            SpawnAsteroid(mid, position, dir);
        }
        public void SpawnBigAsteroid()
        {
            var posdir = scene.GenRandomPosDirCornerScrenPoint();
            SpawnAsteroid(big, posdir.position, posdir.direction);
        }
        private void SpawnAsteroid(AsteroidSpawnInfo spwninf, Vector2 position, Vector2 direction)
        {
            if (!scene.isStarted)
            {
                return;
            }
            var ast = FindFree(spwninf.pull);
            if (ast != null)
            {
                ast.Transform.Position = position;
                ast.Physic.Velocity = direction * random.NextSingle(spwninf.minForce, spwninf.maxForce);
                int sgn = random.Next(0, 1) == 0 ? -1 : 1;
                ast.Physic.AngularSpeed = (sgn * random.NextSingle(spwninf.minAngularForce, spwninf.maxAngularForce));
                ast.SetActive(true);
            }
        }
        private Vector2 RandomDirections()
        {
            float r1 = random.NextSingle(-1, 1);
            float r2 = random.NextSingle(-1, 1);
            var vec = new Vector2(r1, r2);
            vec.Normalized();
            return vec;
        }
        private Asteroid FindFree(Asteroid[] ar)
        {
            foreach (var x in ar)
            {
                if (!x.IsActive())
                {
                    return x;
                }
            }
            return null;
        }
    }
}
