using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class triggeringEffects : MonoBehaviour
{
    public static bool module1HadLoaded = false;
    public static bool module2HadLoaded = false;
    public static bool tutorialHadLoaded = false;
    static bool mainMenuHadLoaded = false;
    static bool level1HadLoaded = false;
    static bool level2HadLoaded = false;
    static bool level3HadLoaded = false;
    static bool level4HadLoaded = false;

    public Text moduleHadloaded;
    public Text levelHadLoaded;
    public AudioSource triggeringSound;
    public AudioClip triggeringClip_1, triggeringClip_2;

    private string Name = null;
    private string Tag = null;
    private bool triggerHadPlay = false;
    private GameObject currentHittedObject = null;
    public static bool startTrigger = false;
    public static bool rightAnswerHitted = false;
    public static bool wrongAnswerHitted = false;



    private void Awake()
    {
        ControllerStatus.TriggerDown += PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject += PlayDependOnHittedObject;
    }

    private void Start()
    {
        if (module1HadLoaded)
        {
            moduleHadloaded.text = "1"; 
        }
        else if (module2HadLoaded)
        {
            moduleHadloaded.text = "2";
        }
        else
        {
            moduleHadloaded.text = "0";
        }

        if (level1HadLoaded)
        {
            levelHadLoaded.text = "1";
        } else if (level2HadLoaded)
        {
            levelHadLoaded.text = "2";
        } else if (level3HadLoaded)
        {
            levelHadLoaded.text = "3";
        } else if (level4HadLoaded)
        {
            levelHadLoaded.text = "4";
        } else
        {
            levelHadLoaded.text = "0";
        }
    }

    private void OnDestroy()
    {
        ControllerStatus.TriggerDown -= PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject -= PlayDependOnHittedObject;
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
        else if (!triggeringSound.isPlaying)
        {  
            startTrigger = false;
        }
        else
        {
            triggerHadPlay = false;
        }
    }

    // play the triggering sound
    private void PlaySounds()
    {
        if (Tag != null)
        {
            if(currentHittedObject.tag == "navObjects" || currentHittedObject.tag == "distractor")
                triggeringSound.PlayOneShot(triggeringClip_2);
            else
                triggeringSound.PlayOneShot(triggeringClip_1);
        }
    }

    private void LoadScenes()
    { 
        if (Name != null)
        {
            SceneManager.LoadScene(Name);
            GetSceneLoaded();
        }    
    }

    private void GetSceneLoaded()
    {
        if(Tag == "navObjects")
        {
            switch (Name)
            {
                case "MainMenu":
                    mainMenuHadLoaded = true;

                    tutorialHadLoaded = false;
                    module1HadLoaded = false;
                    module2HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "_Tutorial":
                    tutorialHadLoaded = true;

                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    module2HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_1-1":
                    level1HadLoaded = true;
                    module1HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module2HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_1-2":
                    level2HadLoaded = true;
                    module1HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module2HadLoaded = false;
                    level1HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_1-3":
                    level3HadLoaded = true;
                    module1HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module2HadLoaded = false;
                    level2HadLoaded = false;
                    level1HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_1-4":
                    level4HadLoaded = true;
                    module1HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module2HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    break;
                case "Level_2-1":
                    level1HadLoaded = true;
                    module2HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_2-2":
                    level2HadLoaded = true;
                    module2HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    level1HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_2-3":
                    level3HadLoaded = true;
                    module2HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level4HadLoaded = false;
                    break;
                case "Level_2-4":
                    level4HadLoaded = true;
                    module2HadLoaded = true;

                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    break;
                default:
                    tutorialHadLoaded = false;
                    mainMenuHadLoaded = false;
                    module1HadLoaded = false;
                    module2HadLoaded = false;
                    level1HadLoaded = false;
                    level2HadLoaded = false;
                    level3HadLoaded = false;
                    level4HadLoaded = false;
                    break;
            }
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

        getRightAnswer();
    }

    private void getRightAnswer()
    {
        if(Tag == "rightAnswer")
        {
            rightAnswerHitted = true;
        }
        else
        {
            rightAnswerHitted = false;
        }
        if(Tag == "distractor")
        {
            wrongAnswerHitted = true;
        }
        else
        {
            wrongAnswerHitted = false;
        }
    }

}
