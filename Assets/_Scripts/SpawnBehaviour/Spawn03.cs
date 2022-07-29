using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn03 : SpawnBehaviour{
        private void Start() {
            Initialize();
        }
        
        private void FixedUpdate() {
            //Should be called in FixedUpdates.
            SetLife(300);
            if (SetTimerWithPeriod(200,1, 10)) {
                Spawn();
            }
        }
        protected override void Spawn() {
            var degree = Random.Range(0f, 360f);
            for (int i = 0; i < 3; i++) {
                TempBullet = BulletManager.Manager.BulletActivate();
                
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.05f;
                TempProp.direction = Calc.Direction(degree + i * 60f);
                TempProp.rotation = degree + i * 60f + 90f;
                TempProp.worldPosition = transform.position;
                TempProp.speed = 0.3f;
                TempProp.color = Color.white;

                //initialize the bullet
                //BulletManager.Manager.BulletInitialize(_tempBullet, 21,true);
                BulletManager.Manager.BulletInitialize(TempBullet, 24, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step04;
            }
        }
    }
}