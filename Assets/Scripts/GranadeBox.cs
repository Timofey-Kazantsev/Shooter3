using UnityEngine;

namespace XtremeFPS.FPSController
{
    public class GrenadeBox : MonoBehaviour
    {
        [SerializeField] private int grenadesInBox = 3; //  оличество гранат в €щике
        [SerializeField] private KeyCode interactKey = KeyCode.E; //  лавиша дл€ взаимодействи€
        [SerializeField] private float interactionDistance = 3f; // ћаксимальное рассто€ние дл€ взаимодействи€

        private void Update()
        {
            // Ќайти игрока по тегу
            GameObject player = GameObject.FindWithTag("Player");

            // ѕровер€ем, существует ли игрок и находитс€ ли он в пределах рассто€ни€
            if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
            {
                // ѕолучаем компонент GranadeController у игрока
                GranadeController grenadeController = player.GetComponent<GranadeController>();

                // ѕровер€ем, нажата ли клавиша взаимодействи€
                if (Input.GetKeyDown(interactKey) && grenadeController != null)
                {
                    // ѕровер€ем, есть ли еще гранаты в €щике и меньше ли у игрока максимальное количество гранат
                    if (grenadesInBox > 0 && grenadeController.CurrentGrenades < grenadeController.MaxGrenades)
                    {
                        // ƒобавл€ем одну гранату из €щика
                        grenadeController.AddGrenade(1);
                        grenadesInBox--; // ”меньшаем количество гранат в €щике
                        Debug.Log($"»грок пополнил 1 гранату. ќсталось в €щике: {grenadesInBox}");

                        // ≈сли гранат в €щике больше нет, уничтожаем €щик
                        if (grenadesInBox <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        Debug.Log("¬ €щике больше нет гранат или у игрока максимальное количество гранат.");
                    }
                }
            }
        }
    }
}
