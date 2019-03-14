using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerController : MonoBehaviour
{
    public float speed = 100f;
    Rigidbody plunger;
    void Start(){
        plunger = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground"){
            plunger.AddForce(plunger.transform.up * (-1), ForceMode.Impulse);
        }
    }
}
