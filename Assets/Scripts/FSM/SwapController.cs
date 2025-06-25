using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwapController : MonoBehaviour
{
    
    //public Transform CameraTransform1;
    //SWAP
    [SerializeField] float swapRange = 2000;
    [SerializeField] LayerMask controllableLayer;
    List<float> objectsinRange = new List<float>();
    float currentobjectDistance;
    float closestTarget;
    Vector3 closesttargetPosition;

    [SerializeField] GameObject controller;
    GameObject targetofSwap;
    public PlayerMovement playerMovement;
    [SerializeField] private CinemachineFreeLook CinemachineFreeLook;
    public bool swapenabled = true; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Collider[] collider = Physics.OverlapSphere(controller.transform.position, swapRange, controllableLayer);

            
            foreach (Collider c in collider)
            {
                
                    currentobjectDistance = Vector3.Distance(c.transform.position, controller.transform.position);

                    objectsinRange.Add(currentobjectDistance);
                


            }
            closestTarget = objectsinRange.Min();
            
            foreach (Collider c in collider)
            {
                if (closestTarget == Vector3.Distance(c.transform.position, controller.transform.position))
                {
                    
                    targetofSwap = c.gameObject;
                    
                    targetofSwap.AddComponent<PlayerMovement>();
                    Rigidbody newrb = targetofSwap.gameObject.GetComponent<Rigidbody>();
                    
                    CinemachineFreeLook.LookAt = targetofSwap.transform;
                    CinemachineFreeLook.Follow = targetofSwap.transform;
                    controller.layer = 7;
                    
                    controller = targetofSwap;
                    //newrb.collisionDetectionMode = playerMovement.Rb.collisionDetectionMode;
                    //newrb.constraints = playerMovement.Rb.constraints;

                    swapenabled = true;

                    if (swapenabled)
                    {
                        playerMovement.Swap(newrb);
                        swapenabled = false;
                       
  
                    }
                }
            }
            objectsinRange.Clear();
        }

    }

   




}
