using System.Collections;
using System.Collections.Generic;
using Cathei.LinqGen;
using UnityEngine;

public class LinqGenSample : MonoBehaviour
{
    public List<int> myList;

    void Start()
    {
        foreach (var i in myList.Gen().Order().Select(x => x * 2))
        {
            Debug.Log(i);
        }

        Debug.Log(myList.Gen().Take(0).Any());
    }
}
