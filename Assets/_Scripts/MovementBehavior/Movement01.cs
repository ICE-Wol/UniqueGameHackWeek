using UnityEngine;

namespace _Scripts.MovementBehavior {
    public class Movement01 : MovementBehaviour{
        private float _speed;
        private void Start() {
            _speed = Random.Range(2f,3f);
        }
        
        private void FixedUpdate() {
            Movement();
            if (_speed > 0f) _speed -= 0.01f;
            else _speed = 0f;
        }

        protected override void Movement() {
            var pos = transform.position;
            pos.y -= _speed * Time.fixedDeltaTime;
            transform.position = pos;
        }
    }
}