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
    string[] STATE_PANEL_MESSAGE = new string[3] {
        "スペースを押すと記録準備",
        "スペースを押すと記録開始",
        "スペースを押すともう一度記録"
    };

    [SerializeField] PannelArray pannelArray;
    [SerializeField] DirectionPanel directionPanel;
    [SerializeField] Image statePanel;
    [SerializeField] Text statePanelText;
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
        statePanelText.text = STATE_PANEL_MESSAGE[0];

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
            SetSampleState(1);
            statePanel.color = Color.yellow;
            pannelArray.ResetPattern();
            return;
        }
        else if (sampleState == 1)
        {
            SetSampleState(2);
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
            SetSampleState(0);
            return;
        }
        sampleTimer.Start(Pattern.INTERVAL);
    }

    void SetSampleState(int s) {
        sampleState = s;
        pannelArray.editable = (sampleState == 0);
        statePanelText.text = STATE_PANEL_MESSAGE[s];
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
