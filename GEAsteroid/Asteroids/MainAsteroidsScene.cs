using GEAsteroid.Core;
using GEAsteroid.Engine.Extensions;
using GEAsteroid.Engine.Input;
using GEAsteroid.GameFraemwork;
using GEAsteroid.GameFraemwork.ActorComponents;
using GEAsteroid.GameFraemwork.Actors;
using GEAsteroid.GameFraemwork.Collisions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GEAsteroid.Asteroids
{
    public class MainAsteroidsScene : Scene
    {
        public Vector2 Dimensions
        {
            private set;
            get;
        }

        private Random random;

        public CollisionWorld collidersWorld
        {
            get;
        }
        private AsteroidsSpawner asteroidsMaster;
        private SpaceShip ship;
        private UFO ufo;
        private TextActor message;
        private TextActor score;
        private TextActor laserCounter;

        private float bigAsteroidSpawnIntreval = 3;
        private float degradeStepBySpawn = 0.05f;
        private float _time = 0;
        private float _timeInterval = 3;
        private float ufoSpawnInterval = 10;
        private float _ufoSpawnInterval;

        private int _score = 0;
        public int Score
        {
            set
            {
                _score = value;
                score.Text = _score.ToString();
            }
            get
            {
                return _score;
            }
        }
        public string LaserCounterText
        {
            set
            {
                laserCounter.Text = value;
            }
            get
            {
                return laserCounter.Text;
            }
        }
        public MainAsteroidsScene()
        {
            _ufoSpawnInterval = ufoSpawnInterval;
            _timeInterval = bigAsteroidSpawnIntreval;

            Dimensions = new Vector2(Application.Width, Application.Height);
            random = new Random();
            collidersWorld = new CollisionWorld();

            score = new TextActor(this);

            laserCounter = new TextActor(this);
            laserCounter.Transform.Position = new Vector2(0, 60);

            message = new TextActor(this);
            message.Text = "Press SPACE to start";
            message.Transform.Position = new Vector2(Dimensions.X / 2, Dimensions.Y / 2);

            ship = new SpaceShip(this);
            ship.Transform.Position = new Vector2(Dimensions.X / 2, Dimensions.Y / 2);

            ufo = new UFO(this, ship);
            ufo.SetActive(false);

            asteroidsMaster = new AsteroidsSpawner(this);

            AttachTeleporterToScene();
            CollectBullets();
            CollectRenders();

            InputEvents.KeyBoard_SPACE.KeyReleased += Start;
            InputEvents.KeyBoard_LCTRL.KeyReleased += SwitchActorsRender;

            collidersWorld.Start();
            message.SetActive(true);

            IsNeedDraw = true;
            IsNeedUpdate = true;
        }

        private List<PolygonRender> polygonRenders = new List<PolygonRender>();
        private List<SpriteRender> spriteRenders = new List<SpriteRender>();
        private void CollectRenders()
        {
            var actors = GetActorsOnScene();
            foreach (var x in actors)
            {
                var comp = x.GetComponents();
                foreach(var y in comp)
                {
                    var pl = y as PolygonRender;
                    var sp = y as SpriteRender;
                    if(pl != null)
                    {
                        polygonRenders.Add(pl);
                    }
                    if(sp != null)
                    {
                        spriteRenders.Add(sp);
                    }
                }
            }
            
        }
        private List<Bullet> bulletsOnScene = new List<Bullet>();
        private void CollectBullets()
        {
            var actors = GetActorsOnScene();
            foreach (var x in actors)
            {
                var sa = x as Bullet;
                if (sa != null)
                {
                    bulletsOnScene.Add(sa);
                }
            }
        }
        private void AttachTeleporterToScene()
        {
            var actors = GetActorsOnScene();
            foreach (var x in actors)
            {
                var sa = x as SceneActor;
                if (sa != null)
                {
                    sa.AddComponent(new SceneTeleporter(sa, Dimensions));
                }
            }
        }
        public bool isStarted
        {
            private set;
            get;
        }
        public void Stop()
        {
            if (isStarted)
            {
                message.Text = "GameOver.\nPress SPACE to restart";
                message.SetActive(true);
                isStarted = false;
                ship.Physic.Velocity = new Vector2(0, 0);
                ship.GunLaser.LaserRay.SetActive(false);
                ship.SetActive(false);
                TimerInvoker.CancelAll();
            }
        }
        public void Start()
        {
            if (!isStarted)
            {
                foreach(var x in bulletsOnScene)
                {
                    x.SetActive(false);
                }
                
                Score = 0;
                score.SetActive(true);
                laserCounter.SetActive(true);
                message.SetActive(false);

                ufo.SetActive(false);
                asteroidsMaster.Clear();
                ship.GunLaser.Refresh();

                _ufoSpawnInterval = ufoSpawnInterval;
                _timeInterval = bigAsteroidSpawnIntreval;
                isStarted = true;
                ship.Transform.Position = new Vector2(Dimensions.X / 2, Dimensions.Y / 2);
                ship.SetActive(true);
            }
        }
        private bool isSpritedRenderingMode = false;
        public void SwitchActorsRender()
        {
            isSpritedRenderingMode = !isSpritedRenderingMode;
            foreach (var x in spriteRenders)
            {
                x.SetActive(isSpritedRenderingMode);
            }
            foreach(var x in polygonRenders)
            {
                x.SetActive(!isSpritedRenderingMode);
            }
        }
        public override void Update()
        {
            base.Update();
            collidersWorld.UpdateTick();
            if (isStarted)
            {
                _time -= Engine.EngineCore.DeltaTime;
                _ufoSpawnInterval -= Engine.EngineCore.DeltaTime;
                if (_time < 0)
                {
                    asteroidsMaster.SpawnBigAsteroid();
                    _time = _timeInterval;
                    _timeInterval -= degradeStepBySpawn;
                }
                if(_ufoSpawnInterval < 0)
                {
                    _ufoSpawnInterval = ufoSpawnInterval;
                    if (!ufo.IsActive())
                    {
                        var pd = GenRandomPosDirCornerScrenPoint();
                        ufo.Transform.Position = pd.position;
                        ufo.SetActive(true);
                    }
                }
            }
        }
        public struct PosDir
        {
            public Vector2 position;
            public Vector2 direction;
        }
        public PosDir GenRandomPosDirCornerScrenPoint()
        {
            int yc = (int)Dimensions.Y - 1;
            int xc = (int)Dimensions.X - 1;
            PosDir posDir = new PosDir();
            int con = random.Next(0, 4);
            if (con == 0)
            {
                //left
                posDir.position = new Vector2(1, random.Next(0, yc));
                float x = random.NextSingle(0, 1);
                float y = random.NextSingle(-1, 1);
                posDir.direction = new Vector2(x, y);
            }
            if (con == 1)
            {
                //up
                posDir.position = new Vector2(random.Next(0, xc), 1);
                float x = random.NextSingle(-1, 1);
                float y = random.NextSingle(0, 1);
                posDir.direction = new Vector2(x, y);
            }
            if (con == 2)
            {
                //down
                posDir.position = new Vector2(random.Next(0, xc), yc);
                float x = random.NextSingle(-1, 1);
                float y = random.NextSingle(-1, 0);
                posDir.direction = new Vector2(x, y);
            }
            if (con == 3)
            {
                //right
                posDir.position = new Vector2(xc, random.Next(0, yc));
                float x = random.NextSingle(-1, 0);
                float y = random.NextSingle(-1, 1);
                posDir.direction = new Vector2(x, y);
            }
            posDir.direction = posDir.direction.Normalized();
            return posDir;
        }
        public override bool IsNeedDraw { get; }

        public override bool IsNeedUpdate { get; }
    }
}
