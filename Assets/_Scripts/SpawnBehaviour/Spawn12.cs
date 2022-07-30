using System;
using System.Security.Cryptography.X509Certificates;
using _Scripts.Bullet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.SpawnBehaviour {
    public class Spawn12 : SpawnBehaviour {
        private float _degree;
        private float _rand;
        private void Start() {
            Initialize();
            _rand = Random.Range(0f, 360f);
        }

        private void FixedUpdate() {
            //Should be called in FixedUpdates.
            SetLife(180);
            if (SetTimerWithPeriod(1, 10)) {
                Spawn();
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 24; i++) {
                if (i % 4 != 0) continue;
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
                TempProp.worldPosition = transform.position;
                TempProp.speed = 2.5f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 21, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step01;
                TempBullet.DestroyEvent += BulletManager.Manager.Destroy04;
            }
        }
    }
}