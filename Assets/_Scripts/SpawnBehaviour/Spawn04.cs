using System;
using _Scripts.Bullet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.SpawnBehaviour {
    public class Spawn04 : SpawnBehaviour {
        private float _radius;
        private float _degree;
        private float _rand;
        public int cnt;
        private void Start() {
            Initialize();
            _radius = 2.2f;
            _rand = Random.Range(0, 360f);
        }

        private void FixedUpdate() {
            //Should be called in FixedUpdates.
            SetLife(150);
            if (SetTimerWithPeriod(1, 10)) {
                Spawn();
                _radius -= 3/8f;
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 24; i++) {
                if(i % 12 > 6) continue;
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //some necessary calculations.
                if(cnt % 2 == 0)
                    _degree = (_rand + i * 15 + Timer[0]) * Mathf.Deg2Rad;
                else _degree = (_rand + i * 15 - Timer[0]) * Mathf.Deg2Rad;
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
                TempBullet.DestroyEvent += BulletManager.Manager.Destroy02;
            }
        }
    }
}