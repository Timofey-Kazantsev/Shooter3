using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // �������� ����� "Stroika"
    private string sceneName = "Stroika";

    // ����� ��� �������� �����
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
