using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FpsIndicator : MonoBehaviour
{
    public Text txt;

    private void Update()
    {
       
        txt.text = (1 / Time.deltaTime).ToString();
    }
}
