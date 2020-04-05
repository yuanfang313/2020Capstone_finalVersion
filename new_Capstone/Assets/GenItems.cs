using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenItems : MonoBehaviour
{
    // public region
    public List<GameObject> prefabTargets = new List<GameObject>();
    public List<Transform> target_p = new List<Transform>();
    public Transform bubbleTransformAnchor;
    public GameObject ringEffect_0, ringEffect_1, ringEffect_2;
    public GameObject bubbleParticles;
    public GameObject prefabTrailObject;

    // private region
    private GameObject target1InScene;
    private GameObject target2InScene;
    private GameObject trailsInScene;
    private GameObject ringInScene0, ringInScene1, ringInScene2;
    private GameObject bubbleInScene;
    

    private ParticleSystem[] ringParticleInScene0;
    private ParticleSystem ringParticleInScene1, ringParticleInScene2;
    private Animator deerAnimator;
    private Transform rightAnswerTransformForRings;
 
    private bool startRing0 = false;
    private bool startRing1 = false;
    private bool startRing2 = false;
    private bool startBubbles = false;

    public float startTimeBtwShots;
    private float timeBtwShots;

    private Transform controllerTransform;
    private OVRInput.Controller controllerMask;



    private void Awake()
    {
        ControllerStatus.OnControllerSource += UpdateOrigin;
    }
    private void OnDestroy()
    {
        ControllerStatus.OnControllerSource -= UpdateOrigin;
    }
    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        controllerTransform = controllerObject.transform;
        controllerMask = controller;
    }

    public void GenerateTargetsForTutorial()
    {
        int positions = Random.Range(1, 3);

        if (positions == 1)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[0].position, Quaternion.AngleAxis(230, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }
        else if (positions == 2)
        {
            target1InScene = Instantiate(prefabTargets[0], target_p[1].position, Quaternion.AngleAxis(130, Vector3.up));
            rightAnswerTransformForRings = GameObject.FindGameObjectWithTag("rightAnswerPosition_rings").transform;
        }

        deerAnimator = target1InScene.GetComponent<Animator>();
    }

    public void GenerateTrailObject()
    {
        if (timeBtwShots <= 0)
        {
            trailsInScene = Instantiate(prefabTrailObject, controllerTransform.position, Quaternion.identity);
            OVRInput.SetControllerVibration(0.5f, 0.5f, controllerMask);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            OVRInput.SetControllerVibration(0.1f, 0.1f, controllerMask);
        }
    }
    // generate ring1 + ring2
    public void GenerateRingObject0()
    {
        if (!startRing0)
        {
            ringInScene0 = Instantiate(ringEffect_0, rightAnswerTransformForRings.position, Quaternion.AngleAxis(0, Vector3.left));
            ringParticleInScene0 = ringInScene0.GetComponentsInChildren<ParticleSystem>();
            startRing0 = true;
        }
    }
    // generate ring1
    public void GenerateRingObject1()
    {
        if (!startRing1)
        {
            ringInScene1 = Instantiate(ringEffect_1, rightAnswerTransformForRings.position, Quaternion.AngleAxis(-90, Vector3.left));
            ringParticleInScene1 = ringInScene1.GetComponent<ParticleSystem>();
            startRing1 = true;
        }
    }
    // generate ring2
    public void GenerateRingObject2()
    {
        if (!startRing2)
        {
            ringInScene2 = Instantiate(ringEffect_2, rightAnswerTransformForRings.position, Quaternion.AngleAxis(-90, Vector3.left));
            ringParticleInScene2 = ringInScene2.GetComponent<ParticleSystem>();
            startRing2 = true;
        }
    }
    // generate bubbles
    public void GenerateBubbles()
    {
        if (!startBubbles)
        {
            bubbleInScene = Instantiate(bubbleParticles, bubbleTransformAnchor.position, Quaternion.AngleAxis(-90, Vector3.left));
            startBubbles = true;
        }
    }

}
