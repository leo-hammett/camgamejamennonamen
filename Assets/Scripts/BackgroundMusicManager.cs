using System.Collections;
using UnityEngine;

/// <summary>
/// Manages two background music tracks that alternate with a 10-second
/// silence gap between them. Attach this script to any GameObject in the scene.
///
/// Track order: Ost1 → (10s silence) → Ost2 → (10s silence) → Ost1 → ...
/// </summary>
public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [Tooltip("First music track (Ost1.mp3)")]
    [SerializeField] private AudioClip ost1;

    [Tooltip("Second music track (Ost2.mp3)")]
    [SerializeField] private AudioClip ost2;

    [Header("Settings")]
    [Tooltip("Duration of silence between tracks, in seconds")]
    [SerializeField] private float silenceDuration = 10f;

    // Single AudioSource used for both tracks.
    // One source is sufficient since only one track plays at a time.
    private AudioSource audioSource;

    // Tracks which clip to play next (toggles between 0 and 1)
    private int currentTrackIndex = 0;

    // Reference to the running coroutine so we can stop it if needed
    private Coroutine musicCoroutine;

    // -----------------------------------------------------------------------
    // Unity Lifecycle
    // -----------------------------------------------------------------------

    private void Awake()
    {
        // Get or add an AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure the AudioSource does NOT loop automatically —
        // we handle all looping logic manually in the coroutine
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // Validate that both clips are assigned before starting
        if (!ValidateClips())
        {
            Debug.LogError("[BackgroundMusicManager] One or both audio clips are missing. " +
                           "Please assign Ost1 and Ost2 in the Inspector.");
            return;
        }

        // Begin the alternating music cycle
        musicCoroutine = StartCoroutine(MusicCycleCoroutine());
    }

    private void OnDestroy()
    {
        // Safely stop the coroutine when this object is destroyed
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
        }
    }

    // -----------------------------------------------------------------------
    // Core Coroutine
    // -----------------------------------------------------------------------

    /// <summary>
    /// Runs indefinitely, alternating between Ost1 and Ost2 with silence gaps.
    ///
    /// Sequence:
    ///   1. Play current track
    ///   2. Wait for it to finish
    ///   3. Wait 10 seconds of silence
    ///   4. Switch to the other track → repeat
    /// </summary>
    private IEnumerator MusicCycleCoroutine()
    {
        while (true)
        {
            // --- Step 1: Select and play the current track ---
            AudioClip clipToPlay = GetCurrentClip();

            Debug.Log($"[BackgroundMusicManager] Now playing: {clipToPlay.name}");

            audioSource.clip = clipToPlay;
            audioSource.Play();

            // --- Step 2: Wait until the track finishes ---
            // We poll audioSource.isPlaying each frame rather than using
            // WaitForSeconds(clip.length) so that any external pause/stop
            // is also respected correctly.
            yield return new WaitWhile(() => audioSource.isPlaying);

            Debug.Log($"[BackgroundMusicManager] Track finished. " +
                      $"Waiting {silenceDuration}s of silence...");

            // --- Step 3: Silence gap ---
            yield return new WaitForSeconds(silenceDuration);

            // --- Step 4: Switch to the other track ---
            ToggleTrack();
        }
    }

    // -----------------------------------------------------------------------
    // Helper Methods
    // -----------------------------------------------------------------------

    /// <summary>
    /// Returns the AudioClip that corresponds to the current track index.
    /// Index 0 → Ost1, Index 1 → Ost2
    /// </summary>
    private AudioClip GetCurrentClip()
    {
        return currentTrackIndex == 0 ? ost1 : ost2;
    }

    /// <summary>
    /// Toggles the track index between 0 and 1.
    /// </summary>
    private void ToggleTrack()
    {
        currentTrackIndex = (currentTrackIndex == 0) ? 1 : 0;
    }

    /// <summary>
    /// Returns true only if both audio clips have been assigned.
    /// </summary>
    private bool ValidateClips()
    {
        bool valid = true;

        if (ost1 == null)
        {
            Debug.LogError("[BackgroundMusicManager] Ost1 is not assigned!");
            valid = false;
        }

        if (ost2 == null)
        {
            Debug.LogError("[BackgroundMusicManager] Ost2 is not assigned!");
            valid = false;
        }

        return valid;
    }

    // -----------------------------------------------------------------------
    // Public API (optional convenience methods)
    // -----------------------------------------------------------------------

    /// <summary>
    /// Stops the music cycle entirely and silences the AudioSource.
    /// </summary>
    public void StopMusic()
    {
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }

        audioSource.Stop();
        Debug.Log("[BackgroundMusicManager] Music stopped.");
    }

    /// <summary>
    /// Restarts the music cycle from Ost1.
    /// </summary>
    public void RestartMusic()
    {
        StopMusic();
        currentTrackIndex = 0;

        if (ValidateClips())
        {
            musicCoroutine = StartCoroutine(MusicCycleCoroutine());
            Debug.Log("[BackgroundMusicManager] Music restarted.");
        }
    }
}