using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    //Camera mCamera;
    PlayerDamn p;
    // Start is called before the first frame update
    void Start()
    {
        //mCamera = Camera.main;
        p = FindObjectOfType<PlayerDamn>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(p.transform.position);
        //transform.forward = mCamera.transform.forward;
    }
}
