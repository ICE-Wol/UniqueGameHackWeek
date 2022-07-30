using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using _Scripts;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public int ord;

    private Vector3 _tarScale;
    // Start is called before the first frame update
    void Start() {
        _tarScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("SlowMode") >= 0.5)
            _tarScale = 1.5f * Vector3.one;
        else _tarScale = Vector3.zero;
        transform.localScale = Calc.Approach(transform.localScale, _tarScale, 8f * Vector3.one);
        transform.rotation = Quaternion.Euler(0f,0f,GameManager.Manager.WorldTimer * ord);
    }
}
