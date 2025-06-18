using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapController : MonoBehaviour
{
    
    public Transform CameraTransform1;
    //SWAP
    [SerializeField] float swapRange = 2000;

    [SerializeField] LayerMask controllableLayer;
    List<float> objectsinRange = new List<float>();
    float currentobjectDistance;
    float closestTarget;
    Vector3 closesttargetPosition;

    [SerializeField] GameObject controller;
    GameObject targetofSwap;
    PlayerMovement playerMovement; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, swapRange, controllableLayer);


            foreach (Collider c in collider)
            {
                if (c.gameObject.layer == 7)
                {
                    currentobjectDistance = Vector3.Distance(c.transform.position, transform.position);

                    objectsinRange.Add(currentobjectDistance);
                }


            }
            closestTarget = objectsinRange.Min();
            
            foreach (Collider c in collider)
            {
                if (closestTarget == Vector3.Distance(c.transform.position, transform.position))
                {
                    
                    targetofSwap = c.gameObject;
                    targetofSwap.AddComponent<PlayerMovement>();
                    Rigidbody newrb = targetofSwap.gameObject.GetComponent<Rigidbody>();
                    playerMovement.Swap(newrb);
                    //newrb.collisionDetectionMode = playerMovement.Rb.collisionDetectionMode;
                    //newrb.constraints = playerMovement.Rb.constraints;

                    
                    
                   
                    //SaveControllerData(targetofSwap);



                    //targetofSwap. = CameraTransform1;
                    //targetofSwap.gameObject.layer = 8;
                    //controller = targetofSwap;

                }

            }
            objectsinRange.Clear();
        }

    }






}
