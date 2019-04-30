using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    public float speed = 100f;
    Rigidbody poo;
    void Start(){
        poo = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other){
        Destroy(this.gameObject);
    }
}
