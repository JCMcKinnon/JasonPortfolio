using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicalCube : MonoBehaviour
{
    public GameObject spectrumObject;
    public SpectrumData spectrumData;
    public int channel;
    private Vector3 scaleTo = new Vector3();
    private Vector3 scaleFrom = new Vector3();
    private bool hasReachedBuffer;
    private bool hasReachedBuffer2;

    public GameObject[] objects2;

    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        spectrumObject = GameObject.FindGameObjectWithTag("SpectrumData");
        spectrumData = spectrumObject.GetComponent<SpectrumData>();
        hasReachedBuffer = true;
        scaleFrom = transform.localScale;
        objects = GameObject.FindGameObjectsWithTag("Background1");
        objects2 = GameObject.FindGameObjectsWithTag("Background2");


    }

    // Update is called once per frame
    void Update()
    {
        if(objects.Length != 40)
        {
            objects = GameObject.FindGameObjectsWithTag("Background1");

        }
        if (objects2.Length != 40)
        {
            objects2 = GameObject.FindGameObjectsWithTag("Background2");

        }


        if (objects.Length == 40)
        {
            if (hasReachedBuffer)
            {
                scaleTo = new Vector3(0, Mathf.Clamp(spectrumData.averageSamples[channel] * 100f, 0.2f, 0.6f), 0);
               var scaleTo2 = new Vector3(0, Mathf.Clamp(spectrumData.averageSamples[4] * 200f, 0.3f, 0.8f), 0);

                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].transform.DOPunchScale(scaleTo, 1f,5,0.5f).OnComplete(Completed);
                    objects2[i].transform.DOPunchScale(scaleTo2, 1f, 4, 0.2f).OnComplete(Completed);

                }

                hasReachedBuffer = false;
            }
        }


    }
    void Completed()
    {
        hasReachedBuffer = true;
    }
}
