using System.Collections.Generic;
using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpeechRecognitionTest : MonoBehaviour {
    // [SerializeField] private Button startButton;
    // [SerializeField] private Button stopButton;
    // [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Dropdown dropdown;

    private string lastSaid = "";

    private AudioClip clip;
    private byte[] bytes;
    private bool recording;

    private void Start() {
        // foreach (var device in Microphone.devices)
        // {
        //     Debug.Log("Name: " + device);
        // }
        
        foreach (var device in Microphone.devices)
        {
            dropdown.options.Add(new Dropdown.OptionData(device));
        }
        dropdown.onValueChanged.AddListener(ChangeMicrophone);
            
        var index = PlayerPrefs.GetInt("user-mic-device-index");
        dropdown.SetValueWithoutNotify(index);
    }
    
    private void ChangeMicrophone(int index)
    {
        PlayerPrefs.SetInt("user-mic-device-index", index);
    }

    private void Update() {
        if (recording && Microphone.GetPosition(null) >= clip.samples) {
            StopRecording();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartRecording();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopRecording();
        }
    }

    private void StartRecording() {
        // text.color = Color.white;
        // text.text = "Recording...";
        // startButton.interactable = false;
        // stopButton.interactable = true;
        clip = Microphone.Start(null, false, 10, 44100);
        recording = true;
    }

    private void StopRecording() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        recording = false;
        SendRecording();
    }

    private void SendRecording() {
        // text.color = Color.yellow;
        // text.text = "Sending...";
        // stopButton.interactable = false;
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response => {
            // text.color = Color.white;
            // text.text = response;
            
            //Debug.Log(response);
            lastSaid = response;
            MagicManager.Instance.CastSpell(response);
            //Debug.Log(CalculateLevenshteinDistance(response, "Testing one two three"));

            //startButton.interactable = true;
        }, error => {
            //text.color = Color.red;
            //text.text = error;
            //startButton.interactable = true;
        });
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
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

                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    string GetLastSaid()
    {
        return lastSaid;
    }
    
   
}
