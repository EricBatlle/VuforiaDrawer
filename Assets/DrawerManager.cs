using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour {

    public void CreateDraw(JsonInfo[] drawPositions)
    {
        //Get ARDrawer
        ARDrawer drawer = GameObject.Find("ARDrawer").GetComponent<ARDrawer>();
        //Set positions
        drawer.createDraw(drawPositions);
    }
}
