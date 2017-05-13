using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadokenHand : MonoBehaviour {
    public HadokenBehavior hadoken;
    public bool comboReady;
    public ComboHadoken comboHandler;

    public HadokenBehavior spawnedHadoken;
    private SteamVR_TrackedObject obj;
    private SteamVR_Controller.Device controller;

	// Use this for initialization
	void Start () {
        obj = gameObject.GetComponent<SteamVR_TrackedObject>();
        controller = SteamVR_Controller.Input((int)obj.index);
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && spawnedHadoken == null) {
            spawnedHadoken = Instantiate(hadoken, transform.position, transform.rotation);
            spawnedHadoken.handle = transform;
            if (comboReady) {
                comboHandler.StartCombo(spawnedHadoken);
            }
        } else if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && spawnedHadoken != null) {
            spawnedHadoken.Release(controller.velocity, controller.angularVelocity);
            spawnedHadoken = null;
        }
	}
}
