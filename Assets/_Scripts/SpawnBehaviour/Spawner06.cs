using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    /// <summary>
    /// Spawn A circle of Bullets which move towards the player.
    /// </summary>
    public class Spawn06 : SpawnBehaviour {
        private float _radius;
        private float _degree;
        private int _cnt;
        private void Start() {
            Initialize();
            _radius = 0.5f;
            _cnt = 0;
        }

        private void FixedUpdate() {
            SetLife(60);
            if (SetTimerWithPeriod(1, 10)) {
                _cnt++;
                Spawn();
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 24; i++) {
                if(i % 4 != 0) continue;
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //some necessary calculations.
                _degree = (i * 15 + Timer[0] * 5) * Mathf.Deg2Rad;
                Vector3 direction =
                    new Vector3(Mathf.Cos(_degree), Mathf.Sin(_degree), 0f);

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.5f;
                TempProp.order = _cnt;
                TempProp.direction = direction;
                TempProp.worldPosition = _radius * direction + transform.position;
                TempProp.speed = 6f;
                var c = Color.white;
                c.a = 0.6f;
                TempProp.color = c;

                //initialize the bullet
                BulletManager.Manager.BulletInitialize(TempBullet, 27, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step07;
            }
        }
    }
}