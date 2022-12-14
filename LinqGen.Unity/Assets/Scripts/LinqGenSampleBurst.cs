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

        var enumerable = nativeArray.Specialize();
        var enumerable2 = enumerable.Where(x => x % 2 == 0);

        foreach (var i in enumerable)
        {
            Debug.Log(i);
        }

        foreach (var i in nativeArray.Specialize().OrderBy().Select(x => x * 2))
        {
            Debug.Log(i);
        }
    }
}
