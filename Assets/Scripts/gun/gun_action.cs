using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_action : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject bullet_holder;
    public GameObject bullet;

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            GameObject bullet1 = Instantiate(bullet, bullet_holder.transform.position, bullet_holder.transform.rotation);
         
            //RaycastHit hit;
            //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
            //{
            //    Debug.Log(hit.transform.name);
            //}
        }
    }
}
