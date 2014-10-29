using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Achievement
{
    public int countToUnlock { get; set; }
    public bool isUnlocked { get; set; }
    public string Message { get; set; }
}

public enum AchievementType
{
    Tap,
    Score,
    Die,
    Start
};

public class AchievementEventArg : EventArgs
{
    public Achievement Data;
    public AchievementEventArg(Achievement e)
    {
        Data = e;
    }
}

public class AchievementsManager
{
    private Dictionary<AchievementType, List<Achievement>> _achievements;
    private Dictionary<AchievementType, int> _achievementKeeper;

    public event EventHandler<AchievementEventArg> AchievementUnlocked;
    protected virtual void RaiseAchievementUnlocked(Achievement ach)
    {
        var del = AchievementUnlocked as EventHandler<AchievementEventArg>;
        if (del != null)
        {
            del(this, new AchievementEventArg(ach));
        }
    }

    public AchievementsManager()
    {
        _achievementKeeper = new Dictionary<AchievementType, int>();
        // ideally, load here previous, saved values.
        // tap = 0
        // die = 1
        // start = 12
        // score = 1231

        _achievements = new Dictionary<AchievementType, List<Achievement>>();

        var listStart = new List<Achievement>();
        listStart.Add(new Achievement() { countToUnlock = 3, isUnlocked = false, Message = "First Time Playing!" });
        listStart.Add(new Achievement() { countToUnlock = 8, isUnlocked = false, Message = "Fifth Time is the Charm?" });
        listStart.Add(new Achievement() { countToUnlock = 10, isUnlocked = false, Message = "Hello and Welcome Back!" });
        listStart.Add(new Achievement() { countToUnlock = 16, isUnlocked = false, Message = "Tapping Time!!" });
        listStart.Add(new Achievement() { countToUnlock = 20, isUnlocked = false, Message = "Perseverance Lvl 1!" });

        _achievements.Add(AchievementType.Score, listStart);

        _achievementKeeper.Add(AchievementType.Score, 0);
    }

    public void RegisterEvent(AchievementType type)
    {
        if (!_achievementKeeper.ContainsKey(type))
            return;

        _achievementKeeper[type]++;
        
        ParseAchievements(type);
    }

    public void ParseAchievements(AchievementType type)
    {
        foreach (var kvp in _achievements.Where(a => a.Key == type))
        {
            foreach (var ach in kvp.Value.Where(a => a.isUnlocked == false))
            {
                if (type == AchievementType.Score)
                {
                    if (_achievementKeeper[type] >= ach.countToUnlock)
                    {
                        ach.isUnlocked = true;
                        RaiseAchievementUnlocked(ach);
                    }
                }
                else if (_achievementKeeper[type] == ach.countToUnlock)
                {
                    RaiseAchievementUnlocked(ach);
                }
            }
        }
    }
}