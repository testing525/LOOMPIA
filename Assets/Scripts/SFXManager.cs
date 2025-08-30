using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Sources---------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("---------Music Clips---------")]
    public AudioClip startMenuMusic;
    public AudioClip subjectMusic;
    public AudioClip questionMusic;
    public AudioClip activityMusic;

    [Header("---------SFX Clips---------")]
    public AudioClip gameStartSFX;
    public AudioClip gameOverSFX;
    public AudioClip trashcanOpenSFX;
    public AudioClip trashcanCloseSFX;
    public AudioClip correctAnswerSFX;
    public AudioClip paperSFX;
    public AudioClip wrongAnswerSFX;
    public AudioClip gameScoreSFX;
    public AudioClip buttonClickedSFX;
    public AudioClip ActivityCorrectSFX;
    public AudioClip ActivityWrongSFX;

    public enum SFXSignal
    {
        Correct,
        Wrong,
        TrashOpen,
        TrashClose,
        GameOver,
        Start,
        Score,
        Button,
        ActivityWrong,
        ActivityCorrect
    }

    public enum MusicSignal
    {
        StartMenu,
        Subject,
        Question,
        Activity
    }

    public static event Action<SFXSignal> OnSFXEvent;
    public static event Action<MusicSignal> OnMusicEvent;

    private Coroutine musicFadeCoroutine;

    private void Start()
    {
        // Default music
        PlayMusic(startMenuMusic, true);

        OnSFXEvent += HandleSFXEvent;
        OnMusicEvent += HandleMusicEvent;
    }

    private void OnDestroy()
    {
        OnSFXEvent -= HandleSFXEvent;
        OnMusicEvent -= HandleMusicEvent;
    }

    public static void FireSFX(SFXSignal signal) => OnSFXEvent?.Invoke(signal);
    public static void FireMusic(MusicSignal signal) => OnMusicEvent?.Invoke(signal);

    private void HandleSFXEvent(SFXSignal signal)
    {
        switch (signal)
        {
            case SFXSignal.Correct:
                PlaySFX(correctAnswerSFX);
                PlaySFX(paperSFX);
                break;
            case SFXSignal.Wrong:
                PlaySFX(wrongAnswerSFX);
                break;
            case SFXSignal.TrashOpen:
                PlaySFX(trashcanOpenSFX);
                break;
            case SFXSignal.TrashClose:
                PlaySFX(trashcanCloseSFX);
                break;
            case SFXSignal.GameOver:
                PlaySFX(gameOverSFX);
                break;
            case SFXSignal.Start:
                PlaySFX(gameStartSFX);
                break;
            case SFXSignal.Score:
                PlaySFX(gameScoreSFX);
                break;
            case SFXSignal.Button:
                PlaySFX(buttonClickedSFX);
                break;
            case SFXSignal.ActivityWrong:
                PlaySFX(ActivityWrongSFX);
                break;
            case SFXSignal.ActivityCorrect:
                PlaySFX(ActivityCorrectSFX);
                break;
        }
    }

    private void HandleMusicEvent(MusicSignal signal)
    {
        switch (signal)
        {
            case MusicSignal.StartMenu:
                PlayMusic(startMenuMusic, true);
                break;
            case MusicSignal.Subject:
                PlayMusic(subjectMusic, true);
                break;
            case MusicSignal.Question:
                PlayMusic(questionMusic, true);
                break;
            case MusicSignal.Activity:
                PlayMusic(activityMusic, true);
                break;
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip, bool smooth = false)
    {
        if (clip == null) return;

        if (musicFadeCoroutine != null) StopCoroutine(musicFadeCoroutine);

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (smooth)
            musicFadeCoroutine = StartCoroutine(SmoothTransition(clip, 1f, savedVolume)); // pass saved vol
        else
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.volume = savedVolume;
            musicSource.Play();
        }
    }

    private IEnumerator SmoothTransition(AudioClip newClip, float fadeDuration, float targetVolume)
    {
        float startVol = musicSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = 0f;

        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = targetVolume;
    }

}
