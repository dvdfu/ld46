using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Consts
    [SerializeField]
    private Vector2 MAX_SPEED = new Vector2(30.0f, 30.0f);

    // Member vars
    private Vector2 speed = Vector2.zero;

    // Unity vars
    [SerializeField]
    private Rigidbody2D body;

    void Start() {
        
    }

    void Update() {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        speed += ((moveDirection * MAX_SPEED) - speed);
        body.AddForce(speed);
    }
}
