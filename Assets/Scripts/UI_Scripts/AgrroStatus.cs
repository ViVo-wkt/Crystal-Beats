using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgrroStatus : MonoBehaviour
{
    public static AgrroStatus instance;
    public Image agrroimage;
    // Start is called before the first frame update
    void Start()
    {
        agrroimage = GetComponent<Image>();
        instance = this;
    }

    
    public void SetAggroState(bool isAggro)
    {
        if (agrroimage != null)
        {
            
            if(isAggro)
            {
                agrroimage.color = Color.red;
            }
            else
            {
                agrroimage.color = Color.green;
            }
        }
    }
}
