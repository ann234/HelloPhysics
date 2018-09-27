using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour {
    private UISlider slider;
    private AudioSource audio;

    void Awake()
    {
        slider = GetComponent<UISlider>();
    }

    public void OnSliderChanged()
    {
        //change global variable bgmVolume
        Global_Variable.bgmVolume = slider.value;

        //get audio source
        if(GameObject.Find("World").GetComponent<AudioSource>())
        {
            audio = GameObject.Find("World").GetComponent<AudioSource>();
        }

        //change audio's volume
        audio.volume = Global_Variable.bgmVolume;
        //Debug.Log(slider.value);
    }

	// Use this for initialization
	void Start () {
        slider.value = Global_Variable.bgmVolume;
        slider.ForceUpdate();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
