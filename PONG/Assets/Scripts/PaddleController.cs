using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public int playerNumber = 0;
    private float moveSpeed = 5f;
    private float paddleDistance = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNumber == 0)
        {
            transform.Translate(0, Input.GetAxis("PLeftMove") * moveSpeed * Time.deltaTime, 0);
        }
        if (playerNumber == 1)
        {
            transform.Translate(0, Input.GetAxis("PRightMove") * moveSpeed * Time.deltaTime, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -paddleDistance, paddleDistance), transform.position.z);
    }
}
