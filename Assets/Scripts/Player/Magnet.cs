using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Magnet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float TPRange = 20;
    [SerializeField] float pullRange = 200;
    [SerializeField] LayerMask magnetLayer;
    [SerializeField] GameObject magnet;
    [SerializeField] LayerMask playerMask;
    List<float> magnetsinRange = new List<float>();
    float currentMagnetDistance;
    Vector3 tpTarget;
    Vector3 closesttargetPosition;
    float closesttarget;

    //shoot
    [SerializeField] Rigidbody magnetRB;
    Vector3 rotation = new Vector3(1, 1, 1);
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        magnetRB = GetComponent<Rigidbody>();
        isGrounded = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        GoToMagnet();
        
        AttractMagnets();

    }

    private void GoToMagnet()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Collider[] collider = Physics.OverlapSphere(player.transform.position, TPRange, magnetLayer);
            if (collider.Length > 0)
            {

                foreach (Collider c in collider)
                {
                    currentMagnetDistance = Vector3.Distance(c.transform.position, player.transform.position);
                    magnetsinRange.Add(currentMagnetDistance);
                }
                closesttarget = magnetsinRange.Min();
                foreach (Collider c in collider)
                {
                    if (closesttarget == Vector3.Distance(c.transform.position, player.transform.position))
                    {
                        closesttargetPosition = c.transform.position;
                        player.transform.position = Vector3.MoveTowards(player.transform.position, closesttargetPosition, 10f * Time.deltaTime);
                        //closesttargetPosition;
                    }
                }
                magnetsinRange.Clear();
            }
        }
    }
  
    private void AttractMagnets()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Collider[] collider = Physics.OverlapSphere(player.transform.position, pullRange, magnetLayer);

            if (collider.Length > 0)
            {
                foreach (Collider c in collider)
                {
                    c.transform.position = Vector3.MoveTowards(c.transform.position, player.transform.position, 50f * Time.deltaTime);
                }

                magnetsinRange.Clear();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (CheckLayerInMask(groundMask, collision.gameObject.layer))
        {
            Debug.Log(groundMask.value);
            magnetRB.velocity = Vector3.zero;
            magnetRB.angularVelocity = Vector3.zero;
            isGrounded = true;      
            Suspension();
        }

        if (isGrounded)
        {
            if (CheckLayerInMask(playerMask, collision.gameObject.layer))
            {
                Debug.Log("toco jugador");
                ObjectPool.Instance.ReturnToQueue("Magnet", magnet);
                magnet.SetActive(false);
                isGrounded = false;
                magnetRB.useGravity = true;
            }
            magnetRB.velocity = Vector3.zero;
            magnetRB.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
         
            
        }
    }
    private void Suspension()
    {
        magnetRB.useGravity = false;
        Vector3 position = gameObject.transform.position;
        Vector3 suspension = new Vector3(0, 1, 0);

        var magia = position + suspension;
        gameObject.transform.position = magia;
        magnetRB.AddTorque(transform.rotation * rotation, ForceMode.Impulse);

    }
    public static bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
