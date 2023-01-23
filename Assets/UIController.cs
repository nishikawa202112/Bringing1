using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public string[] myText = new string[8];
    public string[] mumText = new string[8];
    private float time = 0f;
    private int i = 1;
    public GameObject MyText;
    public GameObject MumText;
    public GameObject MyText1;
    public GameObject MumText1;
    private string mytext = "わたし:";
    private string mumtext = "ママ:";


    // Start is called before the first frame update
    void Start()
    {
        myText[0] = null;
        myText[1] = "今日ねえ、\n公園で家の鍵を無くしたんだけどココアちゃんが探してくれたんだよ。";
        myText[2] = null;
        myText[3] = "うん、そう。";
        myText[4] = null;
        myText[5] = "そうだよ。";
        myText[6] = null;
        myText[7] = "うん、いい子だった。";
        mumText[0] = null;
        mumText[1] = null;
        mumText[2] = "・・・。　えっ。\nココアちゃん？";
        mumText[3] = null;
        mumText[4] = "天国から助けに来てくれたのかしら。";
        mumText[5] = null;
        mumText[6] = "ママも会いたかったなー。\nココアちゃんいい子だった？";
        mumText[7] = null;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / 4.0f;

        if ((time > i) && (i < 8))
        {
            
            MumText.GetComponent<Text>().text = mumText[i];     
            MyText.GetComponent<Text>().text = myText[i];
            if (mumText[i] != null)
            {
                MumText1.GetComponent<Text>().text = mumtext;
                MyText1.GetComponent<Text>().text = null;
            }
            if (myText[i] != null)
            {
                MyText1.GetComponent<Text>().text = mytext;
                MumText1.GetComponent<Text>().text = null;
            }
            i += 1;
        }
        
    }
}
