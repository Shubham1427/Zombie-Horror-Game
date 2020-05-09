using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawerOpen : MonoBehaviour
{
    public GameObject crosshair, actionCrosshair, drawerOpenText, buttonText, hinge;
    RaycastHit hit;
    public Camera cam;
    int active = 0;

    private void Update()
    {
        if (cam == null)
        {
            if (GameObject.Find("MyPlayer") == null)
                return;
            else
                cam = GameObject.Find("MyPlayer").GetComponentInChildren<Camera>();
        }
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1.5f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                drawerOpenText.SetActive(true);
                buttonText.SetActive(true);
                active = 1;
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    crosshair.SetActive(true);
                    actionCrosshair.SetActive(false);
                    drawerOpenText.SetActive(false);
                    buttonText.SetActive(false);
                    hinge.GetComponent<Animation>().Play();
                    GetComponent<AudioSource>().Play();
                    active = 0;
                    enabled = false;
                }
            }
            else if (drawerOpenText.activeInHierarchy && active == 1)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                drawerOpenText.SetActive(false);
                buttonText.SetActive(false);
                active = 0;
            }
        }
        else if (drawerOpenText.activeInHierarchy && active == 1)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            drawerOpenText.SetActive(false);
            buttonText.SetActive(false);
            active = 0;
        }
    }

}
