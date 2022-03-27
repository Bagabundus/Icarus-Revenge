using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADamnWraith : MonoBehaviour
{
    public float speed = 5;
    public int Health = 10;
    private PlayerDamn p;
    private float f;
    public float floatingStrength;
    public float floatingSpeed;
    private Animator anim;
    private int maxLife;

    void Start()
    {
        p = FindObjectOfType<PlayerDamn>();
        anim = GetComponent<Animator>();
        maxLife = Health;
    }

    void Update()
    {
        transform.forward = p.transform.position - transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;

        f = (f > 0) ? f - Time.deltaTime * floatingSpeed : 1;
        transform.position += new Vector3(0, Mathf.Sin(2 * Mathf.PI * f) * Time.deltaTime * floatingStrength, 0);
    }
    public void takeDamages(int i)
    {
        Health = Mathf.Clamp(Health - i, 0, 999);
        if (Health == 0)
        {
            anim.Play("Death");
        }
    }
    public void FullLife()
    {
        Health = maxLife;
    }
    public void Restock()
    {
        
        PoolWraith.instance.Restock(GetComponent<IADamnWraith>());
    }


}
