using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public GameObject GameManager;
    private ParticleSystem.MainModule main;
    public bool isStopped = false;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(194.0f, 825.0f, Camera.main.nearClipPlane));
        main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.GetComponent<GameManager>().state == 10) && (count == 0))
        {
            GetComponent<ParticleSystem>().Play();
            count = 1;
        }
    }
    private void OnParticleSystemStopped()
    {
        isStopped = true;
    }
}
