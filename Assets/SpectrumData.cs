using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumData : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public float[] samples;
    public float[] averageSamples;
    public int numberofChunks = 8;
    public float[] frequencyBands;


    // Start is called before the first frame update
    void Start()
    {
        samples = new float[512];
        averageSamples = new float[numberofChunks];
        frequencyBands = new float[8];
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        int samplesPerChunk = samples.Length / numberofChunks;
        for (int i = 0; i < numberofChunks; i++)
        {
            float average = 0;
            for (int j = 0; j < samplesPerChunk; j++)
            {
                average += samples[(i * samplesPerChunk) + j];
            }
            average /= samplesPerChunk;
            averageSamples[i] = average;
        }
    }
    public void GetFrequecyBands()
    {
        /*
         *  22000 / 512 = 48
         *  
         * 
         * 
         * 
         */
    }
}
