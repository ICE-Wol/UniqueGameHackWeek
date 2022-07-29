using System;
using System.Security.Cryptography;
using _Scripts.Bullet;
using _Scripts.MovementBehavior;
using _Scripts.SpawnBehaviour;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.BossBehaviour {
    public class BossSt00 : BossBehaviour {
        public static BossSt00 BSt00;
        [SerializeField] private SpellProclamation sp;
        private void Start() {
            if (BSt00 == null) {
                BSt00 = this;
            }
            else {
                Destroy(this.gameObject);
            }
            CurrentForm = 0;
            CurrentCard = 1;
            isClaimed = false;
            TarPos = transform.position;
        }

        private void FixedUpdate() {
            transform.position
                = Calc.Approach(transform.position, TarPos, 16f * Vector3.one);

            if (!isClaimed) {
                ClaimCard(CurrentForm,CurrentCard);
                isClaimed = true;
            }
            SwitchCard(CurrentForm,CurrentCard);
        }
        
        
        
        protected override void ClaimCard(int ordForm, int ordCard) {
            StartTime = GameManager.Manager.WorldTimer;
            CurrentTime = timeCard[ordForm * 2 + ordCard];
            CurrentHealth = hpCard[ordForm * 2 + ordCard];
            if (ordCard != 0) sp.ResetWithName(nameCard[ordForm * 2 + ordCard]);
        }

        protected override void CheckCard(int ordForm, int ordCard) {
            int type = 0;
            if (CurrentHealth <= 0) type = 1;
            if (CurrentTime <= 0) type = 2;
            if (type > 0) {
                CurrentCard += 1;
                if (CurrentCard > 2) {
                    CurrentCard = 0;
                    CurrentForm += 1;
                }
                isClaimed = false;
            }
        }

        protected override void SwitchCard(int ordForm, int ordCard) {
            CurrentTime -= Time.fixedDeltaTime;
            int ord = ordForm * 10 + ordCard;
            switch (ord) {
                case 0:
                    CreateSpawner(360, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn01>();
                    });
                    break;
                case 1:
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
                case 10:
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn06>();
                        var spawn2 = obj.AddComponent<Spawn08>();
                    });
                    break;
                case 11:
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn09>();
                        var movement = obj.AddComponent<Movement02>();
                    });
                    break;
                case 20:
                    CreateSpawner(300, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        var spawn = obj.AddComponent<Spawn04>();
                        spawn.cnt = 1;
                    });
                    break;
            }
        }
        
    }
}
