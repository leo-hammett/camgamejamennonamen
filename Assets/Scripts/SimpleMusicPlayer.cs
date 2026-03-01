using UnityEngine;
using System.Collections;

public class SimpleMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip[] musicClips;
    private int currentClipIndex = 0;
    
    void Awake()
    {
        // Create audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f; // Set to 50% volume
        
        // Load music from Resources or Audio folder
        musicClips = new AudioClip[2];
        musicClips[0] = Resources.Load<AudioClip>("ost1");
        musicClips[1] = Resources.Load<AudioClip>("ost 2");
        
        // If not in Resources, try loading from the Audio folder path
        if (musicClips[0] == null)
        {
            Debug.LogWarning("Could not find ost1 in Resources folder");
        }
        if (musicClips[1] == null)
        {
            Debug.LogWarning("Could not find ost 2 in Resources folder");
        }
    }
    
    void Start()
    {
        // Start playing music
        if (musicClips[0] != null || musicClips[1] != null)
        {
            StartCoroutine(PlayMusicLoop());
        }
        else
        {
            Debug.LogError("No music clips found! Music files need to be in Resources folder or assigned manually.");
        }
    }
    
    IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            // Find next valid clip
            AudioClip clipToPlay = null;
            int attempts = 0;
            while (clipToPlay == null && attempts < musicClips.Length)
            {
                clipToPlay = musicClips[currentClipIndex];
                if (clipToPlay == null)
                {
                    currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
                    attempts++;
                }
                else
                {
                    break;
                }
            }
            
            if (clipToPlay != null)
            {
                Debug.Log($"Playing music: {clipToPlay.name}");
                audioSource.clip = clipToPlay;
                audioSource.Play();
                
                // Wait for clip to finish
                yield return new WaitForSeconds(clipToPlay.length);
                
                // 10 second pause between tracks
                yield return new WaitForSeconds(10f);
            }
            else
            {
                Debug.LogError("No valid music clips to play!");
                yield break;
            }
            
            // Next clip
            currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        }
    }
}