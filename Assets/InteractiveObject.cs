using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class InteractiveObject : NetworkBehaviour, IPointerClickHandler {

    [SerializeField] private GameObject cube;
    [SerializeField] private Material[] materials;
    private int materialsCount = 0;

    public Material currMaterial;
    private Material startMaterial;

    private void Start()
    {
        cube = GameObject.FindWithTag("Cube");
        currMaterial = cube.GetComponent<Renderer>().material;
        startMaterial = currMaterial;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        print("ONCLICK");
        nextColor();
    }

    #region nextColor
    private void nextColor()
    {
        if (isServer)
        {
            print("ISSERVER");

            RpcNextColor();
        }
        else
        {
            print("ISCLIENT");

            CmdNextColor();
        }
    }

    [Command]
    private void CmdNextColor()
    {
        RpcNextColor();
    }
    [ClientRpc]
    private void RpcNextColor()
    {
        if (materialsCount >= materials.Length)
        {
            materialsCount = 0;
        }
        cube.GetComponent<Renderer>().material = materials[materialsCount];
        currMaterial = materials[materialsCount];
        materialsCount++;
    }
    #endregion
}
