using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintResults : MonoBehaviour
{
    public Text[] ScoreOfLevel1;
    public Text[] ScoreOfLevel2;
    public Text[] ScoreOfLevel3;
    public Text[] ScoreOfLevel4;

    private static string[] scoreOflevel1;
    private static string[] scoreOflevel2;
    private static string[] scoreOflevel3;
    private static string[] scoreOflevel4;
    private string zero = "5/0";

    private void Start()
    {
        PrintScores();
    }

    private void Update()
    {
        PrintScores();
    }

    private void UpdateResultOfLevel3(string score1, string score2)
    {
        scoreOflevel3[0] = score1;
        scoreOflevel3[1] = score2;
    }

    private void UpdateResultOfLevel4(string score1, string score2)
    {
        scoreOflevel4[0] = score1;
        scoreOflevel4[1] = score2;
    }

    private void PrintScores()
    {
        // Level1
        if (practiceForLevel12.scoreOfLevel1_1 != null)
            ScoreOfLevel1[0].text = practiceForLevel12.scoreOfLevel1_1;
        else
            ScoreOfLevel1[0].text = zero;

        if (practiceForLevel12.scoreOfLevel1_2 != null)
            ScoreOfLevel1[1].text = practiceForLevel12.scoreOfLevel1_2;
        else
            ScoreOfLevel1[1].text = zero;

        // Level2
        if (practiceForLevel12.scoreOfLevel2_1 != null)
            ScoreOfLevel2[0].text = practiceForLevel12.scoreOfLevel2_1;
        else
            ScoreOfLevel2[0].text = zero;

        if (practiceForLevel12.scoreOfLevel2_2 != null)
            ScoreOfLevel2[1].text = practiceForLevel12.scoreOfLevel2_2;
        else
            ScoreOfLevel2[1].text = zero;

        // Level3
        if (practiceForLevel3.scoreOfLevel3_1 != null)
            ScoreOfLevel3[0].text = practiceForLevel3.scoreOfLevel3_1;
        else
            ScoreOfLevel3[0].text = zero;

        if (practiceForLevel3.scoreOfLevel3_2 != null)
            ScoreOfLevel3[1].text = practiceForLevel3.scoreOfLevel3_2;
        else
            ScoreOfLevel3[1].text = zero;

        // Level4
        if (practiceForLevel3.scoreOfLevel4_1 != null)
            ScoreOfLevel4[0].text = scoreOflevel4[0];
        else
            ScoreOfLevel4[0].text = zero;

        if (practiceForLevel3.scoreOfLevel4_2 != null)
            ScoreOfLevel4[1].text = practiceForLevel3.scoreOfLevel4_2;
        else
            ScoreOfLevel4[1].text = zero;
    }


}
