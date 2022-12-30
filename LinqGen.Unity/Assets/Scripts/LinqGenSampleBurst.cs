using System;
using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class LinqGenSampleBurst : MonoBehaviour
{
    public int[] myArray;

    void Start()
    {
        var input = new NativeArray<int>(myArray, Allocator.Persistent);
        var output = new NativeArray<int>(myArray.Length, Allocator.Persistent);

        var job = new LinqGenSampleJob()
        {
            Input = input,
            Output = output
        };

        job.Schedule().Complete();

        foreach (var item in output)
        {
            Debug.Log(item);
        }

        input.Dispose();
        output.Dispose();
    }
}

[BurstCompile(CompileSynchronously = true)]
public struct LinqGenSampleJob : IJob
{
    [ReadOnly]
    public NativeArray<int> Input;

    [WriteOnly]
    public NativeArray<int> Output;

    public void Execute()
    {
        int index = 0;

        foreach (var item in Input.Gen()
                     .Select(new Selector())
                     .Order(new Comparer()))
        {
            Output[index++] = item;
        }
    }
}

public struct Selector : IStructFunction<int, int>
{
    public int Invoke(int arg) => arg * 10;
}

public struct Comparer : IComparer<int>
{
    public int Compare(int x, int y) => x - y;
}
