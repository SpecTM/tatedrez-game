using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelsToEnableWithDynamic = new();
    [SerializeField] private List<GameObject> panelsToDisableWithDynamic = new();

    private void Start()
    {
        EventManager.OnEnterDynamicMode += OnEnterDynamicMode;
    }

    private void OnDisable()
    {
        EventManager.OnEnterDynamicMode -= OnEnterDynamicMode;
    }

    private void OnEnterDynamicMode()
    {
        foreach (var panel in panelsToEnableWithDynamic)
        {
            panel.SetActive(true);
        }

        foreach (var panel in panelsToDisableWithDynamic)
        {
            panel.SetActive(false);
        }
    }
}
