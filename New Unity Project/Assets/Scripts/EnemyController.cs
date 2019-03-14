using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject pooPrefab;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxFollowFrom = 10f;

    [SerializeField] private float turnAngle = 15f;
    [SerializeField] private float tryAgainDist = 5f;
    [SerializeField] private float fireDelay = 200f;
    private Rigidbody rb;
    
    private Collider enemyCollider;

    private GameObject player;

    private float _tryAgainDist = 0f;

    private Vector3 playerDirection;
    private Vector3 _rotation;

    private bool inAWall;

    private bool dead;

    private float _fireDelay = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
        player = GameObject.FindWithTag("Player");
        dead = false;
        _fireDelay = fireDelay;
        OrientEnemies();
    }

    void FixedUpdate(){
        if(!dead){
        FollowPlayer();
        TestFight();
        }
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

        if(other.gameObject.tag == "Wall"){
            inAWall = true;
        }
        
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Wall"){
            inAWall = false;
        }
    }


    void FollowPlayer(){
        if(!inAWall && _tryAgainDist <= 0){
            //Make sure enemy is 'looking' at the player so they can aim
            playerDirection = player.transform.position - this.transform.position + Vector3.up;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(playerDirection), 1);
        }
        //Move towards the player
        if(Vector3.Distance(transform.position, player.transform.position) >= minDistance && Vector3.Distance(transform.position, player.transform.position) < maxFollowFrom){
            //Check if I'm crashing into a wall, then determine which way to turn
            if(inAWall){
                _tryAgainDist = tryAgainDist;
                //If moving right makes me closer than moving straight or left, turn right, then move straight.
                if(Vector3.Distance(transform.position + Vector3.right, player.transform.position) <= Vector3.Distance(transform.position - Vector3.right, player.transform.position)){
                    _rotation = new Vector3(0f, turnAngle, 0f);
                    rb.MoveRotation(transform.rotation * Quaternion.Euler(_rotation));
                    transform.position += transform.right * moveSpeed * Time.fixedDeltaTime;
                    
                }
                //If moving left makes me closer than moving straight or right, turn left, then move straight.
                if(Vector3.Distance(transform.position - Vector3.right, player.transform.position) < Vector3.Distance(transform.position + Vector3.right, player.transform.position)){
                    _rotation = new Vector3(0f, -turnAngle, 0f);
                    rb.MoveRotation(transform.rotation * Quaternion.Euler(_rotation));
                    transform.position -= transform.right * moveSpeed * Time.fixedDeltaTime;
                }
            }
            
            else{
                transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime;
            }
            _tryAgainDist--;
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= maxDistance){
            _fireDelay--;
            if(_fireDelay == 0){
                FireProjectile();
            }
        }
        
    }
    void TestFight(){
        if(Input.GetKeyDown(KeyCode.Q)){
            FireProjectile();
        }
    }

    void FireProjectile(){
        GameObject pooProjectile = (GameObject)Instantiate(pooPrefab, transform.position, transform.rotation);
        Physics.IgnoreCollision(pooProjectile.GetComponent<Collider>(), GetComponent<Collider>());
        float force = pooProjectile.GetComponent<EnemyProjectileController>().speed;
        pooProjectile.GetComponent<Rigidbody>().AddForce(pooProjectile.transform.forward * force, ForceMode.Impulse);
        _fireDelay = fireDelay;
    }

    void OrientEnemies(){
        playerDirection = player.transform.position - this.transform.position + Vector3.up;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(playerDirection), 1);
    }
}
