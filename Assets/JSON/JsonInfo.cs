using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonInfo {
    public float x;
    public float y;
    public float z;
    public int index;
}

[System.Serializable]
public class JsonInfoList
{
    public List<JsonInfo> jsonInfoList;
}