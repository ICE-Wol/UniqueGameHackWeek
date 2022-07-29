using System;
using System.Security.Cryptography;
using _Scripts.Bullet;
using _Scripts.MovementBehavior;
using _Scripts.SpawnBehaviour;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.BossBehaviour {
    public class BossSt00 : BossBehaviour
    {
        protected override void SwitchForm(int ordForm) {
            
        }

        protected override void SwitchCard(int ordForm, int ordCard) {
            int ord = ordForm * 10 + ordCard;
            StartTime = GameManager.Manager.WorldTimer;
            switch (ord) {
                case 10:
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn01>();
                    });
                    break;
                case 11:
                    var pos = this.transform.position;
                    pos.x -= 1f * 5;
                    //Create Boss Ring
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn05>();
                    });
                    
                    //Create Branches
                    for (int i = 0; i < 10; i++) {
                        pos.x += 1f;
                        pos.y = 8f + Random.Range(-2f, 0f);
                        CreateSpawner(300, () => {
                            var obj = new GameObject {
                                transform = {
                                    position = pos
                                }
                            };
                            var spawn = obj.AddComponent<Spawn02>();
                            var spawn2 = obj.AddComponent<Spawn03>();
                            var movement = obj.AddComponent<Movement01>();
                        });
                    }
                    break;
                case 20:
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn04>();
                    });
                    break;

            }
        }

        private void Start() {
            SwitchCard(1,1);
        }
    }
}
