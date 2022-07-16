using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioScource;

    // Microphone input
    public AudioClip _audioClip;
    public bool _useMicrophone;
    public string _selectedDevice;

    // FFT values
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];

    private float[] _freqBand = new float[8];
    private float[] _bandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];
    private float[] _freqBandHighest = new float[8];

    // Audio band values
    [HideInInspector]
    public float[] _audioband, _audioBandBuffer;

    // Amplitude variables
    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;

    // Audio profile
    public float _audioProfiles;

    // Stereo channels
    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    // Audio64
    float[] _freqBand64 = new float[64];
    float[] _bandBuffer64 = new float[64];
    float[] _bufferDecrease64 = new float[64];
    float[] _freqBandHighest64 = new float[64];

    // Audio band64 values
    public float[] _audioBand64, _audioBandBuffer64;

    // Use this for initialization
    private void Start()
    {
        _audioband = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
        _audioScource = GetComponent<AudioSource>();
        //AudioProfile(_audioProfile);

        if (_useMicrophone)
        {
            if (Microphone.devices.Length > 0)
            {
                _selectedDevice = Microphone.devices[0].ToString();
                _audioScource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
            }
            else
                _useMicrophone = false;
        }
        else if (!_useMicrophone)
            _audioScource.clip = _audioClip;

        _audioScource.Play();
    }

    private void Update()
    {
        if (_audioScource.clip != null)
        {
            //GetSpectrumAudioSource();
            //MakefrequencyBands();
            //MakeFrequencyBands64();
            //BandBuffer();
            //CreateAudioBands();
            //CreateAudioBands64();
            //GetAmplitude();
        }
    }
}
