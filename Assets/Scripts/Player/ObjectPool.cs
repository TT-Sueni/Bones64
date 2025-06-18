using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int sizeofPool;
    }
    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    [SerializeField] GameObject magazine;
    [SerializeField] GameObject enemySpawner;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.sizeofPool; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                if (pool.tag == "Magnet")
                {
                    obj.transform.parent = magazine.transform;
                    
                }
              
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " does not exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();


        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }
    public void ReturnToQueue(string tag, GameObject prefab)
    {
        poolDictionary[tag].Enqueue(prefab);
        prefab.transform.position = Vector3.zero;

    }
}
