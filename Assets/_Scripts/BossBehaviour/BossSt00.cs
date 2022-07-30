using System;
using System.Collections.Generic;
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
        [SerializeField] private CircularHealthBar ch;
        [SerializeField] private GameObject normalBg;
        [SerializeField] private GameObject spellBg;
        [SerializeField] private GameObject stars;
        [SerializeField] private GameObject emission;
        //private List<GameObject> _stars;
        private GameObject[] _spawnerList;
        private void Start() {
            if (BSt00 == null) {
                BSt00 = this;
            }
            else {
                Destroy(this.gameObject);
            }
            CurrentForm = 0;
            CurrentCard = 0;
            isClaimed = false;
            isChangingCard = false;
            TarPos = transform.position;
            /*_stars = new List<GameObject>();
            for (int i = 0; i <= 2; i++) {
                var obj = Instantiate(stars);
                _stars.Add(obj);
            }*/
        }

        private void FixedUpdate() {
            transform.position
                = Calc.Approach(transform.position, TarPos, 16f * Vector3.one);
            if (!isClaimed) {
                ClaimCard(CurrentForm,CurrentCard);
                isClaimed = true;
            }
            SwitchCard(CurrentForm,CurrentCard);
            //if (Input.anyKey) CurrentHealth -= 10;
            //.Log(CurrentHealth + " " + CurrentMaxHealth);
            CheckCard();
            ch.Refresh((float)CurrentHealth / CurrentMaxHealth);
            //ch.transform.position = this.transform.position;
        }
        
        protected override void ClaimCard(int ordForm, int ordCard) {
            if (ordForm * 2 + ordCard >= 6) {
                sp.ResetWithName();
                normalBg.SetActive(true);
                spellBg.SetActive(false);
                return;
            }
            StartTime = GameManager.Manager.WorldTimer;
            CurrentTime = timeCard[ordForm * 2 + ordCard];
            CurrentHealth = hpCard[ordForm * 2 + ordCard];
            CurrentMaxHealth = hpCard[ordForm * 2 + ordCard];
            if (ordCard != 0) {
                sp.ResetWithName(nameCard[ordForm * 2 + ordCard]);
                normalBg.SetActive(false);
                spellBg.SetActive(true);
            }
            else {
                sp.ResetWithName();
                normalBg.SetActive(true);
                spellBg.SetActive(false);
            }
        }

        protected override void CheckCard() {
            int type = 0;
            if (CurrentHealth <= 0) type = 1;
            if (CurrentTime <= 0) type = 2;
            if (type > 0) {
                CurrentCard += 1;
                if (CurrentCard >= 2) {
                    CurrentCard = 0;
                    CurrentForm += 1;
                }
                isClaimed = false;
                isChangingCard = true;
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
                        obj.tag = "Spawner";
                        var spawn = obj.AddComponent<Spawn01>();
                    });
                    break;
                case 1:
                    var pos = this.transform.position;
                    pos.x -= 1f * 5;
                    //Create Boss Ring
                    CreateSpawner(600, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        obj.tag = "Spawner";
                        var spawn = obj.AddComponent<Spawn05>();
                        spawn.dir = Vector2.SignedAngle(Vector2.right,
                            PlayerController.Controller.transform.position - transform.position);
                    });
                    
                    //Create Branches
                    for (int i = 0; i < 10; i++) {
                        pos.x += 1f;
                        pos.y = 8f + Random.Range(-2f, 0f);
                        CreateSpawner(600, () => {
                            var obj = new GameObject {
                                transform = {
                                    position = pos
                                }
                            };
                            obj.tag = "Spawner";
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
                        obj.tag = "Spawner";
                        var spawn = obj.AddComponent<Spawn06>();
                        var spawn2 = obj.AddComponent<Spawn08>();
                    });
                    break;
                case 11:
                    CreateSpawner(600, () => {
                        var obj = new GameObject {
                            transform = {
                                position = this.transform.position
                            }
                        };
                        obj.tag = "Spawner";
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
                        obj.tag = "Spawner";
                        var spawn = obj.AddComponent<Spawn04>();
                        spawn.cnt = 1;
                    });
                    break;
                case 21:
                    for (int i = 0; i < 6; i++) {
                        CreateSpawner(600000, () => {
                            var obj = new GameObject {
                                transform = {
                                    position = this.transform.position
                                }
                            };
                            obj.tag = "Spawner";
                            var spawn = obj.AddComponent<Spawn11>();
                            spawn.ord = i;
                        });
                    }
                    CreateSpawner(600, () => {
                        var obj = new GameObject {
                            transform = {
                                position = (GameManager.Manager.BottomRight + GameManager.Manager.TopLeft) / 2
                            }
                        };
                        obj.tag = "Spawner";
                        var spawn = obj.AddComponent<Spawn12>();
                    });
                    break;
                case 31:
                    break;
            }
        }
        
    }
}
