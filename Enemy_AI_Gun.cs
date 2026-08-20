using UnityEngine;

public class Enemy_AI_Gun : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    private AI_LineOfSight lineOfSight;
    private Transform target;
    [SerializeField] private float minPitch = 0.7f;
    [SerializeField] private float maxPitch = 1.2f;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 1f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip gunshotSound;

    private float nextFireTime;

    void Start()
    {
        lineOfSight = GetComponent<AI_LineOfSight>();
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (lineOfSight.CanSeeTarget())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        if (target == null)
        {
            GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

            if (targetObject == null)
            {
                Debug.LogWarning("No target found.");
                return;
            }

            target = targetObject.transform;
        }

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0f;

        // Face the enemy
        transform.rotation = Quaternion.LookRotation(directionToTarget);

        nextFireTime = Time.time + 1f / fireRate;

        Debug.DrawRay(
            transform.position,
            transform.forward * 20f,
            Color.red,
            1f
        );

        Health health = target.GetComponent<Health>();


        if (health != null)
        {
            health.TakeDamage(damage);
        }
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(gunshotSound);
        audioSource.PlayOneShot(gunshotSound);
        Debug.Log("NPC fired!");
    }
}