using System.Buffers.Text;
using _Scripts.BossBehaviour;
using _Scripts.Bullet;
using _Scripts.MovementBehavior;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public class Spawn09 : SpawnBehaviour {
        private Bullet.Bullet[] _bullets;
        private float _radius;
        private float _speed;
        private bool _spawned;
        private void Start() {
            Initialize();
            _bullets = new Bullet.Bullet[6];
        }

        private void FixedUpdate() {
            if(!_spawned) {
                Spawn();
                _spawned = true;
            }
            _radius = Calc.Approach(_radius, 2.5f, 32f);
            _speed = Calc.Approach(_speed, 720f, 32f);
            Control();
            if (Calc.Equal(_speed, 720f, 10f)) Destroy();
        }

        private void Control() {
            for (int i = 0; i < 6; i++) {
                var pos = transform.position;
                var offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * ((i * 60f) + _speed)),
                    Mathf.Sin(Mathf.Deg2Rad * ((i * 60f) + _speed)));
                _bullets[i].transform.position = pos + _radius * offset;

            }
        }
        
        private void Destroy() {
            for (int i = 0; i < 6; i++) {
                var pos = _bullets[i].transform.position;
                var dir = Vector2.SignedAngle(Vector2.left, transform.position - pos);
                //var dir = Vector2.SignedAngle(Vector2.left, PlayerController.Controller.transform.position - pos);
                for (int j = 0; j <= 6; j++) {
                    BossSt00.BSt00.CreateSpawner(() => {
                        var obj = new GameObject {
                            transform = {
                                position = _bullets[i].transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn10>();
                        spawn.dir = dir + j * 60f;
                        spawn.life = 150;
                        var movement = obj.AddComponent<Movement03>();
                        movement.dir = dir + j * 60f;
                        movement.isOut = j is > 1 and < 5;
                        //Debug.Log(movement.dir);
                    });
                }
                _bullets[i].SetState(BulletStates.Destroying);
                _bullets[i] = null;
            }
            Destroy(this.gameObject);
        }

        protected override void Spawn() {
            _radius = 0;
            _speed = 0;
            for (int i = 0; i < 6; i++) {
                //pick a bullet out of the pool
                TempBullet = BulletManager.Manager.BulletActivate();
                if (TempBullet == null) Debug.Log("NullRef!!");

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                TempProp.bullet = TempBullet;
                TempProp.radius = 0.2f;
                TempProp.order = i;
                TempProp.worldPosition = transform.position;
                var c = Color.white;
                c.a = 0.6f;
                TempProp.color = c;

                //initialize the bullet
                BulletManager.Manager.BulletInitialize(TempBullet, 27, true);
                BulletManager.Manager.BulletRefresh(TempBullet, TempProp);
                _bullets[i] = TempBullet;
            }
        } 
    }
}
