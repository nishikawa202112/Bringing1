using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject GameManager;
    private Vector3 startPosition;
    public Vector3 zoomPosition;
    private float time = 0;
    private float zoomTime = 0;
    private float backTime = 0;
    private Vector3 localAngle;
    public Vector3 ballFindPosition;
    private Transform firstTransform;
    private Vector3 firstLocalAngles;

    // Start is called before the first frame update
    void Start()
    {
        firstTransform = this.transform;
        ballFindPosition = new Vector3(0f, 1.2f, -26.5f);
        firstLocalAngles = firstTransform.localEulerAngles;
        startPosition = transform.position;
        zoomPosition = new Vector3(0f, 1.05f, -26.51f);
        //time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().state == State.Prologue)
        {
            localAngle.x = 90f;
            localAngle.y = 0f;
            localAngle.z = 0f;
            this.transform.localEulerAngles = localAngle;
            time += Time.deltaTime / 4.0f;
            this.transform.position = Vector3.Lerp(startPosition, ballFindPosition, time);
        }
        if (GameManager.GetComponent<GameManager>().state == State.CanThrowBall)
        {
            this.transform.localEulerAngles = firstLocalAngles;
            this.transform.position = startPosition;
        }

        if (GameManager.GetComponent<GameManager>().state == (State)11) //&& (startPosition != zoomPosition))
        {
            zoomTime += Time.deltaTime/2.0f;
            this.transform.position = Vector3.Lerp(startPosition, zoomPosition, zoomTime);
        }
        if (GameManager.GetComponent<GameManager>().state == State.BringTheKey)
        {
            backTime += Time.deltaTime / 2.0f;
            this.transform.position = Vector3.Lerp(zoomPosition, startPosition, backTime);
        }
    }
}
