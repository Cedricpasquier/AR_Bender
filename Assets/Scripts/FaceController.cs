using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System;
using System.Timers;

[RequireComponent(typeof(ARFaceManager))]

public class FaceController : MonoBehaviour
{
    private ARFaceManager arFaceManager;

    [SerializeField]
    private Button swapFilter;

    private int swapCounter = 0;

    [SerializeField]
    private FaceMaterial[] materials;

    float eulerAngY;

    private int flag;

    // Start is called before the first frame update
    void Start()
    {
        arFaceManager = GetComponent<ARFaceManager>();
        swapFilter.onClick.AddListener(SwapeFaces);
        flag = 0;
    }


    void Update()
    {
        
        foreach (ARFace face in arFaceManager.trackables)
        {
            eulerAngY = face.transform.localEulerAngles.y;
            if (eulerAngY <= 30f  || eulerAngY >= 330f)
            {
                flag = 0;
            } else if(flag ==0)
            {
                SwapeFaces();
                flag = 1;
            } 
        }
    }


    void SwapeFaces()
    {
        swapCounter = swapCounter == materials.Length-1 ? 0 : swapCounter + 1;

        foreach (ARFace face in arFaceManager.trackables)
        {
            face.GetComponent<MeshRenderer>().material = materials[swapCounter].Material;
        }
    }


    [System.Serializable]

    public class FaceMaterial
    {
        public Material Material;

        public string Name;
    }
}

