using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public float speed = 100f;
    Rigidbody poo;
    void Start(){
        poo = GetComponent<Rigidbody>();
        print("I got made!");
    }
    void OnCollisionEnter(Collision other){
        Destroy(this.gameObject);
    }
}
