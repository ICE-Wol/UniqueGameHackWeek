using UnityEngine;

namespace _Scripts.MovementBehavior {
    public class Movement01 : MovementBehaviour{
        private float _speed;
        private float _rand;
        private float _x;
        private void Start() {
            _speed = Random.Range(2f, 3f);
            _rand = Random.Range(0, 360f);
            _x = transform.position.x;
        }
        
        private void FixedUpdate() {
            Movement();
            if (_speed > 0f) _speed -= 0.01f;
            else _speed = 0f;
        }

        protected override void Movement() {
            var pos = transform.position;
            
            pos.x = _x + Mathf.Sin(Mathf.Deg2Rad * (GameManager.Manager.WorldTimer * 2f + _rand)) / 16f * _speed;
            pos.y -= _speed * Time.fixedDeltaTime;
            transform.position = pos;
        }
    }
}