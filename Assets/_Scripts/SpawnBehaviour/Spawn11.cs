using System;
using _Scripts.BossBehaviour;
using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn11 : SpawnBehaviour {
        public int ord;
        private float _radius;
        private float _tarRad;
        private float _degree;
        private Vector3 _center;
        private void Start() {
            Initialize();
            _tarRad = 3.5f;
            _center = (GameManager.Manager.BottomRight + GameManager.Manager.TopLeft) / 2;
        }

        private void FixedUpdate() {
            _tarRad = 4f + 0.5f * Mathf.Cos(Mathf.Deg2Rad * GameManager.Manager.WorldTimer);
            _radius = Calc.Approach(_radius, _tarRad, 64f);
            _degree = ord * 60 + GameManager.Manager.WorldTimer;
            transform.position = _center + _radius * Calc.Direction(_degree);
            //Should be called in FixedUpdates.
            SetLife(36000);
            if (SetTimerWithPeriod(1, 3)) {
                Spawn();
                Spawn2();
            }

            if (SetTimerWithPeriod(2, 240)) {
                Spawn3();
            }
        }

        protected override void Spawn() {
            for (int i = 0; i < 3; i++) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.06f;
                TempProp.direction = Calc.Direction(_degree);
                TempProp.rotation = _degree - 90f;
                TempProp.worldPosition = transform.position;
                TempProp.speed = 3f + i;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 30, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step04;
            }
        }
        
        protected void Spawn2() {
            //pick a bullet out of the pool
            TempBullet = BulletManager.Manager.BulletActivate();
            if (TempBullet == null) Debug.Log("NullRef!!");

            //fill in the initial properties
            //**Remember to initialize it before use.**
            //fill in the index of the bullet
            TempProp.bullet = TempBullet;
            TempProp.radius = 0.07f;
            TempProp.direction = Calc.Direction(_degree);
            TempProp.worldPosition = transform.position;
            TempProp.speed = 5f;
            TempProp.color = Color.white;

            //initialize the bullet
            //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
            BulletManager.Manager.BulletInitialize(TempBullet, 32, true);
            BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

            //register the target event
            TempBullet.StepEvent += BulletManager.Manager.Step00;
        }

        protected void Spawn3() {
            var dir = Vector2.SignedAngle(Vector2.left,
                PlayerController.Controller.transform.position - transform.position);
            for (int i = 0; i < 12; i++) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.06f;
                TempProp.direction = Calc.Direction(dir + i * 30);
                TempProp.worldPosition = transform.position;
                TempProp.speed = 4f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 29, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step05;
            }
        }
    }
}