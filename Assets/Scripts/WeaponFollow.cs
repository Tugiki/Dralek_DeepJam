using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 0.8f;

    private PlayerController playerController;

    private void Awake()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        if (player == null || playerController == null)
        {
            return;
        }

        float direction = playerController.FacingDirection;

        transform.localPosition = new Vector3(
            distance * direction,
            0f,
            0f
        );
    }
}