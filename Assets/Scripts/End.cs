using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject EndPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnDestroy()
    {
        EndPanel.SetActive(true);
    }
}
