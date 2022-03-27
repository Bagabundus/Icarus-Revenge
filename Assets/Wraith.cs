using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Wraith : MonoBehaviour
{
    public enum IAState {alive, ATK , dead}
    public IAState currentState;
    public int life;
    public float speed = 2;
    public float floatingSpeed = 5;
    public float floatingStrength = 0.2f;
    public int damages;
    private int maxLife;
    private Animator anim;
    private float f = 1;


    [Space(20)]
    public PlayerDamn p;


    public void Awake()
    {
        maxLife = life;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(currentState == IAState.alive)
        {
         Chase();
        }
    }
    public void Chase()
    {
        //go to player
        transform.forward = p.transform.position - transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;
        //floating
        f = (f > 0) ? f - Time.deltaTime * floatingSpeed : 1;
        transform.position += new Vector3(0, Mathf.Sin(2 * Mathf.PI * f) * Time.deltaTime * floatingStrength, 0);


        if (Vector3.Distance(transform.position,p.transform.position) < 1)
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
       //PoolWraith.instance.Restock(this);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
           //p.takeDamages(damages);
        }
    }
}
