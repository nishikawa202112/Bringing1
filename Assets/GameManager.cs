using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int state;
    public float happyPoint;           //最大値0.8f
    public GameObject Dog;
    public GameObject Ball;
    public GameObject BackGround;
    public GameObject GuideText;
    public GameObject PositiveButton;
    public GameObject NegativeButton;
    public GameObject FullMarksParticle;
    public GameObject MainCamera;
    private GameObject child;              //DogControllerから持ってきたものを入れる
    private string devidePosiText;         //PositiveButtonの中身を判断する
    private string devideNegaText;         //NegativeButtonの中身を判断する
    private string[] guideText = new string[2];
    private string[] posiText = new string[10];
    private string[] negaText = new string[10];
    
    // Start is called before the first frame update
    void Start()
    {
        guideText[0] = "ボールを投げて欲しそう";
        guideText[1] = "ちゃんとボールを持ってました！";

        posiText[0] = "▶︎ボールを投げるよ！とってきて！";
        posiText[1] = "▶︎よし、いい子!";
        posiText[2] = "▶︎ココアちゃん、ボールね！";

        posiText[3] = "▶︎（なんか違うけど）よし、いい子！";
        negaText[3] = "▶︎あれっ、これ違うよ";

        state = 0;
        happyPoint = 0f;
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)   //ボールを投げられる状態
        {
            GuideText.GetComponent<Text>().text = guideText[0];
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[0];
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == 1)   //ボールを投げて待っている状態
        {

            if (Dog.GetComponent<DogController>().child != null)
            {
                child = Dog.GetComponent<DogController>().child;
                if (child.tag == "BallTag")
                {
                    state = 2;
                }
                if (child.tag == "BarTag")
                {
                    state = 4;
                }
                if (child.tag == "CanTag")
                {
                    state = 4;
                }
            }
        }
        else if (state == 2)    //ちゃんとボールを持ってきた時(2→0 または 2→10)
        {
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[1];      //よし、いい子！
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == 3)  //違うものを持ってきた後、再度ボールを取りに行く
        {
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[2];　　　　//ココアちゃん、ボールね
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == 4)   //違うものを持ってきた時(4→3 または 4→10)
        {
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[3];
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
            devideNegaText = negaText[3];
            NegativeButton.GetComponentInChildren<Text>().text = devideNegaText;
            NegativeButton.SetActive(true);
        }
        else if (state == 10)  //グラフが満タンになった時
        {
            Destroy(child);
            //FullMarksParticle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(194.0f, 825.0f, Camera.main.nearClipPlane));
            //FullMarksParticle.GetComponent<ParticleSystem>().Play();
            if (FullMarksParticle.GetComponent<ParticleController>().isStopped == true)
            {
                state = 11;
            }
        }
        else if (state == 11)  //カメラをズームする
        {
            if (MainCamera.GetComponent<Transform>().position == MainCamera.GetComponent<MainCameraController>().zoomPosition)
            {
                state = 12;
            }
        }
        else if (state == 12)  //鍵を取りに行く
        {
            //Debug.Log("12");
        }

    }

    //Positiveボタンの内容による振り分け
    //PositiveButtonのOnClick()で呼ばれる
    public void PositiveDivide()
    {
        GuideText.GetComponent<Text>().text = null;
        BackGround.GetComponent<Image>().enabled = false;
        if (devidePosiText == posiText[0])
        {
            Throw();
        }
        else if (devidePosiText == posiText[1])
        {
            happyPoint += 0.1f;
            if (happyPoint >= 0.8f)
            {
                state = 10;
            }
            else
            {
                state = 0;
            }
        }
        else if (devidePosiText == posiText[2])
        {
            FindAgain();
        }
        else if (devidePosiText == posiText[3])
        {
            happyPoint += 0.2f;
            if (happyPoint >= 0.8f)
            {
                state = 10;
            }
            else
            {
                state = 3;    //FindAgain();
            }
        }
        PositiveButton.SetActive(false);
        NegativeButton.SetActive(false);
    }

    //Negativeボタンの内容による振り分け
    //NegativeButtonのOnClickで呼ばれる
    public void NegativeDevide()
    {
        GuideText.GetComponent<Text>().text = null;
        BackGround.GetComponent<Image>().enabled = false;
        if (devideNegaText == negaText[3])
        {
            happyPoint += 0.05f;
            if (happyPoint >= 0.8f)
            {
                state = 10;
            }
            else
            {
                state = 3;            //FindAgain();
            }
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

    //違うものを拾ったのでもう一度ボールを取りに行く(state 3 → 1)
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
