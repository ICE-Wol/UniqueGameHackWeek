﻿using _Scripts.BossBehaviour;
using _Scripts.Bullet;
using UnityEngine;

namespace _Scripts.SpawnBehaviour {
    public abstract class SpawnBehaviour : MonoBehaviour {
        protected Bullet.Bullet TempBullet;
        protected BulletProperties TempProp;
        protected int[] Timer;

        /// <summary>
        /// Reset all timer value to zero for initialization.
        /// </summary>
        protected void ResetTimerAll() {
            for (int i = 0; i < 8; i++) {
                Timer[i] = 0;
            }
        }
        
        /// <summary>
        /// Set a circular timer for you.Should be Called in <code>FixedUpdate()</code>.
        /// </summary>
        /// <param name="ord">The order of the timer, range from 1 to 7.</param>
        /// <param name="period">The period of the timer in frame.</param>
        protected bool SetTimerWithPeriod(int ord, int period) {
            Timer[ord] ++;
            return (Timer[ord] % period == 0) ;
        }

        /// <summary>
        /// Set a circular timer for you.Should be Called in <code>FixedUpdate()</code>.
        /// </summary>
        /// <param name="start">The start time of this timer.</param>
        /// <param name="ord">The order of the timer, range from 1 to 7.</param>
        /// <param name="period">The period of the timer in frame.</param>
        protected bool SetTimerWithPeriod(int start, int ord, int period) {
            Timer[ord] ++;
            if (Timer[ord] < start) return false;
            else return ((Timer[ord] - start) % period == 0) ;
        }

        /// <summary>
        /// Set this spawner's life time.Should be Called in <code>FixedUpdate()</code>.
        /// </summary>
        /// <param name="life">The life time in frame.</param>
        protected void SetLife(int life) {
            CheckBossState();
            Timer[0]++;
            if(Timer[0] >= life) Destroy(this.gameObject);
        }
        
        /// <summary>
        /// Initialize the bullet spawner.
        /// </summary>
        protected void Initialize() {
            //**One time for all**
            TempProp = new BulletProperties();
            Timer = new int[8];
            ResetTimerAll();
        }

        protected void CheckBossState() {
            if (BossSt00.BSt00.isChangingCard) {
                Destroy(this.gameObject);
            }
        }
        protected abstract void Spawn();
    }
}