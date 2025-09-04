using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAllS : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<GameObject> listaControllers; // Controladores de preguntas
    GameObject controlSelected;
    public GameObject panelAbiertas;
    public GameObject panelMultiples;
    public GameObject panelFinal;
    public GameObject panelFV;
    public TextMeshProUGUI txtResultadoFinal;
    public Image barraRio;
    private int lecturasListas = 0;   // contador de scripts cargados
    private int totalLecturas = 3;
    public float saludRio = 0f; // 1 limpio, 0 sucio

    void Start()
    {
        ActualizarBarraRio();
    }

    void Update()
    {
    }
    public void AvisarLecturaLista()
    {
        lecturasListas++;

        // Si los 3 ya cargaron, arrancamos el juego
        if (lecturasListas == totalLecturas)
        {
            Debug.Log("Todas las lecturas listas, iniciando juego...");
            SelectQuestionFaciles();
        }
    }
    public void SelectQuestionFaciles()
    {
        // Verificar si todavía hay controladores disponibles
        if (listaControllers.Count > 0)
        {
            System.Random random = new System.Random();
            int numero = random.Next(0, listaControllers.Count);
            controlSelected = listaControllers[numero];

            if (controlSelected.GetComponent<LecturaPMultiples>() != null)
            {
                LecturaPMultiples controlMulti = controlSelected.GetComponent<LecturaPMultiples>();
                if (controlMulti.indicadorPreguntaM > 0)
                {
                    panelFV.SetActive(false);
                    panelAbiertas.SetActive(false);
                    controlMulti.asignarPregunta();
                    panelMultiples.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas múltiples, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                }
            }
            else if (controlSelected.GetComponent<LecturaPFV>() != null)
            {
                LecturaPFV controlFV = controlSelected.GetComponent<LecturaPFV>();
                if (controlFV.indicadorPreguntaFV > 0)
                {
                    panelAbiertas.SetActive(false);
                    panelMultiples.SetActive(false);
                    controlFV.asignarPregunta();
                    panelFV.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas FV, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                }
            }
            else if (controlSelected.GetComponent<LecturaPAbiertas>() != null)
            {
                LecturaPAbiertas controlAbiertas = controlSelected.GetComponent<LecturaPAbiertas>();
                if (controlAbiertas.indicadorPreguntaA > 0)
                {
                    panelFV.SetActive(false);
                    panelMultiples.SetActive(false);
                    controlAbiertas.asignarPregunta();
                    panelAbiertas.SetActive(true);
                }
                else
                {
                    // Si no hay más preguntas abiertas, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                }
            }
        }
        else
        {
            panelFinal.SetActive(true);
            Debug.Log("Todas las preguntas de todos los tipos se han terminado.");
            // Mostrar el resultado en el TextMeshProUGUI
            if (txtResultadoFinal != null)
            {
                txtResultadoFinal.text = ResultadoJuego.ObtenerResumen();
            }
            else
            {
                Debug.LogWarning("No se ha asignado txtResultadoFinal en el Inspector.");
            }
            ActualizarSaludRio();
        }
        FindObjectOfType<Timer>().ReiniciarTiempo();
    }
    public void ActualizarSaludRio()
    {
        // Obtener porcentaje de aciertos del juego (0 a 1)
        float porcentajeAciertos = ResultadoJuego.ObtenerPorcentajeAciertos(); // Crear método en ResultadoJuego
        saludRio = porcentajeAciertos;
        ActualizarBarraRio();
    }

    private void ActualizarBarraRio()
    {
        if (barraRio != null)
        {
            barraRio.fillAmount = saludRio;

            // Cambio de color por rangos
            if (saludRio > 0.6f)
            {
                barraRio.color = Color.blue; // limpio
            }
            else if (saludRio > 0.3f)
            {
                barraRio.color = Color.yellow; // medio
            }
            else
            {
                barraRio.color = Color.red; // sucio
            }
        }
    }
    public void VolverAlMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu"); // Cambiar por tu escena principal
    }

    public void CerrarAplicacion()
    {
        Debug.Log("Cerrando aplicación...");
        Application.Quit();
    }

}
