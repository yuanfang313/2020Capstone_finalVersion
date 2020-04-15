﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class practiceForLevel12 : MonoBehaviour
{
    public PlayVoice playVoice;
    public GenItems genItems;
    public PrintStatus printStatus;

    public static string resultOfScore1_1, resultOfScore2_1, resultOfScore1_2, resultOfScore2_2, resultOfScore1_3, resultOfScore2_3;

    public static UnityAction<bool> Level12HadLoaded = null;
    public static UnityAction<bool> LevelHadPassed = null;
    public static UnityAction<string, string> resultOfLevel = null;

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

    #region privateTimers
    private float _welcomeTimer = 0;
    private float _intervalTimer = 0;
    private float _rightAnswerTimer = 0;
    private float _voicePTempTimer = 0;
    #endregion

    #region privateBool
    private bool level12HadLoaded = false;
    private bool[] hadPlay1 = new bool[8];
    private bool[] hadPlay3 = new bool[4];
    
    private bool generated = false;
    private bool answered = false;
    private bool answerIsCorrect = false;
    private bool startPrompts = false;
    private bool startTrail = false;
    private bool roundHadFinished = false;
    private bool sessionIsTesting = true;
    private bool sessionIsPassed_1 = false;
    private bool sessionIsPassed_2 = false;
    private bool transmitScores = false;
    private bool sessionHadFinished = false;
    private bool sessionHadStarted_2 = false;
    private bool levelHadPassed = false;
    #endregion

    #region printedArea
    [HideInInspector] public int roundCount = 0;
    [HideInInspector] public int voicePromptCounter = 0;
    [HideInInspector] public int visualPromptCounter = 0;

    [HideInInspector] public int testingScores1 = 0;
    [HideInInspector] public int testingScores2 = 0;
    [HideInInspector] public int teachingScores = 0;

    [HideInInspector] public float _visualPTempTimer_f = 0;
    [HideInInspector] public float _visualPTempTimer_d = 0;
    #endregion

    private void Start()
    {
        level12HadLoaded = true;

        if (Level12HadLoaded != null)
            Level12HadLoaded(level12HadLoaded);

        hadPlay1[0] = false;
        hadPlay1[1] = false;
        hadPlay1[2] = false;
        hadPlay1[3] = false;
        hadPlay1[4] = false;
        hadPlay1[5] = false;
        hadPlay1[6] = false;
        hadPlay1[7] = false;

        hadPlay3[0] = false;
        hadPlay3[1] = false;
        hadPlay3[2] = false;
        hadPlay3[3] = false;

        CleanTimerCounter();
        ToZero();
    }

    private void Update()
    {
        SetupQuestion();
        voicePromt();
        visualPrompt();
        AnswerIsRightEventHandler();
        AnswerIsWrongEventHandlder();
        SessionIsPassedEventHandler();

        WelcomeTimer();
        IntervalTimer();
        voicePromptTimer();
        visualPromptTimer_fd();
        RightAnswerTimer();

        if (answered && answerIsCorrect)
            AnswerIsRight();
        else if (answered && !answerIsCorrect && !hadPlay1[4])
            AnswerIsWrong();
       
    }

    private void SetupQuestion()
    {
        // play welcome clip
        if (!hadPlay1[0] && _welcomeTimer <= 0)
        {
            playVoice.playVoice_1(0);
            hadPlay1[0] = true;
            roundHadFinished = true;
        }

        // play readyTouch clip
        if (!hadPlay1[1] && _intervalTimer <= 0 && roundHadFinished && roundCount < 5)
        {
            playVoice.playVoice_1(1);
            hadPlay1[1] = true;
            roundCount += 1;
            printStatus.PrintRounds();
        }

        // generate items randomly
        if (hadPlay1[1] && roundHadFinished && !generated)
        {
            genItems.GenerateTargetsForLevel12();
            generated = true;
            roundHadFinished = false;
        }
    }

    private void AnswerIsRightEventHandler()
    {
        if (triggeringEffects.rightAnswerHitted && !triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = true;
        }
    }

    private void AnswerIsWrongEventHandlder()
    {
        if (!hadPlay1[4] && !triggeringEffects.rightAnswerHitted && triggeringEffects.wrongAnswerHitted && triggeringEffects.startTrigger)
        {
            answered = true;
            answerIsCorrect = false;
        }
    }

    private void WelcomeTimer()
    {
        if (hadPlay1[0])
            _welcomeTimer = welcomeTimer;
        else
            _welcomeTimer -= Time.deltaTime;
    }

    private void IntervalTimer()
    {
        if (!PlayVoice.voiceAudioSource.isPlaying && roundHadFinished)
            _intervalTimer -= Time.deltaTime;
        else
            _intervalTimer = intervalTimer;
    }

    private void RightAnswerTimer()
    {
        if (answerIsCorrect && answered && !PlayVoice.voiceAudioSource.isPlaying && !triggeringEffects.triggeringSound.isPlaying)
            _rightAnswerTimer -= Time.deltaTime;
    }

    private void CleanTimerCounter()
    {
        _welcomeTimer = welcomeTimer;
        _intervalTimer = intervalTimer;
        _rightAnswerTimer = rightAnswerTimer;
        _voicePTempTimer = voicePTimer1;
        _visualPTempTimer_f = visualPromptTimer1_f;
        _visualPTempTimer_d = visualPromptTimer1_d;
        visualPromptCounter = 0;
        voicePromptCounter = 0;
    }

    private void SessionIsPassedEventHandler()
    {
        if (5 - testingScores1 <= 1 && sessionIsTesting)
            sessionIsPassed_1 = true;
        else
            sessionIsPassed_1 = false;

        if (5 - testingScores2 <= 1 && sessionIsTesting)
            sessionIsPassed_2 = true;
        else
            sessionIsPassed_2 = false;
    }

    private void voicePromt()
    {
        if (_voicePTempTimer <= 0 && !hadPlay1[3])
        {
            playVoice.playVoice_1(3);
            hadPlay1[3] = true;
        }

        if (hadPlay1[3])
        {
            voicePromptCounter += 1;
            printStatus.PrintVoicePrompts();

            if (voicePromptCounter <= 5)
                _voicePTempTimer = voicePTimer1;
            else if (voicePromptCounter > 5)
                _voicePTempTimer = voicePTimer2;

            hadPlay1[3] = false;
            answered = false;
            answerIsCorrect = false;
            triggeringEffects.CleanTrigger();
            generated = true;
            roundHadFinished = false;
        }
    }

    private void voicePromptTimer()
    {
        if (generated && !answered && !PlayVoice.voiceAudioSource.isPlaying)
            _voicePTempTimer -= Time.deltaTime;
    }

    private void visualPrompt()
    {
        if(_visualPTempTimer_f <= 0 && !startPrompts && !answered)
        {
            switch (visualPromptCounter)
            {
                case 0:
                    genItems.GenerateRingObject1();
                    genItems.GenerateRingObject2();
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    sessionIsTesting = false;
                    break;
                case 1:
                    GenItems.ringParticleInScene1.Play();
                    GenItems.ringParticleInScene2.Play();
                    GenItems.targetAnimator.SetBool("isWalking", true);
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    break;
                case 2:
                    GenItems.ringParticleInScene1.Play();
                    GenItems.ringParticleInScene2.Play();
                    GenItems.targetAnimator.SetBool("isWalking", true);
                    _visualPTempTimer_f = visualPromptTimer2_f;
                    startPrompts = true;
                    startTrail = true;
                    break;
                default:
                    break;
            }
        }

        if (startTrail)
            genItems.GenerateTrailObject();
        else if (!startTrail)
            OVRInput.SetControllerVibration(0, 0, GenItems.controllerMask);

        if(_visualPTempTimer_d <= 0 && startPrompts && !answered)
        {
            if(visualPromptCounter <= 1)
            {
                if (GenItems.ringParticleInScene1 != null)
                    GenItems.ringParticleInScene1.Stop();

                if (GenItems.ringParticleInScene2 != null)
                    GenItems.ringParticleInScene2.Stop();

                if (GenItems.targetAnimator != null)
                    GenItems.targetAnimator.SetBool("isWalking", false);

                if (visualPromptCounter == 0)
                    _visualPTempTimer_d = visualPromptTimer1_d;
                else if (visualPromptCounter == 1)
                    _visualPTempTimer_d = visualPromptTimer2_d;

                visualPromptCounter += 1;
                printStatus.PrintVisualPrompts();

                startTrail = false;
                startPrompts = false;
            }
            else if (visualPromptCounter == 2)
            {
                if(roundCount < 5 && !hadPlay3[0])
                {
                    playVoice.playVoice_3(0);
                    hadPlay3[0] = true;
                }
                if(roundCount == 5 && !hadPlay1[6] && !hadPlay3[3])
                {
                    SessionIsPassedEventHandler();
                    if (sessionIsPassed_1 && sessionIsPassed_2)
                    {
                        // congratulations!
                        playVoice.playVoice_1(6);
                        hadPlay1[6] = true;
                        getScores();
                    }
                    else
                    {
                        // don't worry! Let's take a break!
                        playVoice.playVoice_3(3);
                        hadPlay3[3] = true;
                        cleanSession();
                    }       
                }
                if (hadPlay3[0] || hadPlay3[3] || hadPlay1[6])
                    cleanField();
            }
        }
    }

    private void visualPromptTimer_fd()
    {
        if (generated && !answered)
        {
            if (!startPrompts)
                _visualPTempTimer_f -= Time.deltaTime;
            else if (startPrompts)
                _visualPTempTimer_d -= Time.deltaTime;
        }
        printStatus.PrintVisualPrompts_f();
        printStatus.PrintVisualPrompts_d();
    }

    private void AnswerIsRight()
    {
        if(!triggeringEffects.triggeringSound.isPlaying && !PlayVoice.voiceAudioSource.isPlaying)
        {
            if (!hadPlay1[2])
            {
                // good job!
                playVoice.playVoice_1(2);
                CalculateScores();
                hadPlay1[2] = true;
                hadPlay1[4] = true;
            }
        }

        rightAnswerEffects();

        if (_rightAnswerTimer <= 0 && hadPlay1[2])
        {
            if (roundCount < 5 && !hadPlay3[0])
            {
                // let's try for another round!
                playVoice.playVoice_3(0);
                hadPlay3[0] = true;
            }

            if (roundCount == 5 && !hadPlay3[2] && !hadPlay1[6])
            {
                SessionIsPassedEventHandler();
                if (sessionIsPassed_1 && sessionIsPassed_2)
                {
                    // congratulations!
                    playVoice.playVoice_1(6);
                    hadPlay1[6] = true;
                    getScores();
                }
                else
                {
                    // greate! Let's take a break!
                    playVoice.playVoice_3(2);
                    hadPlay3[2] = true;
                    cleanSession();
                }
            }
        }

        if (hadPlay3[0] || hadPlay3[2] || hadPlay1[6])
            cleanField();
    }

    private void AnswerIsWrong()
    {
        if (roundCount < 5 && !triggeringEffects.triggeringSound.isPlaying && !PlayVoice.voiceAudioSource.isPlaying)
        {
            // wrong, try again!
            playVoice.playVoice_1(4);
            hadPlay1[4] = true;
        }
        else if (roundCount == 5 && !hadPlay1[5] && !hadPlay1[7])
        {
            SessionIsPassedEventHandler();
            if (sessionIsPassed_1 && sessionIsPassed_2)
            {
                // wrong, but congratulation!
                playVoice.playVoice_1(7);
                hadPlay1[7] = true;
                getScores();
            }
            else
            {
                // wrong, let's take a break!
                playVoice.playVoice_1(5);
                hadPlay1[5] = true;
                cleanSession();
            }
        }

        if (hadPlay1[4] || hadPlay1[5] || hadPlay1[7])
            cleanField();
    }

    private void CalculateScores()
    {
        if (!sessionHadFinished)
        {
            if (sessionIsTesting)
            {
                testingScores1 += 1;
            }
            else if (!sessionIsTesting)
            {
                if (!transmitScores)
                {
                    teachingScores = testingScores1;
                    transmitScores = true;
                    sessionIsPassed_1 = false;
                }
                teachingScores += 1;
                testingScores1 = 0;
            }
        }
        else if (sessionIsPassed_1 && sessionHadFinished)
        {
            if (sessionIsTesting)
            {
                testingScores2 += 1;
                sessionHadStarted_2 = true;
            }
            else if (!sessionIsTesting)
            {
                if (!transmitScores)
                {
                    teachingScores = testingScores2;
                    transmitScores = true;
                    sessionHadStarted_2 = false;
                    sessionIsPassed_1 = false;
                }
                teachingScores += 1;
                testingScores1 = 0;
                testingScores2 = 0;
            }
        }
        printStatus.PrintScores();
    }

    private void cleanField()
    {
        genItems.CleanGenItems_t();
        CleanTimerCounter();

        printStatus.PrintVoicePrompts();
        printStatus.PrintVisualPrompts();
        printStatus.PrintVisualPrompts_f();
        printStatus.PrintVisualPrompts_d();

        hadPlay1[1] = false;
        hadPlay1[2] = false;
        hadPlay1[3] = false;
        hadPlay1[4] = false;
        hadPlay1[5] = false;
        hadPlay1[6] = false;
        hadPlay1[7] = false;
        hadPlay3[0] = false;
        hadPlay3[1] = false;
        hadPlay3[2] = false;
        hadPlay3[3] = false;

        startTrail = false;
        startPrompts = false;
        answered = false;
        answerIsCorrect = false;
        triggeringEffects.CleanTrigger();
        generated = false;
        roundHadFinished = true;
    }

    private void rightAnswerEffects()
    {
        if (hadPlay1[2] && !hadPlay3[1] && !PlayVoice.voiceAudioSource.isPlaying)
        {
            playVoice.playVoice_3(1);
            hadPlay3[1] = true;
        }
        if (hadPlay3[1])
            genItems.GenerateBubbles();

        if (GenItems.ringInScene1 != null)
            GenItems.ringParticleInScene1.Stop();

        if (GenItems.ringInScene2 != null)
            GenItems.ringParticleInScene2.Stop();

        if (startTrail)
            startTrail = false;

        if (GenItems.targetAnimator != null)
            GenItems.targetAnimator.SetBool("isWalking", true);

        genItems.GenerateRingObject0();
    }

    private void cleanSession()
    {
        roundCount = 0;
        sessionHadFinished = true;
        SessionIsPassedEventHandler();

        // sessions checking
        if (!sessionIsPassed_1 || !sessionIsTesting)
        {
            testingScores1 = 0;
            testingScores2 = 0;
            teachingScores = 0;
            sessionHadFinished = false;
        }

        if (sessionIsPassed_1 && !sessionIsPassed_2 && sessionHadStarted_2)
        {
            testingScores1 = 0;
            testingScores2 = 0;
            teachingScores = 0;
            sessionIsPassed_1 = false;
            sessionHadFinished = false;
        }
        printStatus.PrintScores();

        sessionIsTesting = true;
        transmitScores = false;
        sessionHadStarted_2 = false;   
    }

    private void getScores()
    {
        resultOfScore1_1 = "5/" + testingScores1.ToString();
        resultOfScore2_1 = "5/" + testingScores2.ToString();
    }

    private void ToZero()
    {
        roundCount = 0;
        voicePromptCounter = 0;
        visualPromptCounter = 0;

        testingScores1 = 0;
        testingScores2 = 0;
        teachingScores = 0;

        printStatus.PrintVoicePrompts();
        printStatus.PrintVisualPrompts();
        printStatus.PrintRounds();
        printStatus.PrintScores();
    }

}