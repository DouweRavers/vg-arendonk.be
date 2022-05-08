using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class MainUIManager : MonoBehaviour
{
    VisualElement _root;
    float _radius = 0f;
    bool _updateUI = false;

    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        SetUpTitleMenu(); 
    }

    void Update()
    {
        if(Time.frameCount == 2) _updateUI = true;
        if (_updateUI) UpdateMenu(); 
    }

    void SetUpTitleMenu()
    {
        VisualElement titleMenu = _root.Q<VisualElement>("TitleMenu");
        List<VisualElement> children = new List<VisualElement>(titleMenu.Children());
        for (int i = 0; i < children.Count; i++)
        {
            Label label = children[i] as Label;
            if (label == null) {
                children[i].style.display = DisplayStyle.None;
                children.AddRange(children[i].Children());
                continue;
            };
            label.RegisterCallback<MouseDownEvent>((evt) => { 
                Debug.Log(label.text);
                VisualElement subMenu = _root.Q<VisualElement>(label.name + "-menu");
                if (subMenu != null) {
                    subMenu.style.display = DisplayStyle.Flex;
                    _updateUI = true;
                }
            }, TrickleDown.TrickleDown);
        }
    }

    void UpdateMenu()
    {
        _updateUI = false;
        VisualElement titleMenu = _root.Q<VisualElement>("TitleMenu");
        List<VisualElement> children = new List<VisualElement>(titleMenu.Children());
        
        Vector2 uiCenter = _root.worldBound.center;
        for (int i = 0; i < children.Count; i++)
        {
            Label label = children[i] as Label;
            if (label == null)
            {
                children.AddRange(children[i].Children());
                continue;
            };
            Vector2 elementCenter = label.worldBound.center;
            if (_radius == 0) _radius = elementCenter.x;
            float angle = Mathf.Asin((elementCenter.y-uiCenter.y)/ _radius);
            float offset = (1 - Mathf.Cos(angle)) * _radius;
            if(i == 0)Debug.Log(elementCenter);
            label.transform.position = Vector3.left * offset;
        }
    }
}
