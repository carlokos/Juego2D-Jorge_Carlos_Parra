using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    /*
     * Script para los dialogos del juego, se le pasa los elementos de la UI y con
     * una array de string se le va colocando el texto.
     * Ademas comprueba si el jugador esta en el rango para hablar y muestra la señal para activar
     * los dialogos
     */
    private bool PlayerInRange;
    [SerializeField] private GameObject mark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    [SerializeField] private CombatPlayer player;

    private bool didDialogueStart;
    private int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && Input.GetKeyDown(KeyCode.F)){
            if (!didDialogueStart)
            {
                StartDialogue();
            }else if (dialogueText.text == dialogueLines[index])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index];
            }
        } 
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        player.CanAttack = false;
        player.IsTalking = true;
        dialoguePanel.SetActive(true);
        mark.SetActive(false);
        index = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        index++;
        if(index < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
            mark.SetActive(true);
            player.CanAttack = true;
            player.IsTalking = false;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[index])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
            mark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
            mark.SetActive(false);
        }
    }
}
