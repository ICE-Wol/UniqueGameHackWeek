using UnityEngine;

namespace _Scripts {
    public class CircularHealthBar : MonoBehaviour {
        [SerializeField] private float radius;
        [SerializeField] private float width;
        private int _lifeNum;
        private int[] _cardNum;
        //360 degree full.
        private int[,] _cardScale;

        private Mesh _mesh;
        private Vector3[] _fullVertices;

        private void Initialize() {
            _fullVertices = new Vector3[720];
            for (int i = 0; i < 720; i += 2) {
                var degree = i / 2;
                var c = Mathf.Cos(Mathf.Deg2Rad * degree);
                var s = Mathf.Sin(Mathf.Deg2Rad * degree);
                _fullVertices[i] = new Vector3(radius * c, radius * s, 0f);
                _fullVertices[i + 1] = new Vector3((radius + width) * c, (radius + width) * s, 0f);
            }
        }
        void Start() {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[3];
            Vector2[] uv = new Vector2[3];
            int[] triangles = new int[3];
            GetComponent<MeshFilter>().mesh = mesh;
        }
        
    }
}
