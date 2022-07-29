using System;
using System.Security.Cryptography.X509Certificates;
using _Scripts.Bullet;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace _Scripts.SpawnBehaviour {
    public class Spawn02 : SpawnBehaviour {
        private void Start() {
            Initialize();
        }
        
        private void FixedUpdate() {
            //Should be called in FixedUpdates.
            SetLife(300);
            if (SetTimerWithPeriod(1, 10)) {
                Spawn();
            }
            if (SetTimerWithPeriod(2, 5)) {
                Spawn2();
            }
        }

        protected override void Spawn() {
            //if (_speed > 0f) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.1f;
                TempProp.direction = Vector3.zero;
                TempProp.worldPosition = transform.position;
                TempProp.speed = 10f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 23, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step00;
                if (Timer[0] >= 260) {
                    TempBullet.DestroyEvent += BulletManager.Manager.Destroy01;
                }
            //}
        }
        protected void Spawn2() {
            //if (_speed > 0f) {
            //pick a bullet out of the pool
            TempBullet = BulletManager.Manager.BulletActivate();

            //fill in the initial properties
            //**Remember to initialize it before use.**
            //fill in the index of the bullet
            TempProp.bullet = TempBullet;
            TempProp.radius = 0.05f;
            TempProp.direction = Vector3.zero;
            TempProp.worldPosition = transform.position;
            TempProp.speed = 20f;
            TempProp.color = Color.white;

            //initialize the bullet
            //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
            BulletManager.Manager.BulletInitialize(TempBullet, 22, true);
            BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

            //register the target event
            TempBullet.StepEvent += BulletManager.Manager.Step00;
            //}
        }
    }
}