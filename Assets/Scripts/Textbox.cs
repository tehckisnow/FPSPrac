using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//TODO:
//typewriter effect
//sound effect frequence (every x characters) (OR, play at every spacebar)
//interrupt typing

//typewriter effect timing must be adjusted for <tags>

public class Textbox : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool destroy = false; //destroy after closing or keep for future use
    public float textSpeed = 0.1f;
    //text sound effect
    public AudioSource soundEffect;
    
    private Animator animator;
    private bool currentlyTyping = false;
    private string content;
    //private int contentLength; //number of visible characters that are not tags
    private int charCount = 0; //number of visible characters and chars in tags
    private int visibleCharCount = 0; //number of visible characters NOT in tags

    // Start is called before the first frame update
    void Start()
    {
        text.ForceMeshUpdate(); //!
        //text = transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();
        //soundEffect = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = TypeText(content, charCount);
    }

    public void Open(string newText)
    {
        //enable
        gameObject.SetActive(true);
        text.text = "";
        content = newText;
        //contentLength = CharactersOutsideOfTags(newText);

        //set active element
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject);
        
        //begin typing
        currentlyTyping = true;
        StartCoroutine("TypeText");
    }

    public IEnumerator TypeText()
    {
        while(currentlyTyping)
        {

            // Debug.Log(text.firstOverflowCharacterIndex);
            // Debug.Log(text.textInfo.characterCount);
            // Debug.Log("page info " + text.textInfo.pageInfo);
            // Debug.Log(text.textInfo.lineCount);
            //Debug.Log("page number: " + text.textInfo.characterInfo[charCount].pageNumber);
            //int currentPage = text.pageToDisplay;
            //Debug.Log(text.textInfo.pageInfo[currentPage].lastCharacterIndex);
            // Debug.Log("Page Count: " + text.textInfo.pageCount);
            // Debug.Log("CurrentPage: " + text.pageToDisplay);


            text.text = InsertTag(content, charCount);
            //! update charcount in InsertTag instead?
            charCount++;

            //!
            text.ForceMeshUpdate(); //!
            int currentPageLength = text.textInfo.pageInfo[text.pageToDisplay].lastCharacterIndex;
            //Debug.Log("current page: " + text.pageToDisplay);
            Debug.Log("last char index: " + currentPageLength);

            if(charCount > currentPageLength) //!
            {
                currentlyTyping = false;
                break;
            }
            soundEffect.Play();
            //Debug.Log(text.textInfo.characterCount);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void Close()
    {
        //clear text
        text.text = "";
        charCount = 0;
        text.pageToDisplay = 1;
        //close animation
        animator.Play("Textbox_Close");
    }
    
    public void Advance()
    {
        if(currentlyTyping)
        {
            InterruptTyping();
        }
        else
        {
            text.pageToDisplay += 1;
            currentlyTyping = true;
            //if last page
            text.ForceMeshUpdate();
            if(text.pageToDisplay == text.textInfo.pageCount + 1)
            {
                Close();
            }
        }
    }

    private void Kill()
    {
        if(destroy)
        {
            Destroy(gameObject);
        }
        else
        {
        gameObject.SetActive(false);
        }
    }

    public void InterruptTyping()
    {
        currentlyTyping = false;
        text.text = content;

        //int currentPageLength = text.textInfo.pageInfo[text.pageToDisplay].lastCharacterIndex;
        //!set charCount?
    }

    public static string InsertTag(string text, int visibleLetters)
    {
      string invisTag = "<alpha=#00>";
      string visible = "";
      string tag = "";
      int tagChars = 0;
      string invisible = "";
      bool inTag = false;
      
      int i;
      for(i = 0; i < text.Length; i++)
      {
        if(inTag)
        {
            tag += text[i];
            tagChars++;
            if(text[i] == '>')
            {
                visible += tag;
                tag = "";
                inTag = false;
            }
        }
        else if(text[i] == '<')
        {
            inTag = true;
            tag += text[i];
            tagChars++;
        }
        else if(i < visibleLetters) //! visible letters + tagchars
        {
            visible += text[i];
        }
        else
        {
            invisible += text[i];
        }
      }

      //string result = text.Substring(0, i) + invisTag + new string(' ', (visibleLetters) - visibleCount);
      string result = visible + invisTag + invisible;
      //Debug.Log(result);
      //Debug.Log(currentlyTyping);
      return result;
    }


}
