using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonVertexInfo {
    public float x;
    public float y;
    public float z;
    public int index;

    public JsonVertexInfo(Vector3 vector, int index)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
        this.index = index;
    }
}

[System.Serializable]
public class JsonVertexInfoList
{
    public List<JsonVertexInfo> jsonInfoList;
}