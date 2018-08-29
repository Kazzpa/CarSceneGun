using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasHandler : MonoBehaviour {

    public TextMeshProUGUI UISpeed, UIBoost, UIScore, UIScoreAlert;
    // Use this for initialization
    void Start () {
        
        UISpeed.SetText("Speed 0");
        UIScore.SetText("Score: 0");
        UIScoreAlert.enabled = false;
        UIBoost.enabled = false;
    }
	public void UpdateSpeed(float speed)
    {
        UISpeed.SetText("Speed " + (speed * (120 / 30)).ToString("0.0"));
    }
    public void UpdateBoost(int num)
    {
        UIBoost.SetText(num.ToString());
    }
    public void ActivateBoost(bool b)
    {
        UIBoost.enabled = b;
    }
    public bool IsBoostActive()
    {
        return UIBoost.enabled;
    }
    public void UpdateScore(int points)
    {
        UIScore.SetText("Score: " + points);
    }
    public void ActivateScoreAlert(bool b)
    {
        UIScoreAlert.enabled = b;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
