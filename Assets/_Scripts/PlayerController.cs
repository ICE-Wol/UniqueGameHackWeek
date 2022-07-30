using System.Security.Cryptography.X509Certificates;
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
        [SerializeField] private GameObject missile;
    
        private float _horizontal;
        private float _vertical;
        private float _slowMode;
        private int _timer;
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
            var offset = transform.position + (_slowMode > 0.5f ? speed * slowRate : speed) 
                         * Time.fixedDeltaTime * _direction;
            if (offset.x <= GameManager.Manager.BottomRight.x ||
                offset.y >= GameManager.Manager.BottomRight.y ||
                offset.x >= GameManager.Manager.TopLeft.x ||
                offset.y <= GameManager.Manager.TopLeft.y ) {
                transform.position = offset;
            }

        }

        private void Fire() {
            if(_timer % 2 == 0)
                Instantiate(missile, transform.position,Quaternion.Euler(0f,0f,0f));
        }

        private void Awake() {
            //Singleton check
            if (Controller == null) Controller = this;
            else DestroyImmediate(this.gameObject);
        }

        private void FixedUpdate() {
            _timer++;
            PlayerMovement();
            if (Input.GetAxisRaw("Fire1") >= 0.5f) {
                Fire();
            }
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
