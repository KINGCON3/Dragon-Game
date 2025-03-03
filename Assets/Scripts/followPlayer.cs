using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform character;  // Reference to the character
    public float circleRadius = 5f;  // Radius of the circle
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to a point in the world, assuming the plane is at y = 0
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPosition = ray.GetPoint(distance);

            // Calculate the direction vector from the character to the mouse
            Vector3 direction = mouseWorldPosition - character.position;
            direction.y = 0;  // Ensure the movement is in the XZ plane

            // Normalize the direction vector to get a unit direction
            Vector3 normalizedDirection = direction.normalized;

            // Calculate the angle using Atan2 to determine the direction
            float angle = Mathf.Atan2(normalizedDirection.z, normalizedDirection.x);

            // Calculate the position on the circle's circumference
            Vector3 squarePosition = new Vector3(
                character.position.x + circleRadius * Mathf.Cos(angle),
                character.position.y + 0.5f,  // Keep the original Y position
                character.position.z + circleRadius * Mathf.Sin(angle)
            );

            // Set the square's position
            transform.position = squarePosition;

            // Set the rotation to face the direction of movement
            // Quaternion.LookRotation expects the forward direction, so pass the direction vector
            transform.rotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
        }
    }
}
