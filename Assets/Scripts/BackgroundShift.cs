using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


    public class BackgroundShift : MonoBehaviour
    {

        [SerializeField]
        private Material background;

        [SerializeField]
        private Color32 color1, color2;
        private Color32 startCol1, startCol2;

        [SerializeField]
        private float pitch = 140, yaw = 180;
        
        // Start is called before the first frame update
        void Awake()
        {

        color1 = new Color32(227, 189, 255, 255);
        color2 = new Color32(144, 117, 245, 255);

        background.SetColor("_Color1", color1);
        background.SetColor("_Color2", color2);
        background.SetFloat("_DirY", pitch);
        background.SetFloat("_DirX", yaw);

        }

        // Update is called once per frame
        void Update()
        {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            background.SetColor("_Color1", color1);
            background.SetColor("_Color2", color2);
            background.SetFloat("_DirY", pitch);
            background.SetFloat("_DirX", yaw);
        }


        }
    }
