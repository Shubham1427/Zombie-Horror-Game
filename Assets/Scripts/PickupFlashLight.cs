using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFlashLight : MonoBehaviour
{
    public GameObject crosshair, actionCrosshair, pickupText, buttonText;
    RaycastHit hit;
    public Camera cam;

    private void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1.5f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                pickupText.SetActive(true);
                buttonText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    crosshair.SetActive(true);
                    actionCrosshair.SetActive(false);
                    pickupText.SetActive(false);
                    buttonText.SetActive(false);
                    cam.GetComponentInChildren<FlashLightControl>(true).gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    enabled = false;
                }
            }
            else if (pickupText.activeInHierarchy)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                pickupText.SetActive(false);
                buttonText.SetActive(false);
            }
        }
        else if (pickupText.activeInHierarchy)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            pickupText.SetActive(false);
            buttonText.SetActive(false);
        }
    }
}
