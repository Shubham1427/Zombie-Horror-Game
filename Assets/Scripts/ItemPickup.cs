using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemPickup : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public GameObject crosshair, actionCrosshair, pickUpText, buttonText, dropText, dropButtonText;
    int active = 0;
    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.5f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                crosshair.SetActive(false);
                actionCrosshair.SetActive(true);
                pickUpText.SetActive(true);
                buttonText.SetActive(true);
                active = 1;
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    if (cam.GetComponent<PickedItem>().picked == 1)
                    {
                        dropButtonText.SetActive(false);
                        dropText.SetActive(false);
                        cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.AddComponent<Rigidbody>();
                        cam.GetComponentInChildren<ItemPickup>().enabled = true;
                        cam.GetComponentInChildren<ItemPickup>().transform.parent.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -5f, 0f));
                        StartCoroutine(cam.GetComponentInChildren<ItemPickup>().RemoveRigid());
                        cam.GetComponentInChildren<ItemPickup>().transform.parent.parent = null;
                        StartCoroutine(DelayedNewPickup());
                    }       
                    else
                    {
                        dropButtonText.SetActive(true);
                        dropText.SetActive(true);
                        transform.parent.parent = cam.transform;
                        if (transform.parent.name == "Hammer(Clone)")
                        {
                            transform.parent.localPosition = new Vector3(-0.25f, -0.05f, 0.6f);
                            transform.parent.localRotation = Quaternion.Euler(0f, 10f, 90f);
                        }
                        else
                        {
                            transform.parent.localPosition = new Vector3(-0.3f, -0.25f, 0.5f);
                            transform.parent.localRotation = Quaternion.Euler(180f, -80f, 0f);
                            if (transform.parent.name == "Code(Clone)")
                                transform.parent.localRotation = Quaternion.Euler(0f, -80f, 0f);
                        }
                        crosshair.SetActive(true);
                        actionCrosshair.SetActive(false);
                        pickUpText.SetActive(false);
                        buttonText.SetActive(false);
                        active = 0;
                        cam.GetComponent<PickedItem>().picked = 1;
                        enabled = false;
                    }
                }
            }
            else if (pickUpText.activeInHierarchy && active == 1)
            {
                crosshair.SetActive(true);
                actionCrosshair.SetActive(false);
                pickUpText.SetActive(false);
                buttonText.SetActive(false);
                active = 0;
            }
        }      
        else if (pickUpText.activeInHierarchy && active == 1)
        {
            crosshair.SetActive(true);
            actionCrosshair.SetActive(false);
            pickUpText.SetActive(false);
            buttonText.SetActive(false);
            active = 0;
        }
        if (cam.GetComponent<PickedItem>().picked == 1 && Input.GetKeyDown(KeyCode.Keypad1) && dropText.activeInHierarchy)
        {
            cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.AddComponent<Rigidbody>();
            cam.GetComponentInChildren<ItemPickup>().enabled = true;
            cam.GetComponentInChildren<ItemPickup>().transform.parent.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -5f, 0f));
            dropButtonText.SetActive(false);
            dropText.SetActive(false);
            StartCoroutine(cam.GetComponentInChildren<ItemPickup>().RemoveRigid());
            cam.GetComponentInChildren<ItemPickup>().transform.parent.parent = null;
            cam.GetComponent<PickedItem>().picked = 0;
            if (zombie.GetComponent<ZombieAI>().enabled)
            {
                zombie.GetComponent<ZombieAI>().chasing = true;
                StopCoroutine(zombie.GetComponent<ZombieAI>().Idle(true));
                zombie.GetComponent<ZombieAI>().chaseIdle = false;
                zombie.GetComponent<ZombieAI>().ChasePlayer();
            }
        }
    }

    public IEnumerator DelayedNewPickup()
    {
        yield return new WaitForSeconds(0.25f);
        dropButtonText.SetActive(true);
        dropText.SetActive(true);
        transform.parent.parent = cam.transform;
        if (transform.parent.name == "Hammer(Clone)")
        {
            transform.parent.localPosition = new Vector3(-0.25f, -0.05f, 0.75f);
            transform.parent.localRotation = Quaternion.Euler(0f, 10f, 90f);
        }
        else
        {
            transform.parent.localPosition = new Vector3(-0.3f, -0.25f, 0.5f);
            transform.parent.localRotation = Quaternion.Euler(180f, -80f, 0f);
            if (transform.parent.name == "Code(Clone)")
                transform.parent.localRotation = Quaternion.Euler(0f, -80f, 0f);
        }
        cam.GetComponent<PickedItem>().picked = 1;
        enabled = false;
    }

    public IEnumerator RemoveRigid()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(transform.parent.GetComponent<Rigidbody>());
        if (zombie.GetComponent<ZombieAI>().enabled)
        {
            zombie.GetComponent<ZombieAI>().chasing = true;
            StopCoroutine(zombie.GetComponent<ZombieAI>().Idle(true));
            zombie.GetComponent<ZombieAI>().chaseIdle = false;
            zombie.GetComponent<ZombieAI>().ChasePlayer();
        }
    }
}