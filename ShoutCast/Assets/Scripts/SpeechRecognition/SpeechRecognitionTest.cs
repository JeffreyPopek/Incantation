using System.Collections.Generic;
using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpeechRecognitionTest : MonoBehaviour {
    [SerializeField] private Dropdown dropdown;

    private string _lastSaid;
    private AudioClip _clip;
    private byte[] _bytes;
    private bool _recording;

    private void Start() 
    {
        foreach (var device in Microphone.devices)
            dropdown.options.Add(new Dropdown.OptionData(device));

        dropdown.onValueChanged.AddListener(ChangeMicrophone);
        var index = PlayerPrefs.GetInt("user-mic-device-index");
        dropdown.SetValueWithoutNotify(index);
    }
    
    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }

    private void Update() 
    {
        // if (_recording && Microphone.GetPosition(null) >= _clip.samples)
        //     StopRecording();
        //
        // if (Input.GetMouseButtonDown(0))
        //     StartRecording();
        //
        // if (Input.GetMouseButtonUp(0))
        //     StopRecording();
    }

    private void StartRecording() 
    {
        _clip = Microphone.Start(null, false, 10, 44100);
        _recording = true;
    }

    private void StopRecording() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * _clip.channels];
        _clip.GetData(samples, 0);
        _bytes = EncodeAsWAV(samples, _clip.frequency, _clip.channels);
        _recording = false;
        SendRecording();
    }

    private void SendRecording() {
        // Send API call to get string of said phrase back
        HuggingFaceAPI.AutomaticSpeechRecognition(_bytes, response => {
            _lastSaid = response;
            MagicManager.Instance.CastSpell(response);
        }, _ => {

        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using var memoryStream = new MemoryStream(44 + samples.Length * 2);
        using (var writer = new BinaryWriter(memoryStream)) {
            writer.Write("RIFF".ToCharArray());
            writer.Write(36 + samples.Length * 2);
            writer.Write("WAVE".ToCharArray());
            writer.Write("fmt ".ToCharArray());
            writer.Write(16);
            writer.Write((ushort)1);
            writer.Write((ushort)channels);
            writer.Write(frequency);
            writer.Write(frequency * channels * 2);
            writer.Write((ushort)(channels * 2));
            writer.Write((ushort)16);
            writer.Write("data".ToCharArray());
            writer.Write(samples.Length * 2);

            foreach (var sample in samples)
                writer.Write((short)(sample * short.MaxValue));
        }
        return memoryStream.ToArray();
    }
}
