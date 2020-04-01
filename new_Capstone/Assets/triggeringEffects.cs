using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggeringEffects : MonoBehaviour
{
    public AudioSource triggeringSound;
    public AudioClip triggeringClip_1, triggeringClip_2;
    private bool triggerHadPlay = false;
    private GameObject currentHittedObject = null;
    private bool startTrigger = false;

    private void Awake()
    {
        ControllerStatus.TriggerDown += PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject += PlayDependOnHittedObject;
    }

    private void OnDestroy()
    {
        ControllerStatus.TriggerDown -= PlayTriggeringSound;
        PointerStatus.OnPointerUpdateForObject -= PlayDependOnHittedObject;
    }

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
        } else
        {
            triggerHadPlay = false;
        }
    }

    private void PlaySounds()
    {
        if (currentHittedObject.tag == "target")
        {
            triggeringSound.PlayOneShot(triggeringClip_1);
        }
        else if (currentHittedObject != null)
        {
            triggeringSound.PlayOneShot(triggeringClip_2);
        }
    }

    private void LoadScenes()
    {
            if (currentHittedObject.tag == "module1")
            {
                SceneManager.LoadScene("Level_1-1");
            }
            else if (currentHittedObject.tag == "tutorial")
            {
                SceneManager.LoadScene("_Tutorial");
            }
            else if (currentHittedObject.tag == "main menu")
            {
                SceneManager.LoadScene("MainMenu");
            }
    }

    private void PlayDependOnHittedObject(GameObject hitObject)
    {
        currentHittedObject = hitObject;
        if(startTrigger && !triggeringSound.isPlaying)
        {
            LoadScenes();
        }
    }
}
