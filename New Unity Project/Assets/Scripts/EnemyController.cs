using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    
    private Collider enemyCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        TestFight();
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Plunger"){
            Destroy(other.gameObject);
            if(rb.useGravity == false){
                rb.useGravity = true;
            }
            
            if(rb.velocity.y == 0){
                rb.isKinematic = true;
            }
        }
        
    }

    void TestFight(){

    }
}
