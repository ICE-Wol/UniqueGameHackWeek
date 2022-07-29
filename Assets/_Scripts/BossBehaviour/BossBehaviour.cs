using System;
using UnityEngine;

namespace _Scripts.BossBehaviour {
    public abstract class BossBehaviour : MonoBehaviour {
        [SerializeField] protected int numForm;
        [SerializeField] protected int[] healthForm;
        [SerializeField] protected int[] numCard;
        [SerializeField] protected int[] hpCard;
        [SerializeField] protected int[] timeCard;
        protected int StartTime;
        protected int CurrentForm;
        protected int CurrentCard;

        protected abstract void SwitchForm(int ordForm);
        protected abstract void SwitchCard(int ordForm, int ordCard);
        public void CreateSpawner(float loopTime, Action action) {
            if((GameManager.Manager.WorldTimer - StartTime) % loopTime == 0)
                action?.Invoke();
        }
    }
}
