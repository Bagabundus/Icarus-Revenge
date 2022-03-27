using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIA : MonoBehaviour
{
    public float speed = 5;
    private PlayerDamn p;
    private float f;
    public float floatingStrength;
    public float floatingSpeed;
    void Start()
    {
        p = FindObjectOfType<PlayerDamn>();
    }

    void Update()
    {
        transform.forward = p.transform.position - transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;

        f = (f > 0) ? f - Time.deltaTime * floatingSpeed : 1;
        transform.position += new Vector3(0, Mathf.Sin(2 * Mathf.PI * f) * Time.deltaTime * floatingStrength, 0);
    }
}
