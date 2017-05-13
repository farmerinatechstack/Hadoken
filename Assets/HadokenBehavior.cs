using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenBehavior : MonoBehaviour {
    public ParticleSystem explosion;
    public Transform handle;
    public float scaleLimit = 1.5f;
    public AudioSource src;

    private ParticleSystem ball;
    private Rigidbody rb;
    private float scaleFactor = 1.0f;
    private float velocityDamp = 1f;

	// Use this for initialization
	void Start () {
        ball = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Grow());
	}
	
	// Update is called once per frame
	void Update () {
        if (handle) {
            transform.position = Vector3.Lerp(transform.position, handle.position, 0.9f);
        }
    }

    public void Throw(Vector3 velocity, Vector3 angularVelocity) {
        StopAllCoroutines();
        handle = null;

        rb.isKinematic = false;
        rb.velocity = velocity * velocityDamp;
        rb.angularVelocity = angularVelocity * velocityDamp;
    }

    public void Kill() {
        ball.Stop();
        explosion.gameObject.SetActive(true);
        src.Play();
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision) {
        Kill();
    }

    private IEnumerator Grow() {
        while (scaleFactor < scaleLimit) {
            scaleFactor += 0.1f;
            Vector3 newScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            rb.mass += 10;
            velocityDamp -= 0.01f;
            transform.localScale = newScale;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
