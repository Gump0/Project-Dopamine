using UnityEngine;

public class BackgroundColorShift : MonoBehaviour
{
    public Color color1, color2;

    bool flip = false;
    Camera cam;
    float loop = 0;
    private void Start() {
        cam = GetComponent<Camera>();
    }

    private void Update() {
        loop += Time.deltaTime;

        float t = loop / 5;
        cam.backgroundColor = Color.Lerp(flip ? color2 : color1, flip ? color1 : color2, t);

        if (loop > 5) {
            loop = 0;
            flip = !flip;
        }
    }
}
