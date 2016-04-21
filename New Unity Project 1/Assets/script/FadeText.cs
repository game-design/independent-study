using UnityEngine;
using System.Collections;

public class FadeText : MonoBehaviour {
    public float transitionSpeed = 30;

    float currentGoal = 0;
    TextMesh textMesh;


    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        textMesh.color = new Vector4(textMesh.color.r, textMesh.color.g, textMesh.color.b, currentGoal);
    }

    void Update()
    {
        textMesh.color = Vector4.MoveTowards(textMesh.color, new Vector4(textMesh.color.r, textMesh.color.g, textMesh.color.b, currentGoal), Time.deltaTime * transitionSpeed);
    }



    void OnTriggerEnter(Collider collider)
    {
        TriggerValue value = collider.GetComponent<TriggerValue>();
        if (value != null)
        {
            currentGoal = value.triggerFloat;
        }
    }
}
