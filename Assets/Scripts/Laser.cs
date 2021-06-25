using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _laserSpeed = 8.0f;
    
    // Update is called once per frame
    void Update()
    {
        //Laser Moves Up
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //if laser position is greater than 6 destory laser
        if (transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }

    }
}
