using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstSelected : MonoBehaviour
{
    [SerializeField] private Selectable selectable;

    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(selectable.gameObject);
    }

}
