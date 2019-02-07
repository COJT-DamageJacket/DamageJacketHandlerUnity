using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorrorGame : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Image image;
    [SerializeField] GameObject title;
    [SerializeField] DamageSerialSend damageSerialSend;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip0;
    [SerializeField] AudioClip audioClip1;
    [SerializeField] AudioClip audioClip2;
    [SerializeField] AudioClip audioClip3;
    [SerializeField] AudioClip audioClip4;
    [SerializeField] AudioClip audioClip5;
    [SerializeField] AudioClip audioClip6;

    private AudioClip[] audioClips = new AudioClip[N];
    bool isPlaying;

    private int idx;
    const int N = 7;

    readonly string[] story = {
        "いわくつきのトンネルに友達とドライブしに行った",
        "何もないと思っていたら急に友達が苦しみだした。",
        "オレもなんだか息が苦しい",
        "早くトンネルを出ないと！",
        "オレはアクセルをふんだ",
        "トンネルを過ぎて一安心したと思ったら",
        "手に血がべっとりついていた"
    };
    private AudioSource[] sources;
    Vector2 dis = new Vector2();
    const float RATE1 = 1.4f;
    const float RATE2 = 2.6f;

    void Next() {
        idx++;
        if (idx == -1) {
            return;
        }
        title.SetActive(false);
        if (idx >= N) return;
        text.text = story[idx];

        if (idx == 2 || idx == 3)
        { // 心臓音
            damageSerialSend.SendDamage(0, "heart", 2);
        }
        if (idx == 3)
        {
            image.GetComponent<RectTransform>().localScale = RATE1 * new Vector2(1, 1);
            image.GetComponent<RectTransform>().localPosition = RATE1 * new Vector2(1, 1) + new Vector2(60, 70);
        }
        else if (idx == 4)
        {
            image.GetComponent<RectTransform>().localScale = RATE2 * new Vector2(1, 1);
            image.GetComponent<RectTransform>().localPosition = RATE1 * new Vector2(1, 1) + new Vector2(250, 120);
            damageSerialSend.SendDamage(2, "accel");
        }
        else if (idx == 5)
        {
            image.gameObject.SetActive(false);
        }
        else if (idx == 6)
        {
            damageSerialSend.SendDamage(4, "kya---", 2);
            Sprite sprite = Resources.Load<Sprite>("Images/hand");
            image.gameObject.SetActive(true);
            image.sprite = sprite;
            image.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            image.GetComponent<RectTransform>().localPosition = new Vector2(0, 50);
        }

        audioSource.PlayOneShot(audioClips[idx]);
    }

	// Use this for initialization
	void Start () {
        idx = -2;
        Next();

        Sprite sprite = Resources.Load<Sprite>("Images/tunnel");
        image.sprite = sprite;

        audioClips[0] = audioClip0;
        audioClips[1] = audioClip1;
        audioClips[2] = audioClip2;
        audioClips[3] = audioClip3;
        audioClips[4] = audioClip4;
        audioClips[5] = audioClip5;
        audioClips[6] = audioClip6;

        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (!audioSource.isPlaying || idx == -1))
        {
            Next();
        }
    }
}
