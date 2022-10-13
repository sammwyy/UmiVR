using UnityEngine;

public class MicrophoneLevel : MonoBehaviour
{
    private int _samples = 12;
    private AudioClip _clip = null;

    private string _device = null;
    private int _percent = 0;
    private int _threshold = 10;

    float GetLevel()
    {
        float levelMax = 0;
        float[] waveData = new float[_samples];
        int micPosition = Microphone.GetPosition(_device) - (_samples + 1); // null means the first microphone

        if (micPosition < 0) return 0;

        _clip.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        for (int i = 0; i < _samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        return levelMax;
    }

    float GetDB()
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(GetLevel()));
        return float.IsInfinity(db) ? -180 : db;
    }

    int GetPercent()
    {
        float db = GetDB();
        float dbAbs = Mathf.Abs(db);
        float fix = 180 - dbAbs;
        float percent = (fix / 180) * 100;
        return (int)Mathf.Floor(Mathf.Abs(percent));
    }

    public bool HasActivity()
    {
        return GetPercent() >= _threshold;
    }

    public void SetDevice(string deviceName)
    {
        _device = deviceName;
        _clip = Microphone.Start(deviceName, true, 999, 44100);
    }

    public void SetDevice(int index)
    {
        this.SetDevice(Microphone.devices[index]);
    }

    public string[] Devices
    {
        get
        {
            return Microphone.devices;
        }
    }

    public string Device
    {
        get
        {
            return this._device;
        }
    }

    public int Percent
    {
        get
        {
            return this._percent;
        }
    }

    public int Threshold
    {
        get
        {
            return this._threshold;
        }
        set
        {
            this._threshold = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.SetDevice(1);
    }

    // Update is called once per frame
    void Update()
    {
        _percent = GetPercent();
    }
}
