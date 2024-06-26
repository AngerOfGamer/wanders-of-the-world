using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private Image faceExpressionImage;
    [SerializeField] private Image sceneImage; // Tambahkan ini

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewritterEffect typeWritterEffect;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypewritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            // Update ekspresi wajah
            if (dialogueObject.FaceExpressions != null && dialogueObject.FaceExpressions.Length > i)
            {
                faceExpressionImage.sprite = dialogueObject.FaceExpressions[i];
                faceExpressionImage.gameObject.SetActive(true);
            }
            else
            {
                faceExpressionImage.gameObject.SetActive(false);
            }

            // Update gambar scene
            if (dialogueObject.SceneImages != null && dialogueObject.SceneImages.Length > i)
            {
                sceneImage.sprite = dialogueObject.SceneImages[i];
                sceneImage.gameObject.SetActive(true);
            }
            else
            {
                sceneImage.gameObject.SetActive(false);
            }

            yield return RunTypingEffect(dialogue);

            textlabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWritterEffect.Run(dialogue, textlabel);

        while (typeWritterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeWritterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textlabel.text = string.Empty;
        faceExpressionImage.gameObject.SetActive(false);
        sceneImage.gameObject.SetActive(false); // Tambahkan ini
    }
}
