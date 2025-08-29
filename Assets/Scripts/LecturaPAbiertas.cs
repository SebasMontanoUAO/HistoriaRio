using Models;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LecturaPAbiertas : MonoBehaviour
{
    // Lista de preguntas abiertas
    List<PreguntaAbierta> listaPreguntasA;
    public TextMeshProUGUI textoPregunta;
    public TMP_InputField inputRespuesta;
    public GameObject panelTerminarA;
    public int indicadorPreguntaA;
    string respuestaA;

    void Start()
    {
        listaPreguntasA = new List<PreguntaAbierta>();
        lecturaPreguntasA();
        FindObjectOfType<ControllerAllS>().AvisarLecturaLista();
    }

    public void lecturaPreguntasA()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "Scenes/RecursosTXT/FALSO_VERDADERO_2024.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                string lineaLeida;
                while ((lineaLeida = sr.ReadLine()) != null)
                {
                    string[] lineaPartida = lineaLeida.Split('-');

                    if (lineaPartida.Length >= 2) // ahora solo pregunta y respuesta
                    {
                        string preguntaAbierta = lineaPartida[0];
                        string respuesta = lineaPartida[1];

                        PreguntaAbierta preguntaA = new PreguntaAbierta(preguntaAbierta, respuesta);
                        listaPreguntasA.Add(preguntaA);
                    }
                    else
                    {
                        Debug.LogWarning("Línea malformada: " + lineaLeida);
                    }
                }

                Debug.Log("Tamaño de la lista de preguntas abiertas: " + listaPreguntasA.Count);
                indicadorPreguntaA = listaPreguntasA.Count;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
    }

    public void asignarPregunta()
    {
        if (listaPreguntasA.Count > 0)
        {
            System.Random random = new System.Random();
            int numeroPregunta = random.Next(0, listaPreguntasA.Count);
            PreguntaAbierta pregunta = listaPreguntasA[numeroPregunta];

            textoPregunta.text = pregunta.Pregunta;
            respuestaA = pregunta.Respuesta;

            listaPreguntasA.RemoveAt(numeroPregunta);
            inputRespuesta.text = "";
            indicadorPreguntaA -= 1;
        }
        else
        {
            panelTerminarA.SetActive(true);
        }
    }

    public void validarRespuesta(string respuestaUsuario)
    {
        bool esCorrecta = (respuestaUsuario == respuestaA);

        ResultadoJuego.RegistrarRespuesta(esCorrecta);
        Debug.Log("Respuesta del jugador: " + (esCorrecta ? "Correcta" : "Incorrecta"));
        FindObjectOfType<ControllerAllS>().SelectQuestionFaciles();
    }
}
