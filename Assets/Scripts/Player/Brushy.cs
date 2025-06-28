
using Unity.VisualScripting;
using UnityEngine;

public class Brushy : MonoBehaviour
{
    [SerializeField] GameObject brushy;

    [SerializeField] Material[] materials = new Material[4];
    int colorCounter = 0;

    private void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            ChangeColor(colorCounter);
        }
    }


    private void ChangeColor(int color)
    {
        brushy.GetComponent<Renderer>().material = materials[color];

        colorCounter++;
        //Debug.Log(colorCounter);

        if (colorCounter == materials.Length) 
            colorCounter = 0;
        
    }
 

}
