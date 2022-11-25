using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValues : MonoBehaviour
{
    public Slider _Slider;
    public Text _Text;

    private void Start()
    {
        _Slider.onValueChanged.AddListener((_) => { _Text.text = _.ToString("#.##"); });

        _Text.text = _Slider.value.ToString("#.##");
    }
}
