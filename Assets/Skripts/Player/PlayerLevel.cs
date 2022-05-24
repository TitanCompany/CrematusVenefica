using UnityEngine;

public enum Level
{
	Nobody,
	ANovice = 100,
	StrokedACat = 200,
	SawAWitch = 400,
	AWitch = 800
}

public class PlayerLevel : MonoBehaviour
{
	public Level level;
	private Level prevLevel;
	public int experience;

	private void Start()
	{
		level = Level.Nobody;
		prevLevel = Level.Nobody;
	}

	public void AddExp(int expCount)
	{
		experience += expCount;

		if (experience >= (int)Level.ANovice && experience <= (int)Level.StrokedACat)
			level = Level.ANovice;
		if (experience >= (int)Level.StrokedACat && experience <= (int)Level.SawAWitch)
			level = Level.StrokedACat;
		if (experience >= (int)Level.SawAWitch && experience <= (int)Level.AWitch)
			level = Level.SawAWitch;
		if (experience >= (int)Level.AWitch)
			level = Level.AWitch;
	}
}
