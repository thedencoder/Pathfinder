using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 0.2f;
    private float verticalLookRotation = 0f;

    [Header("Shooting")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float shootRange = 100f;
    [SerializeField] private float fireRate = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunshotSound;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;

    private float nextFireTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        LookAround();

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            
            Shoot();
        }
    }

    void Move()
    {
        if (Keyboard.current == null)
            return;

        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1f;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1f;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1f;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1f;

        Vector3 movement =
            transform.right * input.x +
            transform.forward * input.y;

        movement.y = 0f;
        movement = movement.normalized;

        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    void LookAround()
    {
        if (Mouse.current == null || playerCamera == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // Rotate the whole player left/right
        transform.Rotate(0f, mouseX, 0f);

        // Rotate only the camera up/down
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(
            verticalLookRotation,
            -90f,
            90f
        );

        playerCamera.transform.localRotation =
            Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }

    void Shoot()
    {
        if (playerCamera == null)
            return;

        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / fireRate;

        if (Physics.Raycast(
            playerCamera.transform.position,
            playerCamera.transform.forward,
            out RaycastHit hit,
            shootRange))
        {
            Debug.Log("Hit: " + hit.transform.name);

            Health health = hit.transform.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        if (audioSource != null && gunshotSound != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(gunshotSound);
        }
    }
}