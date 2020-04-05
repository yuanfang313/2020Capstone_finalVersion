using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class practiceForTutorial : MonoBehaviour
{
    public PlayVoice playVoice;
    public GenItems genItems;


    #region publicTimers
    [Header("TIMERS")]

    [Tooltip("welcomeTimer is the timer before 'welcome clip'")]
    [SerializeField] private float welcomeTimer = 0;

    [Tooltip("intervalTimer is the timer between 'welcome clip' and 'ready-touch clip'")]
    [SerializeField] private float intervalTimer = 0;

    [Tooltip("rightAnswerTimer is the timer between 'goodJob clip' and 'tryAnotherRound clip' ")]
    [SerializeField] private float rightAnswerTimer = 0;

    [Tooltip("voicePTimer1 is the first timer of the frequency of voice prompt")]
    [SerializeField] private float voicePTimer1 = 0;

    [Tooltip("voicePTimer2 is the second timer of the frequency of voice prompt")]
    [SerializeField] private float voicePTimer2 = 0;

    [Tooltip("visualPromptTimer1_f is the timer before the first visual prompt appearing")]
    [SerializeField] private float visualPromptTimer1_f = 0;

    [Tooltip("visualPromptTimer2_f is the timer between the visual prompts")]
    [SerializeField] private float visualPromptTimer2_f = 0;

    [Tooltip("visualPromptTimer1_d is the first timer of the duration of the visual prompts")]
    [SerializeField] private float visualPromptTimer1_d = 0;

    [Tooltip("visualPromptTimer2_d is the second timer of the duration of the visual prompts")]
    [SerializeField] private float visualPromptTimer2_d = 0;
    #endregion

    #region publicStaticTimers
    private float _welcomeTimer = 0;
    private float _intervalTimer = 0;
    private float _rightAnswerTimer = 0;
    private float _voicePTempTimer = 0;
    private float _visualPTempTimer_f = 0;
    private float _visualPTempTimer_d = 0;
    #endregion

    public static bool roundHadFinished = false;
    private int count = 0;
    private int voicePromptCounter = 0;
    private bool hadPlay0 = false;
    private bool hadPlay1 = false;
    private bool hadPlay4 = false;
    private bool generated = false;
    private bool answered = false;
    private bool answerIsCorrect = false;

   private void Awake()
    {
        
    }

    private void Start()
    {
        _welcomeTimer = welcomeTimer;
        _intervalTimer = intervalTimer;
        _rightAnswerTimer = rightAnswerTimer;
        _voicePTempTimer = voicePTimer1;
        _visualPTempTimer_f = visualPromptTimer1_f;
        _visualPTempTimer_d = visualPromptTimer1_d;
    }
    private void Update()
    {
        SetupQuestion();
        voicePromt();
        AnswerIsRightEventHandler();
        AnswerIsWrongEventHandlder();

        WelcomeTimer();
        IntervalTimer();
        voicePromptTimer();
    }
    private void OnDestroy()
    {
        
    }

    private void SetupQuestion()
    {
        // play welcome clip
        if(!hadPlay0 && _welcomeTimer <= 0)
        {
            playVoice.playVoice(0);
            hadPlay0 = true;
            roundHadFinished = true;
        }

        // play readyTouch clip
        if(!hadPlay1 && _intervalTimer <= 0 && roundHadFinished && count < 5)
        {
            playVoice.playVoice(1);
            hadPlay1 = true;
        }

        // generate items randomly
        if(hadPlay1 && roundHadFinished && !generated)
        {
            genItems.GenerateTargetsForTutorial();
            generated = true;
            roundHadFinished = false;
        }

    }

    private void AnswerIsRightEventHandler()
    {
        if(triggeringEffects.rightAnswerHitted && !triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = true;
        }
    }

    private void AnswerIsWrongEventHandlder()
    {
        if(!triggeringEffects.rightAnswerHitted && triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = false;
        }
    }

    private void WelcomeTimer()
    {
        if (hadPlay0)
            _welcomeTimer = welcomeTimer;
        else
            _welcomeTimer -= Time.deltaTime;
    }

    private void IntervalTimer()
    {
        if (!playVoice.voiceAudioSource.isPlaying && roundHadFinished)
            _intervalTimer -= Time.deltaTime;
        else
            _intervalTimer = intervalTimer;
    }

    private void voicePromt()
    {
        if (_voicePTempTimer <= 0)
        {
            playVoice.playVoice(4);
            hadPlay4 = true;
        }
            
        if(hadPlay4)
        {
            voicePromptCounter = voicePromptCounter + 1;

            if (voicePromptCounter <= 5)
                _voicePTempTimer = voicePTimer1;
            else if (voicePromptCounter > 5)
                _voicePTempTimer = voicePTimer2;

            hadPlay0 = false;
            hadPlay1 = false;
            hadPlay4 = false;
            answerIsCorrect = false;
            triggeringEffects.wrongAnswerHitted = false;
            triggeringEffects.rightAnswerHitted = false;
            triggeringEffects.startTrigger = false;
            generated = true;
            roundHadFinished = false;
        }
    }

    private void voicePromptTimer()
    {
        if (generated && !answered && !playVoice.voiceAudioSource.isPlaying)
            _voicePTempTimer -= Time.deltaTime;
    }

    private void visualPrompt()
    {

    }
}
