using UnityEngine;

public class ReachHome : MonoBehaviour
{
    // Target object (Home)
    public Transform homeObject;

    // Distance threshold to consider as 'reached' (set to 100)
    public float reachDistance = 100.0f;

    void Update()
    {
        // Check the distance between the current object and the home object
        float distanceToHome = Vector3.Distance(transform.position, homeObject.position);

        // If the distance is less than or equal to the threshold
        if (distanceToHome <= reachDistance)
        {
            // Print the message
            Debug.Log("You have reached Home");
        }
    }
}
