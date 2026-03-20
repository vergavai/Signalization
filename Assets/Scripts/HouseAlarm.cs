using System;
using System.Collections;
using UnityEngine;

public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeDuration = 5;
    [SerializeField] private AudioSource _audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Burglar burglar))
        {
            Debug.Log("Burglar entered");
            StartCoroutine(ChangeSound(true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Burglar burglar))
        {
            StartCoroutine(ChangeSound(false));
        }
    }

    private IEnumerator ChangeSound(bool isIncrease)
    {
        if (isIncrease)
        {
            _audioSource.Play();
        }

        float current = isIncrease ? 0 : 1;
        float target = isIncrease ? 1 : 0;
        
        float currentTime = 0;
        while (currentTime < _volumeChangeDuration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(current, target, currentTime / _volumeChangeDuration);
            yield return null;
        }

        if (!isIncrease)
        {
            _audioSource.Stop();
        }
    }
    
    
    
}
