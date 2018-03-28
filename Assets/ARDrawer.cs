using System.Collections.Generic;
using UnityEngine;

//Creaed by Eric Batlle Clavero

public class ARDrawer : MonoBehaviour {


    [SerializeField] private float startWidth = 0.01f; //Try to maintain the ratio of start-endWitdh...
    [SerializeField] private float endWidth = 0.01f;  //...if you don't want the "stencil" effect
    [SerializeField] private Shader shader;          //Shader to render the lines: LegacyShader/Diffuse works

    private List<Vector3> positionsLine = new List<Vector3>();  //Auxiliar vector to store LR points

    void Start()
    {
        //Create LR component
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        //Set LR attributes
        lineRenderer.material = new Material(shader);
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.useWorldSpace = false;
        //lineRenderer.SetVertexCount(0);
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

    public void createDraw(JsonInfo[] drawPositions)
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        foreach (JsonInfo drawPos in drawPositions)
        {
            //Generate new vertex position from JSON
            Vector3 newVertexPosition = new Vector3(drawPos.x, drawPos.y, drawPos.z);

            //Add the new position to line renderer
            positionsLine.Add(newVertexPosition);                 //Add to the auxiliar vector the new position
            lr.positionCount = positionsLine.Count;         //Equalize positions from auxiliar vector to current LR positions vector
            lr.SetPosition(drawPos.index, newVertexPosition);     //Add the new position 
        }
    }
}
