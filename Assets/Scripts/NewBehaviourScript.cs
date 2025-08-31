using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Configuración del tiempo")]
    public float tiempoLimite = 30f; // en segundos
    private float tiempoRestante;
    private bool contando = false;

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
            if (GameObject.FindObjectOfType<LecturaPFV>()?.gameObject.activeSelf == true)
            {
                GameObject.FindObjectOfType<LecturaPFV>().validarRespuesta(false);
            }
            else if (GameObject.FindObjectOfType<LecturaPMultiples>()?.gameObject.activeSelf == true)
            {
                GameObject.FindObjectOfType<LecturaPMultiples>().validarRespuesta("");
            }
            else if (GameObject.FindObjectOfType<LecturaPAbiertas>()?.gameObject.activeSelf == true)
            {
                GameObject.FindObjectOfType<LecturaPAbiertas>().InvalidarRespuesta();
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
