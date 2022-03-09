using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shake : MonoBehaviour
{
    public static Shake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    private void Awake(){
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float frequency, float time){
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update(){
        if(shakeTimer > 0f){
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f){
                //Timer over
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1-(shakeTimer/shakeTimerTotal)));
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
            }
        }
        
    }
}
