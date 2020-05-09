using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLock : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public GameObject crosshair, actionCrosshair, UseText, buttonText, cylLight;
    public Material green;
    public Light codeLockLight;
    GameObject code;
    int active = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
            if (hit.collider.gameObject == gameObject && cam.GetComponent<PickedItem>().picked == 1 && cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.name == "Code(Clone)")
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
                    code = cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject;
                    code.transform.parent = null;
                    code.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    code.transform.parent = transform;
                    code.transform.localPosition = new Vector3(0f, 0f, 0.25f);
                    codeLockLight.color = Color.green;
                    cylLight.GetComponent<MeshRenderer>().material = green;
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
