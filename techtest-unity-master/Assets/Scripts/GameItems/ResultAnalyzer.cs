using UnityEngine;
using System.Collections;

public enum Result
{
	Won,
	Lost,
	Draw
}

public class ResultAnalyzer
{	
	/// <summary>
	/// Streamlined process
	/// </summary>
	/// <param name="firstHand"></param>
	/// <param name="secondHand"></param>
	/// <returns>player hand win/lose/draw against second hand</returns>
	public static Result GetResultState(ThrowableScriptable playerHand, ThrowableScriptable enemyHand)
	{
		return CalculateResult(playerHand, enemyHand);
	}

	/// <summary>
	/// Renamed and reformatted
	/// </summary>
	/// <param name="firstHand"></param>
	/// <param name="secondHand"></param>
	/// <returns>first hand win/lose/draw against second hand</returns>
	private static Result CalculateResult(ThrowableScriptable firstHand, ThrowableScriptable secondHand)
	{
		return firstHand.CalculateResult(secondHand);
	}
}