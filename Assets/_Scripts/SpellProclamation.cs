using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts {
    public class SpellProclamation : MonoBehaviour {
        public RectTransform spellImage;
        public RectTransform bossImage;
        public TMP_Text spellName;
        public Vector3 oriPos1;
        public Vector3 oriPos2;
        public Vector3 tarPos1;
        public Vector3 tarPos2;
        public Vector3 tarPos3;
        private bool _isShowing;

        public void ResetWithName(String name) {
            _isShowing = false;
            spellName.text = name;
            spellImage.anchoredPosition = oriPos1;
            bossImage.anchoredPosition = oriPos2;
        }

        void Update()
        {
            spellImage.anchoredPosition = Calc.Approach(spellImage.anchoredPosition, tarPos1, 32f * Vector3.one);
            if (!_isShowing) {
                bossImage.anchoredPosition = Calc.Approach(bossImage.anchoredPosition, tarPos2, 32f * Vector3.one);
                _isShowing = Calc.Equal(bossImage.anchoredPosition, tarPos2,1f);
            }

            if (_isShowing) {
                bossImage.anchoredPosition = Calc.Approach(bossImage.anchoredPosition, tarPos3, 32f * Vector3.one);
            }
        }
    }
}