
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Avatar
{
    public int id;
    public string name;

    [TextArea]
    public string description;
    public Sprite sprite;
}