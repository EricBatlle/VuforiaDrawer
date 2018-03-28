using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creaed by Eric Batlle Clavero

public class ARDrawer : MonoBehaviour {

    [SerializeField] private GameObject ARCamera;
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int vertexCount = 10;

    List<Vector3> positionsLine = new List<Vector3>();
    
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Diffuse"));
        lineRenderer.SetVertexCount(0);
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(.01f, .01f);
        lineRenderer.useWorldSpace = false;

    }
    void Update()
    {        
        //while user is holding down mouse
        if (Input.GetMouseButton(0))
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            Vector3 mp = Input.mousePosition;
            mp.z = 0.5f;
            Vector3 mwc = Camera.main.ScreenToWorldPoint(mp);
            Debug.Log(mwc);
            UpdateLine(mwc);
        }
    }

    void UpdateLine(Vector3 newPosWorld)
    {
        #region comments
        //find the line gameobject (there should be only one)
        //GameObject lineObject = GameObject.FindWithTag("line");
        //get line renderer component
        //LineRenderer lr = lineObject.GetComponent<LineRenderer>();

        //ToDo: Move this out of the updateLineLoop
        //Custom LR settings
        //lr.transform.SetParent(transform);//Set the line renderer new objects as a child of the object who calls the script
        //lr.material = new Material(shader);
        //lr.material.color = color;
        //lr.startWidth = startWidth;
        //lr.endWidth = endWidth;
        //instance.gameObject.GetComponent<LineRenderer>().useWorldSpace = false;//Set positions to relative, in this case, to the parent object --> the camera
        #endregion
        //get line renderer component
        LineRenderer lr = GetComponent<LineRenderer>();
        // the line renderer's points are set to 'local', not world.
        // so we have to transform the world position of new point in local coords  
        Vector3 newPosLocal = lr.transform.InverseTransformPoint(newPosWorld);
        //add the new position to line renderer
        positionsLine.Add(newPosLocal);

        //lr.positionCount++;
        lr.positionCount = positionsLine.Count;
        lr.SetPosition(lr.positionCount - 1, newPosLocal);
    }
}
