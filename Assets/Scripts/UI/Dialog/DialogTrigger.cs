using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    private Rigidbody2D rb;
    private Movement movement;
    public DialogManager dM;

    [Header("NPC")]
    public bool interactable;
    public bool tim;
    public bool mysteriousMan;

    private void Start()
    {
        dM = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!interactable)
        {
            movement = other.GetComponent<Movement>();
            if (tim && other.tag == "Player")
            {
                if (movement.talkedAboutLetter == 0)
                {
                    rb = other.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.right * 100, ForceMode2D.Impulse);
                    StartDialog();
                }
            }

            if (mysteriousMan && other.tag == "Player")
            {
                if (InventoryScript.instance.waterBottle == 0)
                {
                    rb = other.GetComponent<Rigidbody2D>();
                    rb.AddForce(-transform.right * 100, ForceMode2D.Impulse);
                    transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
                }
                if (InventoryScript.instance.waterBottle >= 1)
                {
                    transform.Find("Dialog2").GetComponent<DialogTrigger>().StartDialog();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(mysteriousMan && other.tag == "Player")
        {
            if(InventoryScript.instance.waterBottle >= 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartDialog()
    {
        FindObjectOfType<DialogManager>().OpenDialog(messages, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
}
