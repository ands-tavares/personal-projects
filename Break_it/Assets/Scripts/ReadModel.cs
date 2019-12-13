using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReadModel : MonoBehaviour {

    public float moveSpeed;
    public float rotSpeed;
    public float fireInterval;

    private GameObject player1;
    public Vector3 player1Origin;
    public char player1ModelId;
    public GameObject player1Unity;
    public GameObject player1Bullet;

    private GameObject player2;
    public Vector3 player2Origin;
    public char player2ModelId;
    public GameObject player2Unity;
    public GameObject player2Bullet;

    private int player1Life;
    private int player2Life;

    // Use this for initialization
    void Start () {

        player1 = PlayerCreator("1", player1Bullet, player1Origin, moveSpeed, rotSpeed, fireInterval);
        ModelCreator(player1ModelId, player1Unity, player1.transform);

        player2 = PlayerCreator("2", player2Bullet, player2Origin, moveSpeed, rotSpeed, fireInterval);
        ModelCreator(player2ModelId, player2Unity, player2.transform);
        player2.transform.Rotate(new Vector3(0.0f, 0.0f, 180));
    }
	
	// Update is called once per frame
	void Update () {
        player1Life = player1.transform.childCount;
        player2Life = player2.transform.childCount;
    }

    private void ModelCreator(char modelId,GameObject unity, Transform transformPlayer)
    {
        //Make the player shape using it modelId
        Vector3 position;
        BoxCollider2D colliderUnity;
        char[] rowLimiter = { '\n' };
        char[] colLimiter = { ',' };

        TextAsset modelData = (TextAsset)AssetDatabase.LoadAssetAtPath(string.Concat("Assets/Models/", modelId, ".csv"), typeof(TextAsset));
        string[] data = modelData.text.Split(rowLimiter);
        string[] row = data[1].Split(colLimiter);

        colliderUnity = unity.GetComponent<BoxCollider2D>();
        Vector2 sizeUnity = colliderUnity.size;

        Vector2 size = new Vector2((data.Length - 1), (row.Length));
        Vector2 initial = new Vector2(-(size.y - 1) * sizeUnity.y / 2, -(size.x - 1) * sizeUnity.x / 2);

        for (int j = 0; j <= size.y - 1; j++)
        {
            row = data[j].Split(colLimiter);
            for (int i = 0; i <= size.x - 1; i++)
            {
                if (row[i] == "1")
                {
                    position = new Vector3(initial.x + i * sizeUnity.x, initial.y + j * sizeUnity.y, 0) + transformPlayer.position;
                    Instantiate(unity, position, Quaternion.identity, transformPlayer);
                }
            }
        }
    }

    private GameObject PlayerCreator(string playerId, GameObject bullet,  Vector3 playerOrigin,float moveSpeed, float rotSpeed, float fireInterval)
    {
        //Create the player game object at the right position
        GameObject player;
        Transform transformPlayer;
        PlayerController playerControl;

        player = new GameObject(string.Concat("Player",playerId));
        transformPlayer = player.GetComponent<Transform>();
        transformPlayer.Translate(playerOrigin, Space.World);
        player.AddComponent<Rigidbody2D>().gravityScale = 0;

        player.AddComponent<PlayerController>();
        playerControl = player.GetComponent<PlayerController>();
        playerControl.moveSpeed = moveSpeed;
        playerControl.rotSpeed = rotSpeed;
        playerControl.fireInterval = fireInterval;
        playerControl.bullet = bullet;
        playerControl.shotSpawn = transformPlayer;
        playerControl.playerId = playerId;

        return player;
    }
}
