using UnityEngine;

public class Exit : MonoBehaviour
{
    // Метод для выхода из игры
    public void QuitGame()
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
