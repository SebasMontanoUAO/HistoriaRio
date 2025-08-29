using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAllS : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<GameObject> listaControllers; // Controladores de preguntas
    GameObject controlSelected;
    public GameObject panelAbiertas;
    public GameObject panelMultiples;
    public GameObject panelFV;
    public GameObject panelFinal;

    void Start()
    {
    }

    void Update()
    {
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
                    controlMulti.panelTerminarM.SetActive(true);
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
                    controlFV.panelTerminarFV.SetActive(true);
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
                    controlAbiertas.panelTerminarA.SetActive(true);
                    // Si no hay más preguntas abiertas, eliminar el controlador
                    listaControllers.Remove(controlSelected);
                }
            }
        }
        else
        {
            panelFinal.SetActive(true);
            Debug.Log("Todas las preguntas de todos los tipos se han terminado.");
        }
    }

}
