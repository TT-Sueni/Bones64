

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
        Renderer rendererCollision = collision.gameObject.GetComponent<Renderer>();
        Renderer rendererPlat = platform.gameObject.GetComponent<Renderer>();
        if (rendererCollision == null || rendererPlat == null)
        {
            Debug.Log("Nulo");
            
            return; 
        }
           

       

        if (rendererCollision.material.color != rendererPlat.material.color)
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



