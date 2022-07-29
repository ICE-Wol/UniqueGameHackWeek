using System;
using _Scripts.Bullet;
using _Scripts.SpawnBehaviour;
using UnityEngine;

namespace _Scripts.BossBehaviour {
    public class BossSt00 : BossBehaviour
    {
        protected override void SwitchForm(int ordForm) {
            
        }

        protected override void SwitchCard(int ordForm, int ordCard) {
            StartTime = GameManager.Manager.WorldTimer;
            CreateSpawner(300, () => {
                var obj = new GameObject();
                obj.transform.position = this.transform.position;
                var spawn = obj.AddComponent<Spawn01>();
            });
        }

        private void Start() {
            SwitchCard(0,0);
        }
    }
}
