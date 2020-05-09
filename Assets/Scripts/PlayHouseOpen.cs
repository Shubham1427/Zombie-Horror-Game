using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHouseOpen : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public GameObject crosshair, actionCrosshair, UseText, buttonText, hinge, playHouseLock, playHouseLockHandle, door, zombie;
    public GameObject[] playHouseLockColliders;
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
            if (hit.collider.gameObject == gameObject && cam.GetComponent<PickedItem>().picked == 1 && cam.GetComponentInChildren<ItemPickup>().transform.parent.gameObject.name == "PlayHouse Key(Clone)")
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
                    hinge.GetComponent<Animation>().Play();
                    //playHouseLockHandle.SetActive(false);
                    playHouseLock.transform.position = new Vector3(playHouseLock.transform.position.x, playHouseLock.transform.position.y - 0.05f, playHouseLock.transform.position.z);
                    for (int i = 0; i < playHouseLockColliders.Length; i++)
                        playHouseLockColliders[i].GetComponent<BoxCollider>().enabled = true;
                    playHouseLock.transform.parent = null;
                    playHouseLock.AddComponent<Rigidbody>();
                    StartCoroutine(RemoveRigid());
                    active = 0;
                    door.GetComponent<PlayHouseDoorOpen>().locked = 0;
                    if (zombie.GetComponent<ZombieAI>().enabled)
                    {
                        zombie.GetComponent<ZombieAI>().chasing = true;
                        StopCoroutine(zombie.GetComponent<ZombieAI>().Idle(true));
                        zombie.GetComponent<ZombieAI>().chaseIdle = false;
                        zombie.GetComponent<ZombieAI>().ChasePlayer();
                    }
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

    IEnumerator RemoveRigid ()
    {
        yield return new WaitForSeconds(3f);
        Destroy(playHouseLock.GetComponent<Rigidbody>());
    }
}
