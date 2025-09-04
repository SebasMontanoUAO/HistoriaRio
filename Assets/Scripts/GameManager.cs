using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float resultadoBasura = 0f; // 0 = mal, 1 = perfecto

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
