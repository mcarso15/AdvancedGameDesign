using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject pooPrefab;
    private Rigidbody rb;
    
    private Collider enemyCollider;

    private GameObject player;
    private float distanceToPlayer;

    private Vector3 playerDirection;

    private bool dead;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
        player = GameObject.FindWithTag("Player");
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = player.transform.position - this.transform.position + Vector3.up;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(playerDirection), 1);
        
    }
    void FixedUpdate(){
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

            dead = true;
        }
        
    }

    void TestFight(){
        if(Input.GetKeyDown(KeyCode.Q)){
            GameObject pooProjectile = (GameObject)Instantiate(pooPrefab, transform.position, transform.rotation);
            Physics.IgnoreCollision(pooProjectile.GetComponent<Collider>(), GetComponent<Collider>());
            float force = pooProjectile.GetComponent<EnemyProjectileController>().speed;
            pooProjectile.GetComponent<Rigidbody>().AddForce(pooProjectile.transform.forward * force, ForceMode.Impulse);
        }
    }
}
