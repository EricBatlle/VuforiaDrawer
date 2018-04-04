using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour {

    [SerializeField] private ARDrawer drawer;

    private void Start()
    {
        //Get ARDrawer
        drawer = GameObject.Find("ARDrawer").GetComponent<ARDrawer>();
    }

    public void loadDraw(JsonVertexInfo[] drawPositions)
    {
        //Set positions
        LoadDrawFromJSON(drawPositions);
    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            SaveDrawToJSON();
        }
    }

    public void LoadDrawFromJSON(JsonVertexInfo[] drawPositions)
    {
        LineRenderer lr = drawer.GetComponent<LineRenderer>();
        foreach (JsonVertexInfo drawPos in drawPositions)
        {
            //Generate new vertex position from JSON
            Vector3 newVertexPosition = new Vector3(drawPos.x, drawPos.y, drawPos.z);

            //Add the new position to line renderer
            drawer.positionsLine.Add(newVertexPosition);            //Add to the auxiliar vector the new position
            lr.positionCount = drawer.positionsLine.Count;          //Equalize positions from auxiliar vector to current LR positions vector
            lr.SetPosition(drawPos.index, newVertexPosition);       //Add the new position 
        }
    }

    public void SaveDrawToJSON()
    {
        createDrawFile(createDrawString());
    }

    public String createDrawString()
    {
        LineRenderer lr = drawer.GetComponent<LineRenderer>();
        Vector3 vector = new Vector3(2f, 3.4f, 4.56f);

        JsonVertexInfoList list = new JsonVertexInfoList();         //Auxiliar vector to store LR points
        list.jsonInfoList = new List<JsonVertexInfo>();

        for (int i = 0; i < lr.positionCount; i++)
        {
            //get lr vertex
            Vector3 auxVertex = lr.GetPosition(i);
            //create JSONVertexInfo from auxVertex
            JsonVertexInfo vertexInfo = new JsonVertexInfo(auxVertex, i);
            //add it to the list
            list.jsonInfoList.Add(vertexInfo);
        }

        //Handle string conversion to our JSON format
        String result = JsonUtility.ToJson(list);
        result = result.Substring(result.IndexOf("["));             //Remove everything before [ 
        result = result.Substring(0, result.LastIndexOf("]") + 1);  //Remove everything after ] 

        return result;
    }

    void createDrawFile(String text)
    {
        System.IO.File.WriteAllText(@"C:\var\www\test\drawTry.json", text);
        //post it to the server
        //POST(text);
    }

}
