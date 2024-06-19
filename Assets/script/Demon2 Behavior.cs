using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Demon2Behavior : MonoBehaviour
{
    public AIPath aIPath;

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    void Flip(){
        if(aIPath.desiredVelocity.x >= 0.01f){
            transform.localScale = new Vector3(-4f,4f,1f);
        }else if (aIPath.desiredVelocity.x <= -0.01f){
            transform.localScale = new Vector3(4f,4f,1f);
        }
    }
}
