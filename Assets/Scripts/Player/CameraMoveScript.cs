using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    Vector2 offset = new Vector2(0, 7f);
    public GameObject player;
    public GameObject firstBoss;
    public GameObject secondBoss;
    public AcceptOrRejectScript aOR_Script;
    public Camera thisCamera;

    public OniEnemyBehaviorScript oniEnemyScript;

    private void Start()
    {
        if(oniEnemyScript == null)
            oniEnemyScript = GameObject.Find("OniEnemy").GetComponent<OniEnemyBehaviorScript>();
        if (!player)
            player = GameObject.Find("Player");
        if (aOR_Script == null)
            aOR_Script = GameObject.Find("Player").GetComponent<AcceptOrRejectScript>();
        if (thisCamera == null)
            thisCamera = Camera.main;
    }

    private void Update()
    {
        if(oniEnemyScript.IsOnCutscene)
        {
            transform.position = firstBoss.transform.position + new Vector3(-3, 11.2f, transform.position.z);
            thisCamera.orthographicSize = 5f; // Adjust camera size for cutscene
            return;
        }
        else if (aOR_Script.IsDeciding && Vector2.Distance(secondBoss.transform.position, player.transform.position) < 5f)
        {
            transform.position = new Vector3(0, 11.75f, transform.position.z);
            thisCamera.orthographicSize = 5f; // Adjust camera size for decision screen
        }
        else
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (player)
        {
            Vector3 playerPosition = player.transform.position;
            transform.position = new Vector3(0, playerPosition.y + offset.y, transform.position.z);
            thisCamera.orthographicSize = 15f; // Reset camera size when not deciding
        }
        else
        {
            Debug.LogWarning("Player GameObject not found. Please assign the player GameObject in the inspector or ensure it is named 'Player'.");
        }
    }
}
