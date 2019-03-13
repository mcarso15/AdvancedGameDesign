using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float mouseHorizSens = 6f;
    [SerializeField] private float mouseVertSens = 8f;
    [SerializeField] private float fallMultiplier = 2f;

    private Camera cam;

    private bool grounded;

 
    

    private PlayerMovement movement;

    void Start(){
        movement = GetComponent<PlayerMovement>();
    }

    void Update(){
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
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Ground"){
            grounded = false;
        }
    }
}
