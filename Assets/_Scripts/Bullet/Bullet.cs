using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Bullet {
    /// <summary>
    /// The basic properties of a bullet.
    /// It has to be the struct because it only copy its value while the class will transfer its address.
    /// So, if u want to use a constant temporary argument, it has to be a struct. 
    /// </summary>

    [Serializable]
    public struct BulletProperties {
        [Header("Basic Properties")]
        public float radius;
        public Color color;
        public Vector3 worldPosition;
        public float speed;
        public Vector3 direction;
        public float acceleration;
        public float rotation;
        public int order;
        public Bullet parent;
        public Bullet bullet;

        /// <summary>
        /// Return the screen position of the bullet in vector2.
        /// if <c>Camera.main == null</c>, the default value of vector2 will be returned instead. 
        /// </summary>
        public Vector2 ScreenPosition 
             => Camera.main != null ? Camera.main.WorldToScreenPoint(worldPosition) : default(Vector2);
        
    }
    
    public enum BulletStates {
        Inactivated,
        Spawning,
        Activated,
        Destroying
    };

    public class Bullet : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private long[] _timer;
        private BulletProperties _prop;
        private Vector3 _tarScale;
        private Vector3 _curScale;
        public bool Grazed { set; get; }
        public int SpawnTime { private set; get; }
        private int _type;
        private bool _isGlowing;
        private bool _isLaser;

        public void SetInitialProperties(Sprite sprite, Material material, bool isLaser) {
            SpawnTime = GameManager.Manager.WorldTimer;
            spriteRenderer.sprite = sprite;
            spriteRenderer.material = material;
            _states = BulletStates.Spawning;
            transform.localScale = Vector3.zero;
            _isLaser = isLaser;

        }
        public Vector3 CurVerDir { private set; get; }
        
        public Vector3 CurHorDir => Vector3.Cross(CurVerDir, Vector3.back).normalized;


        public BulletProperties Prop {
            get => _prop;
            set {
                _prop = value;
                var tf = transform;
                var c = value.color;
                if (_isLaser) c.a /= 2.5f;
                spriteRenderer.color = c;
                CurVerDir = (value.worldPosition - tf.position).normalized;
                tf.position = value.worldPosition;
                tf.rotation = Quaternion.Euler(0f, 0f, value.rotation);
            }
        }

        /// <summary>
        /// Things being done per frame.
        /// </summary>
        public event Action<Bullet> StepEvent;

        /// <summary>
        /// Things being done when a bullet is destroyed including the basic effects of destruction.
        /// Will be executed only once after being triggered.
        /// /// </summary>
        public event Action<Bullet> DestroyEvent;

        /// <summary>
        /// To unregister the Step and Destroy event when a bullet is inactivated.
        /// Will be <b>immediately</b> called by the func BulletInactivate(),
        /// So you dont need to call this by yourself.
        /// </summary>
        public void InactivateEvent() {
            StepEvent = null;
            DestroyEvent = null;
        }

        public int GetSelfTime() {
            return GameManager.Manager.WorldTimer - SpawnTime;
        }
        
        /// <summary>
        /// The state machine of the bullet.
        /// </summary>
        private BulletStates _states;

        private void CheckState() {
            switch (_states) {
                case BulletStates.Spawning:
                    StepEvent?.Invoke(this);
                    var tarScale = Vector3.one;
                    if (_isLaser) tarScale.x /= 8f;
                    _curScale = Calc.Approach(_curScale, tarScale, 8f * Vector3.one);
                    transform.localScale = _curScale;
                    if (Calc.Equal(_curScale, Vector3.one)) _states = BulletStates.Activated;
                    break;
                case BulletStates.Activated: 
                    //Debug.Log("Activated!");
                    StepEvent?.Invoke(this);
                    //CheckDistance();
                    CheckOnField();
                    break;
                case BulletStates.Destroying:
                    //Debug.Log("Destroying!");
                    DestroyEvent?.Invoke(this);
                    DestroyEvent = null;
                    _curScale = Calc.Approach(_curScale, Vector3.zero, 8f * Vector3.one);
                    transform.localScale = _curScale;
                    if (Calc.Equal(_curScale, Vector3.zero)) _states = BulletStates.Inactivated;
                    break;
                case BulletStates.Inactivated:
                    //Debug.Log("Inactivated!");
                    BulletManager.Manager.BulletInactivate(this);
                    break;
            }
        }

        public void SetState(BulletStates state) {
            this._states = state;
        }
        

        /// <summary>
        /// Check the distance between the bullet and player.
        /// Alert that the z position will be ignored.
        /// </summary>
        private void CheckDistance() {
            var player = PlayerController.Controller;
            float distance = ((Vector2) player.transform.position - 
                              (Vector2) _prop.worldPosition).magnitude;

            if (!Grazed && (_prop.radius + player.GrazeRadius) >= distance) {
                Grazed = true;
                GameManager.Manager.NumGraze += 1;
            }

            if (_prop.radius + player.HitRadius >= distance) {
                GameManager.Manager.NumHit += 1;
                InactivateEvent();
                BulletManager.Manager.BulletInactivate(this);
            }
        }
        
        
        /// <summary>
        /// check the current bullet is on screen or not.
        /// If it leaves the screen, then inactivate it;
        /// </summary>
        private void CheckOnField() {
            Vector2 position = Camera.main.WorldToScreenPoint(transform.position);
            //Debug.Log(position + " " + (Screen.width + 200f) + " " + (Screen.height+ 500f));
            if (position.x >= Screen.width + 200f || position.x <= -200f ||
                position.y >= Screen.height + 500f || position.y <= -500f) {
                //I WILL NEVER OMIT THE BRACES EVER AGAIN.
                BulletManager.Manager.BulletInactivate(this);
            }
        }

        //In unity, Update is called once a frame,
        //so its ok to use update here to get a stable refresh rate.
        //On the contrast FixedUpdate will be executed more or less than once
        //according to the length of the frame.
        private void FixedUpdate() {
            _curScale = transform.localScale;
            CheckState();
        }

        private void OnDrawGizmos() { 
            Gizmos.color = Grazed ? Color.green : Color.magenta;
            Gizmos.DrawWireSphere(_prop.worldPosition, _prop.radius);
        }
    }
}