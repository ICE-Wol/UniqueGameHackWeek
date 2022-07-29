using UnityEngine;

namespace _Scripts {
    public class PlayerController : MonoBehaviour {
        public static PlayerController Controller;
        [SerializeField] private float hitRadius;
        public float HitRadius {
            get {
                return hitRadius;
            }
        }
        [SerializeField] private float grazeRadius;
        public float GrazeRadius {
            get {
                return grazeRadius;
            }        
        }

        public Vector2 ScreenPosition {
            get {
                if (Camera.main != null)
                    return Camera.main.WorldToScreenPoint(transform.position);
                else return Vector2.zero;
            }
        }

        [SerializeField] private float speed;
        [SerializeField] private float slowRate;
    
        private float _horizontal;
        private float _vertical;
        private float _slowMode;
        private Vector3 _direction;

        /// <summary>
        /// Movement control, called once per frame.
        /// </summary>
        private void PlayerMovement() {
            //Handle Input
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
            _slowMode = Input.GetAxis("SlowMode");
        
            //Refresh the position
            _direction
                = new Vector3(_horizontal, _vertical, 0).normalized;
            transform.position
                += (_slowMode > 0.5f ? speed * slowRate : speed) 
                   * Time.deltaTime * _direction;
        
        }

        private void Awake() {
            //Singleton check
            if (Controller == null) Controller = this;
            else DestroyImmediate(this.gameObject);
        }

        private void Update() {
            PlayerMovement();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.cyan;
            Vector3 position = transform.position;
            Gizmos.DrawWireSphere(position, grazeRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, hitRadius);
        }
    }
}
