using System;

[Serializable]
public struct LayoutStruct
{
    public Page[] Pages;
}


[Serializable]
public struct Page
{
    public string Title;
    public SubPage[] SubPages;
}


[Serializable]
public struct SubPage
{
    public string Title;
}