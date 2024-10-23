using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private GameObject selectedCube;
    private Vector3 initialPosition;
    private Vector3 offset;

    public static CubeInteraction instance;
    
    [HideInInspector] public int blueCubes;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Cube"))
            {
                selectedCube = hit.collider.gameObject;
                initialPosition = selectedCube.transform.position;
                offset = initialPosition - hit.point;
            }
        }

        if (selectedCube != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - selectedCube.transform.position.z)));

            targetPosition.z = initialPosition.z;
            selectedCube.transform.position = targetPosition;
        }

        if (Input.GetMouseButtonUp(1) && selectedCube != null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(selectedCube.transform.position, 0.5f);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Cube") && collider.gameObject != selectedCube)
                {
                    selectedCube.transform.position = collider.transform.position;
                    Destroy(collider.gameObject);
                    blueCubes++;
                    break;
                }
            }

            selectedCube = null;
        }
    }
}
