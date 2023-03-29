using UnityEngine;

public class ShaderInteractor : MonoBehaviour
{
    public static ShaderInteractor Instance;
    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_PositionMoving", transform.position);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
