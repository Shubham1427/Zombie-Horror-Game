using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom_light_on_off : MonoBehaviour
{
    public Light zomLight;
    public bool isZombie;

    private void OnPreRender()
    {
        if (!isZombie)
            zomLight.enabled = true;
        else
            zomLight.enabled = false;
    }
}