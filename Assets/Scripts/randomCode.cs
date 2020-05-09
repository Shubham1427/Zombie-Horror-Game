using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class randomCode : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
        GetComponentInChildren<TextMeshPro>().text = "" + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
    }
}
