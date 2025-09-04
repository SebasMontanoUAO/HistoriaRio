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
    }
    public void Jugar()
    {
        SceneManager.LoadScene("MovimientoJugador");
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
