using UnityEngine;

public class MicTest2 : MonoBehaviour
{
    //#region SingleTon
    //public static MicInput Instance { set; get; }
    //#endregion

    [HideInInspector] public float MicLoudness;
    [HideInInspector] public float MicLoudnessinDecibels;

    private string _device;

    private AudioClip _clipRecord = null;

    private int _sampleWindow = 128;

    // Mic initialization
    private void InitMic()
    {
        if (_device == null)
            _device = Microphone.devices[0];

        _clipRecord = Microphone.Start(_device, true, 999, 44100);
    }

    // Get data from microphone into audioclip
    private float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // Null means the first microphone

        if (micPosition < 0)
            return 0;

        _clipRecord.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];

            if (levelMax < wavePeak)
                levelMax = wavePeak;
        }

        return levelMax;
    }

    // Get data from microphone into audioclip
    private float MicrophoneLevelMaxDecibels()
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));
        return db;
    }

    private void Update()
    {
        // LevelMax equals to the highest normalized value power 2, a small number because < 1
        // Pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessinDecibels = MicrophoneLevelMaxDecibels();

        Debug.Log($"Mic loudness: {MicLoudness} || Mic loudness decibell: {MicLoudnessinDecibels}");
    }

    // Start mic when scene starts
    private void OnEnable()
    {
        InitMic();
        //Instance = this;
    }

    // Stop mic when loading a new level or quit application
    private void OnDisable()
    {
        StopMicrophone();
    }

    private void OnDestroy()
    {
        StopMicrophone();
    }

    private void StopMicrophone()
    {
        Microphone.End(_device);
    }
}
