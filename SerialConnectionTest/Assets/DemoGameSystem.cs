using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoGameSystem : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text hpText;
    [SerializeField] Slider hpGage;
    [SerializeField] UnityChanShooter shooter;

    const int INITIAL_SCORE = 0;
    const int INITIAL_HP = 100;

    private int score;
    private int hp;

	// Use this for initialization
	void Start () {
        hp = INITIAL_HP;
        score = INITIAL_SCORE;
        hpGage.maxValue = INITIAL_HP;
        hpGage.minValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
        hpGage.value = hp = shooter.hp;
        hpText.text = hp.ToString();
        scoreText.text = score.ToString();
	}
}
