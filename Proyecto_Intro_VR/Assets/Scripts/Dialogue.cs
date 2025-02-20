using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action = null;
    public UnityEvent OnPress = new UnityEvent();
    public TextMeshProUGUI textComponent;
    public LocalizationController localizationController;
    public GameObject dialoguePanel;
    public float textSpeed;
    private List<string[]> dialogues;
    private string[] lines;
    private int index;
    private Dictionary<int, bool> dialogueStarted;
    private Coroutine typingCoroutine;
    
    public event System.Action OnDialogueFinished;

    private void Awake()
    {
        action.started += Pressed;
    }

    private void OnDestroy()
    {
        action.started -= Pressed;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }

    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
            return;
        }

        if (localizationController == null)
        {
            Debug.LogError("LocalizationController is not assigned.");
            return;
        }

        textComponent.text = string.Empty;
        dialogues = new List<string[]>();
        dialogueStarted = new Dictionary<int, bool>();
        localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void OnLocalizationReady()
    {
        dialogues = localizationController.GetAllLocalizedLines();
    }

    public void SetupDialogue()
    {
        dialogues = localizationController.GetAllLocalizedLines();
    }

    public bool IsDialogueIndexValid(int dialogueIndex)
    {
        return dialogueIndex >= 0 && dialogueIndex < dialogues.Count && dialogues[dialogueIndex] != null && dialogues[dialogueIndex].Length > 0;
    }

    public void StartDialogue(int dialogueIndex, bool isRepeatable)
    {
        if (!dialogueStarted.ContainsKey(dialogueIndex))
        {
            dialogueStarted[dialogueIndex] = false;
        }

        if (!dialogueStarted[dialogueIndex] || isRepeatable)
        {
            if (IsDialogueIndexValid(dialogueIndex))
            {
                StopAllCoroutines();
                textComponent.text = string.Empty;
                lines = dialogues[dialogueIndex];
                dialogueStarted[dialogueIndex] = !isRepeatable;
                StartDialogue();
            }
            else
            {
                Debug.LogError("Invalid dialogue index.");
            }
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
        OnPress.RemoveAllListeners();
        OnPress.AddListener(ShowFullText);
    }

    void ShowFullText()
    {
        if (lines != null && textComponent.text != lines[index])
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = lines[index];
        }
    }

    IEnumerator TypeLine()
    {
        if (lines == null || lines.Length == 0 || lines[index] == null)
        {
            Debug.LogError("Current line is null or lines array is not initialized properly.");
            yield break;
        }

        textComponent.text = "";
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
    public void NextLineUI()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
            dialoguePanel.SetActive(false);
            OnDialogueFinished?.Invoke();
        }
    }
}
