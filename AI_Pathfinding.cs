using UnityEngine;
public class AI_Pathfinding : MonoBehaviour
{
    [SerializeField] private string targetTag = "Ignore";
    private GameObject currentTarget;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentTarget = GameObject.FindGameObjectWithTag("Target");

    }

    void RunAway()
    {
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

            Vector3 fleeDirection =
                transform.position - enemy.transform.position;

            Vector3 desiredPosition =
                transform.position + fleeDirection.normalized * 10f;

            UnityEngine.AI.NavMeshHit hit;

            if (UnityEngine.AI.NavMesh.SamplePosition(
                desiredPosition,
                out hit,
                5f,
                UnityEngine.AI.NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }

    void DecisionMaking()
    {
        //note: for optimization see if we can cache the LOS data so we dont have to do the raycast every frame.
        if (GetComponent<AI_LineOfSight>().CanSeeTarget())
        {
            // Debug.Log("I can see the target!");
            if (targetTag == "Ignore")
            {
                currentTarget = GameObject.FindGameObjectWithTag("Target");
                agent.SetDestination(currentTarget.transform.position);
            }
            else if (targetTag == "Rush")
            {
                currentTarget = GameObject.FindGameObjectWithTag("Enemy");
                agent.SetDestination(currentTarget.transform.position);
            }

            else if (targetTag == "Cover")
            {
                currentTarget = GameObject.FindGameObjectWithTag("Cover");
                agent.SetDestination(currentTarget.transform.position);
            }
            else if (targetTag == "StandStill")
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
            else if (targetTag == "Afraid")
            {
                RunAway();
            }
            else
            {
                Debug.LogError("Invalid target tag. Please use 'Ignore', 'Cover', or 'Rush'.");
                currentTarget = null;
            }
        }
        else
        {
            // Debug.Log("I cannot see the target!");
            currentTarget = GameObject.FindGameObjectWithTag("Target");
            agent.SetDestination(currentTarget.transform.position);
        }
    }


    void Update()
    {
        DecisionMaking();
        


    }
}
