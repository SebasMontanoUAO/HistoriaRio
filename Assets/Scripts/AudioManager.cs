using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips de Música")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip dangerMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Detectar cuando cambia la escena
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Minijuego")
        {
            PlayMusic(dangerMusic); // Música de apuro
        }
        else if (scene.name == "MovimientoJugador")
        {
            PlayMusic(gameMusic); // Música normal de juego
        }
        else if (scene.name == "MenuPrincipal")
        {
            PlayMusic(menuMusic); // Música tranquila de menú
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
