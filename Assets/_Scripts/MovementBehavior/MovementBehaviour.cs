using UnityEngine;

namespace _Scripts.MovementBehavior {
    public abstract class MovementBehaviour : MonoBehaviour {
        protected Vector3 Position;
        protected Vector3 Direction;
        protected float Speed;

        protected abstract void Movement();

    }
}