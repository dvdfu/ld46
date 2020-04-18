using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    [SerializeField]
    Vector2 MAX_SPEED = new Vector2(30.0f, 30.0f);

    // Member vars
    Vector2 speed = Vector2.zero;

    // Unity vars
    [SerializeField]
    Rigidbody2D body;

    void Start() {
        
    }

    void Update() {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("PlayerHorizontal"), Input.GetAxisRaw("PlayerVertical"));
        speed += ((moveDirection * MAX_SPEED) - speed);
        body.AddForce(speed);
    }
}
