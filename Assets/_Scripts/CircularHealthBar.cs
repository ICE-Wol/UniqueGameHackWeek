using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using _Scripts.BossBehaviour;
using UnityEngine;

namespace _Scripts {
    public class CircularHealthBar : MonoBehaviour {
        //[SerializeField] private float radius;
        //[SerializeField] private float width;
        private int _lifeNum;
        private int[] _cardNum;
        //360 degree full.
        private int[,] _cardScale;
        [SerializeField] private GameObject element;
        private HealthElement[] _elements;
        

        //private Mesh _mesh;
        //private Vector3[] _fullVertices;

        private void Initialize() {
            //this.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            _elements = new HealthElement[122];
            for (int i = -60; i <= 60; i++) {
                var obj = Instantiate(element);
                obj.transform.position = this.transform.position;
                _elements[i + 60] = obj.GetComponent<HealthElement>();
                _elements[i + 60].ord = i;
                _elements[i + 60].SetEnable();
            }
            /*_fullVertices = new Vector3[720];
            for (int i = 0; i < 720; i += 2) {
                var degree = i / 2;
                var c = Mathf.Cos(Mathf.Deg2Rad * degree);
                var s = Mathf.Sin(Mathf.Deg2Rad * degree);
                _fullVertices[i] = new Vector3(radius * c, radius * s, 0f);
                _fullVertices[i + 1] = new Vector3((radius + width) * c, (radius + width) * s, 0f);
            }*/
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">0-1,float.</param>
        public void Refresh(float num) {
            int rate = (int)(num * 60f) + 1;
            for (int i = -60; i <= 60; i++) {
                if (Mathf.Abs(i) < rate)
                    _elements[i + 60].SetEnable();
                else _elements[i + 60].SetDisable();
                _elements[i + 60].transform.position = this.transform.position;
            }
        }

        private void FixedUpdate() {
            /*if (Input.anyKeyDown) {
                for (int i = -60; i <= 60; i++) {
                    _elements[i + 60].SetDisable();
                }
            }*/
        }

        void Start() {
            Initialize();
            /*Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[3];
            Vector2[] uv = new Vector2[3];
            int[] triangles = new int[3];
            GetComponent<MeshFilter>().mesh = mesh;*/
        }
        
    }
}
