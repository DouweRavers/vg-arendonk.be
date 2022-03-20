using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] float _scrollScaler = 0.05f;
    float _scrollValue = 0f;

    void Update()
    {
        _scrollValue = Mathf.Clamp(_scrollValue - Input.mouseScrollDelta.y * _scrollScaler, 0, 0.99f);
        Text[] items = GetComponentsInChildren<Text>();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].fontSize = 0.25f * i <= _scrollValue && _scrollValue < 0.25f * (i+1) ? 80 : 50;
            items[i].rectTransform.anchoredPosition = new Vector2(
                300 * Mathf.Cos(Mathf.Deg2Rad * Mathf.Lerp(0 - 15 * i, 45 - 15 * i, _scrollValue)),
                300 * Mathf.Sin(Mathf.Deg2Rad * Mathf.Lerp(0-15*i, 45 - 15 * i, _scrollValue))
                );
        }
        Debug.Log(_scrollValue);
    }

}
