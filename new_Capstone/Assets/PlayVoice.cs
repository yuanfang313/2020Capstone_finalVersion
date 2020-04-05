using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayVoice : MonoBehaviour
{
    public AudioSource voiceAudioSource;
    public List<AudioClip> voiceClips = new List<AudioClip>();
   
    public void playVoice (int index)
    {
        voiceAudioSource.PlayOneShot(voiceClips[index]);
    }

}
