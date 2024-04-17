using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{


    [SerializeField]
    private float moveForce = 5f;


    [SerializeField]
    private float movementX;


    void Start()
    {
    }


    void Update()
    {
        PlayerMoveKeyboard();
    }


    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxis("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }





}