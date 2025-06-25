
using System.Drawing;
using Unity.Android.Types;
using UnityEngine;

public class ColoPlatformBehavior : MonoBehaviour
{
    [SerializeField] Material[] materials = new Material[4];
    [SerializeField] GameObject platform;
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask defaultmask;
    public float targetTime = 5.0f;
    int colorCounter = 0;
    Collider platcollider;

    void Start()
    {
        platcollider = platform.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;



        if (targetTime <= 0.0f)
        {
            TimedColorChange(colorCounter);
        }


    }

    private void TimedColorChange(int color)
    {
        platform.GetComponent<Renderer>().material = materials[color];

        colorCounter++;


        if (colorCounter == materials.Length)
            colorCounter = 0;
        

        targetTime = 5.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer + " y " + collision.gameObject.GetComponent<Renderer>().material);
        Debug.Log(" material plataforma" + platform.GetComponent<Renderer>().material);

        if ( collision.gameObject.GetComponent<Renderer>().material.color != platform.GetComponent<Renderer>().material.color)
        {
            Debug.Log("entro");
            platcollider.excludeLayers = playerMask;
        }
        


    }
    private void OnCollisionExit(Collision collision)
    {
        platcollider.excludeLayers = default;
        platcollider.includeLayers = playerMask;
    }


}



