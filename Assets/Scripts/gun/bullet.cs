using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour

{
    public float speed = 400;
    public int predictionofstepspersecond = 6;
    public Vector3 bulletvelocity;
    public GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");



    }

    void Update()
    {

        bulletvelocity = Player.transform.forward * speed;
        Vector3 point1 = transform.position;
        float stepsize = 0.01f;

        for (float steps = 0f; steps < 1; steps += stepsize)
        {
            bulletvelocity += Physics.gravity * stepsize;
            Vector3 point2 = point1 + bulletvelocity * stepsize;

            Ray ray = new Ray(point1, point2 - point1);
            transform.position = point2;
            if (Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                
            }
            point1 = point2;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = transform.position;
        Vector3 predictedbulletvelocity = bulletvelocity;
        float stepsize = 0.01f;
        for(float steps = 0f; steps < 1; steps += stepsize)
        {
            predictedbulletvelocity += Physics.gravity * stepsize;
            Vector3 point2 = point1 + predictedbulletvelocity * stepsize;

            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
