using System.Security.Cryptography;
using UnityEngine;

namespace _Scripts.MovementBehavior {
    public class Movement02 : MovementBehaviour {
        public float dir;
        private Vector3 _rand;
        private void Start() {
            _rand = Vector3.zero;
            _rand.x = Random.Range(64f, 96f);
            _rand.y = Random.Range(64f, 96f);
            _rand.z = 1f;
        }
        
        private void FixedUpdate() {
            Movement();
        }

        protected override void Movement() {
            transform.position =
                Calc.Approach(transform.position, PlayerController.Controller.transform.position, _rand);
        }
    }
}