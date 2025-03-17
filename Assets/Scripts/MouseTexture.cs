using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseTexture : MonoBehaviour
{
    public Texture2D CursorTexture;

    private CursorMode cursorMode = CursorMode.Auto;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.SetCursor(CursorTexture, new Vector2(CursorTexture.width / 2, 0), cursorMode);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() != null)
        {
            Cursor.visible = true;
        }
    }
}
