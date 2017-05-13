using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHadoken : MonoBehaviour {
    public HadokenHand[] hands;
    public Transform[] handPositions;

    private float offsetLimit = 0.5f;
    public HadokenBehavior spawnedHadoken;

    void Update() {
        float handOffset = (handPositions[0].position - handPositions[1].position).magnitude;
        if (handOffset <= offsetLimit) {
            SetCombo(true);
        } else {
            SetCombo(false);
            if (spawnedHadoken) spawnedHadoken.Kill();
        }
    }

    public void StartCombo(HadokenBehavior hadoken) {
        hadoken.scaleLimit = 3f;

        spawnedHadoken = hadoken;
        hands[0].spawnedHadoken = hands[1].spawnedHadoken = hadoken;
    }

    public void EndCombo() {
        spawnedHadoken = null;
        hands[0].spawnedHadoken = hands[1].spawnedHadoken = null;
    }

    private void SetCombo(bool value) {
        hands[0].comboReady = hands[1].comboReady = value;
    }
}