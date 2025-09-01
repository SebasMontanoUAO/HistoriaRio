using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerMinijuegoPlataforma : MonoBehaviour
{
    [SerializeField]
    private List<Transform> puntosDeAparición;
    [SerializeField]
    private GameObject prefabBasura;
    [SerializeField]
    private TextMeshProUGUI textBasuraCount;

    private float spawnCooldown = 1f;

    private bool spawnActivo = true;

    private int basuraCount;
    private int basuraTotal;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBasura());
        basuraCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnBasura()
    {
        while (spawnActivo)
        {
            int puntoRandom = Random.Range(0, puntosDeAparición.Count);
            Instantiate(prefabBasura, puntosDeAparición[puntoRandom]);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void SumarPunto()
    {
        basuraCount++;
        textBasuraCount.text = basuraCount.ToString();
    }
}
