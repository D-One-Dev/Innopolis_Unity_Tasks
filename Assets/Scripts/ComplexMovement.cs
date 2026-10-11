using UnityEngine;

public class ComplexMotion : MonoBehaviour
{
    [SerializeField] private float pathScale = 3f;
    [SerializeField] private float pathSpeed = 0.8f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float axisChangeSpeed = 1.2f;
    [SerializeField] private bool enableScalePulse = true;
    [SerializeField] private Vector3 baseScale = Vector3.one;
    [SerializeField] private float scaleAmplitude = 0.35f;
    [SerializeField] private float scaleSpeed = 2.5f;

    private Vector3 startPosition;
    private float time;

    void Start()
    {
        startPosition = transform.position;
        baseScale = transform.localScale;
    }

    void Update()
    {
        time += Time.deltaTime;

        float t = time * pathSpeed;
        float x = pathScale * Mathf.Sin(t);
        float z = pathScale * Mathf.Sin(t) * Mathf.Cos(t);
        float y = Mathf.Sin(t * 0.7f) * 0.8f;

        transform.position = startPosition + new Vector3(x, y, z);

        Vector3 axis = new Vector3(
            Mathf.Sin(time * axisChangeSpeed),
            Mathf.Cos(time * axisChangeSpeed * 0.7f),
            Mathf.Sin(time * axisChangeSpeed * 1.3f)
        ).normalized;

        transform.Rotate(axis, rotationSpeed * Time.deltaTime, Space.World);

        if (enableScalePulse)
        {
            float pulse = 1f + Mathf.Sin(time * scaleSpeed) * scaleAmplitude;
            transform.localScale = baseScale * pulse;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            startPosition = transform.position;

        Gizmos.color = Color.cyan;
        Vector3 prev = startPosition;

        for (int i = 1; i <= 100; i++)
        {
            float t = (i / 100f) * Mathf.PI * 2f;
            float x = pathScale * Mathf.Sin(t);
            float z = pathScale * Mathf.Sin(t) * Mathf.Cos(t);
            float y = Mathf.Sin(t * 0.7f) * 0.8f;

            Vector3 point = startPosition + new Vector3(x, y, z);
            Gizmos.DrawLine(prev, point);
            prev = point;
        }
    }
}
