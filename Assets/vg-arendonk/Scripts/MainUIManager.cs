using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Cinemachine;
public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    Transform Cameras;
    
    VisualElement _root;
    VisualElement _activeSubMenu = null;
    
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        SetUpTitleMenu(); 
    }

    void Update()
    {
        UpdateMenu();
    }

    void SetUpTitleMenu()
    {
        VisualElement titleMenu = _root.Q<VisualElement>("TitleMenu");
        List<VisualElement> children = new List<VisualElement>(titleMenu.Children());
        for (int i = 0; i < children.Count; i++)
        {
            Label label = children[i] as Label;
            if (label == null) {
                children[i].AddToClassList("menu-hide");
                children[i].AddToClassList("menu-show");
                children[i].EnableInClassList("menu-hide", true);
                children[i].EnableInClassList("menu-show", false);
                foreach (VisualElement child in children[i].Children())
                {
                    label = child as Label;
                    if (label == null) continue;
                    label.RegisterCallback<MouseDownEvent>((evt) =>
                    {
                        Debug.Log(label.name);
                    });
                }
                continue;
            }; 
            int x = i;

            label.RegisterCallback<MouseDownEvent>((evt) => {
                if (_activeSubMenu != null)
                {
                    _activeSubMenu.EnableInClassList("menu-hide", true);
                    _activeSubMenu.EnableInClassList("menu-show", false);
                }
                VisualElement subMenu = _root.Q<VisualElement>(label.name + "-menu");
                if (subMenu != null) {
                    _activeSubMenu = subMenu;
                    _activeSubMenu.EnableInClassList("menu-hide", false);
                    _activeSubMenu.EnableInClassList("menu-show", true);
                }
                CinemachineVirtualCamera[] cameras = Cameras.GetComponentsInChildren<CinemachineVirtualCamera>();
                CinemachineVirtualCamera camera = cameras[x % cameras.Length];
                foreach (var cam in cameras) cam.Priority = 0;
                camera.Priority = 10;
                Debug.Log(x + ": " + camera.name);
            });
        }
    }

    void UpdateMenu()
    {
        VisualElement titleMenu = _root.Q<VisualElement>("TitleMenu");
        VisualElement logo = _root.Q<VisualElement>("Logo");
        List<VisualElement> children = new List<VisualElement>(titleMenu.Children());
        
        Vector2 uiCenter = _root.worldBound.center;
        float radius = logo.worldBound.width * 1.3f;
        if (radius.Equals(float.NaN)) return;
        for (int i = 0; i < children.Count; i++)
        {
            Label label = children[i] as Label;
            if (label == null)
            {
                children.AddRange(children[i].Children());
                continue;
            };
            Vector2 elementCenter = label.worldBound.center;
            float angle = Mathf.Asin((elementCenter.y - uiCenter.y) / radius);
            float offset = (1 - Mathf.Cos(angle)) * radius;
            label.transform.position = Vector3.left * offset;
        }
    }
}
