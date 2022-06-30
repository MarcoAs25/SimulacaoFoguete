using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private UiChannel uich;
    [SerializeField] private Slider slider1;
    [SerializeField] private Slider slider2;
    [SerializeField] private TMPro.TextMeshProUGUI textH1,textH2, velocity1,velocity2, maxh, time;
    [SerializeField] private FirstStage firstStageScript;
    [SerializeField] private bool op = true;
    [SerializeField] private float timef;
    void Start()
    {
        uich.Reset();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!op)
        {
            timef += Time.fixedDeltaTime;
        }

        time.text = "Time: "+Mathf.RoundToInt(timef).ToString();
        slider1.value = uich.hFstage / 4000f;
        slider2.value = uich.hStage / 4000f;

        textH1.text = "Height: " + Mathf.RoundToInt(uich.hFstage).ToString() + " m";
        textH2.text = "Height: " + Mathf.RoundToInt(uich.hStage).ToString() + " m";
        maxh.text = "Max Height: " + uich.maxH.ToString()+" m";

        velocity1.text = "VY1: " + Mathf.RoundToInt(uich.velfirstStage).ToString() + "m/s";
        velocity2.text = "VY2: " + Mathf.RoundToInt(uich.velSecondStage).ToString() + "m/s";
    }
    public void STL()
    {
        if (op)
        {
            firstStageScript.start = true;
            GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Restart";
            op = false;
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
}
