using UnityEngine;

public class AmbientCameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float amplitude = 0.5f;      // How far the camera moves (in meters)
    public float speed = 0.5f;          // How fast the camera oscillates
    public float rotationAmplitude = 1f; // How much to tilt/rotate (degrees)
    public float rotationSpeed = 0.3f;   // Speed of the rotation motion

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        float t = Time.time * speed;
        float r = Time.time * rotationSpeed;

        Vector3 offset = new Vector3(
            Mathf.Sin(t * 0.8f) * amplitude * 0.5f,
            Mathf.Sin(t) * amplitude,
            Mathf.Cos(t * 0.6f) * amplitude * 0.5f
        );
        transform.position = startPos + offset;

        Quaternion rotOffset = Quaternion.Euler(
            Mathf.Sin(r * 1.1f) * rotationAmplitude,
            Mathf.Cos(r * 0.9f) * rotationAmplitude,
            Mathf.Sin(r * 0.7f) * rotationAmplitude * 0.5f
        );
        transform.rotation = startRot * rotOffset;
    }
}
