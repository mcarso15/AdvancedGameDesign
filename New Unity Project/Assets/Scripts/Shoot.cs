using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject plungerPrefab;

    void Update(){
        DoShoot();
    }
    void DoShoot(){
        if(Input.GetButtonDown("Fire1")){
            print("click");
            GameObject plungerProjectile = (GameObject)Instantiate(plungerPrefab, transform.position, transform.parent.rotation * Quaternion.Euler(0,180f,0));
            float force = -1 * plungerProjectile.GetComponent<PlungerController>().speed;
            plungerProjectile.GetComponent<Rigidbody>().AddForce(plungerProjectile.transform.forward * force, ForceMode.Impulse);
        }
    }
}
