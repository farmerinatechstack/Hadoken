using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenHand : MonoBehaviour {
    public HadokenBehavior hadokenPrefab;
    public HadokenBehavior spawnedHadoken;
    public SteamVR_Controller.Device controller;
    public AudioSource src;
    public Vector3 handPos {
        get { return transform.position; }
    }

	// Use this for initialization
	void Start () {
        SteamVR_TrackedObject obj = gameObject.GetComponent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)obj.index);
	}
	
	public void SpawnHadoken(float hadokenScale) {
        src.Play();
        spawnedHadoken = Instantiate(hadokenPrefab, transform.position, transform.rotation);
        spawnedHadoken.handle = transform;
        spawnedHadoken.scaleLimit = hadokenScale;
    }

    public void ThrowHadoken() {
        if (spawnedHadoken != null) {
            spawnedHadoken.Throw(controller.velocity, controller.angularVelocity);
            spawnedHadoken = null;
        }
    }

    public void KillHadoken() {
        if (spawnedHadoken != null) {
            spawnedHadoken.Kill();
            spawnedHadoken = null;
        }
    }
}
