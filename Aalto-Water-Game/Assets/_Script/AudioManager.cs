using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> TileCreationSounds = new List<AudioClip>();

    public List<AudioClip> BuildingCreationSounds = new List<AudioClip>();

    public void PlaySound(AudioClip audioClip, bool playOnLoop=false)
    {
        if (audioClip == null) return;

        GameObject tempAudio = new GameObject("Temp Audio");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.playOnAwake = false;
        tempAudioSource.loop = playOnLoop;
        tempAudioSource.clip = audioClip;
        tempAudioSource.Play();

        if (!playOnLoop) StartCoroutine(DestroyTempAudio(tempAudio));
    }

    public void PlayBuildingCreationSound(BuildingType type)
    {
        if (BuildingCreationSounds.Count < (int)type)
        {
            Debug.Log($"Audio for {type.ToString()} Creation Does not exist for type: ");
            return;
        }

        PlaySound(BuildingCreationSounds[(int)type]);
    }

    public void PlayTileCreationSound(TileType type)
    {
        if (TileCreationSounds.Count < (int)type)
        {
            Debug.Log($"Audio for {type.ToString()} Creation Does not exist for type: ");
            return;
        }

        PlaySound(TileCreationSounds[(int)type]);
    }

    private IEnumerator DestroyTempAudio(GameObject tempAudio)
    {
        float clipLength = tempAudio.GetComponent<AudioSource>().clip.length;
        yield return new WaitForSeconds(clipLength + 1f);
        Destroy(tempAudio);
    }
}