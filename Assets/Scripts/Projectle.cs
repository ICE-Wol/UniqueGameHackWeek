using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectle : MonoBehaviour
{  
    public float bulletSpeed;
    public Vector3 moveDirection;
     void Update() 
    {
        ProjectleMovment();
    }
 
void ProjectleMovment()
 {
    transform.position += bulletSpeed* Time.deltaTime* moveDirection;
 }
    
}