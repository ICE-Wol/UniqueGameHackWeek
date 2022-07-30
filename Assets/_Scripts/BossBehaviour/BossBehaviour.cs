using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.BossBehaviour {
    public abstract class BossBehaviour : MonoBehaviour {
        [SerializeField] protected int numForm;
        [SerializeField] protected int[] healthForm;
        [SerializeField] protected int[] numCard;
        [SerializeField] protected int[] hpCard;
        [SerializeField] protected String[] nameCard;
        [SerializeField] protected int[] timeCard;
        protected int StartTime;
        protected int CurrentForm;
        protected int CurrentCard;
        protected int CurrentHealth;
        protected int CurrentMaxHealth;
        protected float CurrentTime;
        protected bool isClaimed;
        protected Vector3 TarPos;

        protected abstract void SwitchCard(int ordForm, int ordCard);

        protected abstract void ClaimCard(int ordForm, int ordCard);
        
        protected abstract void CheckCard();

        protected void Movement() {
            var posPlayer = PlayerController.Controller.transform.position;
            var pos = transform.position;
            if (posPlayer.x >= pos.x) {
                pos.x += Random.Range(-0.5f, 1f);
            }
            else {
                pos.x -= Random.Range(-0.5f, 1f);
            }
            pos.y += Random.Range(-0.5f, 0.5f);

            if (pos.x <= GameManager.Manager.TopLeft.x)
                pos.x = GameManager.Manager.TopLeft.x + 0.5f;
            else if (pos.x >= GameManager.Manager.BottomRight.x)
                pos.x = GameManager.Manager.BottomRight.x - 0.5f;

            if (pos.y >= GameManager.Manager.TopLeft.y)
                pos.y = GameManager.Manager.TopLeft.y - 0.5f;
            TarPos = pos;
        }
        
        protected void CreateSpawner(float loopTime, Action action) {
            if ((GameManager.Manager.WorldTimer - StartTime) % loopTime == 90) 
                action?.Invoke();
            if ((GameManager.Manager.WorldTimer - StartTime) % loopTime == 30) 
                Movement();
            
        }
        
        public void CreateSpawner(Action action) {
            action?.Invoke();
        }
    }
}
