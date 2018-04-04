using System;
using System.Collections.Generic;
using UnityEngine;

//Creaed by Eric Batlle Clavero

public class ARDrawer : MonoBehaviour {


    [SerializeField] private float startWidth = 0.01f; //Try to maintain the ratio of start-endWitdh...
    [SerializeField] private float endWidth = 0.01f;  //...if you don't want the "stencil" effect
    [SerializeField] private Shader shader;          //Shader to render the lines: LegacyShader/Diffuse works

    public List<Vector3> positionsLine = new List<Vector3>();  //Auxiliar vector to store LR points

    void Start()
    {
        //Create LR component and set initial attributes        
        setLine();
    }
  
    void Update()
    {        
        //While user is holding down mouse
        if (Input.GetMouseButton(0))
        {
            //Get the user clicks position
            Vector3 mp = Input.mousePosition;                   //Get the users mouse click
            mp.z = 0.5f;                                        //reset z coord so it's the same as model
            Vector3 mwc = Camera.main.ScreenToWorldPoint(mp);   //Get mouse world coords

            //Set LR positions to build the draw
            UpdateLine(mwc);
        }
        if (Input.GetKeyDown("space"))
        {
            //Remove old LR
            removeLine();
            //Create LR component and set initial attributes        
            setLine();
        }

    }

    void setLine()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>(); //Create component
        //Set LR attributes
        lineRenderer.material = new Material(shader);
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.useWorldSpace = false;
        lineRenderer.SetVertexCount(0);                         //Restart the positions just in case buffers make some extrapoints
    }

    void removeLine()
    {
        LineRenderer lr = GetComponent<LineRenderer>(); //Get initial component
        if (lr != null)
        {
            DestroyImmediate(lr);                       //Destroy it inmediatly, in case another line renderer is added just next to it
            positionsLine.Clear();                      //Needed to avoid start drawing from 0,0,0
        }
    }

    void UpdateLine(Vector3 newPosWorld)
    {
        //Get LR component
        LineRenderer lr = GetComponent<LineRenderer>();

        // The line renderer's points are set to 'local', not world...
        // ...so we have to transform the world position of new point in local coords  
        Vector3 newPosLocal = lr.transform.InverseTransformPoint(newPosWorld);

        //Add the new position to line renderer
        positionsLine.Add(newPosLocal);                     //Add to the auxiliar vector the new position
        //lr.positionCount++;                               
        lr.positionCount = positionsLine.Count;             //Equalize positions from auxiliar vector to current LR positions vector
        lr.SetPosition(lr.positionCount - 1, newPosLocal);  //Add the new position
    }
   
}
