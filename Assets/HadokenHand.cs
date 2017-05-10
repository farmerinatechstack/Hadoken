using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenHand : MonoBehaviour {
    public HadokenBehavior hadoken;

    private HadokenBehavior spawnedHadoken;
    private SteamVR_TrackedObject obj;
    private SteamVR_Controller.Device controller;

	// Use this for initialization
	void Start () {
        obj = gameObject.GetComponent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)obj.index);
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) && spawnedHadoken == null) {
            spawnedHadoken = Instantiate(hadoken, transform.position, transform.rotation);
            spawnedHadoken.handle = transform;
        } else if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && spawnedHadoken != null) {
            spawnedHadoken.Release(controller.velocity, controller.angularVelocity);
            spawnedHadoken = null;
        }
	}
}
