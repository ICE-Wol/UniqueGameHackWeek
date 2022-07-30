using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    /// <summary>
    /// Spawn A circle of Bullets which move towards the player.
    /// </summary>
    public class Spawn05 : SpawnBehaviour {
        public float dir;
        private float _radius;
        private float _degree;
        private void Start() {
            Initialize();
            _radius = 0.5f;
        }

        private void FixedUpdate() {
            SetLife(300);
            if(SetTimerWithPeriod(1,15)) Spawn();
        }

        protected override void Spawn() {
            for (int i = 0; i < 24; i++) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //some necessary calculations.
                _degree = (dir + i * 15 + Timer[0] * 5) * Mathf.Deg2Rad;
                Vector3 direction =
                    new Vector3(Mathf.Cos(_degree), Mathf.Sin(_degree), 0f);

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.2f;
                TempProp.direction = direction;
                TempProp.worldPosition = _radius * direction + transform.position;
                TempProp.speed = 6f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                var type = (i % 4 == 0) ? 26 : 25;
                if (type == 25) TempProp.radius /= 2;
                BulletManager.Manager.BulletInitialize(TempBullet, type, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step05;
            }
        }
    }
}