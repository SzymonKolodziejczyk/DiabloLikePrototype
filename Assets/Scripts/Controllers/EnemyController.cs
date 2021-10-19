using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    NavMeshAgent agent;
    Transform target;
    CharacterCombat combat;

    public float lookRadius = 10f;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        //agent.updatePosition = false;
        //agent.updateRotation = false;
        //https://answers.unity.com/questions/889836/using-unity-navigation-in-a-fixed-timestep.html
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //GetComponent<Rigidbody>().velocity = agent.desiredVelocity;

            if(distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    combat.Attack(targetStats);
                }
                FaceTarget();
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
