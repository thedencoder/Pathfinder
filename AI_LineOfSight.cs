using UnityEngine;

public class AI_LineOfSight : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float sightRange = 20f;

    void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            target = enemy.transform;
        }
    }

    private bool hitCheck(Vector3 directionToTarget)
    {
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
        if (target == null)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

            if (enemy == null)
            {
                Debug.LogWarning("No enemies left.");
                return false;
            }

            target = enemy.transform;
        }

        Vector3 directionToTarget = target.position - transform.position;

        if (Physics.Raycast(transform.position, directionToTarget, sightRange)
            && hitCheck(directionToTarget))
        {
            return true;
        }

        return false;
    }
}
