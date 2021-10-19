using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

    Transform target;
    NavMeshAgent agent;

    public float movementSpeed = 8f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
    }

    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        //agent.speed = movementSpeed;
        agent.SetDestination(point);
    }

    public void Warp(Vector3 point)
    {
        //GetComponent<Rigidbody>().AddForce(point);
        //agent.SetDestination(point);
        //agent.Move(point - transform.position);
        agent.Warp(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        //agent.speed = movementSpeed;
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;

        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
