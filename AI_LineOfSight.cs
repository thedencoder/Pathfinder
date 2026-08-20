using UnityEngine;

public class AI_LineOfSight : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float sightRange = 20f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
    }   

    private bool hitCheck(Vector3 directionToTarget){
        if (Physics.Raycast(
        transform.position,
        directionToTarget.normalized,
        out RaycastHit hit,
        sightRange))
        {
        return hit.transform.CompareTag("Enemy");
        }
        else
        {
            return false;
        }
    }

    public bool CanSeeTarget()
    {
        Vector3 directionToTarget = target.position - transform.position;
        if(Physics.Raycast(transform.position, directionToTarget, sightRange)&&hitCheck(directionToTarget))
        {
             
             return true;
        }
        return false;   
    }
}
