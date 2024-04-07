using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SodaManager : MonoBehaviour
{
    //public int timerTime;
    public int sodaCount;
    public int batCount;
    //public Text timerTextt;
    public Text batText;
    public Text sodaText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timerTextt.text = timerTime.ToString() + "s";
        batText.text = "x" + batCount.ToString();
        sodaText.text = "x" + sodaCount.ToString();
    }
}
