using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour {

    private int point;

	// Use this for initialization
	void Start () {
        point = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void add(int p) {
        point += p;
    }

    public int get() {
        return point;
    }
}
