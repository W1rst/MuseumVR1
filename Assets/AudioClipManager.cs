using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject[] audioPanels; // ������ ������� ��� ������� �����-�����
    public AudioClip[] audioClips; // ������ �����-������ ��� ������ ������
    private AudioSource[] audioSources; // ������ ���������� ����� ��� ��������������� �����-������

    public Button[] audioButtons; // ������ ������ ��� �������� � �������� �������
    public Button[] playPauseButtons; // ������ ������ ��� ��������������� � �����
    public Slider[] seekSliders; // ������ ��������� ��� ���������

    void Start()
    {
        // �������������� ������ ���������� �����
        audioSources = new AudioSource[audioPanels.Length];

        // ������� �������� ����� ��� ������ ������
        for (int i = 0; i < audioPanels.Length; i++)
        {
            GameObject panel = audioPanels[i];
            AudioSource audioSource = panel.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = audioClips[i];
            audioSources[i] = audioSource;

            // ��������� ������ �� ���������
            ClosePanel(i);

            // ����������� ������ ���������� ���������������� � ���������� � ������� � ���������
            int index = i;
            playPauseButtons[i].onClick.AddListener(() => TogglePlayPause(index));
            seekSliders[i].onValueChanged.AddListener((value) => SeekAudio(index, value));
        }

        // ����������� ������ ��������/�������� ������� � �������
        for (int i = 0; i < audioButtons.Length; i++)
        {
            int buttonIndex = i;
            audioButtons[i].onClick.AddListener(() => TogglePanel(buttonIndex));
        }
    }

    void Update()
    {
        // ��������� �������� �������� �� ������ ������� ������� ���������������
        for (int i = 0; i < audioPanels.Length; i++)
        {
            if (audioSources[i].isPlaying)
            {
                seekSliders[i].value = audioSources[i].time / audioSources[i].clip.length;
            }
        }
    }

    // ����� ��� ��������/�������� ������ � ���������������/��������� �����-�����
    public void TogglePanel(int index)
    {
        // ���� ������ �������, ��������� � � ������������� ���������������
        if (audioPanels[index].activeSelf)
        {
            ClosePanel(index);
        }
        // ����� ��������� ������ � ������������� �����
        else
        {
            OpenPanel(index);
        }
    }

    // ����� ��� �������� ������ � ��������������� �����-�����
    public void OpenPanel(int index)
    {
        // ���������� ��� ������
        for (int i = 0; i < audioPanels.Length; i++)
        {
            // ���� ������� ������ �� ��������� � �������� ����������� ������, �� ��������� �
            if (i != index)
            {
                ClosePanel(i);
            }
        }

        // ���������� ������
        audioPanels[index].SetActive(true);

        // ������������� �����-����
        audioSources[index].Play();
    }

    // ����� ��� �������� ������ � ��������� ��������������� �����-�����
    public void ClosePanel(int index)
    {
        // ������������� ��������������� �����-�����
        audioSources[index].Stop();

        // ������������ ������
        audioPanels[index].SetActive(false);
    }

    // ����� ��� ��������������� � ����� �����
    public void TogglePlayPause(int index)
    {
        // ���� ����� ���������������, ������������� ���
        if (audioSources[index].isPlaying)
        {
            audioSources[index].Pause();
        }
        // ���� ����� �� ���������������, ��������� ���
        else
        {
            audioSources[index].UnPause();
        }
    }

    // ����� ��� ��������� �����
    public void SeekAudio(int index, float value)
    {
        // ��������� �������, � ������� ����� ���������� �����-����
        float newPosition = value * audioSources[index].clip.length;

        // ������������� ����� ������� ��������������� �����-�����
        audioSources[index].time = newPosition;
    }
}