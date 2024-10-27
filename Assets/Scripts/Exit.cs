using UnityEngine;

public class Exit : MonoBehaviour
{
    // Этот метод можно назначить на кнопку в UI
    public void ExitGame()
    {
        // Выйти из режима Play Mode в редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Выйти из приложения при сборке
        Application.Quit();
#endif
    }
}
