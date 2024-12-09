using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Setting")]
    public float sway;
    public float swayClamp = 0.89f;
    [Space]
    public float smoothing = 3f;

    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }


    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -swayClamp, swayClamp);
        input.y = Mathf.Clamp(input.y, -swayClamp, swayClamp);

        Vector3 target = new Vector3(-input.x, -input.y, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.deltaTime * smoothing);
    }

}