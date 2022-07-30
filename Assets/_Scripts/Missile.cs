using System;
using _Scripts.BossBehaviour;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts {
    public class Missile : MonoBehaviour {
        public float speed;
        public int damage;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate() {
            transform.position += speed * Time.fixedDeltaTime * Vector3.up;
            if (Vector3.Distance(transform.position, BossSt00.BSt00.transform.position) <= 1f) {
                BossSt00.BSt00.TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
        

        private void OnBecameInvisible() {
            Destroy(this.gameObject);
        }
    }
}
