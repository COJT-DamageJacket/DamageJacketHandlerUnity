using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakePattern : MonoBehaviour {

    int sampleState;
    bool state;
    Timer sampleTimer;
    int counter;
    int[] patternArray;
    Pattern pattern;

    [SerializeField] PannelArray pannelArray;
    [SerializeField] DirectionPanel directionPanel;
    [SerializeField] Image statePanel;
    [SerializeField] Button testButton;
    [SerializeField] Button saveButton;
    [SerializeField] DamageSerialSend damageSerialSend;
    [SerializeField] InputField patternNameField;
    [SerializeField] Button searchButton;


    // Use this for initialization
    void Start () {
        pattern = new Pattern();
        sampleState = 0;
        sampleTimer = new Timer();

        testButton.onClick.AddListener(() =>
        {
            if (sampleState == 0 && patternArray != null)
                damageSerialSend.SendDamage(directionPanel.direction, patternArray);
        });

        saveButton.onClick.AddListener(() =>
        {
            if (sampleState == 0 && patternArray != null) {
                string key = patternNameField.text.Replace(" ", "");
                pattern.SavePattern(key, patternArray);
            }
        });

        searchButton.onClick.AddListener(() =>
        {
            string key = patternNameField.text;
            if (sampleState == 0) {
                patternArray = pattern.Get(key);
                pannelArray.SetPattern(patternArray);
            }
        });
	}


    // Update is called once per frame
    void Update () {
        state = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartSample();
        }
        if (sampleState == 2) {
            statePanel.color = (state) ? Color.cyan :Color.white;
        } else if (sampleState == 0){
            statePanel.color = new Color(1, 1, 1, 0.5f);
        }

        sampleTimer.UpdateTime(Time.deltaTime);
	}

    void StartSample () {
        if (sampleState == 0) {
            sampleState = 1;
            statePanel.color = Color.yellow;
            pannelArray.ResetPattern();
            return;
        } else if (sampleState == 1) {
            sampleState = 2;
            patternArray = new int[Pattern.RANGE];
            counter = 0;
            sampleTimer.ExpiredReset();
            sampleTimer.expire += GetData;
            GetData();
        } else {

        }
    }

    void GetData () {
        patternArray[counter] = state ? 1 : 0;
        pannelArray.SetPattern(counter, patternArray[counter]);
        counter++;
        if (counter == Pattern.RANGE) {
            PrintPattern();
            sampleState = 0;
            return;
        }
        sampleTimer.Start(Pattern.INTERVAL);
    }

    void PrintPattern()
    {
        string res = "";
        for (int i = 0; i < Pattern.RANGE; i++)
        {
            res += patternArray[i];
        }
        Debug.Log(res);
    }

    void PrintPattern(int[] pattern)
    {
        string res = "";
        for (int i = 0; i < Pattern.RANGE; i++)
        {
            res += pattern[i];
        }
        Debug.Log(res);
    }

}
