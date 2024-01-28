using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionBar : MonoBehaviour
{
    public Slider slider;
    private Image fill;
    private Image sadMouth;
    private Image happyMouth;
    private Image excitedMouth;

    [SerializeField]
    private Color greenColor = new Color(10f / 255f, 255f / 255f, 55f / 255f);
    [SerializeField]
    private Color yellowColor = new Color(255f / 255f, 255f / 255f, 55f / 255f);
    [SerializeField]
    private Color redColor = new Color(255f / 255f, 55f / 255f, 80f / 255f);

    private EmotionController emotionController;


    private void OnEnable()
    {
        EmotionController.OnSatisfactionIncreased += SetSatisfaction;
        EmotionController.OnSatisfactionDecreased += SetSatisfaction;
    }

    private void OnDisable()
    {
        EmotionController.OnSatisfactionIncreased -= SetSatisfaction;
        EmotionController.OnSatisfactionDecreased += SetSatisfaction;
    }

    private void Start()
    {
        Debug.Log(gameObject.transform.Find("Avatar/FaceSad").gameObject);
        // get emotion fill image 
        fill = gameObject.transform.Find("EmotionFill").gameObject.GetComponent<Image>();

        emotionController = FindObjectOfType<EmotionController>();
        // get differnt moth variants
        sadMouth = gameObject.transform.Find("Avatar/FaceSad").gameObject.GetComponent<Image>();  //GetComponent<Image>();
        happyMouth = gameObject.transform.Find("Avatar/FaceSmile").gameObject.GetComponent<Image>();
        excitedMouth = gameObject.transform.Find("Avatar/FaceExcited").gameObject.GetComponent<Image>();
       

        sadMouth.enabled = false;
        happyMouth.enabled = false;

        fill.color = greenColor;
        

    }
    public void SetSatisfaction(int points)
    {

        slider.value = points;

        sadMouth.enabled = false;
        happyMouth.enabled = false;
        excitedMouth.enabled = false;


        // teh theshold points for Emotion color are set in the Emotion controller class
        if (points < emotionController.mediumTheshold 
            && points >= emotionController.lowTheshold )
        {

            fill.color = yellowColor;
            happyMouth.enabled = true;
        } else if (points < emotionController.lowTheshold) {

            fill.color = redColor;
            sadMouth.enabled = true;
        } else {
            fill.color = greenColor;
            excitedMouth.enabled = true;
        }

       
       

    }

    public void SetMaxSatisfaction(int points) {

        slider.maxValue = points;
        slider.value = points;

       
    }


}
