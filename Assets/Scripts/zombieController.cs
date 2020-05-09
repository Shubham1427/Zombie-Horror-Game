using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieController : MonoBehaviour
{
    public float speed, sprintSpeed;
    public Camera cam;
    private bool lr, rr, v, s;
    private RaycastHit hit, zomHit;
    private AudioSource zombieAudio;
    public AudioClip laugh;

    // Start is called before the first frame update
    private void Start()
    {
        zombieAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        v = Input.GetButton("P2Forward");
        lr = Input.GetButton("P2RotateLeft");
        rr = Input.GetButton("P2RotateRight");
        s = Input.GetButton("P2Sprint");
        if (Input.GetKey(KeyCode.Alpha1))
        {
            zombieAudio.clip = laugh;
            zombieAudio.Play();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ZombieAI>().enabled = true;
            GetComponent<ZombieAI>().SetActiveToClosestTarget();
            enabled = false;
        }
        if (v)
        {
            GetComponent<Animator>().SetInteger("statusCheck", 0);
            GetComponent<CharacterController>().Move(transform.forward * speed);
            GetComponent<Animator>().speed = 1.25f;
        }
        else if (s)
        {
            GetComponent<Animator>().SetInteger("statusCheck", 0);
            GetComponent<CharacterController>().Move(transform.forward * sprintSpeed);
            GetComponent<Animator>().speed = 2.75f;
        }
        else
        {
            GetComponent<Animator>().SetInteger("statusCheck", -1);
            GetComponent<Animator>().speed = 1f;
        }
        if (lr)
        {
            transform.Rotate(new Vector3(0f, -Time.deltaTime * 125f, 0f));
        }
        else if (rr)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * 125f, 0f));
        }
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, out zomHit, 1.5f))
        {
            if (zomHit.collider.gameObject.GetComponent<DoorOpen>() != null)
            {
                hit = zomHit;
                if (!zomHit.collider.gameObject.GetComponent<DoorOpen>().CheckZomText())
                    zomHit.collider.gameObject.GetComponent<DoorOpen>().ShowZomText();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    zomHit.collider.gameObject.GetComponent<DoorOpen>().CloseZomText();
                    zomHit.collider.gameObject.GetComponent<DoorOpen>().ZomOpenCloseDoor();
                }
            }
            else if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<DoorOpen>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<DoorOpen>().CheckZomText())
                        hit.collider.gameObject.GetComponent<DoorOpen>().CloseZomText();
                }
            }
            if (zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>() != null)
            {
                hit = zomHit;
                if (zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().locked == 0)
                {
                    if (!zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().CheckZomText())
                        zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().ShowZomText();
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().CloseZomText();
                        zomHit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().ZomOpenCloseDoor();
                    }
                }
            }
            else if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<PlayHouseDoorOpen>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().CheckZomText())
                        hit.collider.gameObject.GetComponent<PlayHouseDoorOpen>().CloseZomText();
                }
            }
        }
        else if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<DoorOpen>() != null)
            {
                if (hit.collider.gameObject.GetComponent<DoorOpen>().CheckZomText())
                    hit.collider.gameObject.GetComponent<DoorOpen>().CloseZomText();
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (GetComponent<CharacterController>().collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(GetComponent<CharacterController>().velocity * 0.25f, hit.point, ForceMode.Impulse);
    }
}