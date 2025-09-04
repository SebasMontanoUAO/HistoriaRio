using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMinijuegoPlataforma : MonoBehaviour
{
    [SerializeField] private List<Transform> puntosDeAparición;
    [SerializeField] private GameObject prefabBasura;
    [SerializeField] private TextMeshProUGUI textBasuraCount;

    private float spawnCooldown = 1f;
    private bool spawnActivo = true;

    private int basuraCount; // recogida
    private int basuraTotal; // generada
    private int maxBasuras = 30;

    public static ControllerMinijuegoPlataforma InstanciaControladorMinijuego;

    private void Awake()
    {
        if (InstanciaControladorMinijuego != null && InstanciaControladorMinijuego != this)
        {
            Destroy(gameObject);
            return;
        }
        InstanciaControladorMinijuego = this;
    }

    void Start()
    {
        basuraCount = 0;
        basuraTotal = 0;
        StartCoroutine(SpawnBasura());
    }

    IEnumerator SpawnBasura()
    {
        while (spawnActivo && basuraTotal < maxBasuras)
        {
            int puntoRandom = Random.Range(0, puntosDeAparición.Count);
            Instantiate(prefabBasura, puntosDeAparición[puntoRandom]);
            basuraTotal++;
            yield return new WaitForSeconds(spawnCooldown);
        }

        // Cuando termina el spawn, esperar un poquito y finalizar el minijuego
        yield return new WaitForSeconds(2f);
        FinalizarMinijuego();
    }

    public void SumarPunto()
    {
        basuraCount++;
        textBasuraCount.text = basuraCount.ToString();
    }

    private void FinalizarMinijuego()
    {
        spawnActivo = false;

        // Por ahora solo mostrar resultado en consola
        float eficiencia = (float)basuraCount / basuraTotal;
        Debug.Log($"Minijuego terminado! Basuras recogidas: {basuraCount}/{basuraTotal} - Eficiencia: {eficiencia:P}");

        // Volver a la escena principal
        SceneManager.LoadScene("MovimientoJugador");
    }
}
