using UnityEngine;

public class TestInteractions : MonoBehaviour
{
    private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = Color.gray5;
    }

    /// <summary>
    /// Cambia el color dependiendo de las interacciones del usuario
    /// </summary>
    public void OnStopTouch()
    {
        material.color = Color.gray5;
    }

    public void OnSwipeDown()
    {
        material.color = Color.blue;
    }

    public void OnSwipeLeft() 
    {
        material.color = Color.red;
    }

    public void OnSwipeRight() 
    {
        material.color = Color.green;
    }
}
