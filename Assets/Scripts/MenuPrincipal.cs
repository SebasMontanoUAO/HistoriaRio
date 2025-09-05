using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject panelControles;

    void Start()
    {
        panelMenu.SetActive(true);
        panelControles.SetActive(false);

        // Reproduce la música del menú
        AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
    }

    public void Jugar()
    {
        // Cambia a la escena de juego
        SceneManager.LoadScene("MovimientoJugador");

        // Cambia la música a la del juego
        AudioManager.instance.PlayMusic(AudioManager.instance.gameMusic);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void AbrirControles()
    {
        panelMenu.SetActive(false);
        panelControles.SetActive(true);
    }

    public void CerrarControles()
    {
        panelControles.SetActive(false);
        panelMenu.SetActive(true);
    }
}
