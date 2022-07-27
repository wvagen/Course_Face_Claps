using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public Transform shine;

    // Update is called once per frame
    void Update()
    {
        shine.transform.Rotate(Vector3.forward * Time.deltaTime * 5);
    }
}
