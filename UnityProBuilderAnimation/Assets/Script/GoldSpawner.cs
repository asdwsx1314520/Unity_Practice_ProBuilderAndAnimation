using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    Pools pools;

    public Transform born;

    private void Start()
    {
        pools = Pools.Instance;
    }

    /// <summary>
    /// 拿取物件
    /// </summary>
    public void getPoolsGold()
    {
        pools.SpawnFromPool("Gold", born.position, Quaternion.identity);
    }
}
