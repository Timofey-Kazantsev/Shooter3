using UnityEngine;

public class Exit : MonoBehaviour
{
    // ����� ��� ������ �� ����
    public void QuitGame()
    {
        // ����� �� ������ Play Mode � ��������� Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� �� ���������� ��� ������
        Application.Quit();
#endif
    }
}
