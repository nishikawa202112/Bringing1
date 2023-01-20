using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject GameManager;
    private Vector3 startPosition;
    public Vector3 zoomPosition;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        zoomPosition = new Vector3(0f, 1.05f, -26.51f);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.GetComponent<GameManager>().state == 11) && (startPosition != zoomPosition))
        {
            time += Time.deltaTime/2.0f;
            this.transform.position = Vector3.Lerp(startPosition, zoomPosition, time);
        }
    }
}
