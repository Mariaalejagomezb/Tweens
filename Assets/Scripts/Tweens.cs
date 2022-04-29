using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweens : MonoBehaviour
{
    [Header("Tween params")]
    [SerializeField]
    private float duration = 1;
    
    [SerializeField]
    private Transform targetPos;

    [SerializeField]
    private AnimationCurve ease;

    //Internals
    private bool isPlaying = false;
    private float accumulatedTime = 0;
    private Vector3 startPosition;

    void Start()
    {
        Debug.Assert(targetPos != null, "Target pos is null");
        startPosition = transform.position;
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            accumulatedTime = 0;
            isPlaying = true;
            startPosition = transform.position;
        }

        if (!isPlaying) return;

        float t = accumulatedTime / duration;
        transform.position = Vector3.LerpUnclamped(startPosition, targetPos.position, ease.Evaluate(t));
        accumulatedTime += Time.deltaTime;

        if(t>=1)
        {
            isPlaying = false;
            Debug.Log("Completed");
        }
    }
    private float easeInBack(float x)
    {
          float c1 = 1.70158f;
          float c3 = c1 + 1f;

          return c3* x * x* x - c1* x * x;
    }
}
