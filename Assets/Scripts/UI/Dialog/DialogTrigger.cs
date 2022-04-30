using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

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
