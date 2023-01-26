using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cathei.LinqGen;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LinqGenSampleUniTask : MonoBehaviour
{
    public List<int> myList;

    async void Start()
    {
        await UniTask.SwitchToThreadPool();

        foreach (var i in myList.Gen().Order().Select(x => x * 2))
        {
            Debug.Log(i);
        }
    }
}
