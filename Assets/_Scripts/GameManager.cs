using TMPro;
using UnityEngine;

namespace _Scripts {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private GameObject[] instantiateList;
        private int _numGraze;
        public int WorldTimer { private set; get; }
        public int NumGraze {
            set;
            get;
        }
    
        private int _numHit;
        public int NumHit {
            set;
            get;
        }
        private void Awake() {
            Manager = this;
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start() {
            foreach (var inst in instantiateList) {
                if(inst != null) Instantiate(inst);
            }

            WorldTimer = 0;
        }

        #region Debug
        private void FixedUpdate() {
            textMesh.text
                = "Graze: " + NumGraze + "\n" + "Hit: " + NumHit + "\nWorldTimer: " + WorldTimer;
            WorldTimer++;
        }
        #endregion
    }
}
