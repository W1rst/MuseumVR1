using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeWallGame : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform wallTransform;
    [SerializeField] private Transform cubesParent;

    [SerializeField] GameObject cubePanelsGame;
    [SerializeField] Button gamePlaye;
    [SerializeField] Button restartButton;
    [SerializeField] TextMeshProUGUI textWin;

    private List<GameObject> raindbowCubes = new List<GameObject>();
    private List<GameObject> blueCubes = new List<GameObject>();
    private List<GameObject> randomCubes = new List<GameObject>();

    void Start()
    {
        gamePlaye.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }


    void StartGame()
    {
        gamePlaye.gameObject.SetActive(false);

        GenerateWall();
        GenerateRandomCubes();
        GenerateUniformCubes();
    }
    void RestartGame()
    {
        CubeInteraction.instance.blueCubes = 0;
        restartButton.gameObject.SetActive(true);
        foreach (GameObject cube in raindbowCubes)
        {
            Destroy(cube);
        }
        raindbowCubes.Clear(); 
        
        foreach (GameObject cube in randomCubes)
        {
            Destroy(cube);
        }
        randomCubes.Clear();

        foreach (GameObject blueCube in blueCubes)
        {
            Destroy(blueCube);
        }
        blueCubes.Clear();
        textWin.gameObject.SetActive(false);
        StartGame();
    }
    private void Update()
    {
        CheckWinCondition();
        
    }
    void GenerateWall()
    {
        Vector3 startPos = wallTransform.position;

        for (int i = 0; i < 6; i++)
        {
            GameObject cube = Instantiate(cubePrefab, startPos + new Vector3(i * 1.5f, 0, 0), Quaternion.identity,cubesParent);
            raindbowCubes.Add(cube);

            Color randomColor = new Color(Random.value, Random.value, Random.value);
            cube.GetComponent<Renderer>().material.color = randomColor;
        }
    }

    void GenerateRandomCubes()
    {
        for (int i = 0; i < 6; i++)
        {
            float randomX = Random.Range(-10f, 10f);
            float randomY = Random.Range(-10f, 10f);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity, cubesParent);
            randomCubes.Add(cube);

            Color wallCubeColor = raindbowCubes[i].GetComponent<Renderer>().material.color;
            cube.GetComponent<Renderer>().material.color = wallCubeColor;
        }
    }

    void GenerateUniformCubes()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector3 position = new Vector3(i * 1.5f, 0, 0);

            GameObject blueCube = Instantiate(cubePrefab, position, Quaternion.identity, cubesParent);
            blueCubes.Add(blueCube);
            blueCube.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 0.5f);
        }
    }

    void CheckWinCondition()
    {
        bool winConditionMet = true;
        float rayLength = 10f;

        for (int i = 0; i < 6; i++)
        {
            GameObject wallCube = raindbowCubes[i];

            Vector3 rayOrigin = wallCube.transform.position;

            Ray ray = new Ray(rayOrigin, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Cube"))
                {
                    if (hitObject.GetComponent<Renderer>().material.color != wallCube.GetComponent<Renderer>().material.color)
                    {
                        winConditionMet = false;
                        break;
                    }
                }
                else
                {
                    winConditionMet = false;
                    break;
                }
            }
            else
            {
                winConditionMet = false;
                break;
            }
        }

        // Проверяем условие выигрыша
        if (CubeInteraction.instance.blueCubes == 6 && winConditionMet)
        {
            Debug.Log("Вы выиграли!");
            cubePanelsGame.gameObject.SetActive(true);
            gamePlaye.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
            textWin.gameObject.SetActive(true);
        }
    }

}
