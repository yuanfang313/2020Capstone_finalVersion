using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class triggeringEffects : MonoBehaviour
{
    public AudioClip triggeringClip_1, triggeringClip_2;

    public static AudioSource triggeringSound;
    public static bool startTrigger = false;
    public static bool rightAnswerHitted = false;
    public static bool wrongAnswerHitted = false;
    public static bool tutorialHadLoaded = false;
    public static bool module1HadLoaded = false;
    public static bool module2HadLoaded = false;

    private string Name = null;
    private string Tag = null;
    private string rightAnswer;
    private string distractor;

    private GameObject currentHittedObject = null;

    private bool Level3HadLoaded = false;
    private bool triggerHadPlay = false;
    private bool AnswerIstarget1 = false;
    private bool AnswerIstarget2 = false;


    private void Awake()
    {
        ControllerStatus.TriggerDown += PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject += PlayDependOnHittedObject;
        practiceForLevel3.Level3HadLoaded += UpdateLevel3Status;
        practiceForLevel3.answerIs += UpdateTarget;
    }

    private void Start()
    {
        triggeringSound = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        ControllerStatus.TriggerDown -= PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject -= PlayDependOnHittedObject;
        practiceForLevel3.Level3HadLoaded -= UpdateLevel3Status;
        practiceForLevel3.answerIs -= UpdateTarget;
    }


    private void UpdateLevel3Status(bool level3HadLoaded)
    {
        Level3HadLoaded = level3HadLoaded;
    }

    // the logic of playing triggering sound
    private void PlayTriggeringSound (bool rightTriggerDown, bool leftTriggerDown)
    {
        if(rightTriggerDown || leftTriggerDown)
        {
            startTrigger = true;
            if (!triggerHadPlay)
            { 
                PlaySounds();
                triggerHadPlay = true;
            }
        }

        if (!triggeringSound.isPlaying)
            startTrigger = false;

        
        if(!rightTriggerDown && !leftTriggerDown)
            triggerHadPlay = false;
    }

    // play the triggering sound
    private void PlaySounds()
    {
        getAnswer();

        if (currentHittedObject.tag != rightAnswer)
            triggeringSound.PlayOneShot(triggeringClip_2);
        else
            triggeringSound.PlayOneShot(triggeringClip_1); 
    }

    private void LoadScenes()
    { 
        if (Name != "Module_1" && Name != "Module_2" && Name != "Tutorial")
        {
            SceneManager.LoadScene(Name);
        }    
        else if (Name == "Module_1")
        {
            SceneManager.LoadScene("Level_1-1");
            tutorialHadLoaded = false;
            module1HadLoaded = true;
            module2HadLoaded = false;
        }
        else if (Name == "Module_2")
        {
            SceneManager.LoadScene("Level_2-1");
            tutorialHadLoaded = false;
            module1HadLoaded = false;
            module2HadLoaded = true;
        }
        else if (Name == "Tutorial")
        {
            SceneManager.LoadScene("_Tutorial");
            tutorialHadLoaded = true;
            module1HadLoaded = false;
            module2HadLoaded = false;
        }
    }

    private void PlayDependOnHittedObject(GameObject hitObject)
    {
        currentHittedObject = hitObject;
        Name = hitObject.name;
        Tag = hitObject.tag;

        if(startTrigger && !triggeringSound.isPlaying)
        {
            LoadScenes();
        }

        HitAnswer();
    }

    private void UpdateTarget (bool answerIsTarget1, bool answerIsTarget2)
    {
        AnswerIstarget1 = answerIsTarget1;
        AnswerIstarget2 = answerIsTarget2;
    }

    private void getAnswer()
    {
        if (Level3HadLoaded)
        {
            if (AnswerIstarget1)
            {
                rightAnswer = "rightAnswer_1";
                distractor = "rightAnswer_2";
            }
            else if (AnswerIstarget2)
            {
                rightAnswer = "rightAnswer_2";
                distractor = "rightAnswer_1";
            }
        }
        else
        {
            rightAnswer = "rightAnswer";
            distractor = "distractor";
        }
    }
    private void HitAnswer()
    {
        getAnswer();
        if (Tag == rightAnswer)
        {
          rightAnswerHitted = true;
          wrongAnswerHitted = false;
        }
        else if (Tag == distractor)
        {
          rightAnswerHitted = false;
          wrongAnswerHitted = true;
        }
        else
        {
          rightAnswerHitted = false;
          wrongAnswerHitted = false;
         }
    }

    public static void CleanTrigger()
    {
        startTrigger = false;
        rightAnswerHitted = false;
        wrongAnswerHitted = false;
    }

}
