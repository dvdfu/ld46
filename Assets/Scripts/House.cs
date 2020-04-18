using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Sprite ashes;

    public void Die() {
        sprite.sprite = ashes;
    }
}
