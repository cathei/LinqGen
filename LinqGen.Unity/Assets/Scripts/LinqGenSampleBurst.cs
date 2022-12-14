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
        var output = new NativeArray<int>(1, Allocator.Persistent);

        var job = new LinqGenSampleJob()
        {
            Input = input,
            Output = output
        };

        job.Schedule().Complete();

        Debug.Log("The result of the sum is: " + output[0]);

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
        Output[0] = Input.Specialize().Select(new Selector()).Sum();
    }
}

public struct Selector : IStructFunction<int, int>
{
    public int Invoke(int arg)
    {
        return arg * 10;
    }
}
