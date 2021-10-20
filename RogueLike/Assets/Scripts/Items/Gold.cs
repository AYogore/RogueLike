using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int _amount;
    private void Start()
    {
        _amount = Random.Range(2, 10);
    }
}
