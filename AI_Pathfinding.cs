using UnityEngine;
public class AI_Pathfinding : MonoBehaviour
{
    [SerializeField] private String targetTag = "Ignore";
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent= GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    void DecisionMaking()
    {
        if(targetTag==="Ignore")
        {
            targetTag = "Enemy";
        }
        else if(targetTag==="Enemy")
        {
            targetTag = "Ignore";
        }

        else if(targetTag==="Target")
        {
            targetTag = "Ignore";
        }
        else
        {
            Debug.LogError("Invalid target tag. Please use 'Ignore', 'Cover', or 'Rush'.");
        }

        if (GetComponent<AI_LineOfSight>().CanSeeTarget())
        {
            Debug.Log("I can see the target!");
        }
        else
        {
            Debug.Log("I cannot see the target!");
        }
    }
    void Update()
    {
        DecisionMaking();
        agent.SetDestination(GameObject.FindGameObjectWithTag("Target").transform.position);
    

    }
}
