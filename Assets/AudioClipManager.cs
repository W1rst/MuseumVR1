using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public GameObject[] audioPanels; // ћассив панелей дл€ каждого аудио-клипа
    public AudioClip[] audioClips; // ћассив аудио-клипов дл€ каждой панели
    private AudioSource[] audioSources; // ћассив источников звука дл€ воспроизведени€ аудио-клипов

    public Button[] audioButtons; // ћассив кнопок дл€ открыти€ и закрыти€ панелей
    public Button[] playPauseButtons; // ћассив кнопок дл€ воспроизведени€ и паузы
    public Slider[] seekSliders; // ћассив слайдеров дл€ перемотки

    void Start()
    {
        // »нициализируем массив источников звука
        audioSources = new AudioSource[audioPanels.Length];

        // —оздаем источник звука дл€ каждой панели
        for (int i = 0; i < audioPanels.Length; i++)
        {
            GameObject panel = audioPanels[i];
            AudioSource audioSource = panel.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = audioClips[i];
            audioSources[i] = audioSource;

            // «акрываем панель по умолчанию
            ClosePanel(i);

            // ѕрив€зываем методы управлени€ воспроизведением и перемоткой к кнопкам и слайдерам
            int index = i;
            playPauseButtons[i].onClick.AddListener(() => TogglePlayPause(index));
            seekSliders[i].onValueChanged.AddListener((value) => SeekAudio(index, value));
        }

        // ѕрив€зываем методы открыти€/закрыти€ панелей к кнопкам
        for (int i = 0; i < audioButtons.Length; i++)
        {
            int buttonIndex = i;
            audioButtons[i].onClick.AddListener(() => TogglePanel(buttonIndex));
        }
    }

    void Update()
    {
        // ќбновл€ем значение слайдера на основе текущей позиции воспроизведени€
        for (int i = 0; i < audioPanels.Length; i++)
        {
            if (audioSources[i].isPlaying)
            {
                seekSliders[i].value = audioSources[i].time / audioSources[i].clip.length;
            }
        }
    }

    // ћетод дл€ открыти€/закрыти€ панели и воспроизведени€/остановки аудио-клипа
    public void TogglePanel(int index)
    {
        // ≈сли панель активна, закрываем еЄ и останавливаем воспроизведение
        if (audioPanels[index].activeSelf)
        {
            ClosePanel(index);
        }
        // »наче открываем панель и воспроизводим аудио
        else
        {
            OpenPanel(index);
        }
    }

    // ћетод дл€ открыти€ панели и воспроизведени€ аудио-клипа
    public void OpenPanel(int index)
    {
        // ѕеребираем все панели
        for (int i = 0; i < audioPanels.Length; i++)
        {
            // ≈сли текуща€ панель не совпадает с индексом открываемой панели, то закрываем еЄ
            if (i != index)
            {
                ClosePanel(i);
            }
        }

        // јктивируем панель
        audioPanels[index].SetActive(true);

        // ¬оспроизводим аудио-клип
        audioSources[index].Play();
    }

    // ћетод дл€ закрыти€ панели и остановки воспроизведени€ аудио-клипа
    public void ClosePanel(int index)
    {
        // ќстанавливаем воспроизведение аудио-клипа
        audioSources[index].Stop();

        // ƒеактивируем панель
        audioPanels[index].SetActive(false);
    }

    // ћетод дл€ воспроизведени€ и паузы аудио
    public void TogglePlayPause(int index)
    {
        // ≈сли аудио воспроизводитс€, останавливаем его
        if (audioSources[index].isPlaying)
        {
            audioSources[index].Pause();
        }
        // ≈сли аудио не воспроизводитс€, запускаем его
        else
        {
            audioSources[index].UnPause();
        }
    }

    // ћетод дл€ перемотки аудио
    public void SeekAudio(int index, float value)
    {
        // ¬ычисл€ем позицию, к которой нужно перемотать аудио-клип
        float newPosition = value * audioSources[index].clip.length;

        // ”станавливаем новую позицию воспроизведени€ аудио-клипа
        audioSources[index].time = newPosition;
    }
}