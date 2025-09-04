using Models;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LecturaPMultiples : MonoBehaviour
{
    List<PreguntasMultiples> listaPreguntasM;
    public TextMeshProUGUI textoPregunta;
    public TextMeshProUGUI textoOp1;
    public TextMeshProUGUI textoOp2;
    public TextMeshProUGUI textoOp3;
    public TextMeshProUGUI textoOp4;
    public GameObject panelTerminarM;

    public int indicadorPreguntaM;
    string respuestaCorrecta;

    void Start()
    {
        listaPreguntasM = new List<PreguntasMultiples>();
        lecturaPreguntasM();
        FindObjectOfType<ControllerAllS>().AvisarLecturaLista();
    }

    public void lecturaPreguntasM()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, "Scenes/RecursosTXT/SELECCION_MULTIPLE_2024.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                string lineaLeida;
                while ((lineaLeida = sr.ReadLine()) != null)
                {
                    string[] lineaPartida = lineaLeida.Split('-');

                    if (lineaPartida.Length >= 6)
                    {
                        string pregunta = lineaPartida[0];
                        string opcion1 = lineaPartida[1];
                        string opcion2 = lineaPartida[2];
                        string opcion3 = lineaPartida[3];
                        string opcion4 = lineaPartida[4];
                        string respuesta = lineaPartida[5];

                        PreguntasMultiples preguntaM = new PreguntasMultiples(pregunta, opcion1, opcion2, opcion3, opcion4, respuesta);
                        listaPreguntasM.Add(preguntaM);
                    }
                    else
                    {
                        Debug.LogWarning("Línea malformada: " + lineaLeida);
                    }
                }

                Debug.Log("Preguntas múltiples cargadas: " + listaPreguntasM.Count);
                indicadorPreguntaM = listaPreguntasM.Count;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Exception: " + e.Message);
        }
    }

    public void asignarPregunta()
    {
        if (listaPreguntasM.Count > 0)
        {
            System.Random random = new System.Random();
            int numeroPregunta = random.Next(0, listaPreguntasM.Count);
            PreguntasMultiples pregunta = listaPreguntasM[numeroPregunta];

            textoPregunta.text = pregunta.Pregunta;
            textoOp1.text = pregunta.Opcion1;
            textoOp2.text = pregunta.Opcion2;
            textoOp3.text = pregunta.Opcion3;
            textoOp4.text = pregunta.Opcion4;
            respuestaCorrecta = pregunta.Respuesta;

            listaPreguntasM.RemoveAt(numeroPregunta);
            indicadorPreguntaM -= 1;
        }
        else
        {
            panelTerminarM.SetActive(true);
        }
    }
    public void InvalidarRespuesta()
    {
        ResultadoJuego.RegistrarRespuesta(false); // ← Siempre false
        Debug.Log("Respuesta invalidada - Incorrecta");
        FindObjectOfType<ControllerAllS>().SelectQuestionFaciles();
        FindObjectOfType<ControllerAllS>().ActualizarSaludRio();
    }
    public void validarRespuesta(TextMeshProUGUI opcionLabel)
    {
        string respuestaUsuario = opcionLabel.text;

        Debug.Log("Respuesta del usuario: '" + respuestaUsuario + "'");
        Debug.Log("Respuesta correcta: '" + respuestaCorrecta + "'");

        bool esCorrecta = respuestaUsuario == respuestaCorrecta;
        ResultadoJuego.RegistrarRespuesta(esCorrecta);
        Debug.Log("Respuesta del jugador: " + (esCorrecta ? "Correcta" : "Incorrecta"));
        FindObjectOfType<ControllerAllS>().SelectQuestionFaciles();
        FindObjectOfType<ControllerAllS>().ActualizarSaludRio();
    }
}
