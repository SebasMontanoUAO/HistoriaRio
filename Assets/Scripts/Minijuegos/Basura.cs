using Unity.VisualScripting;
using UnityEngine;

public class Basura : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1f, 0) * Time.deltaTime * velocidad);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            Debug.Log("Suma 1 punto!");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("ZonaMuerta"))
        {
            Debug.Log("Ha pasado una basura");
            Destroy(this.gameObject);
        }
    }

}
