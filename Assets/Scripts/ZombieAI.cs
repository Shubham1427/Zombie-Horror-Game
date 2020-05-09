using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float speed, sprintSpeed;
    public GameObject[] targets;
    public int active = 0;
    public bool idle = false, rotSet = false, rotating = true, chasing = false, chaseIdle = false, done = false, posSet = false;
    public float ToRotY, currRot;
    GameObject prevTarget;
    public GameObject player;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        prevTarget = targets[0];
        FindNextVisibleTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<zombieController>().enabled = true;
            enabled = false;
        }
        if (Vector3.Angle(transform.forward, (player.transform.position - transform.position).normalized) < 85f && (player.transform.position - transform.position).magnitude < 20f && !Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), (player.transform.position - transform.position).normalized, (player.transform.position - transform.position).magnitude) && !player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().hidden)
        {
            if (!chasing)
            {
                StopCoroutine(Idle(true));
                chaseIdle = false;
                chasing = true;
            }
            ChasePlayer();            
            return;
        }
        else if (chasing)
        {
            ChaseDrop();
            return;
        }
        if (idle == false && rotating == false)
        {
            if (ReachedTarget(targets[active].transform.position))
            {
                idle = true;
                StartCoroutine(Idle(false));
                return;
            }
            Walk();
        }
        else if (rotating == true)
        {
            RotateTowardsTarget(targets[active].transform.position);
            Walk();
        }
        else
        {
            GetComponent<Animator>().SetInteger("statusCheck", -1);
            GetComponent<Animator>().speed = 1f;
        }
    }

    public void FindNextVisibleTarget()
    {
        GameObject[] availableTargets = new GameObject[targets[active].GetComponent<ZombieAITarget>().connections.Length];
        for (int i=0; i < availableTargets.Length; i++)
        {
            availableTargets[i] = targets[active].GetComponent<ZombieAITarget>().connections[i];
        }
        int next = Random.Range(0, availableTargets.Length);
        for (int i=0; i< availableTargets.Length; i++)
        {
            if (prevTarget == availableTargets[next])
            {
                if (next == availableTargets.Length-1)
                    next = 0;
                else
                    next++;
                break;
            }
        }
        for (int i=0; i< targets.Length; i++)
        {
            if (targets[i] == availableTargets[next])
            {
                prevTarget = targets[active];
                active = i;
                return;
            }
        }
    }

    void ChaseDrop()
    {
        if (!posSet)
        {
            pos = player.transform.position;
            posSet = true;
        }
        if ((transform.position - pos).magnitude <= 2f)
        {
            GetComponent<Animator>().SetInteger("statusCheck", -1);
            GetComponent<Animator>().speed = 1f;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            if (!chaseIdle)
            {
                StartCoroutine(Idle(true));
                chaseIdle = true;
                idle = true;
            }
            chasing = false;
            GetComponent<CharacterController>().enabled = true;
            posSet = false;
            return;
        }
        GetComponent<Animator>().SetInteger("statusCheck", 0);
        GetComponent<Animator>().speed = 2.5f;
        GetComponent<NavMeshAgent>().angularSpeed = 1000f;
        GetComponent<NavMeshAgent>().destination = pos;       
    }

    public void ChasePlayer()
    {
        if ((transform.position-player.transform.position).magnitude < 1f)
        {
            GetComponent<CharacterController>().enabled = true;
        }
        GetComponent<CharacterController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Animator>().SetInteger("statusCheck", 0);
        GetComponent<Animator>().speed = 2.5f;
        GetComponent<NavMeshAgent>().angularSpeed = 1000f;
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        posSet = false;
    }

    void RotateTowardsTarget(Vector3 targ)
    {
        if (rotSet == false)
        {
            rotSet = true;
            currRot = transform.rotation.eulerAngles.y;
            if (transform.InverseTransformVector(Vector3.ProjectOnPlane((targ - transform.position).normalized, Vector3.up)).x >= 0f)
            {
                ToRotY = currRot + Vector3.Angle(transform.forward, Vector3.ProjectOnPlane((targ - transform.position).normalized, Vector3.up));
                if (ToRotY > 360f)
                {
                    ToRotY -= 360f;
                    currRot -= 360f;
                }
            }
            else
            {
                ToRotY = currRot - Vector3.Angle(transform.forward, Vector3.ProjectOnPlane((targ - transform.position).normalized, Vector3.up));
                if (ToRotY < -360f)
                {
                    ToRotY += 360f;
                    currRot += 360f;
                }
            }
        }
        if (Mathf.Abs(ActualRotationY() - ToRotY) > 0.5f)
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Lerp(currRot, ToRotY, Mathf.InverseLerp(currRot, ToRotY, ActualRotationY()) + 0.05f), 0f);
            rotating = true;
        }
        else
            rotating = false;
    }

    float ActualRotationY()
    {
        float v = transform.rotation.eulerAngles.y;
        if ((v >= currRot && v <= ToRotY) || (v <= currRot && v >= ToRotY))
        {
            return v;
        }
        else if (((v - 360) >= currRot && (v - 360) <= ToRotY) || ((v - 360) <= currRot && (v - 360) >= ToRotY))
        {
            v -= 360f;
            return v;
        }
        else if (((v + 360) >= currRot && (v + 360) <= ToRotY) || ((v + 360) <= currRot && (v + 360) >= ToRotY))
        {
            v += 360f;
            return v;
        }
        else
            return ToRotY;
    }

    void Walk()
    {
        GetComponent<Animator>().SetInteger("statusCheck", 0);
        GetComponent<CharacterController>().Move(transform.forward * speed);
        GetComponent<Animator>().speed = 1.25f;
    }

    bool ReachedTarget(Vector3 targ)
    {
        if ((transform.position - targ).magnitude < 0.5f)
            return true;
        else
            return false;
    }

    public IEnumerator Idle(bool chaseCalled)
    {
        if (targets[active].GetComponent<ZombieAITarget>().connections.Length == 2)
            yield return null;
        if (!chaseCalled && targets[active].GetComponent<ZombieAITarget>().connections.Length != 2)
            yield return new WaitForSeconds (2.5f);
        else if (chaseCalled)
            yield return new WaitForSeconds(5f);
        rotSet = false;
        if (!chaseCalled)
            FindNextVisibleTarget();
        idle = false;
        rotating = true;
        if (chaseCalled)
        {
            SetActiveToClosestTarget();
            if (chaseIdle)
                chaseIdle = false;
        }
    }

    public void SetActiveToClosestTarget()
    {
        int act = 0;
        float min = (targets[0].transform.position - transform.position).magnitude;
        for (int i=0; i<targets.Length; i++)
        {
            if ((targets[i].transform.position - transform.position).magnitude <= min)
            {
                min = (targets[i].transform.position - transform.position).magnitude;
                act = i;
            }
        }
        rotSet = false;
        rotating = true;
        idle = false;
        active = act;
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
        body.AddForceAtPosition(GetComponent<CharacterController>().velocity * 0.5f, hit.point, ForceMode.Impulse);
    }
}
