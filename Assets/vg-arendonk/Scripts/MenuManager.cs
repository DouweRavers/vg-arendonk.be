using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] float ScrollScaler = 0.05f;

    public float ScrollValue { get { return _scrollValue; } }
    float _scrollValue = 0f;

    MenuItem[] Pages;



    void Start()
    {
        ScanForPages();
        PositionPages();
    }



    void Update()
    {
        _scrollValue = Mathf.Clamp(_scrollValue - Input.mouseScrollDelta.y * ScrollScaler, 0, 1);
        if (Input.mouseScrollDelta.y != 0)
        {
            PositionPages();
        }
    }
    void ScanForPages()
    {
        List<MenuItem> PageItems = new List<MenuItem>();
        for (int i = 0; i < transform.childCount; i++)
        {
            MenuItem item;
            if (transform.GetChild(i).TryGetComponent<MenuItem>(out item)) PageItems.Add(item);
        }
        Pages = PageItems.ToArray();
    }

    void PositionPages()
    {
        float additionalAngle = 0;
        for (int i = 0; i < Pages.Length; i++)
        {
            float angle = Mathf.Lerp(0 - 15 * i, 15 * (Pages.Length - 1) - 15 * i, _scrollValue);
            Pages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                300 * Mathf.Cos(Mathf.Deg2Rad * (angle + additionalAngle)),
                300 * Mathf.Sin(Mathf.Deg2Rad * (angle + additionalAngle))
                ); 
            if (0 <= angle && angle < 15)
            {
                Pages[i].GetComponent<Text>().fontSize = 80;
                if (Pages[i].hasSubmenu) {
                    additionalAngle = -20;
                }
            }
            else {
                Pages[i].GetComponent<Text>().fontSize = 50;

            }
            
        }
    }

}

