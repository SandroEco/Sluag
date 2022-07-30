using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    private Rigidbody2D rb;
    private Movement movement;
    public DialogManager dM;
    public bool gotHorn;

    [Header("NPC")]
    public bool sign;
    public bool tim;
    public bool jim;
    public bool mysteriousMan;
    public bool chronos;
    public bool chronosEnd;
    public bool sluagsMom;
    public bool steven;
    public bool barkeeper;

    private void Start()
    {
        dM = FindObjectOfType<DialogManager>().GetComponent<DialogManager>();
        if (tim && SaveManager.instance.activeSave.talkedAboutLetter != 0f)
        {
            Destroy(gameObject);
        }

        if (jim && SaveManager.instance.activeSave.gotHorn == true)
        {
            Destroy(gameObject);
        }

        if (steven && SaveManager.instance.activeSave.enableWalljump == true)
        {
            Destroy(gameObject);
        }

        if (barkeeper && SaveManager.instance.activeSave.talkedToBarkeeper == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

        if (jim && other.tag == "Player")
        {
            if (InventoryScript.instance.demonsHorn == 0)
            {
                rb = other.GetComponent<Rigidbody2D>();
                rb.AddForce(-transform.right * 100, ForceMode2D.Impulse);
                transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
            }
            if (InventoryScript.instance.demonsHorn >= 1)
            {
                transform.Find("Dialog2").GetComponent<DialogTrigger>().StartDialog();
                gotHorn = true;
                SaveManager.instance.activeSave.gotHorn = gotHorn;
                SaveManager.instance.Save();
            }
        }

        if(chronosEnd && other.tag == "Player")
        {
            transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
        }

        if (barkeeper)
        {
            StartDialog();
            SaveManager.instance.activeSave.talkedToBarkeeper = true;
            SaveManager.instance.Save();
            Destroy(this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        movement = other.GetComponent<Movement>();

        if(sign && other.tag == "Player" && Input.GetButton("Interact"))
        {
            StartDialog();
        }

        if (sluagsMom && other.tag == "Player" && Input.GetButton("Interact"))
        {
            if (movement.readLetter == 0)
            {
                transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
            }
            else if (movement.readLetter == 1 && movement.talkedAboutLetter == 0)
            {
                transform.Find("Dialog2").GetComponent<DialogTrigger>().StartDialog();
                StartCoroutine(Wait());
            }
            else if (movement.talkedAboutLetter == 1)
            {
                transform.Find("Dialog3").GetComponent<DialogTrigger>().StartDialog();
            }
        }

        if (chronos && other.tag == "Player" && Input.GetButton("Interact") && InventoryScript.instance.circleShards == 0)
        {
            transform.Find("Dialog1").GetComponent<DialogTrigger>().StartDialog();
        }
        else if (chronos && other.tag == "Player" && Input.GetButton("Interact") && InventoryScript.instance.circleShards <= 1)
        {
            transform.Find("Dialog2").GetComponent<DialogTrigger>().StartDialog();
        }

        if(steven && other.tag == "Player" && Input.GetButton("Interact"))
        {
            transform.Find("Dialog2").GetComponent<DialogTrigger>().StartDialog();
            FindObjectOfType<WallJumping>().enabled = true;
            SaveManager.instance.activeSave.enableWalljump = true;
        }

        if (chronosEnd && other.tag == "Player")
        {
            if (!DialogManager.isActive)
            {
                Debug.Log("isNotActive");
                //LoadScene && DestroyCollider
            }
        }
    }

    public void StartDialog()
    {
        FindObjectOfType<DialogManager>().OpenDialog(messages, actors);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        movement.talkedAboutLetter = 1;
        SaveManager.instance.activeSave.talkedAboutLetter = movement.talkedAboutLetter;
        SaveManager.instance.Save();
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
