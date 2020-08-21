using Core.City;
using System;
using UnityEngine;

public class UI_CityLifeUpdater : MonoBehaviour
{
    private void Start()
    {
        CityValues.StaticReference.CityLifeChangedEvent.AddListener(OnLifeChanged);
    }
    // TODO: Finish!
    private void OnLifeChanged(int arg0)
    {
        throw new NotImplementedException();
    }
}
