using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject crosshair, actionCrosshair, doorOpenText, buttonText, hinge, zom, zomButtonText, zomDoorOpenText, doorCloseText, zomDoorCloseText;
    private RaycastHit hit, zomHit;
    public Camera cam;
    public int active = 0, zomActive = 0, opened = 0;

    private void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                if (opened == 0)
                    doorOpenText.SetActive(true);
                else
                    doorCloseText.SetActive(true);
                buttonText.SetActive(true);
                active = 1;
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    crosshair.SetActive(true);
                    actionCrosshair.SetActive(false);
                    doorOpenText.SetActive(false);
                    doorCloseText.SetActive(false);
                    buttonText.SetActive(false);
                    if (opened == 0)
                    {
                        hinge.GetComponent<Animation>().Play("DoorOpenAnim");
                        opened = 1;
                    }
                    else
                    {
                        hinge.GetComponent<Animation>().Play("DoorCloseAnim");
                        opened = 0;
                    }
                    GetComponent<AudioSource>().Play();
                    active = 0;
                }
            }
            else if ((doorOpenText.activeInHierarchy || doorCloseText.activeInHierarchy) && active == 1)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                doorOpenText.SetActive(false);
                doorCloseText.SetActive(false);
                buttonText.SetActive(false);
                active = 0;
            }
        }
        else if ((doorOpenText.activeInHierarchy || doorCloseText.activeInHierarchy) && active == 1)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            doorOpenText.SetActive(false);
            doorCloseText.SetActive(false);
            buttonText.SetActive(false);
            active = 0;
        }
        else if (zom.GetComponent<ZombieAI>().enabled)
        {
            if (Physics.Raycast(zom.transform.position + Vector3.up * 1.5f, zom.transform.forward, out zomHit, 4f))
            {
                if (zomHit.collider.gameObject == gameObject)
                {
                    if (opened == 0)
                    {
                        hinge.GetComponent<Animation>().Play("DoorOpenAnim");
                        opened = 1;
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
        }
    }

    public void ShowZomText()
    {
        if (opened == 0)
            zomDoorOpenText.SetActive(true);
        else
            zomDoorCloseText.SetActive(true);
        zomButtonText.SetActive(true);
        zomActive = 1;
    }

    public bool CheckZomText()
    {
        if ((zomDoorOpenText.activeInHierarchy || zomDoorCloseText.activeInHierarchy) && zomActive == 1)
            return true;
        else return false;
    }

    public void CloseZomText()
    {
        zomDoorOpenText.SetActive(false);
        zomDoorCloseText.SetActive(false);
        zomButtonText.SetActive(false);
        zomActive = 0;
    }

    public void ZomOpenCloseDoor()
    {
        if (opened == 0)
        {
            hinge.GetComponent<Animation>().Play("DoorOpenAnim");
            opened = 1;
        }
        else
        {
            hinge.GetComponent<Animation>().Play("DoorCloseAnim");
            opened = 0;
        }
        GetComponent<AudioSource>().Play();
    }
}