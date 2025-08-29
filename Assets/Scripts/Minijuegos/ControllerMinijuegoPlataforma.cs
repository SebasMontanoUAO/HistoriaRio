using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMinijuegoPlataforma : MonoBehaviour
{
    [SerializeField]
    private List<Transform> puntosDeAparición;
    [SerializeField]
    private GameObject prefabBasura;

    private float spawnCooldown = 3f;

    private bool spawnActivo = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBasura());
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
}
