using System.Security.Cryptography;
using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn07 : SpawnBehaviour {
        private float _radius;
        private bool _isLeft;
        private int _cnt;
        private int _mul;
        private float _x;
        private void Start() {
            Initialize();
            _radius = 4f;
            _mul = (int)Random.Range(6f, 18f);
            _x = transform.position.x;
            if (_x >= GameManager.Manager.BottomRight.x) {
                _isLeft = true;
            } else if (_x <= GameManager.Manager.TopLeft.x) {
                _isLeft = false;
            } else {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate() {
            SetLife(240);
            if (SetTimerWithPeriod(1, 10)) {
                _cnt++;
                Spawn();
            }
        }

        protected override void Spawn() {
            var y = transform.position.y - _radius / 2f;
            for (int i = 1; i <= 8; i++) {
                var rand = Random.Range(-0.1f, 0.1f);
                var tag = i;
                if (tag >= 4) {
                    tag -= 4;
                    tag = 4 - tag;
                }
                tag *= _mul;
                if(tag <= _cnt) continue;
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");
                
                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.05f;
                TempProp.order = _cnt;
                TempProp.direction = _isLeft ? Vector3.right : Vector3.left;
                TempProp.rotation = _isLeft ? 90f : -90f;
                TempProp.worldPosition = new Vector3(_x, y + rand, 0f);
                TempProp.speed = 6f + Random.Range(-1f, 1f);
                TempProp.color = Color.white;

                //initialize the bullet
                BulletManager.Manager.BulletInitialize(TempBullet, 28, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);

                //register the target event
                TempBullet.StepEvent += BulletManager.Manager.Step00;

                y += _radius / 8f;
                if (_isLeft) _x -= 0.03f;
                else _x += 0.03f;
            }
        }
    }
}