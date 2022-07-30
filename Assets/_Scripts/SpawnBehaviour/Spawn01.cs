using System;
using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn01 : SpawnBehaviour {
        private float _radius;
        private float _degree;
        private void Start() {
            Initialize();
            _radius = 2.6f;
        }

        private void FixedUpdate() {
            //Should be called in FixedUpdates.
            SetLife(210);
            if (SetTimerWithPeriod(1, 10)) {
                Spawn();
                _radius -= 3/8f;
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 24; i++) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //some necessary calculations.
                _degree = (i * 15 + Timer[0] * 1.5f) * Mathf.Deg2Rad;
                Vector3 direction =
                    new Vector3(Mathf.Cos(_degree), Mathf.Sin(_degree), 0f);

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.2f;
                TempProp.direction = direction;
                TempProp.worldPosition = _radius * direction + transform.position;
                TempProp.speed = 3f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 21, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step01;
                if (Timer[0] <= 120) {
                    TempBullet.DestroyEvent += BulletManager.Manager.Destroy01;
                }
            }
        }
    }
}