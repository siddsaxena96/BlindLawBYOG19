using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private float clampMinX = 0;
    [SerializeField] private float clampMaxX = 25;
    [SerializeField] private float yLevel = 0;
    [SerializeField] private float shakeAmount = 0f;

    void Update()
    {
        if(player!=null)
            transform.position = new Vector3(Mathf.Clamp(player.position.x, clampMinX, clampMaxX), yLevel, transform.position.z);
    }
    
    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    private void DoShake()
    {
        Vector3 camPos = transform.position;
        float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
        float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
        camPos.x += offsetX;
        camPos.y += offsetY;
        transform.position = camPos;
    }

    private void StopShake()
    {
        CancelInvoke("DoShake");
        transform.localPosition = Vector3.zero;
    }
}