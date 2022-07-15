using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSign : MonoBehaviour
{
    public RectTransform map;
    private bool isActive;

    private void Start()
    {
        isActive = false;
        map.transform.localScale = Vector3.zero;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetAxisRaw("Vertical") == 1f)
        {
            map.LeanScale(Vector3.one, 0.3f);
            isActive = true;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isActive == true)
        {
            isActive = false;
            map.LeanScale(Vector3.zero, 0.3f).setEaseInOutExpo();
        }
    }
}
