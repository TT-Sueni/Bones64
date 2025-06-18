using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    

    [SerializeField] public Camera fpsCam;
    [SerializeField] public Transform attackPoint;
    public float shootForce;

    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //rayo en el medio de la camara
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            //checkea si el rayo le pego a algo
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);

            Vector3 direction = targetPoint - attackPoint.position;
            //Instantiate bullet/projectile
            GameObject currentBullet = ObjectPool.Instance.SpawnFromPool("Magnet", attackPoint.position, Quaternion.identity);
            currentBullet.transform.forward = direction.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);


        }


    }
}
