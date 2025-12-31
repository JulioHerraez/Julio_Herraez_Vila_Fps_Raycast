using UnityEngine;
using System.Collections;

public class CamaraController : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -7f);

    
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    
    public float respawnMoveSpeed = 2f;

    private bool isRespawning = false;

    void FixedUpdate()
    {
        if (isRespawning || target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void MoveToRespawn(Vector3 respawnPosition, System.Action onComplete)
    {
        StartCoroutine(MoveCameraCoroutine(respawnPosition, onComplete));
    }

    IEnumerator MoveCameraCoroutine(Vector3 respawnPosition, System.Action onComplete)
    {
        isRespawning = true;

        Vector3 targetPos = respawnPosition + offset;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                Time.deltaTime * respawnMoveSpeed
            );

            yield return null;
        }

        transform.position = targetPos;
        isRespawning = false;
        onComplete?.Invoke();
    }
}