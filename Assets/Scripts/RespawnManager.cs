using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    
    public Transform[] respawnPoints;

    private Transform player;
    private Transform selectedRespawn;

    public static Vector3 respawnPoint;

    private Vector3 initialPosition;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        initialPosition = transform.position;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (RestartData.forceLevelRestart)
        {
            transform.position = initialPosition;
            RestartData.forceLevelRestart = false;
        }
        else
        {
            transform.position = RespawnManager.respawnPoint;
        }

    }

    public void RespawnPlayer()
    {
        selectedRespawn = GetClosestRespawn();

        
        PlayerPrefs.SetFloat("RespawnX", selectedRespawn.position.x);
        PlayerPrefs.SetFloat("RespawnY", selectedRespawn.position.y);
        PlayerPrefs.SetFloat("RespawnZ", selectedRespawn.position.z);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    Transform GetClosestRespawn()
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform point in respawnPoints)
        {
            float dist = Vector3.Distance(player.position, point.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = point;
            }
        }

        return closest;
    }

    public static void ResetToStart()
    {
        
        respawnPoint = Vector3.zero; 
    }

}