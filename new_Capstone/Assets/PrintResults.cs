using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResults : MonoBehaviour
{
    public Text score1_1, score2_1, score1_2, score2_2, score1_3, score2_3;
    

    private void Start()
    {
        PrintResultForLevel1();
    }

    private void Update()
    {
        PrintResultForLevel1();
    }

    private void PrintResultForLevel1()
    {
        if (practiceForLevel12.resultOfScore1_1 != null)
            score1_1.text = practiceForLevel12.resultOfScore1_1;
        else
            score1_1.text = "5/0";

        if (practiceForLevel12.resultOfScore2_1 != null)
            score2_1.text = practiceForLevel12.resultOfScore2_1;
        else
            score2_1.text = "5/0";
    }
}
