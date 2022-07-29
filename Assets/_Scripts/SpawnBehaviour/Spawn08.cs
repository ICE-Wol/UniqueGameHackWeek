using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn08 : SpawnBehaviour {
        private float _radius;
        private float _degree;
        private float _cnt;

        private void Start() {
            Initialize();
            _radius = Random.Range(0f, 360f);
            _radius = 3f;
            _cnt = 0;
        }

        private void FixedUpdate() {
            SetLife(280);
            if (SetTimerWithPeriod(1, 3)) {
                Spawn();
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 36; i++) {
                if (i % 4 != 0) continue;
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //some necessary calculations.
                if(_cnt == 0)
                    _degree = (_radius + i * 10 + Timer[0] * 5) * Mathf.Deg2Rad;
                else _degree = (_radius + i * 10 - Timer[0] * 5) * Mathf.Deg2Rad;
                Vector3 direction =
                    new Vector3(Mathf.Cos(_degree), Mathf.Sin(_degree), 0f);

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.1f;
                TempProp.direction = direction;
                TempProp.worldPosition = transform.position;
                TempProp.speed = 5f;
                var c = Color.white;
                c.a = 0.6f;
                TempProp.color = c;

                //initialize the bullet
                BulletManager.Manager.BulletInitialize(TempBullet, 29, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step08;
            }
        } 
    }
}
