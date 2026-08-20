using UnityEngine;
public class AI_Pathfinding : MonoBehaviour
{
    [SerializeField] private string targetTag = "Ignore";
    private GameObject currentTarget;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent= GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentTarget = GameObject.FindGameObjectWithTag("Target");
        
    }

    void DecisionMaking()
    {
        //note: for optimization see if we can cache the LOS data so we dont have to do the raycast every frame.
        if (GetComponent<AI_LineOfSight>().CanSeeTarget())
        {
            Debug.Log("I can see the target!");
             if(targetTag=="Ignore")
        {
            currentTarget = GameObject.FindGameObjectWithTag("Target");
        }
        else if(targetTag=="Rush")
        {
            currentTarget = GameObject.FindGameObjectWithTag("Enemy");
        }

        else if(targetTag=="Cover")
        {
            currentTarget = GameObject.FindGameObjectWithTag("Cover");
        }
        else
        {
            Debug.LogError("Invalid target tag. Please use 'Ignore', 'Cover', or 'Rush'.");
            currentTarget = null;
        }
        }
        else
        {
            Debug.Log("I cannot see the target!");
            currentTarget = GameObject.FindGameObjectWithTag("Target");
        }
    }

    
    void Update()
    {
        DecisionMaking();
        agent.SetDestination(currentTarget.transform.position);
    

    }
}
