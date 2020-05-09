﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    public GameObject flashLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (flashLight.activeInHierarchy)
                flashLight.SetActive(false);
            else
                flashLight.SetActive(true);
        }
    }
}