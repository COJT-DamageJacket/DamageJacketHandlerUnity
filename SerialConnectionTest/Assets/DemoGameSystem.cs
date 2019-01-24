using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DemoGameSystem : MonoBehaviour {

    [SerializeField] Text scoreText;
    [SerializeField] Text hpText;
    [SerializeField] Text gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] Slider hpGage;
    [SerializeField] UnityChanShooter shooter;

    const int INITIAL_SCORE = 0;
    const int INITIAL_HP = 100;

    private int score;
    private int hp;
    private bool isGameOver;

    private PointManager pointManager;

	// Use this for initialization
	void Start () {
        hp = INITIAL_HP;
        score = INITIAL_SCORE;
        hpGage.maxValue = INITIAL_HP;
        hpGage.minValue = 0;
        isGameOver = false;

        pointManager = GetComponent<PointManager>();
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(ResetGame);
	}
	
	// Update is called once per frame
	void Update () {
        hpGage.value = hp = shooter.getHp();
        hpText.text = hp.ToString();
        scoreText.text = pointManager.get().ToString();
        if (hp == 0) {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameOver = true;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button1) && isGameOver)
        {
            ResetGame();
        }
    }

    public void ResetGame() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
