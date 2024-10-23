using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject opClMenu;

    [SerializeField] private float sensitivity = 2f; // Чувствительность мыши

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isDragging = false; // Флаг, определяющий, нажата ли левая кнопка мыши
    private Vector3 lastPosition; // Предыдущее положение мыши

    void Update()
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastPosition = Input.mousePosition;
        }
        // Проверяем, отпущена ли левая кнопка мыши
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // Если нажата левая кнопка мыши и пользователь двигает мышь, изменяем положение камеры
        if (isDragging)
        {
            float mouseX = Input.mousePosition.x - lastPosition.x;
            float mouseY = Input.mousePosition.y - lastPosition.y;
            rotationY -= mouseX * sensitivity; // Инвертируем направление вращения по горизонтали
            rotationX += mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Ограничиваем вращение по вертикали
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
            lastPosition = Input.mousePosition;
        }
    }

    // Метод для открытия / закрытия меню
    public void opCl()
    {
        opClMenu.SetActive(!opClMenu.activeSelf);
    }
}
