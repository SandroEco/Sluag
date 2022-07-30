using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    private AudioSource skipTextSource;

    Message[] currentMessages;
    Actor[] currentActors;
    public static int activeMessage = 0;
    public static bool isActive;

    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            isActive = false;
            backgroundBox.LeanScale(Vector3.zero, 0.3f).setEaseInOutExpo();
        }
    }

    public void OpenDialog(Message[] messages, Actor[] actors)
    {
        if (!isActive)
        {
            isActive = true;
            currentMessages = messages;
            currentActors = actors;
            activeMessage = 0;
            DisplayMessage();
            backgroundBox.LeanScale(Vector3.one, 0.3f);
        }
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
    }

    private void Start()
    {
        skipTextSource = GameObject.Find("SkipTextSource").GetComponent<AudioSource>();
        backgroundBox.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isActive == true)
        {
            skipTextSource.Play();
            NextMessage();
        }
    }
}
