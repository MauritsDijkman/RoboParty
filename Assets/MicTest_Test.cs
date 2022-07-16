using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MicTest_Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    public float micLoudness;
    private string _device;
    public float treshold = 0.1f;

    public UnityEvent onTresholdPassed = null;
    public UnityEvent onTresholdStopped = null;

    //mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[0];
        Debug.Log($"Mic name: {Microphone.devices[0].ToString()}");
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
    }


    AudioClip _clipRecord = null;
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return Mathf.Sqrt(Mathf.Sqrt(levelMax));
    }

    bool aboveTreshold = false;
    void MicTreshold()
    {
        if (micLoudness < treshold)
        {
            if (aboveTreshold)
            {
                onTresholdStopped?.Invoke();
                if (text != null)
                    text.color = Color.red;
            }
            aboveTreshold = false;
        }
        else if (micLoudness >= treshold)
        {
            if (!aboveTreshold)
            {
                onTresholdPassed?.Invoke();
                if (text != null)
                    text.color = Color.blue;
            }
            aboveTreshold = true;
        }
    }


    void Update()
    {


        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        micLoudness = Mathf.Clamp(LevelMax(), 0, 1);
        if (text != null)
            text.text = $"Mic:{micLoudness}";


        MicTreshold();
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;

        if (text != null)
            text.color = Color.red;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}