using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma strict

public class FloatingTextController : MonoBehaviour
{
    /* to help with popup texts, I watched these videos:
     *   https://www.youtube.com/watch?v=Z5Wm-WZi0g8
     *   https://www.youtube.com/watch?v=_ICCSDmLCX4
    *
    */


    public Transform player;
    public float showOnDistance = 3;

    MeshRenderer textMesh;

    // Use this for initialization
    void Start()
    {
        textMesh = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < showOnDistance)
        {
            textMesh.enabled = true;

        }
        else
        {
            textMesh.enabled = false;
        }


    }

   // void DisableText()
   // {
   //     textMesh.enabled = false;
   // }

}


/*
 * 
 *      public GameObject FloatingTextPrefab; 
 * 
 * 
 * 
 *         //Trigger floating text to explain game.
        if (FloatingTextPrefab)
        {
            ShowFloatingText();
        }



    }

    void ShowFloatingText()
    {
        Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
    }
 * 
 * 
 * 
 * 
 * 
 */
