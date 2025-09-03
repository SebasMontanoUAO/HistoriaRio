using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Configuración del tiempo")]
    public float tiempoLimite = 60f; // en segundos
    private float tiempoRestante;
    private bool contando = false;

    [Header("Referencias a los paneles")]
    public GameObject ControllerFV;
    public GameObject ControllerAbiertas;
    public GameObject ControllerMultiples;

    [Header("Referencias UI (TMP)")]
    public TextMeshProUGUI textoMinutos;
    public TextMeshProUGUI textoSegundos;
    public TextMeshProUGUI textoMilisegundos;

    private void OnEnable()
    {
        ReiniciarTiempo();
    }

    private void Update()
    {
        if (!contando) return;

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0f)
        {
            tiempoRestante = 0f;
            contando = false;
            Debug.Log("Tiempo agotado - Se debe marcar incorrecta la pregunta");

            // Aquí marcas incorrecta la respuesta automáticamente:

            if (ControllerFV != null && ControllerFV.activeSelf)
            {
                ControllerFV.GetComponent<LecturaPFV>().InvalidarRespuesta();
            }
            else if (ControllerAbiertas != null && ControllerAbiertas.activeSelf)
            {
                ControllerAbiertas.GetComponent<LecturaPAbiertas>().InvalidarRespuesta();
            }
            else if (ControllerMultiples != null && ControllerMultiples.activeSelf)
            {
                ControllerMultiples.GetComponent<LecturaPMultiples>().InvalidarRespuesta();
            }
        }

        ActualizarUI();
    }

    public void ReiniciarTiempo()
    {
        tiempoRestante = tiempoLimite;
        contando = true;
        ActualizarUI();
        Debug.Log("Temporizador reiniciado a " + tiempoLimite + " segundos");
    }

    private void ActualizarUI()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        int milisegundos = Mathf.FloorToInt((tiempoRestante * 100f) % 100f);

        if (textoMinutos != null) textoMinutos.text = minutos.ToString("00");
        if (textoSegundos != null) textoSegundos.text = segundos.ToString("00");
        if (textoMilisegundos != null) textoMilisegundos.text = milisegundos.ToString("00");
    }
}
