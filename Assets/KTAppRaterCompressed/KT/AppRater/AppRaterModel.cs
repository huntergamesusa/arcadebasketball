using UnityEngine;
using System.Collections;
using SmartLocalization;

[System.Serializable]
public class AppRaterModel {

	string newTitle;

	public enum Options { 
		TwoButtons = 0, 
		ThreeButtons = 1,
	}
	
	public enum Show { 
		Yes = 0, 
		No = 1,
	}
	
	public enum Number {
		One = 0,
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten
	}
	
	[SerializeField]
	private string appid = "00000000";
	[SerializeField]
	private string reviewTitle = "Rate ProductName";
	[SerializeField]
	private string reviewMessage = "Will you mind giving a minute to rate this app?";
	[SerializeField]
	private string rateNowTitle = "Rate Now";
	[SerializeField]
	private string rateLaterTitle = "Rate Later";
	[SerializeField]
	private string neverRemindTitle = "Never Remind Me";
	[SerializeField]
	private Show shouldAlwaysShow;
	[SerializeField]
	private bool isThirdButton;
	[SerializeField]
	private Show shouldAutoShow;
	[SerializeField]
	private Number numberOfDays;
	[SerializeField]
	private Number numberOfGamePlays;
	[SerializeField]
	private Options numberOfButtons;
	
	public bool IsThirdButton {
		set {
			isThirdButton = value;
		}
		get {
			return isThirdButton;
		}
	}
	public Show ShouldAlwaysShow {
		set {
			shouldAlwaysShow = value;
		}
		get {
			return shouldAlwaysShow;
		}
	}
	public Show ShouldAutoShow {
		set {
			shouldAutoShow = value;
		}
		get {
			return shouldAutoShow;
		}
	}
	public Number NumberOfDays {
		set {
			numberOfDays = value;
		}
		get {
			return numberOfDays;
		}
	}
	public Number NumberOfGamePlays {
		set {
			numberOfGamePlays = value;
		}
		get {
			return numberOfGamePlays;
		}
	}
	public Options NumberOfButtons {
		set {
			numberOfButtons = value;
		}
		get {
			return numberOfButtons;
		}
	}
	public string Appid {
		set {
			appid = value;
		}
		get {
			return appid;
		}
	}

//	void Start () {
//		LanguageManager languageManager = LanguageManager.Instance;
//		//		if (gameObject.name == "SmartLocalization_Nate") {
//		languageManager.OnChangeLanguage += OnChangeLanguage;
//	
//		
//		
//	}
//
//	void OnChangeLanguage(LanguageManager thisLanguageManager){
//
//		newTitle = thisLanguageManager.GetTextValue("BT.freegift");
//			
//
//		
//	}

	public string ReviewTitle {
		set {
			reviewTitle = SmartLocalization_Nate.RateBuddy + " " + SmartLocalization_Nate.RateToss;
		}
		get {
			return SmartLocalization_Nate.RateBuddy + " " + SmartLocalization_Nate.RateToss;
		}
	}
	public string ReviewMessage {
		set {


			reviewMessage = SmartLocalization_Nate.rateBody;
		}
		get {
			return SmartLocalization_Nate.rateBody;
		}
	}
	public string RateNowTitle {
		set {
			rateNowTitle = SmartLocalization_Nate.rateNow;
		}
		get {
			return SmartLocalization_Nate.rateNow;
		}
	}
	public string RateLaterTitle {
		set {
			rateLaterTitle = SmartLocalization_Nate.rateLater;
		}
		get {
			return SmartLocalization_Nate.rateLater;
		}
	}
	public string NeverRemindTitle {
		set {
			neverRemindTitle = SmartLocalization_Nate.rateNever;
		}
		get {
			return SmartLocalization_Nate.rateNever;
		}
	}
}
