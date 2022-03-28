using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Button))]
public class MenuItem : MonoBehaviour
{
    public string Title
    {
        get { return GetComponent<Text>().text; }
        set { GetComponent<Text>().text = value; } 
    }

    public MenuItem[] SubMenuItems
    {
        get { 
            List<MenuItem> items = new List<MenuItem>(GetComponentsInChildren<MenuItem>());
            items.Remove(this);
            return items.ToArray();
        }
    }
}
