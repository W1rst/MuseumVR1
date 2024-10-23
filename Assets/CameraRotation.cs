using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject opClMenu;

    [SerializeField] private float sensitivity = 2f; // ���������������� ����

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isDragging = false; // ����, ������������, ������ �� ����� ������ ����
    private Vector3 lastPosition; // ���������� ��������� ����

    void Update()
    {
        // ���������, ������ �� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastPosition = Input.mousePosition;
        }
        // ���������, �������� �� ����� ������ ����
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���� ������ ����� ������ ���� � ������������ ������� ����, �������� ��������� ������
        if (isDragging)
        {
            float mouseX = Input.mousePosition.x - lastPosition.x;
            float mouseY = Input.mousePosition.y - lastPosition.y;
            rotationY -= mouseX * sensitivity; // ����������� ����������� �������� �� �����������
            rotationX += mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // ������������ �������� �� ���������
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
            lastPosition = Input.mousePosition;
        }
    }

    // ����� ��� �������� / �������� ����
    public void opCl()
    {
        opClMenu.SetActive(!opClMenu.activeSelf);
    }
}
