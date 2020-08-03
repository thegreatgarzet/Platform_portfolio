using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //Imagens do rosto do jogador e dos inimigos
    public Image faceImageInimigo, faceImageAmigo;
    //Textos para onde é inserido o nome do personagem e o que ele está dizendo
    public TMP_Text textDialogueAmigo, textDialogueInimigo;
    public TMP_Text[] textNames;
    //Objeto com todos os itens de dialogo;
    public GameObject dialogueHolder, inimigo, amigo;
    // queue que recebe as falas
    private Queue<string> sentences;
    //sprites dos rostos
    public List<Sprite> faceSprites;
    //lista dos nomes
    public List<string> names;
    //string para verificar os valores no inicio das strings e atribuir 0,1,2 em dainte nas variaveis intNome e intFace
    public string stringCompare;
    int intNome, intFace;
    MovementController player;
    ControleArmas controleArmas;

    GameManager gameManager;

    bool isTyping=false;

    bool triggerBoss = false;

    public bool talking;

    BossValuesControl bossValuesControl;

    AudioControl audioman;
    private void Awake()
    {
        audioman = FindObjectOfType<AudioControl>();
    }
    void Start()
    {
        //zera a lista para não dar problema
        sentences = new Queue<string>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<MovementController>();
        controleArmas = FindObjectOfType<ControleArmas>();
        bossValuesControl = FindObjectOfType<BossValuesControl>();
    }
    private void Update()
    {
        if (isTyping && Input.GetButtonDown("Atirar") || isTyping && Input.GetButtonDown("Submit"))
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue, bool trigger)
    {
        if (!talking)
        {
            triggerBoss = trigger;
            //ativa todos os elementos de dialogo e limpa a lista
            dialogueHolder.SetActive(true);
            sentences.Clear();
            //enfileira as sentenças na lista
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            isTyping = true;
            gameManager.onTyping = true;
            //chama função principal
            player.ispaused = true;
            player.rb.velocity = new Vector2(0.0f, 0.0f);
            controleArmas.canshot = false;
            DisplayNextSentence();
            talking = true;
            
        }
        
    }
    public void DisplayNextSentence()
    {
        //para todas as rotinas para evitar problema
        StopAllCoroutines();
        audioman.SoundStop("talk1");
        audioman.PlaySound("talk1");
        //se não tiver mais sentenças na lista, acaba a execução
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        //tira uma sentença da lista e faz update na UI
        string sentence = sentences.Dequeue();
        UpdateUI(sentence);
        //digita o texto letra por letra
        StartCoroutine(TypeSentence(sentence));
    }
    public void EndDialog()
    {
        audioman.SoundStop("talk1");
        if (!triggerBoss)
        {
            isTyping = false;
            gameManager.onTyping = false;
            player.ispaused = false;
            controleArmas.canshot = true;
            gameManager.canCallMenu = true;
        }
        else
        {
            isTyping = false;
            bossValuesControl.startBossFight = true;
            gameManager.IntroBoss();
        }
        talking = false;
        dialogueHolder.SetActive(false);
    }
    public void UpdateUI(string sentence)
    {
        //pega os primeiros dois caracteres de cada sentença e analisa pra saber qual nome e imagem por na tela
        stringCompare = sentence.Substring(0, 1);
        intNome = int.Parse(stringCompare);
        stringCompare = sentence.Substring(1, 1);
        intFace = int.Parse(stringCompare);
        
        //poe na tela o rosto
        if (intFace!=0)
        {
            amigo.gameObject.SetActive(false);
            inimigo.gameObject.SetActive(true);
            faceImageInimigo.sprite = faceSprites[intFace];
            textNames[1].text = names[intNome];
        }
        else
        {
            inimigo.gameObject.SetActive(false);
            amigo.gameObject.SetActive(true);
            faceImageAmigo.sprite = faceSprites[intFace];
            textNames[0].text = names[intNome];
        }
        
    }
    //digita letra por letra
    IEnumerator TypeSentence(string sentence)
    {
        //remove as duas primeiras letras(que são usadas pra verificar nome e rosto de quem está flando)
        int count;
        string removeFromSentene = sentence.Remove(0, 2);
        sentence = removeFromSentene;
        count = sentence.Length-1;
        textDialogueAmigo.text = "";
        textDialogueInimigo.text = "";
        if (intFace != 0)
        {
            foreach (char letter in sentence.ToCharArray())
            {
                textDialogueInimigo.text += letter;
                count--;
                if (count <= 0)
                {
                    audioman.SoundStop("talk1");
                }
                yield return null;
            }
        }
        else
        {
            foreach (char letter in sentence.ToCharArray())
            {
                textDialogueAmigo.text += letter;
                count--;
                if (count <= 0)
                {
                    audioman.SoundStop("talk1");
                }
                yield return null;
            }
        }
        

    }

}
