using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour { 

    public GameObject bullet;
    public Transform shotSpawn;
    public float moveSpeed=10;
    public float rotSpeed=70;
    public float fireInterval=1;
    public string playerId;

    private Rigidbody2D rb2D;
    private float time;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        time = 0;
    }

    // Update is called once per frame
    void Update() {

        float rotation_z = Input.GetAxis(string.Concat("P",playerId,".Rotation")) * rotSpeed;
        Vector2 movement = new Vector2(Input.GetAxis(string.Concat("P", playerId, ".Horizontal")), Input.GetAxis(string.Concat("P", playerId, ".Vertical")));

        rb2D.velocity = movement * moveSpeed;
        rb2D.angularVelocity = -rotation_z;
        
        time = time + Time.deltaTime;

        //Fire_interval: Intervalo mínimo entre tiros
        if (Input.GetButton(string.Concat("P", playerId, ".Fire")) && time > fireInterval)
        {
            Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
            time = 0;
        }
    }
}