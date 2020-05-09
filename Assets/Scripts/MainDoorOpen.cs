using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDoorOpen : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public GameObject crosshair, actionCrosshair, UseText, buttonText, hammerLock, codeLock, padLock;
    int active = 0, allLocksOpened = 0;

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
        if (hammerLock.GetComponent<HammerLock>().enabled == false && codeLock.GetComponent<CodeLock>().enabled == false && padLock.GetComponent<PadLockOpen>().enabled == false)
            allLocksOpened = 1;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1.5f))
        {
            if (hit.collider.gameObject == gameObject && cam.GetComponent<PickedItem>().picked == 1 && cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.name == "Master Key(Clone)" && allLocksOpened == 1)
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
                    active = 0;
                    SceneManager.LoadScene(2);
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
