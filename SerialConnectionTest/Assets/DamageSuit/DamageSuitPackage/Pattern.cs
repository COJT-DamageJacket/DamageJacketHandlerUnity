using System.Collections.Generic;
using UnityEngine;

public class Pattern {

    public const float INTERVAL = 0.03f;
    public const int RANGE = 32;
    const string DEFAULT_KEY = "default";
    readonly string FILENAME = @"Assets/Resources/pattern" + RANGE + ".csv";

    IDictionary<string, int[]> patternMap;

    public Pattern() {
        LoadPattern();
    }

    public int[] Get(string key) {
        if (patternMap.ContainsKey(key))
        {
            return patternMap[key];
        } else {
            return patternMap[DEFAULT_KEY];
        }
    }

    int[] ConvertPattern(string p)
    {
        int[] res = new int[RANGE];
        for (int k = 0; k < RANGE / 4; k++)
        {
            char c = p[k];
            int v;
            if (c >= '0' && c <= '9') v = c - '0';
            else v = c - 'a' + 10;
            for (int i = 0; i < 4; i++)
            {
                res[k * 4 + i] = (v >> (3 - i)) & 1;
            }
        }
        return res;
    }

    string ConvertPattern(int[] p)
    {
        string res = "";
        for (int k = 0; k < RANGE / 4; k++)
        {
            char v = (char)0;
            for (int i = 0; i < 4; i++)
            {
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

    void LoadPattern()
    {
        patternMap = new Dictionary<string, int[]>();
        try
        {
            using (var sr = new System.IO.StreamReader(FILENAME))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');
                    patternMap.Add(values[0], ConvertPattern(values[1]));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        if (patternMap.ContainsKey(DEFAULT_KEY)) {
            patternMap[DEFAULT_KEY] = GetDefaultPattern();
        } else {
            patternMap.Add(DEFAULT_KEY, GetDefaultPattern());
        }
    }

    void WritePattern()
    {
        try
        {
            using (var sw = new System.IO.StreamWriter(FILENAME))
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

    public void SavePattern(string key, int[] patternArray)
    {
        if (key == "")
            key = "hogehogepoon";
        if (patternMap.ContainsKey(key))
            patternMap[key] = patternArray;
        else
            patternMap.Add(key, patternArray);
        WritePattern();
    }

    int[] GetDefaultPattern() {
        int[] res = new int[RANGE];
        for (int i = 0; i < RANGE; i++)
            res[i] = 1;
        return res;
    }
}
