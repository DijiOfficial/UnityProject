using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class AudioSourceEvent : UnityEvent<AudioSource> { }

public class UpgradeOrbScript : HealthOrbBehaviour
{
    [SerializeField] private AudioSourceEvent _onCollideAudioEvent;

    protected override void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly
        if (other.tag != FRIENDLY_TAG)
            return;

        if (other.name != "Player")
            return;

        _onCollideAudioEvent?.Invoke(_audioSource);
        _tempPlayerInfo._goldCoins += 1;

        /// Disable the visual components and play the sound
        if (_renderer != null) _renderer.enabled = false;
        if (_collider != null) _collider.enabled = false;
        if (_trailRenderer != null) _trailRenderer.enabled = false;

        if (_audioSource != null)
        {
            StartCoroutine(DestroyAfterSound(_audioSource.clip.length));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterSound(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(gameObject);
    }
}

