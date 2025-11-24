using UnityEngine;
using UnityEngine.Android;

public class MicManager : MonoBehaviour
{
    public static MicManager Instance;

    private string _deviceMicrophone;
    private AudioClip _clipRecord;
    private int _sampleWindow = 128;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
        }
        StartMicrophone();
    }

    void StartMicrophone()
    {
        if (Microphone.devices.Length > 0)
        {
            _deviceMicrophone = Microphone.devices[0];
            _clipRecord = Microphone.Start(_deviceMicrophone, true, 999, 44100);
            Debug.Log("Micrófono iniciado: " + _deviceMicrophone);
        }
        else
        {
            Debug.LogError("No se encontró micrófono en este dispositivo.");
        }
    }

    public float GetLoudnessFromMic()
    {
        if (_clipRecord == null) return 0;

        return GetLoudness(_MicrophonePosition(), _clipRecord);
    }

    int _MicrophonePosition()
    {
        return Microphone.GetPosition(_deviceMicrophone);
    }

    float GetLoudness(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - _sampleWindow;
        if (startPosition < 0) return 0;

        float[] waveData = new float[_sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;
        for (int i = 0; i < _sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / _sampleWindow;
    }

    void OnDisable()
    {
        StopMicrophone();
    }

    void StopMicrophone()
    {
        Microphone.End(_deviceMicrophone);
    }
}