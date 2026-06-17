using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerObstacleDetector : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ObstacleBase obstacle = hit.collider.GetComponentInParent<ObstacleBase>();

        if (obstacle != null)
        {
            obstacle.HandlePlayerHit();
        }
    }
}
