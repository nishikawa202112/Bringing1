using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum State
{
    Prologue = -1,                  //始まり　ボールを見つける
    CanThrowBall = 0,               //ボールを投げられる状態
    WaitingDog = 1,                 //ボールを投げて待っている
    BringTheBallCorrectly = 2,      //正しくボールを持ってきた時
    BringAgain = 3,                 //もう一度ボールを探しに行く
    BringSomethingDifferent = 4,    //違うものを持ってきた時
    FullMarks = 10,                 //ポイントが満タンの時
    ZoomTheCamera = 11,             //カメラをDogにズームする
    BringTheKey = 12,               //鍵を見つけに行く
    SayThankYou = 13,               //ココアちゃんありがとう
    Final = 14                      //終わり　次のシーンへ
}

public class GameManager : MonoBehaviour
{
    public State state = (State)(-1);
    public float happyPoint = 0f;           //最大値0.8f
    public GameObject Dog;
    public GameObject Ball;
    public GameObject HouseKey;
    public GameObject BackGround;
    public GameObject GuideText;
    public GameObject PositiveButton;
    public GameObject SecondButton;          //NegativeButton;
    public GameObject FullMarksParticle;
    public GameObject FinalParticle;
    public GameObject MainCamera;
    private GameObject child;              //DogControllerから持ってきたものを入れる
    private string devidePosiText;         //PositiveButtonの中身を判断する
    private string devideSecondText;       //devideNegaText;SecondButtonの中身を判断する
    public string[] guideText = new string[7];
    public string[] posiText = new string[10];
    public string[] secondText = new string[10];  //negaText
    public GameObject HintButton;
    public GameObject HintText;
    public GameObject HintBackGround;
    public GameObject HintTextBackGround;
    public string hintText;
    private float time = 0f;
    public GameObject WanText;
    public string wantext;

    
    // Start is called before the first frame update
    void Start()
    {
        BackGround.GetComponent<Image>().enabled = false;
        GuideText.GetComponent<Text>().text = null;
        PositiveButton.SetActive(false);
        SecondButton.SetActive(false);
        HintClear();
        WanText.GetComponent<Text>().text = null;
        guideText[6] = "これはさっき落として探していた家の鍵\nこれでママの帰りを待たなくてもおうちに帰れる！";
        posiText[0] = "▶︎ボールを投げるよ！\nとってきて！";
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Prologue)
        {
            time += Time.deltaTime;
            if ((time > 0.5f) && (time < 2.0f))
            {
                GuideText.GetComponent<Text>().text = guideText[0];
            }
            if (MainCamera.GetComponent<Transform>().position == MainCamera.GetComponent<MainCameraController>().ballFindPosition)
            {
                GuideText.GetComponent<Text>().text = guideText[1];
            }
            if (time > 6)
            {
                WanText.GetComponent<Text>().text = wantext;
            }
            if (time > 7)
            {
                GuideText.GetComponent<Text>().text = guideText[2];
            }
            if (time > 8)
            {
                state = State.CanThrowBall;
                WanText.GetComponent<Text>().text = null;
            }

        }
        if (state == (State)0)   //ボールを投げられる状態
        {
            GuideText.GetComponent<Text>().text = guideText[3];
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[0];
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == (State)1)   //ボールを投げて待っている状態
        {

            if (Dog.GetComponent<DogController>().child != null)
            {
                child = Dog.GetComponent<DogController>().child;
                if (child.tag == "BallTag")
                {
                    state = (State)2;
                }
                if (child.tag == "BarTag")
                {
                    state = (State)4;
                }
                if (child.tag == "CanTag")
                {
                    state = (State)4;
                }
            }
        }
        else if (state == (State)2)    //ちゃんとボールを持ってきた時(2→0 または 2→10)
        {
            GuideText.GetComponent<Text>().text = guideText[4];
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[1];      //よし、いい子！
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == (State)3)  //違うものを持ってきた後、再度ボールを取りに行く
        {
            HintClear();
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[2];　　　　//ココアちゃん、ボールね
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == (State)4)   //違うものを持ってきた時(4→3 または 4→10)
        {
            GuideText.GetComponent<Text>().text = guideText[5];
            BackGround.GetComponent<Image>().enabled = true;
            devidePosiText = posiText[3];
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
            devideSecondText = secondText[3];
            SecondButton.GetComponentInChildren<Text>().text = devideSecondText;
            SecondButton.SetActive(true);
            HintButton.SetActive(true);
            HintBackGround.GetComponent<Image>().enabled = true;
        }
        else if (state == (State)10)  //グラフが満タンになった時
        {
            HintClear();
            BackGround.GetComponent<Image>().enabled = false;
            Destroy(child);
            if (FullMarksParticle.GetComponent<ParticleController>().isStopped == true)
            {
                state = (State)11;
            }
        }
        else if (state == (State)11)  //カメラをズームする
        {
            if (MainCamera.GetComponent<Transform>().position == MainCamera.GetComponent<MainCameraController>().zoomPosition)
            {
                state = (State)12;
            }
        }
        else if (state == (State)12)  //鍵を取りに行く
        {
            BackGround.GetComponent<Image>().enabled = true;
            GuideText.GetComponent<Text>().text = guideText[2];
            if (Dog.GetComponent<DogController>().child != null)
            {
                if (Dog.GetComponent<DogController>().child.tag == "HouseKeyTag")
                state = State.SayThankYou;
            }
        }
        else if (state == State.SayThankYou)  //お礼を言う
        {
            GuideText.GetComponent<Text>().text = guideText[6];
            devidePosiText = posiText[5];
            PositiveButton.GetComponentInChildren<Text>().text = devidePosiText;
            PositiveButton.SetActive(true);
        }
        else if (state == State.Final)       //天国に帰る
        {
            if (!(Dog) && (FinalParticle.GetComponent<ParticleController>().isStopped == true) )
            {
                SceneManager.LoadScene("FinalScene");
            }
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
                state = (State)10;
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
                state = (State)10;
            }
            else
            {
                state = (State)3;    //FindAgain();
            }
        }
        else if (devidePosiText == posiText[5])
        {
            state = State.Final;
        }
        PositiveButton.SetActive(false);
        SecondButton.SetActive(false);
    }

    //Secondボタンの内容による振り分け
    //SecondButtonのOnClickで呼ばれる
    public void SecondDevide()           //NegativeDevide()
    {
        GuideText.GetComponent<Text>().text = null;
        BackGround.GetComponent<Image>().enabled = false;
        if (devideSecondText == secondText[3])
        {
            happyPoint += 0.05f;
            if (happyPoint >= 0.8f)
            {
                state = (State)10;
            }
            else
            {
                state = (State)3;            //FindAgain();
            }
        }
        else if (devideSecondText == secondText[5])
        {
            state = State.Final;
        }
        PositiveButton.SetActive(false);
        SecondButton.SetActive(false);
    }

    //HintButtonのOnClickで呼ばれる
    public void Hint()
    {
        HintText.GetComponent<Text>().text = hintText;
        HintTextBackGround.GetComponent<Image>().enabled = true;
        
    }

    //ヒントを消す
    private void HintClear()
    {
        HintButton.SetActive(false);
        HintBackGround.GetComponent<Image>().enabled = false;
        HintText.GetComponent<Text>().text = null;
        HintTextBackGround.GetComponent<Image>().enabled = false;
    }

    //ボールを投げる(state 0 → 1)
    public void Throw()
    {
        Ball.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 direction = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(7.0f, 14.0f), Random.Range(7.0f, 14.0f));
        Ball.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        state = (State)1;
    }

    //違うものを拾ったのでもう一度ボールを取りに行く(state 3 → 1)
    public void FindAgain()
    {
        if (child.tag != "BallTag")
        {
            Destroy(child);
            child = new GameObject();
        }
        state = (State)1;
    }
}
