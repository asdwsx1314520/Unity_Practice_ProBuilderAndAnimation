using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    //物件池主體
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public AudioSource aud;
    public AudioClip shoot;

    #region Singleton
    public static Pools Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;

    //物件池的字典
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //遍歷物件池
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //創造物件池物件
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                //將物件加入物件池
                objectPool.Enqueue(obj);
            }

            //將物件池加入字典(標籤, 物件池)
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// 產生物件
    /// </summary>
    /// <param name="tag">給予的標籤</param>
    /// <param name="position">生成的位置</param>
    /// <param name="rotation">生成的旋轉c</param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        aud.PlayOneShot(shoot);
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't excist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        //抓取接口
        IPools pools = objectToSpawn.GetComponent<IPools>();

        if(pools != null)
        {
            pools.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
