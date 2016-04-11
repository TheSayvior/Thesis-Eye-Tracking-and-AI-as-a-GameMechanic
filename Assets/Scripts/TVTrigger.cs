﻿using UnityEngine;
using System.Collections;

public class TVTrigger : MonoBehaviour {
    public bool LookedAt;
    private float timeLookedAt;
    private float timeToTrigger;
    bool _move = false;

    GameObject lm;
    AudioControl _ac;

    public GameObject lamp, lamp2, screen1, screen2;
    private float count = 0;

    // Use this for initialization
    void Start()
    {
        timeToTrigger = 1f;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_move && count == 0)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 6); //make light blink
            lm.GetComponent<LightController>().OneLampBlink(lamp2, 6); //make light blink
            StartCoroutine(TVSequence());
            return;
        }
        if (LookedAt)
        {
            timeLookedAt += Time.deltaTime;
            if (timeLookedAt >= timeToTrigger)
            {
                _move = true;
                Debug.Log("HAR KIGGET i OVER " + timeToTrigger + " SEKUNDER NU");
            }
            return;
        }
        timeLookedAt = 0;
    }

    IEnumerator TVSequence()
    {
        count = 1;
        screen1.SetActive(true); // change texture to "stracht" picture
        _ac.StartTV();// start "flimmer" sound
        yield return new WaitForSeconds(4);
        screen1.SetActive(false);
        _ac.StopTV();
        _ac.ScreamerStart(); // play high sound
        screen2.SetActive(true); // change picture to "screamer" or something
        yield return new WaitForSeconds(1);
        screen2.SetActive(false);
        _ac.ScreamerStop();
        // change picture back to black
    }
}
