using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameView : MonoBehaviour
{
    [SerializeField] private Image fillBarProgress;
    private float lastValue;


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.singleton.GameStarted)
            return;
        float traveledDistance = GameManager.singleton.EntireDistance - GameManager.singleton.DistanceLeft;
        float value = traveledDistance / GameManager.singleton.EntireDistance;

        if (GameManager.singleton.gameObject && value < lastValue)
            return;


        fillBarProgress.fillAmount = Mathf.Lerp(fillBarProgress.fillAmount,value,5*Time.deltaTime);
    }
}
