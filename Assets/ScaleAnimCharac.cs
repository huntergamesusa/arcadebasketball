using UnityEngine;
//using UnionAssets.FLE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SmartLocalization;
using System.IO;
using ChartboostSDK;
using Fabric.Answers;
using UnityEngine.Purchasing;
public class ScaleAnimCharac : MonoBehaviour, IStoreListener {
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;

	public Mesh customHead;
	public Mesh normalHead;

	public Material faceMaterial;
	public Texture2D faceTexture;
	private byte[] faceBytes;

	public Material[] normalMaterials;
	public Material[] faceMaterials;
	public GameObject faceButton;

	public GameObject BottomSheet;



	private bool isInMenu;

	private bool isPurchasingGameOver;
	private bool isPurchasingGameOverHats;
	private bool isPurchasingGameOverHands;
	private bool isPurchasingGameOverFeet;

	private bool movingRight;
	private bool moveRightTrigger;
	private bool movingLeft;
	private bool moveLeftTrigger;

	public GameObject TNTUnlockYESNO;
	public GameObject TNTUnlockYESNODIALOG;
	public GameObject previewBuddy;
	public GameObject PongUnlockYESNO;
	public GameObject PongUnlockYESNODIALOG;

	public GameObject coinCamera;
	public Text creditsUI;
	private bool adBuyBool = false;
	private bool TntLevelBool = false;
	private bool PongLevelBool = false;
	public GameObject removeAdsBttn;

	private static bool IsInitialized = false;
	private static bool IsStoreLoaded = false;
	public Material capeMaterial;

	public Text [] priceBtns;

	public Text characterCountDisplay;
	public Text handCountDisplay;
	public Text hatCountDisplay;
	public Text shoeCountDisplay;

	public Material mainMaterial;
	public Texture [] characterTexture;

	//	public static Texture [] characterTexturePassthrough;
	public string[] characterKeyStrings;
	public string [] characterName;

	public string[] handKeyStrings;
	public string[] shoeKeyStrings;
	public string[] hatKeyStrings;


	public string [] purchaseString;

	public string [] purchaseHandString;

	public string [] purchaseHatString;
	//should be shoe
	public string [] purchaseFeetString;

	public string[] handName;
	public string[] hatName;
	public string[] feetName;

	public Text characterNameTxt;
	Material characterMat;
	public string [] characters;
	static int charact;
	public int hand;
	private int tempHand;
	private int tempShoe;
	private int tempHat;

	static int hat;
	public int shoe;

	private int selectedCharacter;
	private int props;
	private Vector3 startScale;
	public Button characterButton;
	public Button handButton;
	public Button hatButton;
	public Button shoeButton;

	private GameObject mainHandPropGO;

	public GameObject purchaseCharacterButton;
	//	public GameObject puchaseCharacterMain;
	public GameObject purchaseHandMain;
	public GameObject purchaseShoeMain;
	public GameObject purchaseHatMain;
	//	public GameObject puchaseFeetMain;

	public GameObject SelectButton;
	public Image SelectCheck;

	public GameObject HandSelectButton;
	public Image HandSelectCheck;
	public GameObject HatSelectButton;
	public Image HatSelectCheck;
	public GameObject ShoeSelectButton;
	public Image ShoeSelectCheck;


	private int totalCredits;
	private Text credits;

	private GameObject mainCharacterBody;
	private GameObject mainCharacterHead;
	private GameObject mainCharacterLowerBody;
	private GameObject mainCharacterUpperArm_Left;
	private GameObject mainCharacterUpperArm_Right;
	private GameObject mainCharacterLowerArm_Left;
	private GameObject mainCharacterLowerArm_Right;
	private GameObject mainCharacterHand_Left;
	private GameObject mainCharacterHand_Right;
	private GameObject mainCharacterUpperLeg_Left;
	private GameObject mainCharacterUpperLeg_Right;
	private GameObject mainCharacterLowerLeg_Right;
	private GameObject mainCharacterLowerLeg_Left;
	private GameObject mainCharacterFoot_Left;
	private GameObject mainCharacterFoot_Right;
	private GameObject mainCharacter_Hat_Parent;

	private GameObject rightHand;
	private GameObject leftHand;
	private GameObject rightShoe;
	private GameObject leftShoe;
	private GameObject purchasableHat;

	public GameObject mainCharacter_Hat;

	public GameObject blockScreen;

	private int tempPurchased;
	private int tempPurchasedHand;
	private int tempPurchasedShoe;
	private int tempPurchasedHat;



	GameObject mainCharacterRoot;

	//	private bool currentlyInStoreLoc = false;
	private bool isLookingforConnection = false;
	private bool retryLoadStoreBoolMain = false;
	public GameObject BabyPlatform;
	//	public Material galacticMaterial;

	public GameObject TNTLevelLockedBttn;
	public GameObject TNTLevelBttn;

	public GameObject PongLevelLockedBttn;
	public GameObject PongLevelBttn;

	public GameObject PongLevelCanvas;
	public GameObject TNTLevelCanvas;
	public GameObject buttonBlocker;
	private bool isPurchasing = false;

	//	public Button [] allLeftArrows;
	//	public Button [] allRightArrows;

	public GameObject buttonBlockerMainCharacterScreen;

	//localized Strings
	private string noteEnoughCredits;
	private string restoreCompleteString;
	private string sorryNotEnoughString;
	private string successString;
	private string areyouSureTNT;
	private string areyouSurePong;
	private string transactionFailedString;
	private string notEnoughString;
	private string errorString;
	private string unlockTNTString;
	private string unlockPongString;

	// Use this for initialization
	void Start () {
		//		currentlyInStoreLoc=false;
		retryLoadStoreBoolMain =false;
		isLookingforConnection =false;

		totalCredits = PlayerPrefs.GetInt("myCredits");
		//		clampLastObj = transform.childCount - 1;
		mainCharacterRoot = GameObject.Find ("Player");
		mainCharacterBody = GameObject.Find ("Body");
		mainCharacterHead= GameObject.Find ("Head");
		mainCharacterLowerBody= GameObject.Find ("LowerBody");
		mainCharacterUpperArm_Left= GameObject.Find ("UpperArm_Left");
		mainCharacterUpperArm_Right= GameObject.Find ("UpperArm_Right");
		mainCharacterLowerArm_Left= GameObject.Find ("LowerArm_Left");
		mainCharacterLowerArm_Right= GameObject.Find ("LowerArm_Right");
		mainCharacterHand_Left= GameObject.Find ("Hand_Left");
		mainCharacterHand_Right= GameObject.Find ("Hand_Right");
		mainCharacterUpperLeg_Left= GameObject.Find ("UpperLeg_Left");
		mainCharacterUpperLeg_Right= GameObject.Find ("UpperLeg_Right");
		mainCharacterLowerLeg_Right= GameObject.Find ("LowerLeg_Right");
		mainCharacterLowerLeg_Left= GameObject.Find ("LowerLeg_Left");
		mainCharacterFoot_Left= GameObject.Find ("Foot_Left");
		mainCharacterFoot_Right= GameObject.Find ("Foot_Right");
		mainCharacter_Hat_Parent= GameObject.Find ("Hat_jnt");
		rightHand = GameObject.Find ("Right_Hand_Acc");
		leftHand = GameObject.Find ("Hand_Left");

		leftShoe = GameObject.Find ("Shoes_Left");
		rightShoe = GameObject.Find ("Shoes_Right");
		purchasableHat = GameObject.Find("Purchasable_Hats");

		mainMaterial.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterBody.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterHead.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterLowerBody.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterUpperArm_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterUpperArm_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterLowerArm_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterLowerArm_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterHand_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterHand_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterUpperLeg_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterUpperLeg_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterLowerLeg_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterLowerLeg_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterFoot_Left.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];
		mainCharacterFoot_Right.GetComponent<Renderer>().material.mainTexture = characterTexture[PlayerPrefs.GetInt("Character")];

		//START HAND
		//		PlayerPrefs.SetInt("Hand",-1);


		//FIRST 2 PLAYERS IS ALREADY PURCHASED
		PlayerPrefs.SetInt("0",1);
		PlayerPrefs.SetInt("1",1);
		PlayerPrefs.SetInt("hat0",1);

		startScale = mainCharacterRoot.transform.localScale;

		//LOOK FOR HAT LIVES IN SWITCHCAMERA SCRIP
		checkHatStored();
		checkRequiredObjectsStored();
		//NOW CHECK FOR HAND STUFF
		mainHandPropGO  = GameObject.Find ("Right_Hand_Acc");

		if (PlayerPrefs.GetInt ("Ads") == 1) {
			removeAdsBttn.transform.GetChild (1).GetComponent<Button> ().enabled = false;
			removeAdsBttn.transform.GetChild (1).GetComponent<Image> ().color = Color.gray;
			removeAdsBttn.transform.GetChild (1).GetChild (0).GetComponent<Text> ().color = Color.gray;
			removeAdsBttn.transform.GetChild (1).gameObject.GetComponent<adjustTextBtnC>().enabled =false;
		}


	}



	// Update is called once per frame
	void Awake () {
		//NEED TO PASS THROUGH TO RANDOMITEMGIVEAWAY TEXTURES FOR CHARACTERS
		//		characterTexturePassthrough =characterTexture;
		GameObject freeItem = GameObject.Find("FreeItemCam");
		freeItem.SendMessage("receiveTextures",characterTexture);
		//NEED TO PASS THROUGH TO RANDOMITEMGIVEAWAY strings FOR props/characters

		freeItem.SendMessage("receiveCharacNameStrings",characterName);
		freeItem.SendMessage("receiveFeetNameStrings",feetName);
		freeItem.SendMessage("receiveHatNameStrings",hatName);
		freeItem.SendMessage("receiveHandNameStrings",handName);

		//		if(PlayerPrefs.HasKey ("Character")==false){
		//			PlayerPrefs.SetInt("Character",0);
		//
		//		}


		if (File.Exists(Application.persistentDataPath + "/myFaceBT.png"))     {
			faceBytes = File.ReadAllBytes(Application.persistentDataPath + "/myFaceBT.png");


			faceTexture.LoadImage(faceBytes);
			faceMaterial.mainTexture = faceTexture;


		}




		previewBuddy.SendMessage ("receiveTextures", characterTexture);

		if(PlayerPrefs.HasKey("Shoe")==false){
			PlayerPrefs.SetInt("Shoe",-1);

		}
		if(PlayerPrefs.HasKey("Hand")==false){
			PlayerPrefs.SetInt("Hand",-1);

		}
		if(PlayerPrefs.HasKey("Hat")==false){
			PlayerPrefs.SetInt("Hat",-1);

		}


		checkLevelsPurchased ();

		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();

		} 


		LanguageManager languageManager = LanguageManager.Instance;
		languageManager.OnChangeLanguage += OnChangeLanguage;


	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitializedIAP())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier
		// with its store-specific identifiers.
		// Continue adding the non-consumable product.
		//		builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

		builder.AddProduct("removeads", ProductType.NonConsumable);
		builder.AddProduct("tntmode",ProductType.NonConsumable);
		builder.AddProduct("buddypong",ProductType.NonConsumable);


		//load characters IAP
		for(int l = 2; l < purchaseString.Length; l++){

			builder.AddProduct(purchaseString[l],ProductType.NonConsumable);

		}

		//load HAND IAP
		for(int i = 0; i < purchaseHandString.Length; i++){

			builder.AddProduct(purchaseHandString[i],ProductType.NonConsumable);

		}
		//load HAT IAP
		for(int j = 0; j < purchaseHatString.Length; j++){

			builder.AddProduct(purchaseHatString[j],ProductType.NonConsumable);

		}
		//load FEET IAP
		for(int k = 0; k < purchaseFeetString.Length; k++){

			builder.AddProduct(purchaseFeetString[k],ProductType.NonConsumable);

		}

		// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
		// if the Product ID was configured differently between Apple and Google stores. Also note that
		// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
		// must only be referenced here. 


		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);

	}

	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitializedIAP())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				MobileNativeMessage msg = 	new MobileNativeMessage("Restore purchases","Completed");



				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{

		//		// Or ... a non-consumable product has been purchased by this user.
		//		if (String.Equals (args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal)) {
		//
		//
		//		}


		print ("started processing purchase unity for: " + args.purchasedProduct.definition.storeSpecificId);

		Chartboost.trackInAppAppleStorePurchaseEvent("",
			args.purchasedProduct.metadata.localizedTitle, 
			args.purchasedProduct.metadata.localizedDescription, 
			args.purchasedProduct.metadata.localizedPrice.ToString(), 
			args.purchasedProduct.metadata.isoCurrencyCode,"");

		Answers.LogPurchase (
			args.purchasedProduct.metadata.localizedPrice,
			args.purchasedProduct.metadata.isoCurrencyCode,
			true,
			args.purchasedProduct.metadata.localizedTitle,
			 "nonconsum",
			args.purchasedProduct.transactionID,
			null
		);

		for (int i = 0; i < purchaseString.Length; i++) {

			if ( args.purchasedProduct.definition.storeSpecificId== purchaseString [i]) {
				PlayerPrefs.SetInt (i.ToString ("f0"), 1);

				//this is to enable gameover guy that is purchased
				if (isPurchasingGameOver) {
					isPurchasingGameOver = false;
					PlayerPrefs.SetInt (i.ToString ("f0"), 1);
					PlayerPrefs.SetInt ("Character", i);
					turnOffBuyGuyCam ();
					resetScriptStart ();

				}

				if (i == 64) {
					if (PlayerPrefs.HasKey ("Restored")) {

					} else {
						//GIVE 1000 FOR GALACTIC ON RESTORE
						PlayerPrefs.SetInt ("Restored", 1);
						PlayerPrefs.SetInt ("myCredits", PlayerPrefs.GetInt ("myCredits") + 1000);
						//				GameObject credits = GameObject.Find("Credits UI");
						creditsUI.text = PlayerPrefs.GetInt ("myCredits").ToString ("f0");
						StartCoroutine (coinShower ());
					}

				}


			}


		}

		for (int j = 0; j < purchaseHandString.Length; j++) {

			if ( args.purchasedProduct.definition.storeSpecificId== purchaseHandString [j]) {
				PlayerPrefs.SetInt ("hand" + j, 1);
				if (isPurchasingGameOverHands) {
					isPurchasingGameOverHands = false;
					PlayerPrefs.SetInt ("Hand", j);
					turnOffBuyGuyCam ();

					resetScriptStart ();


				}

			}
		}

		for (int k = 0; k < purchaseFeetString.Length; k++) {

			if ( args.purchasedProduct.definition.storeSpecificId== purchaseFeetString [k]) {
				PlayerPrefs.SetInt ("shoe" + k, 1);
				if (isPurchasingGameOverFeet) {
					isPurchasingGameOverFeet = false;
					PlayerPrefs.SetInt ("Shoe", k);
					turnOffBuyGuyCam ();

					resetScriptStart ();

				}
			}
		}

		for (int l = 0; l < purchaseHatString.Length; l++) {

			if ( args.purchasedProduct.definition.storeSpecificId== purchaseHatString [l]) {
				PlayerPrefs.SetInt ("hat" + l, 1);
				if (isPurchasingGameOverHats) {
					isPurchasingGameOverHats = false;
					PlayerPrefs.SetInt ("Hat", l);
					turnOffBuyGuyCam ();

					resetScriptStart ();

				}

			}
		}

		if ( args.purchasedProduct.definition.storeSpecificId == "removeads") {
			PlayerPrefs.SetInt ("Ads", 1);
			removeAdsBttn.transform.GetChild (1).GetComponent<Button> ().enabled = false;
			removeAdsBttn.transform.GetChild (1).GetComponent<Image> ().color = Color.gray;
			removeAdsBttn.transform.GetChild (1).GetChild (0).GetComponent<Text> ().color = Color.gray;
			removeAdsBttn.transform.GetChild (1).gameObject.GetComponent<adjustTextBtnC> ().enabled = false;


		}
		if ( args.purchasedProduct.definition.storeSpecificId == "tntmode") {
			print ("tiititoitjjoit");
			PlayerPrefs.SetInt ("TNTLevel", 1);
			checkLevelsPurchased ();
		}
		if ( args.purchasedProduct.definition.storeSpecificId == "buddypong") {
			PlayerPrefs.SetInt ("PongLevel", 1);
			checkLevelsPurchased ();
		}


		//MAKES SURE WE ARE IN THE MAIN MENU WHEN BUYING THESE CHARACTERS
		if (isInMenu) {


			if (characterButton.interactable == false) {
				UnlockProducts ( args.purchasedProduct.definition.storeSpecificId, tempPurchased);
				print ("I JUST BOUGHT A  CHARACTER IN THE IAP SCRIPT");

			}
			if (handButton.interactable == false) {
				UnlockProductsHand ( args.purchasedProduct.definition.storeSpecificId, tempPurchasedHand);

			}
			if (shoeButton.interactable == false) {
				UnlockProductsHand ( args.purchasedProduct.definition.storeSpecificId, tempPurchasedShoe);
			}
			if (hatButton.interactable == false) {
				UnlockProductsHat ( args.purchasedProduct.definition.storeSpecificId, tempPurchasedHat);
			}

		}

		if (adBuyBool) {
			UnlockProductsAd (args.purchasedProduct.definition.storeSpecificId);
		}
		if (TntLevelBool) {
			UnlockProductsTntLevel (args.purchasedProduct.definition.storeSpecificId);
		}
		if (PongLevelBool) {
			UnlockProductsPongLevel (args.purchasedProduct.definition.storeSpecificId);
		}




		//reenables allbuttons to be used after purchasing is complete
		isPurchasing = false;
		toggleCharacterBttns (isPurchasing);


		if (args.purchasedProduct.hasReceipt) {
			turnoffPurchaseBtn ();
			turnonSelectBtn ();
		}
		//			blockScreen.SetActive(false);




		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + failureReason.ToString());

		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		//		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

	private bool IsInitializedIAP()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	void BuyProductID(string productId)
	{
		// If Purchasing has been initialized ...
		if (IsInitializedIAP())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID(productId);

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			// Otherwise ...
			else
			{
				// ... report the product look-up failure situation  
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase"+": "+productId);

			}
		}
		// Otherwise ...
		else
		{
			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
			// retrying initiailization.
			Debug.Log("BuyProductID FAIL. Not initialized.");

		}
	}


	void OnDestroy(){
		if (LanguageManager.HasInstance)
			LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
	}

	void OnChangeLanguage(LanguageManager thisLanguageManager){
		for(int i = 0; i < characterKeyStrings.Length; i++){
			//changes list of character name strings to right languages
			characterName[i] = thisLanguageManager.GetTextValue(characterKeyStrings[i]);

		}
		for(int j = 0; j < hatKeyStrings.Length; j++){
			//changes list of character name strings to right languages
			hatName[j] = thisLanguageManager.GetTextValue(hatKeyStrings[j]);

		}
		for(int k = 0; k < shoeKeyStrings.Length; k++){
			//changes list of character name strings to right languages
			feetName[k] = thisLanguageManager.GetTextValue(shoeKeyStrings[k]);

		}
		for(int l = 0; l < handKeyStrings.Length; l++){
			//changes list of character name strings to right languages
			handName[l] = thisLanguageManager.GetTextValue(handKeyStrings[l]);

		}
		noteEnoughCredits = thisLanguageManager.GetTextValue ("BT.notenoughcredits");
		restoreCompleteString = thisLanguageManager.GetTextValue ("BT.restorecomplete");
		sorryNotEnoughString = thisLanguageManager.GetTextValue ("BT.sorrynotenough");
		successString = thisLanguageManager.GetTextValue ("BT.success");

		areyouSureTNT = thisLanguageManager.GetTextValue ("BT.TNTcreditsask");
		areyouSurePong = thisLanguageManager.GetTextValue ("BT.BPcreditask");

		transactionFailedString = thisLanguageManager.GetTextValue ("BT.transactionfailed");
		notEnoughString = thisLanguageManager.GetTextValue ("BT.notenoughcredits");
		errorString = thisLanguageManager.GetTextValue ("BT.error");
		unlockTNTString = thisLanguageManager.GetTextValue ("BT.unlocktntlevel");
		unlockPongString = thisLanguageManager.GetTextValue ("BT.unlockbuddypong");






	}

	public void LoadStore() {


		UM_InAppPurchaseManager.Client.Connect();
	}

	public void funcRetryLoadStore(){
		//		currentlyInStoreLoc = true;
		retryLoadStoreBoolMain = true;
		//		StartCoroutine(retryLoadStore());
	}
	public void funcRetryLoadStoreStop(){
		//		currentlyInStoreLoc = false;
		retryLoadStoreBoolMain = false;
		//		StartCoroutine(Loop());
	}


	IEnumerator retryLoadStore()
	{
		//		yield return new WaitForSeconds( 1.0f );
		//		print ("startedreload");
		if(UM_InAppPurchaseManager.Client.IsConnected == false){
			if(!isLookingforConnection){
				isLookingforConnection =true;
				print ("looking for connection");
				//			MoveSomewhere( );
				//			print ("startedreload");
				LoadStore();
				yield return new WaitForSeconds( 3.0f );
				isLookingforConnection =false;
			}
		}




	}

	void checkLevelsPurchased(){
		//CHECK TO SEE IF LEVELS HAVE BEEN PURCHASED
		if (PlayerPrefs.HasKey ("TNTLevel") == false) {
			TNTLevelLockedBttn.SetActive(true);
			TNTLevelBttn.SetActive(false);
		}
		if (PlayerPrefs.HasKey ("PongLevel") == false) {

			PongLevelLockedBttn.SetActive(true);
			PongLevelBttn.SetActive(false);
		}

		if (PlayerPrefs.GetInt ("TNTLevel") > 0) {
			TNTLevelLockedBttn.SetActive(false);
			TNTLevelBttn.SetActive(true);
		}
		if (PlayerPrefs.GetInt ("PongLevel") > 0) {
			PongLevelLockedBttn.SetActive(false);
			PongLevelBttn.SetActive(true);
		}
		PongLevelCanvas.SetActive(false);
		TNTLevelCanvas.SetActive(false);
		buttonBlocker.SetActive (false);
	}

	void Update(){
		//		print(tempPurchasedHand);
		if(retryLoadStoreBoolMain){
			StartCoroutine(retryLoadStore());
		}

		//		if(Input.GetKeyUp(KeyCode.A)){
		//			retryLoadStoreBoolMain = true;
		//			StartCoroutine(retryLoadStore());
		//		}

		if (movingRight) {
			StartCoroutine(engageMoveRight());
		}
		if (movingLeft) {
			StartCoroutine(engageMoveLeft());
		}


	}

	public void pointerDownMoveRight(bool rightMe){
		movingRight = rightMe;
		if (!rightMe) {
			moveRightTrigger =false;
		}
	}

	IEnumerator engageMoveRight(){
		if (!moveRightTrigger) {
			moveRightTrigger = true;
			MoveRight ();
			yield return new WaitForSeconds (.3f);
			moveRightTrigger =false;

		}

	}

	public void pointerDownMoveLeft(bool leftMe){
		movingLeft = leftMe;
		if (!leftMe) {
			moveLeftTrigger =false;
		}
	}

	IEnumerator engageMoveLeft(){
		if (!moveLeftTrigger) {
			moveLeftTrigger = true;
			MoveLeft ();
			yield return new WaitForSeconds (.3f);
			moveLeftTrigger =false;

		}

	}


	public void updateLocalizedPricing(string purch){



		if(UM_InAppPurchaseManager.Client.IsConnected){
			priceBtns[0].text =  UM_InAppPurchaseManager.GetProductById(purch).LocalizedPrice;
			priceBtns[1].text =  UM_InAppPurchaseManager.GetProductById(purch).LocalizedPrice;
			priceBtns[2].text =  UM_InAppPurchaseManager.GetProductById(purch).LocalizedPrice;
			priceBtns[3].text =  UM_InAppPurchaseManager.GetProductById(purch).LocalizedPrice;

			//			if (characterTexture [charact].name == "SuperGoldBuddy") {
			//				priceBtns[0].text =  IOSInAppPurchaseManager.Instance.GetProductById("supergalacticbuddy").LocalizedPrice;
			//
			//			}
			//			else{
			//				priceBtns[0].text =  IOSInAppPurchaseManager.Instance.GetProductById("thunderbuddy").LocalizedPrice;
			//
			//			}

			priceBtns[4].text = UM_InAppPurchaseManager.GetProductById("removeads").LocalizedPrice;
			priceBtns[5].text = UM_InAppPurchaseManager.GetProductById("tntmode").LocalizedPrice;
			priceBtns[6].text = UM_InAppPurchaseManager.GetProductById("buddypong").LocalizedPrice;


		}

		else{
			priceBtns[0].text = "$0.99";
			priceBtns[1].text = "$0.99";
			priceBtns[2].text = "$0.99";
			priceBtns[3].text = "$0.99";
			priceBtns[4].text = "$1.99";
			priceBtns[5].text = "$1.99";
			priceBtns[6].text = "$1.99";
			if (characterTexture [charact].name == "SuperGoldBuddy") {
				priceBtns[0].text = "$3.99";

			}


			//			priceBtns[1].text = "$4.99";
			//			priceBtns[2].text = "$9.99";
			//			priceBtns[3].text = "$19.99";

		}

	}


	public void checkCharacterPurchasedonOpen(){


		if(charact!=PlayerPrefs.GetInt("Character")){
			SelectCheck.enabled = false;
		}
		else{
			SelectCheck.enabled = true;
		}

		if(PlayerPrefs.GetInt(charact.ToString("f0"))<1){
			turnonPurchaseBtn();
			turnoffSelectBtn();

		}
		else{
			turnoffPurchaseBtn();
			turnonSelectBtn();

		}

		if(hand!=PlayerPrefs.GetInt("Hand")){
			HandSelectCheck.enabled = false;
		}
		else{
			HandSelectCheck.enabled = true;
		}

		if(	PlayerPrefs.GetInt("hand"+hand.ToString("f0"))<1)
		{
			turnonPurchaseBtn();
			turnoffSelectBtn();
		}
		else{
			turnoffPurchaseBtn();
			turnonSelectBtn();

		}


		if(shoe!=PlayerPrefs.GetInt("Shoe")){
			ShoeSelectCheck.enabled = false;
		}
		else{
			ShoeSelectCheck.enabled = true;
		}

		if(	PlayerPrefs.GetInt("shoe"+shoe.ToString("f0"))<1)
		{
			turnonPurchaseBtn();
			turnoffSelectBtn();
		}
		else{
			turnoffPurchaseBtn();
			turnonSelectBtn();

		}


		if(shoe!=PlayerPrefs.GetInt("Hat")){
			HatSelectCheck.enabled = false;
		}
		else{
			HatSelectCheck.enabled = true;
		}

		if(	PlayerPrefs.GetInt("hat"+hat.ToString("f0"))<1)
		{
			turnonPurchaseBtn();
			turnoffSelectBtn();
		}
		else{
			turnoffPurchaseBtn();
			turnonSelectBtn();

		}



	}

	void turnonPurchaseBtn(){
		//MAIN CHARACTER
		if(characterButton.interactable == false){
			purchaseCharacterButton.GetComponent<adjustTextBtnC>().enabled = true;
			purchaseCharacterButton.GetComponent<Button>().enabled = true;
			purchaseCharacterButton.GetComponent<Image>().enabled = true;
			for(int i = 0; i < purchaseCharacterButton.transform.childCount; i++)
			{
				purchaseCharacterButton.transform.GetChild (i).gameObject.SetActive(true);
			}
		}
		//HAND

		purchaseHandMain.GetComponent<adjustTextBtnC>().enabled = true;
		purchaseHandMain.GetComponent<Button>().enabled = true;
		purchaseHandMain.GetComponent<Image>().enabled = true;
		for(int i = 0; i < purchaseHandMain.transform.childCount; i++)
		{
			purchaseHandMain.transform.GetChild (i).gameObject.SetActive(true);
		}

		//SHOE

		purchaseShoeMain.GetComponent<adjustTextBtnC>().enabled = true;
		purchaseShoeMain.GetComponent<Button>().enabled = true;
		purchaseShoeMain.GetComponent<Image>().enabled = true;
		for(int i = 0; i < purchaseShoeMain.transform.childCount; i++)
		{
			purchaseShoeMain.transform.GetChild (i).gameObject.SetActive(true);
		}

		//HAT

		purchaseHatMain.GetComponent<adjustTextBtnC>().enabled = true;
		purchaseHatMain.GetComponent<Button>().enabled = true;
		purchaseHatMain.GetComponent<Image>().enabled = true;
		for(int i = 0; i < purchaseHatMain.transform.childCount; i++)
		{
			purchaseHatMain.transform.GetChild (i).gameObject.SetActive(true);
		}

	}
	void turnoffPurchaseBtn(){
		//MAIN CHARACTER
		if(characterButton.interactable == false){
			purchaseCharacterButton.GetComponent<adjustTextBtnC>().enabled = false;
			purchaseCharacterButton.GetComponent<Button>().enabled = false;
			purchaseCharacterButton.GetComponent<Image>().enabled = false;

			for(int i = 0; i < purchaseCharacterButton.transform.childCount; i++)
			{
				purchaseCharacterButton.transform.GetChild (i).gameObject.SetActive(false);
			}

			//HAND
		}
		if(handButton.interactable == false){
			purchaseHandMain.GetComponent<adjustTextBtnC>().enabled = false;
			purchaseHandMain.GetComponent<Button>().enabled = false;
			purchaseHandMain.GetComponent<Image>().enabled = false;

			for(int i = 0; i < purchaseHandMain.transform.childCount; i++)
			{
				purchaseHandMain.transform.GetChild (i).gameObject.SetActive(false);
			}
		}

		if(shoeButton.interactable == false){
			purchaseShoeMain.GetComponent<adjustTextBtnC>().enabled = false;
			purchaseShoeMain.GetComponent<Button>().enabled = false;
			purchaseShoeMain.GetComponent<Image>().enabled = false;

			for(int i = 0; i < purchaseShoeMain.transform.childCount; i++)
			{
				purchaseShoeMain.transform.GetChild (i).gameObject.SetActive(false);
			}
		}

		if(hatButton.interactable == false){
			purchaseHatMain.GetComponent<adjustTextBtnC>().enabled = false;
			purchaseHatMain.GetComponent<Button>().enabled = false;
			purchaseHatMain.GetComponent<Image>().enabled = false;
			for(int i = 0; i < purchaseHatMain.transform.childCount; i++)
			{
				purchaseHatMain.transform.GetChild (i).gameObject.SetActive(false);
			}
		}
	}




	void turnoffSelectBtn(){
		//MAIN CHARACTER
		if(characterButton.interactable == false){
			SelectButton.GetComponent<adjustTextBtnC>().enabled = false;
			SelectButton.GetComponent<Button>().enabled = false;
			SelectButton.GetComponent<Image>().enabled = false;
			for(int i = 0; i < SelectButton.transform.childCount; i++)
			{
				SelectButton.transform.GetChild (i).gameObject.SetActive(false);
			}
		}
		//HAND


		if(handButton.interactable == false){
			HandSelectButton.GetComponent<adjustTextBtnC>().enabled = false;
			HandSelectButton.GetComponent<Button>().enabled = false;
			HandSelectButton.GetComponent<Image>().enabled = false;
			for(int i = 0; i < HandSelectButton.transform.childCount; i++)
			{
				HandSelectButton.transform.GetChild (i).gameObject.SetActive(false);
			}
		}

		//HAND


		if(shoeButton.interactable == false){
			ShoeSelectButton.GetComponent<adjustTextBtnC>().enabled = false;
			ShoeSelectButton.GetComponent<Button>().enabled = false;
			ShoeSelectButton.GetComponent<Image>().enabled = false;
			for(int i = 0; i < ShoeSelectButton.transform.childCount; i++)
			{
				ShoeSelectButton.transform.GetChild (i).gameObject.SetActive(false);
			}
		}
		//hat
		if(hatButton.interactable == false){
			HatSelectButton.GetComponent<adjustTextBtnC>().enabled = false;
			HatSelectButton.GetComponent<Button>().enabled = false;
			HatSelectButton.GetComponent<Image>().enabled = false;
			for(int i = 0; i < HatSelectButton.transform.childCount; i++)
			{
				HatSelectButton.transform.GetChild (i).gameObject.SetActive(false);
			}
		}

	}
	void turnonSelectBtn(){
		//MAIN CHARACTER
		if(characterButton.interactable == false){
			SelectButton.GetComponent<adjustTextBtnC>().enabled = true;
			SelectButton.GetComponent<Button>().enabled = true;
			SelectButton.GetComponent<Image>().enabled = true;

			for(int i = 0; i < SelectButton.transform.childCount; i++)
			{
				SelectButton.transform.GetChild (i).gameObject.SetActive(true);
			}
		}

		if(handButton.interactable == false){
			//HAND



			HandSelectButton.GetComponent<adjustTextBtnC>().enabled = true;
			HandSelectButton.GetComponent<Button>().enabled = true;
			HandSelectButton.GetComponent<Image>().enabled = true;
			for(int i = 0; i < HandSelectButton.transform.childCount; i++)
			{
				HandSelectButton.transform.GetChild (i).gameObject.SetActive(true);
			}
		}

		if(shoeButton.interactable == false){
			//HAND



			ShoeSelectButton.GetComponent<adjustTextBtnC>().enabled = true;
			ShoeSelectButton.GetComponent<Button>().enabled = true;
			ShoeSelectButton.GetComponent<Image>().enabled = true;
			for(int i = 0; i < HandSelectButton.transform.childCount; i++)
			{
				ShoeSelectButton.transform.GetChild (i).gameObject.SetActive(true);
			}
		}

		if(hatButton.interactable == false){
			//HAND



			HatSelectButton.GetComponent<adjustTextBtnC>().enabled = true;
			HatSelectButton.GetComponent<Button>().enabled = true;
			HatSelectButton.GetComponent<Image>().enabled = true;
			for(int i = 0; i < HatSelectButton.transform.childCount; i++)
			{
				HatSelectButton.transform.GetChild (i).gameObject.SetActive(true);
			}
		}

	}

	public void resetCharacterName(){
		characterNameTxt.text = characterName[PlayerPrefs.GetInt("Character")];

	}

	public void resetofMove(){
		//		charact =0;



		if(characterButton.interactable == false){

			if (PlayerPrefs.GetInt ("Character") == 65) {

				faceButton.SetActive(true);


			} else {
				faceButton.SetActive(false);

			}


			characterNameTxt.text = characterName[PlayerPrefs.GetInt("Character")];
			if(charact == PlayerPrefs.GetInt("Character")){
				SelectCheck.enabled = true;
			}
			//checks to see if the item has been purchased
			if(PlayerPrefs.GetInt(charact.ToString("f0"))<1){
				turnonPurchaseBtn();
				turnoffSelectBtn();
				//			SelectButton.GetComponent<adjustTextBtnC>().enabled = false;


			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
			previewBuddy.SendMessage("receiveCharacternumber",charact);

		}

		if(handButton.interactable == false){
			//			characterNameTxt.text = characterName[PlayerPrefs.GetInt("shoe")];

			//			handCountDisplay.text = (hand+1) + "/" + purchaseHandString.Length;
			//			shoeCountDisplay.text = (shoe+1) + "/" + purchaseFeetString.Length;
			//			hatCountDisplay.text = (hat+1) + "/" + purchaseHatString.Length;


			if(hand==PlayerPrefs.GetInt("Hand")){
				HandSelectCheck.enabled = true;
			}

			if(	PlayerPrefs.GetInt("hand"+hand.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}

		if(shoeButton.interactable == false){

			if(shoe==PlayerPrefs.GetInt("Shoe")){
				ShoeSelectCheck.enabled = true;
			}

			if(	PlayerPrefs.GetInt("shoe"+shoe.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}

		if(hatButton.interactable == false){


			if(hat==PlayerPrefs.GetInt("Hat")){
				HatSelectCheck.enabled = true;
			}

			if(	PlayerPrefs.GetInt("hat"+hat.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}


	}

	public void checkPurchase(){
		if(characterButton.interactable == false){
			if(PlayerPrefs.GetInt(charact.ToString("f0"))<1){
				turnonPurchaseBtn();
				turnoffSelectBtn();

			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}
		if(handButton.interactable == false){
			if(	PlayerPrefs.GetInt("hand"+hand.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}
		if(shoeButton.interactable == false){
			if(	PlayerPrefs.GetInt("shoe"+shoe.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}

		if(hatButton.interactable == false){
			if(	PlayerPrefs.GetInt("hat"+hat.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}
		}
	}



	public void resetScriptStart(){



		mainCharacterRoot = GameObject.Find("Player");
		mainCharacterBody = GameObject.Find ("Body");
		mainCharacterHead= GameObject.Find ("Head");
		mainCharacterLowerBody= GameObject.Find ("LowerBody");
		mainCharacterUpperArm_Left= GameObject.Find ("UpperArm_Left");
		mainCharacterUpperArm_Right= GameObject.Find ("UpperArm_Right");
		mainCharacterLowerArm_Left= GameObject.Find ("LowerArm_Left");
		mainCharacterLowerArm_Right= GameObject.Find ("LowerArm_Right");
		mainCharacterHand_Left= GameObject.Find ("Hand_Left");
		mainCharacterHand_Right= GameObject.Find ("Hand_Right");
		mainCharacterUpperLeg_Left= GameObject.Find ("UpperLeg_Left");
		mainCharacterUpperLeg_Right= GameObject.Find ("UpperLeg_Right");
		mainCharacterLowerLeg_Right= GameObject.Find ("LowerLeg_Right");
		mainCharacterLowerLeg_Left= GameObject.Find ("LowerLeg_Left");
		mainCharacterFoot_Left= GameObject.Find ("Foot_Left");
		mainCharacterFoot_Right= GameObject.Find ("Foot_Right");
		mainCharacter_Hat_Parent= GameObject.Find ("Hat_jnt");
		rightHand = GameObject.Find ("Right_Hand_Acc");
		leftHand = GameObject.Find ("Left_Hand_Acc");
		leftShoe = GameObject.Find ("Shoes_Left");
		rightShoe = GameObject.Find ("Shoes_Right");
		purchasableHat = GameObject.Find("Purchasable_Hats");


		//shows hand if exists
		if(PlayerPrefs.GetInt("Hand")>=0){

			hand = PlayerPrefs.GetInt("Hand");
			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);

			if(PlayerPrefs.GetInt("Hand")>=2&&PlayerPrefs.GetInt("Hand")<=5||PlayerPrefs.GetInt("Hand")==12||PlayerPrefs.GetInt("Hand")==16){
				//				lefthand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);
				leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);
			}

		}

		if (PlayerPrefs.GetInt ("Shoe") >= 0) {

			shoe = PlayerPrefs.GetInt ("Shoe");
			leftShoe.transform.GetChild (PlayerPrefs.GetInt ("Shoe")).gameObject.SetActive (true);

			if(PlayerPrefs.GetInt ("Shoe")<rightShoe.transform.childCount){
				rightShoe.transform.GetChild (PlayerPrefs.GetInt ("Shoe")).gameObject.SetActive (true);
			}
			if (leftShoe.transform.GetChild (PlayerPrefs.GetInt ("Shoe")).name == "rollerblades_left" || leftShoe.transform.GetChild (shoe).name == "iceskates_left"||leftShoe.transform.GetChild (shoe).name == "skateboard"||leftShoe.transform.GetChild (shoe).name == "hoverboard") {
				BabyPlatform.transform.position = new Vector3 (BabyPlatform.transform.position.x, -.71f, BabyPlatform.transform.position.z);
				//-.71
			}
			else{
				if (leftShoe.transform.GetChild (PlayerPrefs.GetInt ("Shoe")).name == "skis_left") {
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.568f,BabyPlatform.transform.position.z);

				}
				else{
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.54f,BabyPlatform.transform.position.z);
				}
			}
		} 
		else {
			BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.54f,BabyPlatform.transform.position.z);

		}

		if(PlayerPrefs.GetInt("Hat")>=0){

			hat = PlayerPrefs.GetInt("Hat");
			purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).gameObject.SetActive(true);

		}

		//FINDS CHARACTER POSITION ON BUTTON PUSHC
		charact = PlayerPrefs.GetInt("Character");
		if(characterButton.interactable == false){
			characterNameTxt.text = characterName[PlayerPrefs.GetInt("Character")];
		}
		characterCountDisplay.text = (charact+1) + "/" + characterTexture.Length;

		handCountDisplay.text = (hand+1) + "/" + purchaseHandString.Length;
		shoeCountDisplay.text = (shoe+1) + "/" + purchaseFeetString.Length;
		hatCountDisplay.text = (hat+1) + "/" + purchaseHatString.Length;

		checkGalacticMan ();

		checkHatStored();
		checkRequiredObjectsStored();
		//NOW CHECK FOR HAND STUFF
		mainHandPropGO  = GameObject.Find ("Right_Hand_Acc");


		if (PlayerPrefs.GetInt ("Character") == 65) {
			mainCharacterHead.GetComponent<MeshFilter> ().mesh = customHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = faceMaterials;


		} else {

			mainCharacterHead.GetComponent<MeshFilter> ().mesh = normalHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = normalMaterials;


		}



	}


	public void selectedCharacterFunc(){
		selectedCharacter = charact;
		SelectCheck.enabled = true;
		PlayerPrefs.SetInt("Character",selectedCharacter);
		characterNameTxt.text = characterName[PlayerPrefs.GetInt("Character")];

	}

	public void selectedHandFunc(){


		if(HandSelectCheck.enabled == true){
			HandSelectCheck.enabled = false;
			PlayerPrefs.SetInt("Hand",-1);
			replaceWithSelectedHand(-1);


		}

		else{

			HandSelectCheck.enabled = true;
			PlayerPrefs.SetInt("Hand",hand);
			replaceWithSelectedHand(hand);
		}


	}

	public void selectedShoeFunc(){


		if(ShoeSelectCheck.enabled == true){
			ShoeSelectCheck.enabled = false;
			PlayerPrefs.SetInt("Shoe",-1);
			replaceWithSelectedShoe(-1);

		}

		else{

			ShoeSelectCheck.enabled = true;
			PlayerPrefs.SetInt("Shoe",shoe);
			replaceWithSelectedShoe(shoe);
		}


	}

	public void selectedHatFunc(){


		if(HatSelectCheck.enabled == true){
			HatSelectCheck.enabled = false;
			PlayerPrefs.SetInt("Hat",-1);
			replaceWithSelectedHat(-1);

		}

		else{

			HatSelectCheck.enabled = true;
			PlayerPrefs.SetInt("Hat",hat);
			replaceWithSelectedHat(hat);
		}


	}

	public void replaceWithSelectedCharacter(){

		if(PlayerPrefs.GetInt("Character")!= charact){
			iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",new Vector3(.01f,.01f,.01f),"time",.125,"easeType",iTween.EaseType.easeInCubic, "oncomplete","scaleUpChangeSelected","OnCompleteTarget",gameObject));

		}
		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects();
	}



	public void replaceWithSelectedCharacterHand(){

		if(PlayerPrefs.GetInt("Character")!= charact){
			iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",new Vector3(.01f,.01f,.01f),"time",.125,"easeType",iTween.EaseType.easeInCubic, "oncomplete","scaleUpChangeSelectedHand","OnCompleteTarget",gameObject));

		}
		else{
			showStartHand();
		}
		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects();
	}

	public void replaceWithSelectedCharacterShoe(){

		if(PlayerPrefs.GetInt("Character")!= charact){
			iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",new Vector3(.01f,.01f,.01f),"time",.125,"easeType",iTween.EaseType.easeInCubic, "oncomplete","scaleUpChangeSelectedShoe","OnCompleteTarget",gameObject));

		}
		else{
			showStartShoe();
		}
		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects();
	}


	public void replaceWithSelectedCharacterHat(){

		if(PlayerPrefs.GetInt("Character")!= charact){
			iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",new Vector3(.01f,.01f,.01f),"time",.125,"easeType",iTween.EaseType.easeInCubic, "oncomplete","scaleUpChangeSelectedHat","OnCompleteTarget",gameObject));

		}
		else{
			showStartHat();
		}
		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects();
	}

	public void showStartHand(){
		charact = PlayerPrefs.GetInt("Character");
		for(int i = 0; i < rightHand.transform.childCount; i++)
		{
			rightHand.transform.GetChild(i).gameObject.SetActive(false);

			leftHand.transform.GetChild (i).gameObject.SetActive(false);

		}
		if(PlayerPrefs.GetInt("Hand")>=0){

			hand = PlayerPrefs.GetInt("Hand");
			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);
			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			if(PlayerPrefs.GetInt("Hand")>=2&&PlayerPrefs.GetInt("Hand")<=5||PlayerPrefs.GetInt("Hand")==12 ||PlayerPrefs.GetInt("Hand")==16){
				leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);
				leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).localScale = new Vector3(.001f,.001f,.001f);

				iTween.ScaleTo(leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			}

		}
		if(hand>=0){

			rightHand.transform.GetChild (hand).gameObject.SetActive(true);
			rightHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			if(hand>=2&&hand<=5||hand==12||hand==16){
				leftHand.transform.GetChild (hand).gameObject.SetActive(true);
				leftHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(leftHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			}



		}


	}



	public void showStartHandNoAnim(){
		//when switching to other options this will not scale showing the selected object
		charact = PlayerPrefs.GetInt("Character");
		for(int i = 0; i < rightHand.transform.childCount; i++)
		{
			rightHand.transform.GetChild(i).gameObject.SetActive(false);
			leftHand.transform.GetChild(i).gameObject.SetActive(false);

		}
		if(PlayerPrefs.GetInt("Hand")>=0){

			hand = PlayerPrefs.GetInt("Hand");
			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);

			if(PlayerPrefs.GetInt("Hand")>=2&&PlayerPrefs.GetInt("Hand")<=5||PlayerPrefs.GetInt("Hand")==12||PlayerPrefs.GetInt("Hand")==16){
				leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);

			}
		}
		if(hand>=0){

			rightHand.transform.GetChild (hand).gameObject.SetActive(true);

			if(hand>=2&&hand<=5 ||hand==12||hand==16 ){
				leftHand.transform.GetChild (hand).gameObject.SetActive(true);
			}


			characterNameTxt.text = handName[hand];
		}
	}

	public void showStartShoe(){
		charact = PlayerPrefs.GetInt("Character");
		//LEFT SHOE
		for(int i = 0; i < leftShoe.transform.childCount; i++)
		{
			leftShoe.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){

			shoe = PlayerPrefs.GetInt("Shoe");
			leftShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);
			leftShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(leftShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));




		}
		if(shoe>=0){

			leftShoe.transform.GetChild (shoe).gameObject.SetActive(true);
			leftShoe.transform.GetChild (shoe).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(leftShoe.transform.GetChild (shoe).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
		}
		//RIGHT SHOE
		for(int j = 0; j < rightShoe.transform.childCount; j++)
		{
			rightShoe.transform.GetChild(j).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){
			if(PlayerPrefs.GetInt("Shoe")<rightShoe.transform.childCount){
				shoe = PlayerPrefs.GetInt("Shoe");
				rightShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);
				rightShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(rightShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			}
		}
		if (shoe >= 0) {
			if (shoe < rightShoe.transform.childCount) {

				rightShoe.transform.GetChild (shoe).gameObject.SetActive (true);
				rightShoe.transform.GetChild (shoe).localScale = new Vector3 (.001f, .001f, .001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo (rightShoe.transform.GetChild (shoe).gameObject, iTween.Hash ("scale", new Vector3 (0.1706f, 0.1843f, 0.1843f), "time", .50f, "easeType", iTween.EaseType.easeOutElastic));
			}
		}

	}

	public void showStartShoeNoAnim(){
		//when switching to other options this will not scale showing the selected object

		charact = PlayerPrefs.GetInt("Character");
		//LEFT SHOE
		for(int i = 0; i < leftShoe.transform.childCount; i++)
		{
			leftShoe.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){

			shoe = PlayerPrefs.GetInt("Shoe");
			leftShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);
		}
		if(shoe>=0){

			leftShoe.transform.GetChild (shoe).gameObject.SetActive(true);

		}
		//RIGHT SHOE
		for(int j = 0; j < rightShoe.transform.childCount; j++)
		{
			rightShoe.transform.GetChild(j).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){
			if (PlayerPrefs.GetInt("Shoe") < rightShoe.transform.childCount) {

				shoe = PlayerPrefs.GetInt("Shoe");
				rightShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);
			}
		}
		if(shoe>=0){
			if (shoe < rightShoe.transform.childCount) {

				rightShoe.transform.GetChild (shoe).gameObject.SetActive(true);
				characterNameTxt.text = feetName[shoe];
			}

		}

	}

	public void removeMainCharacterHat(){
		//removes main character hat the -1 is to not disable the IAP items
		for(int i = 0; i < mainCharacter_Hat_Parent.transform.childCount-1; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild (i).gameObject.SetActive(false);

		}


	}

	public void showStartHat(){
		charact = PlayerPrefs.GetInt("Character");



		for(int i = 0; i < purchasableHat.transform.childCount; i++)
		{
			purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Hat")>=0){

			hat = PlayerPrefs.GetInt("Hat");
			purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).gameObject.SetActive(true);
			purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).gameObject, iTween.Hash("scale",new Vector3(1f,1f,1f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
		}
		if(hat>=0){

			purchasableHat.transform.GetChild (hat).gameObject.SetActive(true);
			purchasableHat.transform.GetChild (hat).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(purchasableHat.transform.GetChild (hat).gameObject, iTween.Hash("scale",new Vector3(1f,1f,1f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
		}


	}

	public void showStartHatNoAnim(){
		//when switching to other options this will not scale showing the selected object
		charact = PlayerPrefs.GetInt("Character");

		for(int i = 0; i < purchasableHat.transform.childCount; i++)
		{
			purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Hat")>=0){

			hat = PlayerPrefs.GetInt("Hat");
			purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).gameObject.SetActive(true);

		}
		if(hat>=0){

			purchasableHat.transform.GetChild (hat).gameObject.SetActive(true);
			characterNameTxt.text = hatName[hat];

		}


	}


	//	public void resetHand(){
	//
	//		for(int i = 0; i < rightHand.transform.childCount; i++)
	//		{
	//			rightHand.transform.GetChild(i).gameObject.SetActive(false);
	//		}
	//		if(PlayerPrefs.GetInt("Hand")>=0){
	//			
	//			hand = PlayerPrefs.GetInt("Hand");
	//			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);
	//
	//		}
	//		if(hand>=0){
	//			
	//			rightHand.transform.GetChild (hand).gameObject.SetActive(true);
	//
	//		}
	//		
	//		
	//	}

	public void replaceWithSelectedHand(int startInt){

		tempHand = hand;
		for(int i = 0; i < rightHand.transform.childCount; i++)
		{
			rightHand.transform.GetChild(i).gameObject.SetActive(false);
			leftHand.transform.GetChild (i).gameObject.SetActive(false);


		}
		if(PlayerPrefs.GetInt("Hand")>=0){
			hand = PlayerPrefs.GetInt("Hand");
			rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);

			if(PlayerPrefs.GetInt("Hand")>=2&&PlayerPrefs.GetInt("Hand")<=5 ||PlayerPrefs.GetInt("Hand")==12||PlayerPrefs.GetInt("Hand")==16){
				leftHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject.SetActive(true);

			}
		}
		else{
			//			hand = tempHand;
			//			hand = startInt;
			//			if(hand>0){
			rightHand.transform.GetChild (hand).gameObject.SetActive(false);
			leftHand.transform.GetChild (hand).gameObject.SetActive(false);

		}
		if(hand>0){
			if(hand!=PlayerPrefs.GetInt("Hand")){
				HandSelectCheck.enabled = false;
			}
			else{
				HandSelectCheck.enabled = true;
			}

			handCountDisplay.text = (hand+1) + "/" + purchaseHandString.Length;
		}
	}

	public void replaceWithSelectedShoe(int startInt){

		tempShoe = shoe;

		//LEFT SHOE
		for(int i = 0; i < leftShoe.transform.childCount; i++)
		{
			leftShoe.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){
			shoe = PlayerPrefs.GetInt("Shoe");
			leftShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);

		}
		else{

			leftShoe.transform.GetChild (shoe).gameObject.SetActive(false);

		}
		//RIGHT SHOE
		for(int j = 0; j < rightShoe.transform.childCount; j++)
		{
			rightShoe.transform.GetChild(j).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Shoe")>=0){
			if(PlayerPrefs.GetInt("Shoe")<rightShoe.transform.childCount){

				shoe = PlayerPrefs.GetInt("Shoe");
				rightShoe.transform.GetChild (PlayerPrefs.GetInt("Shoe")).gameObject.SetActive(true);
			}

		}
		else{

			rightShoe.transform.GetChild (shoe).gameObject.SetActive(false);

		}


		if(shoe>0){
			if(shoe!=PlayerPrefs.GetInt("Shoe")){
				ShoeSelectCheck.enabled = false;
			}
			else{
				ShoeSelectCheck.enabled = true;
			}
			//			if(startInt ==0){
			//				shoe=0;
			//			}

			shoeCountDisplay.text = (shoe+1) + "/" + purchaseFeetString.Length;
		}
	}

	public void replaceWithSelectedHat(int startInt){

		tempHat = hat;
		for(int i = 0; i < purchasableHat.transform.childCount; i++)
		{
			purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
		}
		if(PlayerPrefs.GetInt("Hat")>=0){
			hat = PlayerPrefs.GetInt("Hat");
			purchasableHat.transform.GetChild (PlayerPrefs.GetInt("Hat")).gameObject.SetActive(true);

		}
		else{

			purchasableHat.transform.GetChild (hat).gameObject.SetActive(false);

		}
		if(hat>0){
			if(hat!=PlayerPrefs.GetInt("Hat")){
				HatSelectCheck.enabled = false;
			}
			else{
				HatSelectCheck.enabled = true;
			}


			hatCountDisplay.text = (hat+1) + "/" + purchaseHatString.Length;
		}
	}

	public void exitHandFun(){
		charact = PlayerPrefs.GetInt("Character");

		if(PlayerPrefs.GetInt("Hand")==-1){
			for(int j = 0; j < rightHand.transform.childCount; j++)
			{
				rightHand.transform.GetChild(j).gameObject.SetActive(false);
				leftHand.transform.GetChild (j).gameObject.SetActive(false);

			}
		}
	}
	public void exitShoeFun(){
		charact = PlayerPrefs.GetInt("Character");

		if(PlayerPrefs.GetInt("Shoe")==-1){
			for(int j = 0; j < leftShoe.transform.childCount; j++)
			{
				leftShoe.transform.GetChild(j).gameObject.SetActive(false);
			}
			for(int l = 0; l < rightShoe.transform.childCount; l++)
			{
				rightShoe.transform.GetChild(l).gameObject.SetActive(false);
			}
		}
	}

	public void exitHatFun(){
		charact = PlayerPrefs.GetInt("Character");

		if(PlayerPrefs.GetInt("Hat")==-1){
			for(int j = 0; j < purchasableHat.transform.childCount; j++)
			{
				purchasableHat.transform.GetChild(j).gameObject.SetActive(false);
			}
		}
	}



	public void MoveRight(){
		if (characterButton.interactable == false) {
			if (charact >= characterTexture.Length - 1) {
				charact =-1;

			} 

			charact++;
			previewBuddy.SendMessage ("receiveCharacternumber", charact);


			if (charact != PlayerPrefs.GetInt ("Character")) {
				SelectCheck.enabled = false;
			} else {
				SelectCheck.enabled = true;
			}
			characterNameTxt.text = characterName [charact];
			characterCountDisplay.text = (charact + 1) + "/" + characterTexture.Length;

			//checks to see if the item has been purchased
			if (PlayerPrefs.GetInt (charact.ToString ("f0")) < 1) {
				turnonPurchaseBtn ();
				turnoffSelectBtn ();

			} else {
				turnoffPurchaseBtn ();
				turnonSelectBtn ();

			}

			iTween.ScaleTo (mainCharacterRoot, iTween.Hash ("scale", new Vector3 (.01f, .01f, .01f), "time", .125, "easeType", iTween.EaseType.easeInCubic, "oncomplete", "scaleUpChange", "OnCompleteTarget", gameObject));
			//				if (characterTexture [charact].name == "SuperGoldBuddy") {
			updateLocalizedPricing (purchaseString[charact]);
			//				}


		}
		if(handButton.interactable == false){
			//			if(hand<purchaseHandString.Length-1){
			if (hand >= purchaseHandString.Length - 1) {
				hand = -1;

			}
			//				if(hand!=tempHand){
			//					hand = tempHand;
			//				}
			hand++;
			handCountDisplay.text = (hand+1) + "/" + purchaseHandString.Length;

			if(hand!=PlayerPrefs.GetInt("Hand")){
				HandSelectCheck.enabled = false;
			}
			else{
				HandSelectCheck.enabled = true;
			}

			//h is for hand needed to differentiate from 1,2,3,4 used for character

			if(	PlayerPrefs.GetInt("hand"+hand.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}

			for(int i = 0; i < rightHand.transform.childCount; i++)
			{
				rightHand.transform.GetChild(i).gameObject.SetActive(false);
				leftHand.transform.GetChild (i).gameObject.SetActive(false);

			}


			rightHand.transform.GetChild (hand).gameObject.SetActive(true);
			rightHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			if(hand>=2&&hand<=5||hand==12||hand==16){
				leftHand.transform.GetChild (hand).gameObject.SetActive(true);
				leftHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(leftHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			}



			characterNameTxt.text = handName[hand];
			updateLocalizedPricing(purchaseHandString[hand]);

		}


		if(shoeButton.interactable == false){
			//			if(shoe<purchaseFeetString.Length-1){
			//				if(hand!=tempHand){
			//					hand = tempHand;
			//				}
			if (shoe >= purchaseFeetString.Length - 1) {
				shoe = -1;

			}

			shoe++;
			shoeCountDisplay.text = (shoe+1) + "/" + purchaseFeetString.Length;

			if(shoe!=PlayerPrefs.GetInt("Shoe")){
				ShoeSelectCheck.enabled = false;
			}
			else{
				ShoeSelectCheck.enabled = true;
			}

			//h is for hand needed to differentiate from 1,2,3,4 used for character

			if(	PlayerPrefs.GetInt("shoe"+shoe.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}

			for(int i = 0; i < leftShoe.transform.childCount; i++)
			{
				leftShoe.transform.GetChild(i).gameObject.SetActive(false);
			}
			for(int j = 0; j < rightShoe.transform.childCount; j++)
			{
				rightShoe.transform.GetChild(j).gameObject.SetActive(false);
			}


			leftShoe.transform.GetChild (shoe).gameObject.SetActive(true);
			leftShoe.transform.GetChild (shoe).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(leftShoe.transform.GetChild (shoe).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			if(shoe<rightShoe.transform.childCount){
				rightShoe.transform.GetChild (shoe).gameObject.SetActive(true);
				rightShoe.transform.GetChild (shoe).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(rightShoe.transform.GetChild (shoe).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			}
			characterNameTxt.text = feetName[shoe];
			updateLocalizedPricing(purchaseFeetString[shoe]);

			if(leftShoe.transform.GetChild (shoe).name == "rollerblades_left"||leftShoe.transform.GetChild (shoe).name == "iceskates_left"||leftShoe.transform.GetChild (shoe).name == "skateboard"||leftShoe.transform.GetChild (shoe).name == "hoverboard"){
				BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-.71f,BabyPlatform.transform.position.z);
				//-.71
			}
			else{
				if (leftShoe.transform.GetChild (shoe).name == "skis_left") {
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.568f,BabyPlatform.transform.position.z);

				}
				else{
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.54f,BabyPlatform.transform.position.z);
				}

			}

		}

		if(hatButton.interactable == false){
			//			if(hat<purchaseHatString.Length-1){
			//				if(hand!=tempHand){
			//					hand = tempHand;
			//				}
			if (hat >= purchaseHatString.Length - 1) {
				hat = -1;

			}
			hat++;
			hatCountDisplay.text = (hat+1) + "/" + purchaseHatString.Length;

			if(hat!=PlayerPrefs.GetInt("Hat")){
				HatSelectCheck.enabled = false;
			}
			else{
				HatSelectCheck.enabled = true;
			}

			//h is for hand needed to differentiate from 1,2,3,4 used for character

			if(	PlayerPrefs.GetInt("hat"+hat.ToString("f0"))<1)
			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}

			for(int i = 0; i < purchasableHat.transform.childCount; i++)
			{
				purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
			}


			purchasableHat.transform.GetChild (hat).gameObject.SetActive(true);
			purchasableHat.transform.GetChild (hat).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(purchasableHat.transform.GetChild (hat).gameObject, iTween.Hash("scale",new Vector3(1f,1f,1f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			characterNameTxt.text = hatName[hat];
			updateLocalizedPricing(purchaseHatString[hat]);

		}
	}


	public void enableProps(){
		props =0;
		transform.GetChild(props).gameObject.SetActive(true);

	}

	public void MoveLeft(){
		if(characterButton.interactable == false){
			if(charact<=0){
				charact = characterTexture.Length;
			}
			charact--;

			previewBuddy.SendMessage("receiveCharacternumber",charact);

			if(charact!=PlayerPrefs.GetInt("Character")){
				SelectCheck.enabled = false;
			}
			else{
				SelectCheck.enabled = true;
			}

			characterNameTxt.text = characterName[charact];
			characterCountDisplay.text = (charact+1) + "/" + characterTexture.Length;

			//checks to see if the item has been purchased
			if(PlayerPrefs.GetInt(charact.ToString("f0"))<1){
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}

			iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",new Vector3(.01f,.01f,.01f),"time",.125,"easeType",iTween.EaseType.easeInCubic, "oncomplete","scaleUpChange","OnCompleteTarget",gameObject));
			//				PlayerPrefs.SetInt("Character",charact);
			//				if (characterTexture [charact].name == "SuperGoldBuddy") {
			updateLocalizedPricing(purchaseString[charact]);
			//				}




		}
		if(handButton.interactable == false){
			//			if(hand>0){
			if(hand<=0){
				hand = purchaseHandString.Length;
			}
			//					if(hand!=tempHand){
			//					hand = tempHand;
			//				}
			hand--;

			handCountDisplay.text = (hand+1) + "/" + purchaseHandString.Length;

			if(hand!=PlayerPrefs.GetInt("Hand")){
				HandSelectCheck.enabled = false;
			}
			else{
				HandSelectCheck.enabled = true;
			}
			//h is for hand needed to differentiate from 1,2,3,4 used for character
			if(	PlayerPrefs.GetInt("hand"+hand.ToString("f0"))<1)

			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}


			for(int i = 0; i < rightHand.transform.childCount; i++)
			{
				rightHand.transform.GetChild(i).gameObject.SetActive(false);

				leftHand.transform.GetChild (i).gameObject.SetActive(false);

			}


			rightHand.transform.GetChild (hand).gameObject.SetActive(true);
			rightHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			if(hand>=2&&hand<=5 ||hand==12 || hand==16){
				leftHand.transform.GetChild (hand).gameObject.SetActive(true);
				leftHand.transform.GetChild (hand).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (PlayerPrefs.GetInt("Hand")).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(leftHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(0.1198644f,0.1198644f,0.1198644f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			}


			characterNameTxt.text = handName[hand];
			updateLocalizedPricing(purchaseHandString[hand]);


		}


		if(shoeButton.interactable == false){
			//			if(shoe>0){
			if(shoe<=0){
				shoe = purchaseFeetString.Length;
			}
			shoe--;

			shoeCountDisplay.text = (shoe+1) + "/" + purchaseFeetString.Length;

			if(shoe!=PlayerPrefs.GetInt("Shoe")){
				ShoeSelectCheck.enabled = false;
			}
			else{
				ShoeSelectCheck.enabled = true;
			}
			//h is for hand needed to differentiate from 1,2,3,4 used for character
			if(	PlayerPrefs.GetInt("shoe"+shoe.ToString("f0"))<1)

			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}


			for(int i = 0; i < leftShoe.transform.childCount; i++)
			{
				leftShoe.transform.GetChild(i).gameObject.SetActive(false);
			}
			for(int j = 0; j < rightShoe.transform.childCount; j++)
			{
				rightShoe.transform.GetChild(j).gameObject.SetActive(false);
			}


			leftShoe.transform.GetChild (shoe).gameObject.SetActive(true);
			leftShoe.transform.GetChild (shoe).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(leftShoe.transform.GetChild (shoe).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));

			if(shoe<rightShoe.transform.childCount){
				rightShoe.transform.GetChild (shoe).gameObject.SetActive(true);
				rightShoe.transform.GetChild (shoe).localScale = new Vector3(.001f,.001f,.001f);
				//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
				iTween.ScaleTo(rightShoe.transform.GetChild (shoe).gameObject, iTween.Hash("scale",new Vector3(0.1706f,0.1843f,0.1843f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			}

			characterNameTxt.text = feetName[shoe];

			updateLocalizedPricing(purchaseFeetString[shoe]);


			if(leftShoe.transform.GetChild (shoe).name == "rollerblades_left"||leftShoe.transform.GetChild (shoe).name == "iceskates_left"||leftShoe.transform.GetChild (shoe).name == "skateboard"||leftShoe.transform.GetChild (shoe).name == "hoverboard"){
				BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-.71f,BabyPlatform.transform.position.z);
				//-.71
			}
			else{
				if (leftShoe.transform.GetChild (shoe).name == "skis_left") {
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.568f,BabyPlatform.transform.position.z);

				}
				else{
					BabyPlatform.transform.position = new Vector3(BabyPlatform.transform.position.x,-0.54f,BabyPlatform.transform.position.z);
				}
			}

		}


		if(hatButton.interactable == false){
			//			if(hat>0){
			if(hat<=0){
				hat = purchaseHatString.Length;
			}
			hat--;

			hatCountDisplay.text = (hat+1) + "/" + purchaseHatString.Length;

			if(hat!=PlayerPrefs.GetInt("Hat")){
				HatSelectCheck.enabled = false;
			}
			else{
				HatSelectCheck.enabled = true;
			}
			//h is for hand needed to differentiate from 1,2,3,4 used for character
			if(	PlayerPrefs.GetInt("hat"+hat.ToString("f0"))<1)

			{
				turnonPurchaseBtn();
				turnoffSelectBtn();
			}
			else{
				turnoffPurchaseBtn();
				turnonSelectBtn();

			}


			for(int i = 0; i < purchasableHat.transform.childCount; i++)
			{
				purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
			}


			purchasableHat.transform.GetChild (hat).gameObject.SetActive(true);
			purchasableHat.transform.GetChild (hat).localScale = new Vector3(.001f,.001f,.001f);
			//			iTween.ScaleTo(rightHand.transform.GetChild (hand).gameObject, iTween.Hash("scale",new Vector3(.5f,.5f,.5f),"time",.125f,"easeType",iTween.EaseType.easeInCubic));
			iTween.ScaleTo(purchasableHat.transform.GetChild (hat).gameObject, iTween.Hash("scale",new Vector3(1f,1f,1f),"time",.50f,"easeType",iTween.EaseType.easeOutElastic));
			characterNameTxt.text = hatName[hat];
			updateLocalizedPricing(purchaseHatString[hat]);

		}

	}

	public void unEquiptHand(){
		if(HandSelectCheck.enabled ==true){
			PlayerPrefs.SetInt("Hand",-1);
			HandSelectCheck.enabled =false;
			for(int i = 0; i < rightHand.transform.childCount; i++)
			{

				rightHand.transform.GetChild(i).gameObject.SetActive(false);
				leftHand.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
	public void unEquiptShoe(){
		if(ShoeSelectCheck.enabled ==true){
			PlayerPrefs.SetInt("Shoe",-1);
			ShoeSelectCheck.enabled =false;
			for(int i = 0; i < leftShoe.transform.childCount; i++)
			{

				leftShoe.transform.GetChild(i).gameObject.SetActive(false);
			}
			for(int j = 0; j < rightShoe.transform.childCount; j++)
			{

				rightShoe.transform.GetChild(j).gameObject.SetActive(false);
			}
		}
	}

	public void unEquiptHat(){
		if(HatSelectCheck.enabled ==true){
			PlayerPrefs.SetInt("Hat",-1);
			HatSelectCheck.enabled =false;
			for(int i = 0; i < purchasableHat.transform.childCount; i++)
			{

				purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}




	public void scaleUpChange(){


		changeThisCharacter();
		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"easeType",iTween.EaseType.easeOutElastic));

	}

	public void scaleUpChangeSelected(){


		changeThisCharacterCurrentSelected();
		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"easeType",iTween.EaseType.easeOutElastic));

	}

	public void scaleUpChangeSelectedHand(){


		changeThisCharacterCurrentSelected();
		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"oncomplete","showStartHand","OnCompleteTarget",gameObject,"easeType",iTween.EaseType.easeOutElastic));

	}

	public void scaleUpChangeSelectedShoe(){


		changeThisCharacterCurrentSelected();
		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"oncomplete","showStartShoe","OnCompleteTarget",gameObject,"easeType",iTween.EaseType.easeOutElastic));

	}

	public void scaleUpChangeSelectedHat(){


		changeThisCharacterCurrentSelected();
		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"oncomplete","showStartHat","OnCompleteTarget",gameObject,"easeType",iTween.EaseType.easeOutElastic));

	}

	public void changeThisCharacter(){
		//51 IS GALACTIC MAN REQUIRES NEW MATERIAL


		if (charact == 65) {
			mainCharacterHead.GetComponent<MeshFilter> ().mesh = customHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = faceMaterials;
			faceButton.SetActive(true);


		} else {
			faceButton.SetActive(false);

			mainCharacterHead.GetComponent<MeshFilter> ().mesh = normalHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = normalMaterials;


		}


		mainMaterial.mainTexture = characterTexture [charact];

		mainCharacterBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterHead.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterLowerBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterUpperArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterUpperArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterLowerArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterLowerArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterHand_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterHand_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterUpperLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterUpperLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterLowerLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterLowerLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterFoot_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];
		mainCharacterFoot_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [charact];



		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects ();
		//		}
	}
	public void changeThisCharacterCurrentSelected(){


		//51 IS GALACTIC MAN REQUIRES NEW MATERIAL
		if (PlayerPrefs.GetInt ("Character") == 65) {
			mainCharacterHead.GetComponent<MeshFilter> ().mesh = customHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = faceMaterials;
			faceButton.SetActive(true);


		} else {
			faceButton.SetActive(false);

			mainCharacterHead.GetComponent<MeshFilter> ().mesh = normalHead;
			mainCharacterHead.GetComponent<MeshRenderer>().materials = normalMaterials;


		}

		mainMaterial.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];

		mainCharacterBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHead.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHand_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHand_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterFoot_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterFoot_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];



		if (hatButton.interactable == true) {
			checkHat ();
		}
		checkRequiredObjects ();

		//		}
	}


	public void checkGalacticMan(){


		//51 IS GALACTIC MAN REQUIRES NEW MATERIAL

		mainMaterial.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		//			
		mainCharacterBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHead.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerBody.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerArm_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerArm_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHand_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterHand_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterUpperLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerLeg_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterLowerLeg_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterFoot_Left.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];
		mainCharacterFoot_Right.GetComponent<Renderer> ().material.mainTexture = characterTexture [PlayerPrefs.GetInt ("Character")];





		//		}
	}

	public void unEquiptRequiredHead(){
		GameObject requiredHead = GameObject.Find ("RequiredHead");
		for(int i = 0; i < requiredHead.transform.childCount; i++)
		{
			requiredHead.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}

	}

	public void checkRequiredObjects(){


		GameObject requiredBody = GameObject.Find ("RequiredBody");
		for(int i = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent<Cloth>().enabled = false;

		}
		if(characterTexture[charact].name == "SIDEKICK_BUDDY"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTexture[charact].name == "superhawkbuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTexture[charact].name == "EvilEmperor"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTexture[charact].name == "SuperGasBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);

		}

		if(characterTexture[charact].name == "ThunderBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);

		}

		if(characterTexture[charact].name == "SuperGoldBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);

		}
		if(characterTexture[charact].name == "Super_Magnet_Man"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",new Color(0.3f,0f,0.5f));

		}


		GameObject requiredHead = GameObject.Find ("RequiredHead");
		for(int i = 0; i < requiredHead.transform.childCount; i++)
		{
			requiredHead.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask
		if(PlayerPrefs.GetInt("Hat")>=0 )
			return;
		if(characterTexture[charact].name == "johnson"){
			GameObject theMask =GameObject.Find ("JohnsonMask");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name  == "robotBuddy"){
			GameObject theMask =GameObject.Find ("robothelmet");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}



	}
	public void checkRequiredObjectsStored(){



		GameObject requiredBody = GameObject.Find ("RequiredBody");
		for(int i = 0; i < requiredBody.transform.childCount; i++)
		{
			requiredBody.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
			requiredBody.transform.GetChild(i).gameObject.GetComponent<Cloth>().enabled = false;

		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "SIDEKICK_BUDDY"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.gameObject.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);
		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "superhawkbuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "EvilEmperor"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.black);

		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "SuperGasBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.gameObject.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);

		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "ThunderBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.red);

		}

		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "SuperGoldBuddy"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",Color.yellow);

		}
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "Super_Magnet_Man"){
			GameObject theCape =GameObject.Find ("cape");
			theCape.GetComponent<SkinnedMeshRenderer>().enabled = true;
			theCape.GetComponent<Cloth>().enabled = true;
			capeMaterial.SetColor("_Diffusecolor",new Color(0.3f,0f,0.5f));

		}



		GameObject requiredHead = GameObject.Find ("RequiredHead");
		for(int i = 0; i < requiredHead.transform.childCount; i++)
		{
			requiredHead.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
		//makes sure that if a hat is selected doesnt add jason mask and robomask

		if(PlayerPrefs.GetInt("Hat")>=0 )
			return;
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "johnson"){
			GameObject theMask =GameObject.Find ("JohnsonMask");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "robotBuddy"){
			GameObject theMask =GameObject.Find ("robothelmet");
			theMask.GetComponent<MeshRenderer>().enabled = true;
		}


	}

	public void checkHat(){
		if(PlayerPrefs.GetInt("Hat")>=0 )
			return;

		for(int i = 0; i < purchasableHat.transform.childCount; i++)
		{
			purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
		}

		for(int i = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>()){
				mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
				mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			}
		}

		if(characterTexture[charact].name == "SimplePeople_RoadWorker_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Helmet");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;

			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "SimplePeople_Punk_White"){

			mainCharacter_Hat = GameObject.Find ("Hair_Punk");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "princess"){

			mainCharacter_Hat = GameObject.Find ("Hair_Princess");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if (characterTexture [charact].name == "Marley_Finn") {

			mainCharacter_Hat = GameObject.Find ("Hair_Harley");
			mainCharacter_Hat_Parent.SendMessage ("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider> ().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;

		}

		if(characterTexture[charact].name == "SimplePeople_Pimp_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Pimp");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Policeman_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Police");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_RiotCop_Brown"){

			mainCharacter_Hat = GameObject.Find ("RiotHelmet");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_FireFighter_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_StreetMan_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Redneck_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Hobo_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Hobo");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Robber_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Hobo");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Sheriff_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "tom"){

			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "PlumberBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "CarpenterBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "BMXBillyBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hair_Punk");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "mrbaseball"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}




	}
	public void checkHatStored(){
		if(PlayerPrefs.GetInt("Hat")>=0 || hatButton.interactable == false)
			return;

		for(int i = 0; i < purchasableHat.transform.childCount; i++)
		{
			purchasableHat.transform.GetChild(i).gameObject.SetActive(false);
		}

		for(int i = 0; i < mainCharacter_Hat_Parent.transform.childCount; i++)
		{
			mainCharacter_Hat_Parent.transform.GetChild(i).gameObject.SetActive(true);
			if(mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>()){
				mainCharacter_Hat_Parent.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
				mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			}
		}

		//		mainCharacter_Hat=null;
		//		print (characterTexture[PlayerPrefs.GetInt("Character")].name);
		if(characterTexture[PlayerPrefs.GetInt("Character")].name == "SimplePeople_RoadWorker_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Helmet");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;

		}
		if(characterTexture[charact].name == "SimplePeople_Punk_White"){

			mainCharacter_Hat = GameObject.Find ("Hair_Punk");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "princess"){

			mainCharacter_Hat = GameObject.Find ("Hair_Princess");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if (characterTexture [charact].name == "Marley_Finn") {

			mainCharacter_Hat = GameObject.Find ("Hair_Harley");
			mainCharacter_Hat_Parent.SendMessage ("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider> ().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;

		}

		if(characterTexture[charact].name == "SimplePeople_Pimp_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Pimp");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Policeman_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Police");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_RiotCop_Brown"){

			mainCharacter_Hat = GameObject.Find ("RiotHelmet");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_FireFighter_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_FireFighter");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_StreetMan_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap_Backwards");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Redneck_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Hobo_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Hobo");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Robber_White"){

			mainCharacter_Hat = GameObject.Find ("Hat_Hobo");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "SimplePeople_Sheriff_Black"){

			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "tom"){

			mainCharacter_Hat = GameObject.Find ("Hat_Sheriff");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

		if(characterTexture[charact].name == "PlumberBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "CarpenterBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "BMXBillyBuddy"){

			mainCharacter_Hat = GameObject.Find ("Hair_Punk");
			mainCharacter_Hat_Parent.SendMessage("DisableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = false;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=false;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}
		if(characterTexture[charact].name == "mrbaseball"){

			mainCharacter_Hat = GameObject.Find ("Hat_Cap");
			mainCharacter_Hat_Parent.SendMessage("EnableYardSale");
			mainCharacter_Hat_Parent.transform.GetComponent<BoxCollider>().enabled = true;
			//((MonoBehaviour)mainCharacter_Hat_Parent.GetComponent("yardSaleColl")).enabled=true;
			mainCharacter_Hat.GetComponent<MeshRenderer>().enabled = true;
		}

	}



	public void toggleCharacterBttns(bool toggle){
		if (IsInitializedIAP())
		{

			buttonBlockerMainCharacterScreen.SetActive (toggle);

		}
		////		}
	}

	public void replacescaleUpChange(){
		changeThisCharacter();

		GetComponent<AudioSource>().Play ();
		iTween.ScaleTo(mainCharacterRoot, iTween.Hash("scale",startScale,"time",.5,"easeType",iTween.EaseType.easeOutElastic));

	}

	//IN APP PURCHASE STUFF

	public  void buyremoveAds() {
		adBuyBool = true;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID("removeads");

		//		UM_InAppPurchaseManager.Client.Purchase("removeads");

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById("removeads").Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById("removeads").CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById("removeads").DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById("removeads").ProductType.ToString(),
		//			"removeads"
		//			);


	}

	public  void buyItem() {
		isPurchasing = true;

		toggleCharacterBttns (isPurchasing);

		tempPurchased = charact;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseString[charact]);

		//		UM_InAppPurchaseManager.Client.Purchase(purchaseString[charact]);

		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("normalBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[charact]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[charact]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[charact]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[charact]).ProductType.ToString(),
		//			purchaseString[charact],
		//			myObj
		//			);
		print("buy item");

	}

	public  void buyItemfromGameOver(int myInt) {
		isPurchasing = true;
		isPurchasingGameOver = true;

		toggleCharacterBttns (isPurchasing);


		tempPurchased = myInt;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseString[myInt]);

		//		UM_InAppPurchaseManager.Client.Purchase(purchaseString[myInt]);

		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("gameOverBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[myInt]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[myInt]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[myInt]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseString[myInt]).ProductType.ToString(),
		//			purchaseString[charact],
		//			myObj
		//			);
	}

	public  void buyHatsfromGameOver(int myInt) {
		isPurchasing = true;
		isPurchasingGameOverHats = true;
		toggleCharacterBttns (isPurchasing);


		tempPurchased = myInt;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseHatString[myInt]);

		//		UM_InAppPurchaseManager.Client.Purchase(purchaseHatString[myInt]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("gameOverBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[myInt]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[myInt]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[myInt]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[myInt]).ProductType.ToString(),
		//			purchaseHatString[myInt],
		//			myObj
		//			);

	}
	public  void buyHandsfromGameOver(int myInt) {
		isPurchasing = true;
		isPurchasingGameOverHands = true;

		toggleCharacterBttns (isPurchasing);


		tempPurchased = myInt;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseHandString[myInt]);

		//		UM_InAppPurchaseManager.Client.Purchase(purchaseHandString[myInt]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("gameOverBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[myInt]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[myInt]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[myInt]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[myInt]).ProductType.ToString(),
		//			purchaseHandString[myInt],
		//			myObj
		//			);

	}
	public  void buyFeetfromGameOver(int myInt) {
		isPurchasing = true;
		isPurchasingGameOverFeet = true;

		toggleCharacterBttns (isPurchasing);


		tempPurchased = myInt;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseFeetString[myInt]);
		//		UM_InAppPurchaseManager.Client.Purchase(purchaseFeetString[myInt]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("gameOverBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[myInt]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[myInt]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[myInt]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[myInt]).ProductType.ToString(),
		//			purchaseFeetString[myInt],
		//			myObj
		//			);

	}

	public  void buyHand() {
		isPurchasing = true;

		toggleCharacterBttns (isPurchasing);

		tempPurchasedHand = hand;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseHandString[hand]);
		//		UM_InAppPurchaseManager.Client.Purchase(purchaseHandString[hand]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("normalBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[hand]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[hand]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[hand]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHandString[hand]).ProductType.ToString(),
		//			purchaseHandString[hand],
		//			myObj
		//			);
	}

	public  void buyShoe() {
		isPurchasing = true;

		toggleCharacterBttns (isPurchasing);

		tempPurchasedShoe = shoe;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseFeetString[shoe]);

		//		UM_InAppPurchaseManager.Client.Purchase(purchaseFeetString[shoe]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("normalBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[shoe]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[shoe]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[shoe]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseFeetString[shoe]).ProductType.ToString(),
		//			purchaseFeetString[shoe],
		//			myObj
		//			);
	}

	public  void buyHat() {
		isPurchasing = true;

		toggleCharacterBttns (isPurchasing);

		tempPurchasedHat = hat;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID(purchaseHatString[hat]);
		//		UM_InAppPurchaseManager.Client.Purchase(purchaseHatString[hat]);
		Dictionary<string, object> myObj = new Dictionary<string, object>();
		myObj.Add ("normalBuy", 1);

		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[hat]).Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[hat]).CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[hat]).DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById(purchaseHatString[hat]).ProductType.ToString(),
		//			purchaseHatString[hat],
		//			myObj
		//			);
	}

	public  void buyTNTLevel() {
		TntLevelBool = true;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID("tntmode");
		//		UM_InAppPurchaseManager.Client.Purchase("tntmode");
		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById("tntmode").Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById("tntmode").CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById("tntmode").DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById("tntmode").ProductType.ToString(),
		//			"tntmode"
		//			);

	}
	public  void buyPongLevel() {
		PongLevelBool = true;
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;
		BuyProductID("buddypong");
		//		UM_InAppPurchaseManager.Client.Purchase("buddypong");
		//		Answers.LogAddToCart(
		//			(decimal?)IOSInAppPurchaseManager.Instance.GetProductById("buddypong").Price,
		//			IOSInAppPurchaseManager.Instance.GetProductById("buddypong").CurrencyCode,
		//			IOSInAppPurchaseManager.Instance.GetProductById("buddypong").DisplayName,
		//			IOSInAppPurchaseManager.Instance.GetProductById("buddypong").ProductType.ToString(),
		//			"buddypong"
		//			);
	}


	IEnumerator coinShower(){
		coinCamera.SetActive (true);

		GameObject firework = GameObject.Find ("CoinRewardGift");

		yield return new WaitForSeconds (.25f);
		firework.GetComponent<ParticleSystem> ().Play ();

		for (int i = 0; i < 20; i++) {
			firework.GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (.075f);

		}
		coinCamera.SetActive (false);

	}
	private  void UnlockProducts(string productIdentifier, int purchCharact) {

		//		if(characterButton.interactable == false){
		PlayerPrefs.SetInt(purchCharact.ToString("f0"),1);
		if(purchCharact ==50){
			//				if(PlayerPrefs.HasKey("Restored")){
			//				}
			//				else{
			//					PlayerPrefs.SetInt("Restored",1);
			//
			//					PlayerPrefs.SetInt ("myCredits",PlayerPrefs.GetInt ("myCredits")+1000);
			////				GameObject credits = GameObject.Find("Credits UI");
			//				creditsUI.text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
			//				StartCoroutine(coinShower());
			////				}


		}



		//		}
		print (purchCharact);


	}

	public void turnOffBuyGuyCam(){
		GameObject myScriptController = GameObject.Find ("Script_Controller");
		myScriptController.SendMessage ("toggleCamera", false);
	}



	private  void UnlockProductsHand(string productIdentifier, int purchHand) {

		string myPurchased = purchHand.ToString("f0");

		if(handButton.interactable == false){
			PlayerPrefs.SetInt("hand"+ myPurchased,1);
			print (myPurchased);

		}



	}

	private  void UnlockProductsShoe(string productIdentifier, int purchShoe) {

		string myPurchased = purchShoe.ToString("f0");

		if(shoeButton.interactable == false){
			PlayerPrefs.SetInt("shoe"+ myPurchased,1);
			print (myPurchased);

		}



	}

	private  void UnlockProductsHat(string productIdentifier, int purchHat) {

		string myPurchased = purchHat.ToString("f0");

		if(hatButton.interactable == false){
			PlayerPrefs.SetInt("hat"+ myPurchased,1);
			print (myPurchased);

		}



	}

	public void purchaseTNTLevelCredits(){
		if (PlayerPrefs.GetInt ("myCredits") < 1000) {
			//			AndroidMessage msg = AndroidMessage.Create (noteEnoughCredits, sorryNotEnoughString);
			MobileNativeMessage msg = 	new MobileNativeMessage(noteEnoughCredits,sorryNotEnoughString);
			msg.OnComplete += OnMessageClose;
						Answers.LogCustom (
							"couldntAffordTNT"
							);
		} else {
			TNTUnlockYESNODIALOG.SetActive(true);
						Answers.LogCustom (
							"boughtTNTCredits"
							);
			//			IOSDialog dialog = IOSDialog.Create (unlockTNTString, areyouSureTNT);
			//			dialog.OnComplete += onDialogCloseTnt;
		}


	}

	public void purchasePongLevelCredits(){
		if (PlayerPrefs.GetInt ("myCredits") < 1000) {
			//			AndroidMessage msg = AndroidMessage.Create (noteEnoughCredits, sorryNotEnoughString);
			MobileNativeMessage msg = 	new MobileNativeMessage(noteEnoughCredits,sorryNotEnoughString);
			msg.OnComplete += OnMessageClose;
						Answers.LogCustom (
							"couldntAffordPong"
							);
		} else {

			PongUnlockYESNODIALOG.SetActive(true);
						Answers.LogCustom (
							"boughtPongCredits"
							);
			//			IOSDialog dialog = IOSDialog.Create (unlockPongString, areyouSurePong);
			//			dialog.OnComplete += onDialogClosePong;
		}


	}

	private  void UnlockProductsAd(string productIdentifier) {

		PlayerPrefs.SetInt("Ads",1);
		removeAdsBttn.transform.GetChild (1).GetComponent<Button>().enabled =false;
		removeAdsBttn.transform.GetChild (1).GetComponent<Image>().color =Color.gray;
		removeAdsBttn.transform.GetChild (1).GetChild(0).GetComponent<Text>().color =Color.gray;
		removeAdsBttn.transform.GetChild (1).gameObject.GetComponent<adjustTextBtnC>().enabled =false;


	}

	private  void UnlockProductsTntLevel(string productIdentifier) {

		PlayerPrefs.SetInt("TNTLevel",1);
		checkLevelsPurchased();

	}
	private  void UnlockProductsPongLevel(string productIdentifier) {

		PlayerPrefs.SetInt("PongLevel",1);
		checkLevelsPurchased();

	}

	//	private  void OnPurchaseFlowFinishedAction (UM_PurchaseResult response) {
	//		UM_InAppPurchaseManager.Client.OnPurchaseFinished -= OnPurchaseFlowFinishedAction;
	//
	//		
	//		if (response.isSuccess) {
	////			Chartboost.trackInAppGooglePlayPurchaseEvent(
	////				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).DisplayName, 
	////				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).Description, 
	////				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).ActualPrice, 
	////				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).CurrencyCode,response.product.id,response.product.id,response.product.id);
	//			Answers.LogPurchase(
	//				(decimal?)UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).ActualPriceValue,
	//				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).CurrencyCode,
	//				response.isSuccess,
	//				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).DisplayName,
	//				UM_InAppPurchaseManager.GetProductByAmazonId(response.product.id).Type.ToString(),
	//				response.product.id
	//			);
	//
	//			for (int i = 0; i < purchaseString.Length; i++) {
	//			
	//				if (response.product.id == purchaseString [i]) {
	//					PlayerPrefs.SetInt (i.ToString ("f0"), 1);
	//				
	//					//this is to enable gameover guy that is purchased
	//					if (isPurchasingGameOver) {
	//						isPurchasingGameOver = false;
	//						PlayerPrefs.SetInt (i.ToString ("f0"), 1);
	//						PlayerPrefs.SetInt ("Character", i);
	//						turnOffBuyGuyCam ();
	//						resetScriptStart ();
	//
	//					}
	//
	//					if (i == 64) {
	//						if (PlayerPrefs.HasKey ("Restored")) {
	//						
	//						} else {
	//							//GIVE 1000 FOR GALACTIC ON RESTORE
	//							PlayerPrefs.SetInt ("Restored", 1);
	//							PlayerPrefs.SetInt ("myCredits", PlayerPrefs.GetInt ("myCredits") + 1000);
	//							//				GameObject credits = GameObject.Find("Credits UI");
	//							creditsUI.text = PlayerPrefs.GetInt ("myCredits").ToString ("f0");
	//							StartCoroutine (coinShower ());
	//						}
	//
	//					}
	//
	//
	//				}
	//
	//
	//			}
	//
	//			for (int j = 0; j < purchaseHandString.Length; j++) {
	//				
	//				if (response.product.id == purchaseHandString [j]) {
	//					PlayerPrefs.SetInt ("hand" + j, 1);
	//					if (isPurchasingGameOverHands) {
	//						isPurchasingGameOverHands = false;
	//						PlayerPrefs.SetInt ("Hand", j);
	//						turnOffBuyGuyCam ();
	//
	//						resetScriptStart ();
	//
	//
	//					}
	//					
	//				}
	//			}
	//
	//			for (int k = 0; k < purchaseFeetString.Length; k++) {
	//				
	//				if (response.product.id == purchaseFeetString [k]) {
	//					PlayerPrefs.SetInt ("shoe" + k, 1);
	//					if (isPurchasingGameOverFeet) {
	//						isPurchasingGameOverFeet = false;
	//						PlayerPrefs.SetInt ("Shoe", k);
	//						turnOffBuyGuyCam ();
	//
	//						resetScriptStart ();
	//
	//					}
	//				}
	//			}
	//
	//			for (int l = 0; l < purchaseHatString.Length; l++) {
	//				
	//				if (response.product.id == purchaseHatString [l]) {
	//					PlayerPrefs.SetInt ("hat" + l, 1);
	//					if (isPurchasingGameOverHats) {
	//						isPurchasingGameOverHats = false;
	//						PlayerPrefs.SetInt ("Hat", l);
	//						turnOffBuyGuyCam ();
	//
	//						resetScriptStart ();
	//
	//					}
	//					
	//				}
	//			}
	//
	//			if (response.product.id == "removeads") {
	//				PlayerPrefs.SetInt ("Ads", 1);
	//				removeAdsBttn.transform.GetChild (1).GetComponent<Button> ().enabled = false;
	//				removeAdsBttn.transform.GetChild (1).GetComponent<Image> ().color = Color.gray;
	//				removeAdsBttn.transform.GetChild (1).GetChild (0).GetComponent<Text> ().color = Color.gray;
	//				removeAdsBttn.transform.GetChild (1).gameObject.GetComponent<adjustTextBtnC> ().enabled = false;
	//
	//
	//			}
	//			if (response.product.id  == "tntmode") {
	//				PlayerPrefs.SetInt ("TNTLevel", 1);
	//				checkLevelsPurchased ();
	//			}
	//			if (response.product.id  == "buddypong") {
	//				PlayerPrefs.SetInt ("PongLevel", 1);
	//				checkLevelsPurchased ();
	//			}
	//
	//
	//			//MAKES SURE WE ARE IN THE MAIN MENU WHEN BUYING THESE CHARACTERS
	//			if (isInMenu) {
	//
	//
	//				if (characterButton.interactable == false) {
	//					UnlockProducts (response.product.id , tempPurchased);
	//					print ("I JUST BOUGHT A  CHARACTER IN THE IAP SCRIPT");
	//
	//				}
	//				if (handButton.interactable == false) {
	//					UnlockProductsHand (response.product.id , tempPurchasedHand);
	//
	//				}
	//				if (shoeButton.interactable == false) {
	//					UnlockProductsHand (response.product.id , tempPurchasedShoe);
	//				}
	//				if (hatButton.interactable == false) {
	//					UnlockProductsHat (response.product.id , tempPurchasedHat);
	//				}
	//
	//			}
	//
	//			if (adBuyBool) {
	//				UnlockProductsAd (response.product.id );
	//			}
	//			if (TntLevelBool) {
	//				UnlockProductsTntLevel (response.product.id );
	//			}
	//			if (PongLevelBool) {
	//				UnlockProductsPongLevel (response.product.id );
	//			}
	//
	//		} 
	//
	//			
	//			//reenables allbuttons to be used after purchasing is complete
	//			isPurchasing = false;
	//			toggleCharacterBttns (isPurchasing);
	//			
	//		
	//			if (response.isSuccess) {
	//				turnoffPurchaseBtn ();
	//				turnonSelectBtn ();
	//			}
	////			blockScreen.SetActive(false);
	//
	//		
	//
	//	}



	//		private  void onRestoreTransactionFailed() {
	//			IOSNativePopUpManager.showMessage("Fail", "Restore Failed");
	//		}


	//	static void HandleOnVerificationComplete (IOSStoreKitVerificationResponse response) {
	//		IOSNativePopUpManager.showMessage("Verification", "Transaction verification status: " + response.status.ToString());
	//		
	//		Debug.Log("ORIGINAL JSON: " + response.originalJSON);
	//	}




	public void resetPrefs(){
		PlayerPrefs.DeleteAll();

	}

	private  void OnRestoreComplete (IOSStoreKitRestoreResult res) {
		if(res.IsSucceeded) {
			IOSNativePopUpManager.showMessage(successString, restoreCompleteString);
		} else {
			//			IOSNativePopUpManager.showMessage(errorString+": " + res.Error.Code, res.Error.Description);
		}
	}



	public void restorePurchase(){
		//		UM_InAppPurchaseManager.Client.OnPurchaseFinished += OnPurchaseFlowFinishedAction;

		//		UM_InAppPurchaseManager.Client.RestorePurchases();
		//		UM_InAppPurchaseManager.Client.Purchase(purchaseString[charact]);
		RestorePurchases();
	}



	//	public void FREESTUFF(){
	//	for(int i = 0; i < purchaseString.Length; i++){
	//		
	//		
	//			PlayerPrefs.SetInt(i.ToString("f0"),1);
	//			
	//		
	//		
	//		
	//	}
	//	
	//	for(int j = 0; j < purchaseHandString.Length; j++){
	//		
	//
	//			PlayerPrefs.SetInt("hand"+j,1);
	//			
	//		
	//	}
	//	
	//	for(int k = 0; k < purchaseFeetString.Length; k++){
	//		
	//
	//			PlayerPrefs.SetInt("shoe"+k,1);
	//			
	//		
	//	}
	//	
	//	for(int l = 0; l < purchaseHatString.Length; l++){
	//		
	//
	//			PlayerPrefs.SetInt("hat"+l,1);
	//			
	//		
	//		}
	//	}


	IEnumerator shareScreenCharacterMenu(){
		yield return new WaitForEndOfFrame();
		Texture2D shotSqr = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);

		shotSqr.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);



		UM_ShareUtility.ShareMedia("Share Buddy Toss",characterName[charact]  +" #buddytoss", shotSqr);
	}

	IEnumerator shareFreeGiveAwayMenu(){
		yield return new WaitForEndOfFrame();
		Texture2D shotSqr = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
		GameObject freeCharactName = GameObject.Find ("FreeCharactName");
		shotSqr.ReadPixels (new Rect (0, 0, Screen.width, Screen.height), 0, 0, false);



		UM_ShareUtility.ShareMedia("Share Buddy Toss",freeCharactName.GetComponent<Text>().text  +" #buddytoss", shotSqr);
	}




	private void onDialogCloseTnt(IOSDialogResult result) {

		//parsing result
		switch(result) {
		case IOSDialogResult.YES:
			Debug.Log ("Yes button pressed");

			break;
		case IOSDialogResult.NO:
			//NO IS YES FOR SOME REASON
			Debug.Log ("No button pressed");

			if (PlayerPrefs.GetInt ("myCredits") >= 1000) {
				PlayerPrefs.SetInt ("TNTLevel", 1);
				PlayerPrefs.SetInt ("myCredits",PlayerPrefs.GetInt ("myCredits")-1000);
				GameObject credits = GameObject.Find("Credits UI");
				credits.GetComponent<Text>().text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
				checkLevelsPurchased ();
			}
			else {


			}
			break;

		}

		//		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	public void IWantToUnlockTNT(){
		if (PlayerPrefs.GetInt ("myCredits") >= 1000) {
			PlayerPrefs.SetInt ("TNTLevel", 1);
			PlayerPrefs.SetInt ("myCredits",PlayerPrefs.GetInt ("myCredits")-1000);
			GameObject credits = GameObject.Find("Credits UI");
			credits.GetComponent<Text>().text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
			checkLevelsPurchased ();
			TNTUnlockYESNODIALOG.SetActive(false);
			TNTUnlockYESNO.SetActive(false);

		}
	}

	public void IWantToUnlockPong(){
		if (PlayerPrefs.GetInt ("myCredits") >= 1000) {
			PlayerPrefs.SetInt ("PongLevel", 1);
			PlayerPrefs.SetInt ("myCredits", PlayerPrefs.GetInt ("myCredits") - 1000);
			GameObject credits = GameObject.Find("Credits UI");
			credits.GetComponent<Text>().text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
			checkLevelsPurchased ();
			PongUnlockYESNODIALOG.SetActive(false);

			PongUnlockYESNO.SetActive(false);

		} 
	}

	private void onDialogClosePong(IOSDialogResult result) {

		//parsing result
		switch(result) {
		case IOSDialogResult.YES:
			Debug.Log ("No button pressed");


			break;
		case IOSDialogResult.NO:
			Debug.Log ("Yes button pressed");
			if (PlayerPrefs.GetInt ("myCredits") >= 1000) {
				PlayerPrefs.SetInt ("PongLevel", 1);
				PlayerPrefs.SetInt ("myCredits", PlayerPrefs.GetInt ("myCredits") - 1000);
				GameObject credits = GameObject.Find("Credits UI");
				credits.GetComponent<Text>().text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
				checkLevelsPurchased ();

			} 
			else {


			}
			break;

		}

		//		IOSNativePopUpManager.showMessage("Result", result.ToString() + " button pressed");
	}

	public void add1000Credits(){

		PlayerPrefs.SetInt ("myCredits", PlayerPrefs.GetInt ("myCredits") + 1000);
		GameObject credits = GameObject.Find("Credits UI");
		credits.GetComponent<Text>().text = PlayerPrefs.GetInt ("myCredits").ToString("f0");
	}

	public void isInMenuFunc(bool mybool){
		isInMenu = mybool;
	}


	void OnEnable(){

		ReceiveImage.passBytes += this.OnImageLoad;


	}

	void OnDisable(){

		ReceiveImage.passBytes -= this.OnImageLoad;

	}

	void OnImageLoad(byte[] mybytes)
	{
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(mybytes);
		faceTexture = tex;



		faceMaterial.mainTexture = faceTexture;
		faceMaterial.mainTexture=faceTexture;

		File.WriteAllBytes (Application.persistentDataPath + "/myFaceBT.png", mybytes);


		//		GameObject.Find("Cube").GetComponent<Renderer>().material.mainTexture = tex;
		//		log += "\nImage Saved to gallery, loaded :" + path;
	}


	public void startFDCam(){
		#if UNITY_IOS

		FDViewController.PhotoCamPicker();
		#endif

		#if UNITY_ANDROID

		UM_Camera.Instance.OnImagePicked += OnImage;
		UM_Camera.Instance.GetImageFromGallery();
		#endif

	}

	private void OnImage (UM_ImagePickResult result) {
		if(result.IsSucceeded) {

			faceTexture = result.image;

			faceMaterial.mainTexture = faceTexture;
			faceMaterial.mainTexture=faceTexture;

			File.WriteAllBytes (Application.persistentDataPath + "/myFaceBT.png", faceTexture.EncodeToPNG());
			UM_Camera.Instance.OnImagePicked -= OnImage;

		}
	}

	public void shareGame(){
		UM_ShareUtility.ShareMedia("Share Buddy Toss","Buddy Toss by Hunter Games LLC https://appsto.re/us/mYuB6.i");
	}

	private void OnBillingConnectFinishedAction (UM_BillingConnectionResult result) {
		UM_InAppPurchaseManager.Client.OnServiceConnected -= OnBillingConnectFinishedAction;
		if(result.isSuccess) {
			Debug.Log("Connected");
		} else {
			Debug.Log("Failed to connect");
		}
	}  

	private void OnConnectFinished(UM_BillingConnectionResult result) {

		if(result.isSuccess) {
			//			UM_ExampleStatusBar.text = "Billing init Success";
		} else  {
			//			UM_ExampleStatusBar.text = "Billing init Failed";
		}
	}

	private void OnMessageClose() {

		//		new MobileNativeMessage("Result", "Message Closed");
	}

}

