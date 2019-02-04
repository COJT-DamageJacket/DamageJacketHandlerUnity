using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    [SerializeField] private float enemyRate;
    const float RADIUS = 7;
    private int count;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
    void Update () {
        count++;
        if (count / enemyRate > 1 && count >= 10) {
            EnemyAppears();
            count = 0;
        }
	}

    void EnemyAppears() {
        count = 0;
        float theta = 360 * Random.value;
        float t = theta / 180 * Mathf.PI;
        Vector3 pos = new Vector3(Mathf.Sin(t), 0, Mathf.Cos(t)) * RADIUS;
        GameObject prefab = (GameObject)Resources.Load("Prefabs/EnemyUnityChan");
        GameObject miniUnityChan = Instantiate(prefab, pos, Quaternion.identity);
        miniUnityChan.transform.rotation = Quaternion.AngleAxis(theta+180, new Vector3(0, 1, 0));
        miniUnityChan.GetComponent<miniUniZombi>().SetAngle(t);
    }
}
