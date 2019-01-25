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
    int[] pattern;

    [SerializeField] PannelArray pannelArray;
    [SerializeField] DirectionPanel directionPanel;
    [SerializeField] Image statePanel;
    [SerializeField] Button testButton;
    [SerializeField] Button saveButton;
    [SerializeField] DamageSerialSend damageSerialSend;
    [SerializeField] InputField patternNameField;
    [SerializeField] Button searchButton;

    IDictionary<string, int[]> patternMap;

    // Use this for initialization
    void Start () {
        sampleState = 0;
        sampleTimer = new Timer();

        testButton.onClick.AddListener(() =>
        {
            if (sampleState == 0 && pattern != null)
                damageSerialSend.SendDamage(directionPanel.direction, pattern);
        });

        saveButton.onClick.AddListener(() =>
        {
            if (sampleState == 0 && pattern != null) {
                SavePattern();
            }
        });

        searchButton.onClick.AddListener(() =>
        {
            string key = patternNameField.text;
            if (sampleState == 0 && patternMap.ContainsKey(key)) {
                pattern = patternMap[key];
                pannelArray.SetPattern(pattern);
            }
        });

        LoadPattern();
	}

    int[] ConvertPattern(string p) {
        int[] res = new int[Pattern.RANGE];
        for (int k = 0; k < Pattern.RANGE/4; k++) {
            char c = p[k];
            int v;
            if (c >= '0' && c <= '9') v = c - '0';
            else v = c - 'a' + 10;
            for (int i = 0; i < 4; i++) {
                res[k * 4 + i] = (c >> (3-i)) & 1;
            }
        }
        return res;
    }

    string ConvertPattern(int[] p) {
        string res = "";
        for (int k = 0; k < Pattern.RANGE/4; k++) {
            char v = (char)0;
            for (int i = 0; i < 4; i++) {
                v <<= 1;
                v += (char)p[4 * k + i];
            }
            char c;
            if (v >= 10) c = (char)('a' - 10 + v);
            else c = (char)('0' + v);
            res += c;
        }
        return res;
    }

    void LoadPattern() {
        patternMap = new Dictionary<string, int[]>();
        try
        {
            using (var sr = new System.IO.StreamReader(@"Assets/Resources/pattern.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');
                    Debug.Log(values[1]);
                    PrintPattern(ConvertPattern(values[1]));
                    Debug.Log(ConvertPattern(ConvertPattern(values[1])));
                    patternMap.Add(values[0], ConvertPattern(values[1]));
                    foreach (var value in values)
                    {
                        Debug.Log(value);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void WritePattern()
    {
        try
        {
            using (var sw = new System.IO.StreamWriter(@"Assets/Resources/pattern.csv"))
            {
                foreach (string key in patternMap.Keys) 
                {
                    sw.WriteLine(key + "," + ConvertPattern(patternMap[key]));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void Update () {
        state = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartSample();
        }
        if (sampleState == 2) {
            statePanel.color = (state) ? Color.cyan :Color.white;
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
            pattern = new int[Pattern.RANGE];
            counter = 0;
            sampleTimer.ExpiredReset();
            sampleTimer.expire += GetData;
            GetData();
        } else {

        }
    }

    void GetData () {
        pattern[counter] = state ? 1 : 0;
        pannelArray.SetPattern(counter, pattern[counter]);
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
            res += pattern[i];
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

    void SavePattern() {
        string key = patternNameField.text.Replace(" ", ""); ;
        if (key == "")
            key = "hogehogepoon";
        if (patternMap.ContainsKey(key))
            patternMap[key] = pattern;
        else
            patternMap.Add(key, pattern);
        WritePattern();
    }
}
