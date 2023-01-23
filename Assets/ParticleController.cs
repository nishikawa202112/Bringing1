using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public GameObject GameManager;
    private ParticleSystem.MainModule main;
    public bool isStopped = false;
    private int count = 0;
    private int finalcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "FullMarksParticleTag")
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(194.0f, 825.0f, Camera.main.nearClipPlane));
        }
        else if (this.tag == "FinalParticleTag")
        {
            transform.position = new Vector3(0f, 1.1f, -24f);
        }
        main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.tag == "FullMarksParticleTag") && (GameManager.GetComponent<GameManager>().state == (State)10) && (count == 0))
        {
            GetComponent<ParticleSystem>().Play();
            count = 1;
        }
        else if ((this.tag == "FinalParticleTag") && (GameManager.GetComponent<GameManager>().state == State.Final) && (finalcount == 0))
        {
            GetComponent<ParticleSystem>().Play();
            finalcount = 1;
        }
    }
    private void OnParticleSystemStopped()
    {
        isStopped = true;
    }
}
