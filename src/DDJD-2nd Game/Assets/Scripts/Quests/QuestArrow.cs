using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArrow : MonoBehaviour
{
    public Transform target;
    public float buffer;

    public Color farColor;
    public Color closeColor;
    public float maxDistance;
    
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(target != null)
        {
            Vector2 difference = transform.position - target.position;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,angle + buffer);
        }
        
    }

    float DistanceToQuest(){
        return Mathf.Clamp01(Vector2.Distance(transform.position, target.position) / maxDistance);
    }
}