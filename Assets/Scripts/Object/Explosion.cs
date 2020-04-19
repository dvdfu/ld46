using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    const int EXPLOSION_DAMAGE = 50;

    [SerializeField] AudioClip explosionSound;

    void Start() {
        RaycastHit2D[] results = Physics2D.CircleCastAll(transform.position, 32, Vector2.zero, 0);
        foreach (RaycastHit2D result in results) {
            Flammable flammable = result.collider.gameObject.GetComponent<Flammable>();
            if (flammable) {
                flammable.SetOnFire();
            }
            Mortal mortal = result.collider.gameObject.GetComponent<Mortal>();
            if (mortal) {
                mortal.Damage(gameObject.tag, EXPLOSION_DAMAGE);
            }
        }
        Camera.main.gameObject.GetComponent<CameraHelper>().Shake();
        SoundManager.Play(explosionSound);
    }
}
