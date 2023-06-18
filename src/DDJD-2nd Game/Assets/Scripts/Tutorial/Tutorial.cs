using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "TutorialInfo", menuName = "Scriptable Objects/Tutorial/Info")]
public class Tutorial : ScriptableObject
{
    [SerializeField]
    private TutorialPageInfo[] _tutorialInfo;
    public TutorialPageInfo[] TutorialInfo
    {
        get { return _tutorialInfo; }
    }
    private int _currentPage = 0;
    public int CurrentPage
    {
        get { return _currentPage; }
    }

    public void Start() { }

    public void SwitchPage() 
    {
        _currentPage++;
        if (_currentPage >= _tutorialInfo.Length)
        {
            _currentPage = 0;
        }
    }

    public TutorialPageInfo[] TutorialPageInfo
    {
        get { return _tutorialInfo; }
    }
}
