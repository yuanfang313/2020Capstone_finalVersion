using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleCanvas : MonoBehaviour
{

    public static bool canvas1HadOpened = false;
    public static bool canvas2HadOpened = false;
    public static bool canvas0HadOpened = false;
    public Transform centerEyeTransform;
    
    public GameObject canvasInModule1;
    public GameObject canvasInModule2;
    public GameObject canvasInTutorial;

    private Vector3 centerEyePosition;
    private Vector3 canvasPositionAnchor;
    private bool canvasHadOpened = false;
    private bool buttonIsPressed = false;



    private void Awake()
    {
        ControllerStatus.ButtonDown += buttonHadPressed;    
    }
    private void Start()
    {

    }
    private void OnDestroy()
    {
        ControllerStatus.ButtonDown -= buttonHadPressed;
    }

    // Update is called once per frame
    void Update()
    {

        if (!canvasHadOpened && buttonIsPressed)
        {
             
            if (triggeringEffects.module1HadLoaded)
            {
                canvasInModule1.SetActive(true);
                //canvasInModule1.transform.position = SetCanvasPosition();
                canvas1HadOpened = true;
            } else if (triggeringEffects.tutorialHadLoaded)
            {
                canvasInTutorial.SetActive(true);
                //canvasInTutorial.transform.position = SetCanvasPosition();
                canvas0HadOpened = true;
            }

            canvasHadOpened = true;
        }
        else if (canvasHadOpened && buttonIsPressed)
        {
            canvasInModule1.SetActive(false);
            canvasInModule2.SetActive(false);
            canvasInTutorial.SetActive(false);
            canvas1HadOpened = false;
            canvas2HadOpened = false;
            canvas0HadOpened = false;
            canvasHadOpened = false;
        }
    }


    private void buttonHadPressed (bool buttonPressed)
    {
        buttonIsPressed = buttonPressed;
    }

    private Vector3 SetCanvasPosition()
    {
        centerEyePosition = centerEyeTransform.position;

        float y = centerEyePosition.y - 0.9f;
        if(y >= 0.4f)
        {
            canvasPositionAnchor = new Vector3(centerEyePosition.x, y, centerEyePosition.z + 2.0f);
        }
        else if (y < 0.4f)
        {
            canvasPositionAnchor = new Vector3(centerEyePosition.x, 0.4f, centerEyePosition.z + 2.0f);
        }
        
        return canvasPositionAnchor;
    }
}
