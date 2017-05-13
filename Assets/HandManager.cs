using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
    public HadokenHand[] hands;

    private float offsetLimit = 0.3f;
    private bool comboReady;
    private bool comboCharging;

    // Update is called once per frame
    void Update() {
        float handDistance = (hands[0].handPos - hands[1].handPos).magnitude;
        comboReady = (handDistance < offsetLimit);
        if (!comboReady && comboCharging) {
            KillCombo();
        }

        foreach (HadokenHand h in hands) {
            if (h.controller == null) continue;
            VibrateHand(h);

            if (h.controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && h.spawnedHadoken == null && !comboCharging) {
                float hadokenLimit = comboReady ? 3f : 1.5f;
                h.SpawnHadoken(hadokenLimit);
                if (comboReady) comboCharging = true;
            } else if (h.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && h.spawnedHadoken != null) {
                h.ThrowHadoken();
                if (comboCharging) {
                    hands[0].spawnedHadoken = hands[1].spawnedHadoken = null;
                    comboCharging = false;
                }
            }
        }
	}

    private void VibrateHand(HadokenHand h) {
        if (h.spawnedHadoken == null && !comboCharging) return;
        ushort vibrateVal = 200;
        if (comboCharging) vibrateVal = 500;
        h.controller.TriggerHapticPulse(vibrateVal);
    }

    private void KillCombo() {
        foreach (HadokenHand h in hands) {
            if (h.spawnedHadoken != null) h.spawnedHadoken.Kill();
            h.spawnedHadoken = null;
        }
        comboCharging = false;
    }
}
