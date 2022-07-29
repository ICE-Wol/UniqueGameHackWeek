using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

//U should be informed that all classes which u define are implicitly derived from the class Object.
//And among all the types they are all value type except of the class Object and String.
//So if u use a struct here, u cant change '_parent.Section.Child = _child;'like this,
//because the property will only return a copy of the original struct instance,
//and the value in the Bullet object _parent is never changed.
//To avoid this u can simply just change the struct into a class,
//since it has quite a bit fields and functions, it wont do any harm to use a class instead.
//the structs are stored directly in the system stack, 
//as a result it will be faster for u to access it while it cost more memory to be stored.


namespace _Scripts.Bullet {
    public class BulletManager : MonoBehaviour {
        public static BulletManager Manager;
        //using static can make it directly be accessed by BulletManager.Manager
        //without creating an instance of this class

        [SerializeField] private Bullet bulletPrefab;

        //the bullet prefab was filled in the blank
        //it actually get the address of the bullet component attached to the prefab
        //when the bullet instance(component) is created,
        //the system will automatically create a clone of the prefab for you
        [SerializeField] private List<Sprite> bulletSprite;
        [SerializeField] private List<Sprite> laserSprite;
        [SerializeField] private List<Material> material;
    
        private Stack<Bullet> _bulletPool;

        private Bullet _tempBullet;
        private BulletProperties _tempProp;

        #region StepEvents
        /// <summary>
        /// The bullet wont move until its speed reaches below zero.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step00(Bullet bullet) {
            _tempProp = bullet.Prop;
            _tempProp.speed -= 0.05f;
            if(_tempProp.speed < 0f) bullet.SetState(BulletStates.Destroying);
            BulletRefresh(bullet, _tempProp);
        }
        
        /// <summary>
        /// The bullet will move on its direction until its speed reaches below zero.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step01(Bullet bullet) {
            _tempProp = bullet.Prop;
            _tempProp.speed -= 0.05f;
            if(_tempProp.speed < 0f) bullet.SetState(BulletStates.Destroying);
            _tempProp.worldPosition += Time.fixedDeltaTime * _tempProp.speed * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }
            
        //TODO: fix gravity.
        /// <summary>
        /// The bullet will fall down faster and faster.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step02(Bullet bullet) {
            _tempProp = bullet.Prop;
            if(_tempProp.speed <= 6f) _tempProp.speed += 0.01f;
            var dir = Vector2.Angle(Vector2.up, _tempProp.direction);
            if (dir > 70f) {
                _tempProp.direction.x = Calc.Approach(_tempProp.direction.x, 0f, 48f);
                _tempProp.direction.y = Calc.Approach(_tempProp.direction.y, -0.5f, 64f);
            }
            _tempProp.worldPosition += Time.fixedDeltaTime * _tempProp.speed * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }
        
        /// <summary>
        /// Flower petals. Need parent as heart.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step03(Bullet bullet) {
            _tempProp = bullet.Prop;
            if (!_tempProp.parent.gameObject.activeSelf) {
                Manager.BulletInactivate(bullet);
                return;
            }
            var degree = (_tempProp.order * 360f / 5f + GameManager.Manager.WorldTimer / 10f);
            Vector3 direction =
                new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0f);
            _tempProp.worldPosition = 0.15f * direction + _tempProp.parent.transform.position;
            _tempProp.rotation = degree + 90f;
            BulletRefresh(bullet, _tempProp);
        }
        
        /// <summary>
        /// The bullet will move on its direction with a steady speed.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step04(Bullet bullet) {
            _tempProp = bullet.Prop;
            _tempProp.worldPosition += _tempProp.speed * Time.fixedDeltaTime * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }

        /// <summary>
        /// The bullet will move on its direction slower until a steady speed.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step05(Bullet bullet) {
            _tempProp = bullet.Prop;
            if(_tempProp.speed >= 0.5f) _tempProp.speed -= 0.1f;
            _tempProp.worldPosition += _tempProp.speed * Time.fixedDeltaTime * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }
        
        /// <summary>
        /// The bullet will move on its direction with a steady speed with calculation with rotation.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step06(Bullet bullet) {
            _tempProp = bullet.Prop;
            _tempProp.rotation = Vector2.SignedAngle(Vector2.right, _tempProp.direction) + 90f;
            _tempProp.worldPosition += _tempProp.speed * Time.fixedDeltaTime * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }
        
        /// <summary>
        /// The bullet will move on its direction slower until a steady speed according to its order.
        /// </summary>
        /// <param name="bullet"></param>
        public void Step07(Bullet bullet) {
            _tempProp = bullet.Prop;
            if(_tempProp.speed >= 0.5f + 0.1f * _tempProp.order) _tempProp.speed -= 0.1f;
            _tempProp.worldPosition += _tempProp.speed * Time.fixedDeltaTime * _tempProp.direction;
            BulletRefresh(bullet, _tempProp);
        }
        #endregion

        #region DestroyEvent

        public void Destroy01(Bullet bullet) {
            var parent = Manager.BulletActivate();
            var prop = new BulletProperties();
            prop.bullet = parent;
            prop.radius = 0f;
            prop.worldPosition = bullet.transform.position;
            prop.direction = bullet.Prop.direction;
            prop.direction.x += Random.Range(-0.3f, 0.3f);
            prop.speed = 2f + Random.Range(-0.3f, 0.3f);
            prop.color = Color.white;
            prop.color.a = 0.5f;
            Manager.BulletInitialize(parent, 13, true);
            Manager.BulletRefresh(parent, prop);
            parent.StepEvent += BulletManager.Manager.Step02;
            for (int i = 0; i < 5; i++) {
                //pick a bullet out of the pool
                var tempBullet = Manager.BulletActivate();

                //some necessary calculations.
                var degree = (i * 360f / 5f + GameManager.Manager.WorldTimer / 10f);
                Vector3 direction =
                    new Vector3(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad), 0f);

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                var tempProp = new BulletProperties();
                tempProp.bullet = tempBullet;
                tempProp.order = i;
                tempProp.radius = 0.05f;
                tempProp.parent = parent;
                tempProp.direction = direction;
                tempProp.rotation = degree + 90f;
                tempProp.worldPosition = 0.15f * direction + transform.position;
                tempProp.color = Color.white;

                //initialize the bullet
                Manager.BulletInitialize(tempBullet, 4, false);
                Manager.BulletRefresh(tempBullet, tempProp);

                //register the target event
                tempBullet.StepEvent += Manager.Step03;
            }
        }
        
        public void Destroy02(Bullet bullet) {
            for (int i = 0; i < 3; i++) {
                //pick a bullet out of the pool
                var tempBullet = Manager.BulletActivate();
                

                //fill in the initial properties
                //**Remember to initialize it before use.**
                //fill in the index of the bullet
                var tempProp = new BulletProperties();
                tempProp.bullet = tempBullet;
                tempProp.order = i;
                tempProp.speed = (i + 1) * 1f;
                tempProp.radius = 0.05f;
                tempProp.direction = bullet.Prop.direction;
                tempProp.worldPosition = bullet.transform.position;
                tempProp.color = Color.white;

                //initialize the bullet
                Manager.BulletInitialize(tempBullet, 4, true);
                Manager.BulletRefresh(tempBullet, tempProp);

                //register the target event
                tempBullet.StepEvent += Manager.Step06;
            }
        }

        #endregion

        /// <summary>
        /// Creating Some bullets and add them to the bullet pool.
        /// </summary>
        /// <param name="num">The number of bullet to add,range 0 to 512</param>
        private void BulletPoolAdd(int num) {
            if (num < 0) {
                num = 0;
            }

            if (num > 512) {
                num = 512;
            }

            for (int i = 1; i <= num; i++) {
                _tempBullet = Instantiate(bulletPrefab);
                _tempBullet.gameObject.SetActive(false);
                _bulletPool.Push(_tempBullet);
            }
        }

        /// <summary>
        /// Check the number of bullet in the pool
        /// </summary>
        /// <returns></returns>
        public int BulletPoolCheckSize() {
            return _bulletPool.Count;
        }

        /// <summary>
        /// Take a bullet out of the bullet pool and set it activated.
        /// </summary>
        /// <returns>the index of the activated bullet</returns>
        public Bullet BulletActivate() {
            if (_bulletPool.Count <= 16) {
                BulletPoolAdd(16);
            }

            _tempBullet = _bulletPool.Pop();
            _tempBullet.gameObject.SetActive(true);
            _tempBullet.Grazed = false;
            _tempBullet.transform.localScale = Vector3.zero;
            return _tempBullet;
        }

        /// <summary>
        /// Recycle a bullet by inactivating it.
        /// </summary>
        /// <param name="bullet">the bullet which you want to recycle.</param>
        public void BulletInactivate(Bullet bullet) {
            //bullet.transform.position = Vector3.zero;
            bullet.InactivateEvent();
            bullet.gameObject.SetActive(false);
            _bulletPool.Push(bullet);
        }

        /// <summary>
        /// Initialize a bullet.
        /// </summary>
        /// <param name="bullet">The bullet which you want to initialize.</param>
        /// <param name="type">The type of bullet to decide the sprite.</param>
        /// <param name="isGlowing">If it is true, the bullet will be glowing.</param>
        public void BulletInitialize(Bullet bullet, int type, bool isGlowing) {
            int matType = isGlowing ? 1 : 0;
            bullet.SetInitialProperties(bulletSprite[type], material[matType], false);
        }

        public void LaserInitialize(Bullet bullet) {
            bullet.SetInitialProperties(laserSprite[0], material[1], true);
        }
        
        // Insert the bullet into the bullet field for the first time where its root indicates its belonging.
        /// <summary>
        /// Inject the property struct into the bullet and refresh its states.
        /// Alert that this should be called after <c>BulletActivate()</c> <b>immediately</b>
        /// </summary>
        /// <param name="bullet">The index of bullet you want to refresh.</param>
        /// <param name="prop">The property of the bullet you want to inject.</param> 
        public void BulletRefresh(Bullet bullet, BulletProperties prop) {
            bullet.Prop = prop;
        }

        private void Awake() {
            //Singleton check
            if (Manager == null) Manager = this;
            else Destroy(this.gameObject);
            _bulletPool = new Stack<Bullet>();
            _tempProp = new BulletProperties();
            BulletPoolAdd(512);
        }
    }
}