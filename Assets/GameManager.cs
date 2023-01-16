using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int state;
    public GameObject Dog;
    public GameObject Ball;
    public GameObject GuideText;
    public GameObject PositiveButton;
    public GameObject NegativeButton;
    private GameObject child;              //DogControllerから持ってきたものを入れる
    private string devidePosiText;         //PositiveButtonの中身を判断する
    private string devideNegaText;         //NegativeButtonの中身を判断する
    private string startText = "ボールを投げて欲しそう";
    private string throwText = "▶︎ボールを投げるよ！とってきて！";
    private string throwText1 = "▶︎よし、いい子！もう一回！";
    private string posiBringText = "▶︎よし、いい子！ボール持ってきて！";
    private string negaBringText = "▶︎これ違うよ　ボール持ってきて！";

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        devidePosiText = throwText;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)   //ボールを投げられる状態
        {
            PositiveButton.SetActive(false);
            NegativeButton.SetActive(false);
            GuideText.GetComponent<Text>().text = startText;
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        if (state == 1)   //ボールを投げて待っている状態
        {
            if (Dog.GetComponent<DogController>().child != null)
            {
                child = Dog.GetComponent<DogController>().child;
                if (child.tag == "BallTag")
                {
                    devidePosiText = throwText1;
                    state = 0;
                }
                if (child.tag == "BarTag")
                {
                    state = 2;
                }
                if (child.tag == "CanTag")
                {
                    state = 2;
                }
            }
        }
        if (state == 2)   //
        {
            devidePosiText = posiBringText;
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
            devideNegaText = negaBringText;
            NegativeButton.GetComponentInChildren<Text>().text = devideNegaText;
            NegativeButton.SetActive(true);
        }

    }

    //Positiveボタンの内容による振り分け
    //PositiveButtonのOnClick()で呼ばれる
    public void PositiveDivide()
    {
        GuideText.GetComponent<Text>().text = null;
        if ((devidePosiText == throwText) || (devidePosiText == throwText1))
        {
            Throw ();
        }
        if (devidePosiText == posiBringText)
        {
            FindAgain();
        }
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(false);
    }

    //Negativeボタンの内容による振り分け
    //NegativeButtonのOnClickで呼ばれる
    public void NegativeDevide()
    {
        GuideText.GetComponent<Text>().text = null;
        if (devideNegaText == negaBringText)
        {
            FindAgain();
        }
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(false);
    }

    //ボールを投げる(state 0 → 1)
    public void Throw()
    {
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 direction = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(7.0f, 14.0f), Random.Range(7.0f, 14.0f));
        Ball.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        state = 1;
    }

    //違うものを拾ったのでもう一度ボールを取りに行く(state 2 → 1)
    public void FindAgain()
    {
        if (child.tag != "BallTag")
        {
            Destroy(child);
            child = new GameObject();
        }
        state = 1;
    }
}
