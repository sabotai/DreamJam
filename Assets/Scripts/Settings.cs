using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	private static bool showIntructions = true;
	private static int playCount = 0;

	public static bool instructions()
    {
            return showIntructions;
    }
	public static void instructions(bool value)
    {
            showIntructions = value;
    }
	

	public static int timesPlayed()
    {
            return playCount;
    }
	public static void timesPlayed(int value)
    {
            playCount = value;
    }
	
}
