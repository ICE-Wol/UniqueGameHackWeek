using System;
using System.Buffers.Text;
using _Scripts.Bullet;
using UnityEditor.UIElements;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn10 : SpawnBehaviour {
        public float dir;
        public int life;
        private bool _spawned;
        
        private void Start() {
            Initialize();
        }

        private void FixedUpdate() {
            SetLife(life);
            if(SetTimerWithPeriod(1,1,90))
                Spawn();
            if(SetTimerWithPeriod(2,10))
                Spawn2();
        }
        

        protected override void Spawn() {
            for (int i = -1; i <= 1; i += 2) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.05f;
                TempProp.speed = 7f;
                TempProp.direction = Calc.Direction(dir + i * 60);
                TempProp.worldPosition = transform.position;
                TempProp.color = Color.white;

                //initialize the bullet
                BulletManager.Manager.BulletInitialize(TempBullet, 29, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                if(life > 100) TempBullet.StepEvent += BulletManager.Manager.Step09;
                else TempBullet.StepEvent += BulletManager.Manager.Step00;
            }

        } 
        private void Spawn2() {
            //pick a bullet out of the pool
            TempBullet = BulletManager.Manager.BulletActivate();
            if (TempBullet == null) Debug.Log("NullRef!!");

            //fill in the initial properties
            //**Remember to initialize it before use.**
            //fill in the index of the bullet
            TempProp.bullet = TempBullet;
            TempProp.radius = 0.05f;
            TempProp.speed = 6f;
            TempProp.worldPosition = transform.position;
            TempProp.color = Color.white;

            //initialize the bullet
            BulletManager.Manager.BulletInitialize(TempBullet, 22, true);
            BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

            TempBullet.StepEvent += BulletManager.Manager.Step00;
        } 
    }
}