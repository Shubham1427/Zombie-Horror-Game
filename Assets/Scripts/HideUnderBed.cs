using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUnderBed : MonoBehaviour
{
    public GameObject crosshair, actionCrosshair, hideUnderBedText, buttonText, getOutText;
    public Camera cam;
    public int active = 0;
    RaycastHit hit;
    Vector3 pos;
    bool hidden = false;

    private void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1.5f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                active = 1;
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                if (!hidden)
                    hideUnderBedText.SetActive(true);
                else
                    getOutText.SetActive(true);
                buttonText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    crosshair.SetActive(true);
                    actionCrosshair.SetActive(false);
                    buttonText.SetActive(false);
                    if (!hidden)
                    {
                        hideUnderBedText.SetActive(false);
                        pos = cam.transform.parent.position;
                        cam.transform.parent.position = transform.parent.position - Vector3.up;
                        Destroy(cam.transform.parent.GetComponent<Rigidbody>());
                        cam.transform.parent.GetComponent<CharacterController>().enabled = false;
                        cam.transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().hidden = true;
                        hidden = true;
                        active = 0;
                    }
                    else
                    {
                        getOutText.SetActive(false);
                        cam.transform.parent.position = pos + Vector3.up * 0.75f;
                        cam.transform.parent.gameObject.AddComponent<Rigidbody>();
                        cam.transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().UnHide();                                            
                        hidden = false;
                        active = 0;
                    }
                }
            }
            else if ((hideUnderBedText.activeInHierarchy || getOutText.activeInHierarchy) && active == 1)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                hideUnderBedText.SetActive(false);
                buttonText.SetActive(false);
                getOutText.SetActive(false);
                active = 0;
            }
        }
        else if ((hideUnderBedText.activeInHierarchy || getOutText.activeInHierarchy) && active == 1)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            hideUnderBedText.SetActive(false);
            buttonText.SetActive(false);
            getOutText.SetActive(false);
            active = 0;
        }
    }
}
