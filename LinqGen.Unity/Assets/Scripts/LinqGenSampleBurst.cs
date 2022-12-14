using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen;
using Unity.Collections;
using UnityEngine;

public class LinqGenSampleBurst : MonoBehaviour
{
    public int[] myArray;

    void Start()
    {
        var nativeArray = new NativeArray<int>(myArray, Allocator.Temp);

        foreach (var i in nativeArray.Specialize().OrderBy().Select(x => x * 2))
        {
            Debug.Log(i);
        }
    }
}
