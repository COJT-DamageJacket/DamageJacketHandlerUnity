using UnityEngine;

public class GlobeController : MonoBehaviour
{

    const int MAX_RADIUS = 2;
    float r = 0;

    [SerializeField] GameObject LeftShoulder;

    Transform transLSh;

    [SerializeField] GameObject jacket;
    private Material material;

    private void Start()
    {
        this.material = jacket.GetComponent<Renderer>().material;
        transLSh = LeftShoulder.GetComponent<Transform>();
    }

    // TODO 他の関節についても同じように指定する
    private void UpdatePositions()
    {
        material.SetVector("_LeftShoulder", transLSh.position);
    }

    private void Update()
    {
        var hue = Mathf.Repeat(1f, 1.0f); // 赤の色相
        var color1 = Color.HSVToRGB(hue, 0.0f, 1.0f); // 白
        var color2 = Color.HSVToRGB(hue, 1f, 1.0f); // 赤

        material.SetColor("_DefaultColor", color1);
        material.SetColor("_HitColor", color2);

        UpdatePositions();

        if (Input.GetKeyDown("space"))
        {
            r = MAX_RADIUS;
        }
        material.SetFloat("_SeparationRadius", r);
        r -= 0.01f;
    }
}
