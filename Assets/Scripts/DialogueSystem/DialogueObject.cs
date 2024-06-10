using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private Sprite[] faceExpressions;
    [SerializeField] private Sprite[] sceneImages;

    public string[] Dialogue => dialogue;
    public bool HasResponses => Responses != null && Responses.Length > 0;
    public Response[] Responses => responses;
    public Sprite[] FaceExpressions => faceExpressions;
    public Sprite[] SceneImages => sceneImages;
}
