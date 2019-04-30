using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float mouseHorizSens = 6f;
    [SerializeField] private float mouseVertSens = 8f;
    [SerializeField] private float fallMultiplier = 2f;

    [SerializeField] private float damageMultiplier = 0.1f;

    [SerializeField] private float healTimer = 50f;

    
    private float healthWidth;
    private float currHealthWidth;
    private float damageValue;

    private float _healTimer;
    private Camera cam;

    private bool grounded = true;
    private bool healingGround = false;

    GameObject healthBar;

    RectTransform healthTransform;
    

    private PlayerMovement movement;

    void Start(){
        movement = GetComponent<PlayerMovement>();
        //Get the width of the health bar
        healthBar = GameObject.FindWithTag("HealthBar");
        healthTransform = healthBar.GetComponent<RectTransform>();
        healthWidth = healthTransform.rect.width;
        currHealthWidth = healthWidth;

        damageValue = (healthWidth * damageMultiplier);
    }

    void Update(){
        DoMovement();
        
        CheckHealing();

        TestHealth();
    }

    void DoMovement(){
        float xMovement = Input.GetAxisRaw("Horizontal"); //-1 to 1
        float zMovement = Input.GetAxisRaw("Vertical"); //-1 to 1

        Vector3 movHoriz = transform.right * xMovement;
        Vector3 movVert = transform.forward * zMovement;

        Vector3 velocity = (movHoriz + movVert).normalized * speed;

        movement.Move(velocity);

        //Calculate rotation. Only want 'Y' rotation on player. 'Turning'.
        float yRotation = Input.GetAxisRaw("Mouse X");
        
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * mouseHorizSens;

        movement.Rotate(rotation);

        //Calculate camera rotation
        float xRotation = Input.GetAxisRaw("Mouse Y") ;
        Vector3 camRotation = new Vector3(xRotation, 0f, 0f)* mouseVertSens;

        movement.CamRotate(camRotation);

        if(grounded){
            if(Input.GetButtonDown("Jump")){
                movement.Jump(jumpHeight);
                movement.FallMulti(fallMultiplier);
                movement.DoJump();
            }
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Ground"){
            grounded = true;
        }

        if(other.gameObject.tag == "Wall"){
            movement.Move(Vector3.zero);
            movement.Rotate(Vector3.zero);
        }

        if(other.gameObject.tag == "Projectile"){
            //Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            Destroy(other.gameObject);
            healthTransform.sizeDelta = new Vector2(currHealthWidth - damageValue, healthTransform.sizeDelta.y);
            currHealthWidth -= damageValue;
        }

        if(other.gameObject.tag == "HealingGround"){
            //grounded = true;
            healingGround = true;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Ground"){
            grounded = false;
        }

        if(other.gameObject.tag == "HealingGround"){
            //grounded = false;
            healingGround = false;
        }
    }

    void CheckHealing(){
        _healTimer = healTimer;
        if(currHealthWidth < healthWidth && healingGround){                        
            healthTransform.sizeDelta = new Vector2(currHealthWidth + damageValue, healthTransform.sizeDelta.y);
            currHealthWidth += damageValue;
        }
    }

    void TestHealth(){
        if(Input.GetKeyDown(KeyCode.E)){
            healthTransform.sizeDelta = new Vector2(currHealthWidth - damageValue, healthTransform.sizeDelta.y);
            currHealthWidth -= damageValue;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            if(currHealthWidth < healthWidth){
                healthTransform.sizeDelta = new Vector2(currHealthWidth + damageValue, healthTransform.sizeDelta.y);
                currHealthWidth += damageValue;
            }
        }
    }
}
