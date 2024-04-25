using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{
    public float velocidadeCam;

    // Update is called once per frame
    void Update()
    {
        float movimentoX = velocidadeCam * Time.deltaTime;

        transform.Translate(new Vector3(movimentoX, 0, 0));
    }
}
