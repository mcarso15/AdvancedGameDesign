using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 camRotation = Vector3.zero;

    private Rigidbody rigidBody;
    private Vector3 rotation = Vector3.zero;

    float minView = 270f;
    float maxView = 90f;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity){
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation){
        rotation = _rotation;
    }

    public void CamRotate(Vector3 _camRotate){
        camRotation = _camRotate;
    }

    void FixedUpdate(){
        DoMovement();
        DoRotate();
    }

    void DoMovement(){
        if(velocity != Vector3.zero){
            rigidBody.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }

    void DoRotate(){
        rigidBody.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        if(cam != null){
            cam.transform.Rotate(-camRotation); //Inverted camera control if this is positive
        }
    }
}
