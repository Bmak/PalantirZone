using UnityEngine;

public class ZFighter : MonoBehaviour
{
    [SerializeField] private float MIN;
    [SerializeField] private float MAX;

    private Vector3 lastLocalPosition;
    private Vector3 lastCamPos;
    private Vector3 lastPos;
    private float random;

    void Start()
    {
        lastLocalPosition = transform.localPosition;
        lastPos = transform.position;
        lastCamPos = Camera.main.transform.position - Vector3.up; // just to force update on first frame
        random = Random.Range(MIN, MAX);
    }

    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        if (camPos != lastCamPos || transform.position != lastPos)
        {
            lastCamPos = camPos;
            lastPos = lastLocalPosition + (camPos - transform.position) * random / 10;
            transform.localPosition = new Vector3(transform.localPosition.x, lastPos.y, transform.localPosition.z);
            lastPos = transform.position;
        }
    }
}