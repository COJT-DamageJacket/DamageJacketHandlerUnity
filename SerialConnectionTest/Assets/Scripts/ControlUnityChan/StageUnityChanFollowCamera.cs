using UnityEngine;

public class StageUnityChanFollowCamera : MonoBehaviour {

    const float RADIUS = 17.0f;

    private Transform trans;
    [SerializeField] private StageCircleRun stageCircleRun;
    [SerializeField] private ElevateStage elevateStage;

	// Use this for initialization
	void Start () {
        trans = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float theta = stageCircleRun.theta;
        float r = theta / 180 * Mathf.PI;

        trans.rotation = Quaternion.AngleAxis((float)(-90 + theta), new Vector3(0, 1, 0));
        trans.position = new Vector3(Mathf.Cos(r) * RADIUS, elevateStage.height+4, -Mathf.Sin(r) * RADIUS);
    }
}
