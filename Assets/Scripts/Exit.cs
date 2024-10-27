using UnityEngine;

public class Exit : MonoBehaviour
{
    // ���� ����� ����� ��������� �� ������ � UI
    public void ExitGame()
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
