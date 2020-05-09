using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogWheelUse : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public GameObject crosshair, actionCrosshair, UseText, buttonText, other, hinge;
    public GameObject cogWheel;
    int active = 0;
    public bool isSkin;
    string s;

    // Start is called before the first frame update
    void Start()
    {
        if (isSkin)
        {
            s = "Cog Wheel 1(Clone)";
        }
        else
            s = "Cog Wheel(Clone)";
    }

    // Update is called once per frame
    void Update()
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
            if (hit.collider.gameObject == gameObject && cam.GetComponent<PickedItem>().picked == 1 && cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.name == s)
            {
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                UseText.SetActive(true);
                buttonText.SetActive(true);
                active = 1;
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    crosshair.SetActive(true);
                    actionCrosshair.SetActive(false);
                    UseText.SetActive(false);
                    buttonText.SetActive(false);
                    cogWheel = cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject;
                    cogWheel.transform.parent = null;
                    cogWheel.transform.position = transform.position - Vector3.forward * 0.05f;
                    cogWheel.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                    if (other.GetComponent<CogWheelUse>().enabled == false)
                    {
                        cogWheel.GetComponent<Animation>().Play();
                        other.GetComponent<CogWheelUse>().cogWheel.GetComponent<Animation>().Play();
                        hinge.GetComponent<Animation>().Play();
                    }
                    cam.GetComponent<PickedItem>().picked = 0;
                    active = 0;
                    enabled = false;
                }
            }
            else if (UseText.activeInHierarchy && active == 1)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                UseText.SetActive(false);
                buttonText.SetActive(false);
                active = 0;
            }
        }
        else if (UseText.activeInHierarchy && active == 1)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            UseText.SetActive(false);
            buttonText.SetActive(false);
            active = 0;
        }
    }
}
