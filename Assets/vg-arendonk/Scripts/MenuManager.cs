using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] float ScrollScaler = 0.05f;
    [SerializeField] GameObject MainMenuPrefab;
    [SerializeField] GameObject SubMenuPrefab;

    MenuItem[] mainMenuItems;
    int mainActive = 0;
    float location = 0f;

    void Start()
    {
        string[] mainItems = new[] { "Home", "Nieuws", "Contact", "other", "More" };
        string[] subItems = new[] { "Nieuwe website", "Alleh nog wel in ontwikkeling" };
        InitializeMainItems(mainItems, subItems);
        PositionPages();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            PositionPages();
        }
    }

    void InitializeMainItems(string[] mainItems, string[] subItems)
    {
        List<MenuItem> menuItems = new List<MenuItem>();
        foreach (string item in mainItems)
        {
            GameObject menuObject = Instantiate(MainMenuPrefab);
            menuObject.name = item;
            menuObject.transform.SetParent(transform);
            MenuItem menuItem = menuObject.GetComponent<MenuItem>();
            menuItem.Title = item;
            menuItems.Add(menuItem);
            menuItem.GetComponent<Text>().enabled = true;
            if (item == "Nieuws")
            {
                foreach (string subItem in subItems)
                {
                    GameObject subMenuObject = Instantiate(SubMenuPrefab);
                    subMenuObject.name = subItem;
                    subMenuObject.transform.SetParent(menuObject.transform);
                    MenuItem subMenuItem = subMenuObject.GetComponent<MenuItem>();
                    subMenuItem.Title = subItem;
                    subMenuItem.GetComponent<Text>().enabled = false;
                }
            }
        }
        mainMenuItems = menuItems.ToArray();
    }

    void PositionPages()
    {
        List<MenuItem> visibleItems = new List<MenuItem>(mainMenuItems);
        location = Mathf.Clamp(location - Input.mouseScrollDelta.y * ScrollScaler, 0, visibleItems.Count-1);
        if (mainActive <= Mathf.RoundToInt(location) &&
            Mathf.RoundToInt(location) <= mainActive + visibleItems[mainActive].SubMenuItems.Length)
        {
            visibleItems.InsertRange(mainActive + 1, visibleItems[mainActive].SubMenuItems);
            foreach (MenuItem item in visibleItems[mainActive].SubMenuItems) item.GetComponent<Text>().enabled = true;
        }
        else
        {
            foreach (MenuItem item in visibleItems[mainActive].SubMenuItems) item.GetComponent<Text>().enabled = false;
            if(mainActive < Mathf.RoundToInt(location)) location -= visibleItems[mainActive].SubMenuItems.Length;
            mainActive = Mathf.RoundToInt(location);
        }

        for (int i = 0; i < visibleItems.Count; i++)
        {
            MenuItem item = visibleItems[i];
            float angle = 15 * (location - i);
            Vector2 position = new Vector2(
                300 * Mathf.Cos(Mathf.Deg2Rad * (angle)),
                300 * Mathf.Sin(Mathf.Deg2Rad * (angle))
            );
            item.GetComponent<RectTransform>().position = transform.TransformPoint(position);
            if (i == Mathf.RoundToInt(location))
            {
                if(item.transform.parent.GetComponent<MenuItem>() == null) item.GetComponent<Text>().fontSize = 80;
                else item.GetComponent<Text>().fontSize = 60;
            }
            else if (Mathf.Abs(i - Mathf.RoundToInt(location)) < 3)
            {
                if (item.transform.parent.GetComponent<MenuItem>() == null) item.GetComponent<Text>().fontSize = 50;
                else item.GetComponent<Text>().fontSize = 30;
            }
            else
            {
                if (item.transform.parent.GetComponent<MenuItem>() == null) 
                    item.GetComponent<Text>().fontSize = (int)Mathf.Lerp(50, 0, Mathf.Abs(location - i) / 3 - 1);
                else
                    item.GetComponent<Text>().fontSize = (int)Mathf.Lerp(30, 0, Mathf.Abs(location - i) / 3 - 1);
            }
        }
    }
}

