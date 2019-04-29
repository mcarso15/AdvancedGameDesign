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


    // Start is called before the first frame update
    void Start()
    {
        //toilet is red... FF0000
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {
        //toilet turns white... FFFFFF
    }
}
