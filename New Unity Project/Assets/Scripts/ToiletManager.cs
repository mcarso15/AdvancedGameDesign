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

    
    WorldManager wm;
    // Start is called before the first frame update
    void Start()
    {
        
        //toilet is red... FF0000
        //Fetch the Renderer from the GameObject
        rend = GetComponent<Renderer>();
        //Set the main Color of the Material to green
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.red);

        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.red);

        wm = GetComponentInParent<WorldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Plunger")
        {
            Destroy(other.gameObject);
            wm.Plunge();
            //Set the main Color of the Material to white
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.white);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", Color.white);
        }
    }
}
