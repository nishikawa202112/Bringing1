using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    public GameObject GraphBack;
    public GameObject GraphBackMask;
    public GameObject GraphText;
    public GameObject GameManager;
    //public GameObject FullMarksParticle;
    private Image graph;
    private float basepoint = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        graph = GetComponent<Image>();
        graph.enabled = false;
        GraphBack.GetComponent<Image>().enabled = false;
        GraphBackMask.GetComponent<Image>().enabled = false;
        GraphText.GetComponent<Text>().text = null;

    }

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.GetComponent<GameManager>().state >= (State)2) && (GameManager.GetComponent<GameManager>().state <= (State)10))
        {
            graph.enabled = true;
            GraphBack.GetComponent<Image>().enabled = true;
            GraphBackMask.GetComponent<Image>().enabled = true;
            GraphText.GetComponent<Text>().text = "ココアちゃん\nハッピーポイント";
        }
        graph.fillAmount = GameManager.GetComponent<GameManager>().happyPoint + basepoint;
        if (GameManager.GetComponent<GameManager>().state == (State)11)
        {
            graph.enabled = false;
            GraphBack.GetComponent<Image>().enabled = false;
            GraphBackMask.GetComponent<Image>().enabled = false;
            GraphText.GetComponent<Text>().text = null;
        }
    }
}
