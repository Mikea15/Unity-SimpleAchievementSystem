using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private Text _text;

    private Animator _panelAnimator;

    private AchievementsManager _achievementManager;
    


	// Use this for initialization
	void Awake() 
    {
        _panelAnimator = GameObject.FindGameObjectWithTag("PanelAchievements").GetComponent<Animator>();

        _achievementManager = new AchievementsManager();
	}

    void Start()
    {
        _achievementManager.AchievementUnlocked += (sender, e) =>
        {
            TriggerAchievementUnlockedAnimation(e.Data);
        };
    }

    void TriggerAchievementUnlockedAnimation( Achievement data )
    {
        _text.text = data.Message;
        _panelAnimator.SetBool("ShowAchievements", true);

        Debug.Log(_text);
    }

    public void RegisterEventWrapper( )
    {
        _achievementManager.RegisterEvent(AchievementType.Score);
        Debug.Log("Registering Score event");
    }
}
