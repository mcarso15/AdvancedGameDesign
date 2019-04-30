using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletManager : MonoBehaviour
{
    /*
     *  This is where the scripts will be when the toilets need to
     *  change colors, AKA when there is a collision with the toilet
     *  and the plunger gun   
     */

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        //toilet is red... FF0000
        //Fetch the Renderer from the GameObject

        //Set the main Color of the Material to green
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            //Set the main Color of the Material to red
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.red);
        }
    }
}
