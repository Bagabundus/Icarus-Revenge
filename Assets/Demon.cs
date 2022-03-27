using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Demon : MonoBehaviour
{
    public enum IAState { alive, dead }
    public IAState currentState;
    public int life;
    private int maxLife;
    private Animator anim;
    private NavMeshAgent agent;

    [Space(20)]
    public PlayerDamn p;

    public void Awake()
    {
        maxLife = life;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (currentState == IAState.alive)
        {
            Chase();
        }
    }
    public void Chase()
    {
        agent.SetDestination(p.transform.position);

        if (Vector3.Distance(transform.position, p.transform.position) < 1)
        {
            anim.Play("ATK"); //anim + box + stateATK -box + stateAlive;
        }
    }


    public void FullLife()
    {
        life = maxLife;
    }
    public void takeDamages(int i)
    {
        life -= i;
        if (life <= 0)
        {
            anim.Play("Death");// anim + statedead + death()
        }
    }


    public void Death()
    {
        PoolDemons.instance.Restock(this);      
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            //p.takeDamages(damages);
        }
    }

}
