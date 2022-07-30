using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace _Scripts {
    public class HealthElement : MonoBehaviour {
        public List<GameObject> childList;
        public Vector3 oriScale;
        public int ord;
        private Vector2 _tarScale;
        private float _x;

        public void SetEnable() {
            _tarScale.y = oriScale.y;
            _tarScale.x = 1f;
        }
        
        public void SetDisable() {
            _tarScale = new Vector2(3f,0f);
        }

        private void Start() {
            _x = 0f;
        }

        void Update() {
            transform.rotation = Quaternion.Euler(0f, 0f, ord * 3 + 90f);
            _x = Calc.Approach(_x, _tarScale.x, 32f);
            for(int i = 0;i <= 2;i++) {
                var pos = childList[i].transform.localScale;
                pos.x = _x *
                        Mathf.Sin(Mathf.Deg2Rad * (Mathf.Abs(ord) * 4 + GameManager.Manager.WorldTimer + i * 120f));
                pos.y = Calc.Approach(pos.y, _tarScale.y, 16f);
                if (Calc.Equal(pos.y, _tarScale.y, 0.01f)) pos.y = _tarScale.y;
                childList[i].transform.localScale = pos;
            }
        }
    }
}
