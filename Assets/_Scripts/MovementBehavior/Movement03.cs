using _Scripts.BossBehaviour;
using _Scripts.SpawnBehaviour;
using UnityEngine;

namespace _Scripts.MovementBehavior {
    public class Movement03 : MovementBehaviour {
        public float dir;
        public bool isOut;

        private void FixedUpdate() {
            Movement();
        }
        private void OnDestroy() {
            if (isOut) {
                for (int j = 1; j < 6; j++) {
                    BossSt00.BSt00.CreateSpawner(() => {
                        var obj = new GameObject {
                            transform = {
                                position = transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn10>();
                        spawn.life = 100;
                        var movement = obj.AddComponent<Movement03>();
                        movement.dir = dir + j * 60f;
                        movement.isOut = false;
                    });
                }
            }
        }

        protected override void Movement() {
            var direction = Calc.Direction(dir);
            transform.position += 1f * Time.fixedDeltaTime * direction;
        }
    }
}