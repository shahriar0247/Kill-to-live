using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie : MonoBehaviour
{
    public GameObject Player;
    NavMeshAgent agent;
    public GameObject Zom_grap;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = Zom_grap.GetComponent<Animator>();
        anim.SetInteger("State", 2);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.transform.position);

    }
}
