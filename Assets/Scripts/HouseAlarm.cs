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
            StartCoroutine(PlayAndIncrease());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Burglar burglar))
        {
            StartCoroutine(StopAndDecrease());
        }
    }

    private IEnumerator PlayAndIncrease()
    {
        _audioSource.Play();
        
        float currentTime = 0;
        while (currentTime < _volumeChangeDuration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(0, 1, currentTime / _volumeChangeDuration);
            yield return null;
        }
    }
    
    private IEnumerator StopAndDecrease()
    {
        float currentTime = 0;
        while (currentTime < _volumeChangeDuration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.MoveTowards(1, 0, currentTime / _volumeChangeDuration);
            yield return null;
        }
        
        _audioSource.Stop();
    }
    
    
}
