using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HelpManager : MonoBehaviour
{
    
    public static HelpManager Instance;
    
    [SerializeField]
    private TextMeshProUGUI _helpText;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void SetHelpText(string text){
        _helpText.text = text;
    }

    public void ResetText(){
        _helpText.text = "";
    }
}
