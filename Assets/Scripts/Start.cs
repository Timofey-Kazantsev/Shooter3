using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // Название сцены "Stroika"
    private string sceneName = "Stroika";

    // Метод для загрузки сцены
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
