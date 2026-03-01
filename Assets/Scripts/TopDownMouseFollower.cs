using UnityEngine;

/// <summary>
/// Controls a 2D top-down character that continuously moves toward the mouse cursor,
/// using directional sprite animation with 8 directions and 2 frames each.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class TopDownMouseFollower : MonoBehaviour
{
    [Header("Sprites")]
    [Tooltip("First frame sprites for each of the 8 directions (clockwise from North)")]
    public Sprite[] frame1Sprites = new Sprite[8];

    [Tooltip("Second frame sprites for each of the 8 directions (clockwise from North)")]
    public Sprite[] frame2Sprites = new Sprite[8];

    [Header("Animation")]
    [Tooltip("Time in seconds between animation frame swaps")]
    public float animationSpeed = 0.15f;

    [Header("Movement")]
    [Tooltip("Movement speed in units per second")]
    public float moveSpeed = 3f;

    // Internal component references
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    // Animation state
    private float animationTimer = 0f;
    private bool useFrame2 = false;

    // Direction state
    private int currentDirectionIndex = 0;

    // Direction index mapping (clockwise from North):
    // 0 = North (Up)
    // 1 = North-East
    // 2 = East (Right)
    // 3 = South-East
    // 4 = South (Down)
    // 5 = South-West
    // 6 = West (Left)
    // 7 = North-West

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("TopDownMouseFollower: No main camera found in the scene.");
        }
    }

    private void Start()
    {
        ValidateSpriteArrays();
    }

    private void Update()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();

        MoveTowardMouse(mouseWorldPosition);
        UpdateDirectionIndex(mouseWorldPosition);
        UpdateAnimation();
    }

    /// <summary>
    /// Converts the mouse screen position to world space.
    /// </summary>
    /// <returns>Mouse position in world coordinates as Vector2.</returns>
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Ensure the z-depth matches the camera's perspective
        mouseScreenPosition.z = Mathf.Abs(mainCamera.transform.position.z);

        return mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    }

    /// <summary>
    /// Moves the character toward the mouse cursor each frame.
    /// </summary>
    /// <param name="mouseWorldPosition">The mouse's current world position.</param>
    private void MoveTowardMouse(Vector2 mouseWorldPosition)
    {
        Vector2 currentPosition = transform.position;
        Vector2 directionToMouse = (mouseWorldPosition - currentPosition).normalized;

        // Apply movement using deltaTime for frame-rate independence
        transform.position += (Vector3)(directionToMouse * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Calculates the direction index (0-7) based on the angle toward the mouse.
    /// Uses clockwise ordering starting from North (Up).
    /// </summary>
    /// <param name="mouseWorldPosition">The mouse's current world position.</param>
    private void UpdateDirectionIndex(Vector2 mouseWorldPosition)
    {
        Vector2 currentPosition = transform.position;
        Vector2 directionVector = mouseWorldPosition - currentPosition;

        // Atan2 returns angle in radians; convert to degrees
        // Note: Atan2(x, y) is used instead of Atan2(y, x) to measure
        // angle clockwise from North (Up) rather than counter-clockwise from East
        float angleInDegrees = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;

        // Normalize angle to 0-360 range
        if (angleInDegrees < 0f)
        {
            angleInDegrees += 360f;
        }

        // Add 22.5-degree offset to center each 45-degree sector,
        // then divide by 45 and apply modulo 8 to get an index from 0-7
        int directionIndex = Mathf.FloorToInt((angleInDegrees + 22.5f) / 45f) % 8;

        currentDirectionIndex = directionIndex;
    }

    /// <summary>
    /// Advances the animation timer and swaps between frame1 and frame2
    /// sprites for the current direction when the timer threshold is reached.
    /// </summary>
    private void UpdateAnimation()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;
            useFrame2 = !useFrame2;
        }

        // Select the appropriate sprite array based on the current frame toggle
        Sprite[] currentFrameArray = useFrame2 ? frame2Sprites : frame1Sprites;

        // Safety check to avoid index-out-of-range errors
        if (currentFrameArray != null && currentDirectionIndex < currentFrameArray.Length)
        {
            Sprite targetSprite = currentFrameArray[currentDirectionIndex];

            if (targetSprite != null)
            {
                spriteRenderer.sprite = targetSprite;
            }
        }
    }

    /// <summary>
    /// Validates that the sprite arrays are populated and sized correctly.
    /// Logs warnings for any missing sprites.
    /// </summary>
    private void ValidateSpriteArrays()
    {
        if (frame1Sprites == null || frame1Sprites.Length != 8)
        {
            Debug.LogWarning("TopDownMouseFollower: frame1Sprites should contain exactly 8 sprites.");
        }

        if (frame2Sprites == null || frame2Sprites.Length != 8)
        {
            Debug.LogWarning("TopDownMouseFollower: frame2Sprites should contain exactly 8 sprites.");
        }

        // Check for unassigned sprite slots
        for (int i = 0; i < 8; i++)
        {
            if (frame1Sprites != null && i < frame1Sprites.Length && frame1Sprites[i] == null)
            {
                Debug.LogWarning($"TopDownMouseFollower: frame1Sprites[{i}] is not assigned.");
            }

            if (frame2Sprites != null && i < frame2Sprites.Length && frame2Sprites[i] == null)
            {
                Debug.LogWarning($"TopDownMouseFollower: frame2Sprites[{i}] is not assigned.");
            }
        }
    }
}