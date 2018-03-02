//Schneberger Maxime
//XX-XX-16

#pragma strict

import UnityEngine.UI;
private var form: WWWForm;
private var signform: WWWForm;

private var user:String;
private var pass:String;
private var passrep:String;

private var confirm_start:int=0;
private var start_action:String="";

@Header ("InputFields")
var input_un:InputField;
var input_pw:InputField;
var input_pwrep:InputField;
var loginput_un:InputField;
var loginput_pw:InputField;

@Header ("GameObjects")
var register_button:GameObject;
var login_button:GameObject;
var play_as_guest_button:GameObject;
@Space(5)
var confirm_button:GameObject;
var back_arrow:GameObject;
@Space(10)
var GOinput_un:GameObject;
var GOinput_pw:GameObject;
var GOinput_pwrep:GameObject;
var GOloginput_un:GameObject;
var GOloginput_pw:GameObject;
var log_reg_canvas:GameObject;
var after_canvas:GameObject;



//Schneberger Maxime
//Initialisation des formulaires de connection et d'inscription
function Start () {
	form= new WWWForm(); //nouveau formulaire
	signform= new WWWForm();
	input_un.GetComponent(InputField);
	input_un.GetComponent(InputField).ActivateInputField();
	input_pw.GetComponent(InputField);
	input_pw.GetComponent(InputField).ActivateInputField();
	input_pwrep.GetComponent(InputField);
	input_pwrep.GetComponent(InputField).ActivateInputField();
	loginput_un.GetComponent(InputField);
	loginput_un.GetComponent(InputField).ActivateInputField();
	loginput_pw.GetComponent(InputField);
	loginput_pw.GetComponent(InputField).ActivateInputField();
}

function go_to_menu(message:String)
{
	after_canvas.SetActive(true);
	GameObject.Find("Success_Register").GetComponent(Text).enabled=false;
	GameObject.Find("Success_Login").GetComponent(Text).enabled=true;
	GameObject.Find("Success_Login").GetComponent(Text).text=message;
	log_reg_canvas.SetActive(false);
    yield WaitForSeconds(5.0);
    SceneManagement.SceneManager.LoadScene("circuit_test");
}

//Schneberger Maxime
//Quand l'utilisateur clique sur un bouton connection ou inscription, on exécute le bon formulaire à afficher
function OnPointerClick_GUI () {
	switch(this.name)
	{
		case "Confirm_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=1;
			break;

		case "Log_In_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).user=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).pass=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).passrep=null;
			after_canvas.SetActive(false);
			log_reg_canvas.SetActive(true);
			GOloginput_un.SetActive(true);
			GOloginput_pw.SetActive(true);

			register_button.SetActive(false);
			login_button.SetActive(false);
			play_as_guest_button.SetActive(false);

			confirm_button.SetActive(true);
			back_arrow.SetActive(true);

			GameObject.Find("Username_Text_Log").GetComponent(Text).enabled=true;
			GameObject.Find("Password_Text_Log").GetComponent(Text).enabled=true;

			GameObject.Find("Title_Log_Reg").GetComponent(Text).text="CONNECTION";

			GameObject.Find("Script_Source").GetComponent(Register_login).start_action="Login";

			GameObject.Find("Back_Button").GetComponent(RawImage).color = new Color(255, 0, 0);
			break;

		case "Sign_Up_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).user=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).pass=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).passrep=null;
			after_canvas.SetActive(false);
			log_reg_canvas.SetActive(true);
			GOinput_un.SetActive(true);
			GOinput_pw.SetActive(true);
			GOinput_pwrep.SetActive(true);

			register_button.SetActive(false);
			login_button.SetActive(false);
			play_as_guest_button.SetActive(false);

			confirm_button.SetActive(true);
			back_arrow.SetActive(true);

			GameObject.Find("Username_Text").GetComponent(Text).enabled=true;
			GameObject.Find("Password_Text").GetComponent(Text).enabled=true;
			GameObject.Find("PasswordRe_Text").GetComponent(Text).enabled=true;

			GameObject.Find("Title_Log_Reg").GetComponent(Text).text="INSCRIPTION";

			GameObject.Find("Script_Source").GetComponent(Register_login).start_action="Register";

			GameObject.Find("Back_Button").GetComponent(RawImage).color = new Color(255, 0, 0);
			break;

		case "Play_As_Guest_Text":
			register_button.SetActive(false);
			login_button.SetActive(false);
			play_as_guest_button.GetComponent(RawImage).enabled=false;
			GameObject.Find("Play_As_Guest_Text").GetComponent(Text).enabled=false;
			go_to_menu("Vous allez être redirigé vers le menu\nen tant qu'invité");
			break;

		case "Back_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).user=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).pass=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).passrep=null;
			after_canvas.SetActive(false);
			GameObject.Find("Username_Text_Log").GetComponent(Text).enabled=false;
			GameObject.Find("Password_Text_Log").GetComponent(Text).enabled=false;
			GameObject.Find("Username_Text").GetComponent(Text).enabled=false;
			GameObject.Find("Password_Text").GetComponent(Text).enabled=false;
			GameObject.Find("PasswordRe_Text").GetComponent(Text).enabled=false;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled=false;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled=false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled=false;
			GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = false;
			GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = false;
			log_reg_canvas.SetActive(false);
			GOloginput_un.SetActive(false);
			GOloginput_pw.SetActive(false);
			GOinput_un.SetActive(false);
			GOinput_pw.SetActive(false);
			GOinput_pwrep.SetActive(false);

			register_button.SetActive(true);
			login_button.SetActive(true);
			play_as_guest_button.SetActive(true);

			confirm_button.SetActive(false);
			back_arrow.SetActive(false);

			GameObject.Find("Log_In").GetComponent(RawImage).color = new Color(255, 255, 255);
			GameObject.Find("Sign_Up").GetComponent(RawImage).color = new Color(255, 255, 255);
			GameObject.Find("Play_As_Guest").GetComponent(RawImage).color = new Color(255, 255, 255);
			break;
	}
}

function GetInputUser(x:String){
	GameObject.Find("Script_Source").GetComponent(Register_login).user=x;
}

function GetInputPass(y:String){
	GameObject.Find("Script_Source").GetComponent(Register_login).pass=y;
}

function GetInputPassrep(z:String){
	GameObject.Find("Script_Source").GetComponent(Register_login).passrep=z;
}

function Update () {
	if(this.name=="Script_Source" && (Input.GetKeyDown(KeyCode.Return) || GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start==1)
	&& GameObject.Find("Script_Source").GetComponent(Register_login).start_action=="Register")
    {
    	GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=0;
    	if(GameObject.Find("Script_Source").GetComponent(Register_login).user.length==0)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Vous devez renseigner un nom d'utilisateur";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
			input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).user.length>20)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "La longueur du nom d'utilisateur\nne peut excéder 20 caractères";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.text = "";
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if("♫" in user)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Le nom d'utilisateur ne peut contenir le symbole ♫";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).user.length<3)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "La longueur du nom d'utilisateur doit\nêtre d'au moins 3 caractères";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.text = "";
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass.length==0)
    	{
    		GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).text = "Vous devez renseigner un mot de passe";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass.length<6)
    	{
    		GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).text = "La longueur du mot de passe doit\nêtre d'au moins 6 caractères";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass!=passrep)
    	{
    		GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).text = "La répétition du mot de passe ne correspond pas au premier";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else
    	{
			Debug.Log(GameObject.Find("Script_Source").GetComponent(Register_login).pass);
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		towwwsign();
    	}
    }
    if(this.name=="Script_Source" && (Input.GetKeyDown(KeyCode.Return) || GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start==1)
	&& GameObject.Find("Script_Source").GetComponent(Register_login).start_action=="Login")
    {
    	GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=0;
    	if(GameObject.Find("Script_Source").GetComponent(Register_login).user==null || GameObject.Find("Script_Source").GetComponent(Register_login).user.length==0)
    	{
    		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).text = "Vous devez renseigner un nom d'utilisateur";
			GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = false;
			loginput_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass==null || GameObject.Find("Script_Source").GetComponent(Register_login).pass.length==0)
    	{
    		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).text = "Vous devez renseigner un mot de passe";
			GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = false;
			loginput_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else
    	{
    		towwwlog();
    	}
    }
}

function towwwsign() {
	/*form.AddField("username",GameObject.Find("Script_Source").GetComponent(Register_login).user);
    form.AddField("password",GameObject.Find("Script_Source").GetComponent(Register_login).pass);
    form.AddField("passwordrep",GameObject.Find("Script_Source").GetComponent(Register_login).passrep);*/
	after_canvas.SetActive(true);
	GameObject.Find("Success_Register").GetComponent(Text).enabled=true;
	GameObject.Find("Success_Login").GetComponent(Text).enabled=false;
	GameObject.Find("Success_Register").GetComponent(Text).text="Création de votre compte...";
	log_reg_canvas.SetActive(false);
	yield WaitForSeconds(1.0);
    /*var www: WWW = new WWW("http://www.theserazai1.fr/php_files/Toontown/register_account.php",form);
    yield www;
	var log2:String= www.text;
	if(log2=="Pseudo Already Used")
	{
		after_canvas.SetActive(false);
		log_reg_canvas.SetActive(true);
		input_un.text = "";
		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
		GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Ce nom d'utilisateur est déjà utilisé";
		input_un.GetComponent(InputField).ActivateInputField();
	}
	else
	{*/
		go_to_menu("Vous vous êtes correctement inscrit\nVous allez être redirigé vers le menu");
	//}
}

function towwwlog() {
	/*form.AddField("username",GameObject.Find("Script_Source").GetComponent(Register_login).user);
    form.AddField("password",GameObject.Find("Script_Source").GetComponent(Register_login).pass);*/
	after_canvas.SetActive(true);
	GameObject.Find("Success_Register").GetComponent(Text).enabled=false;
	GameObject.Find("Success_Login").GetComponent(Text).enabled=true;
	GameObject.Find("Success_Login").GetComponent(Text).text="Connection...";
	log_reg_canvas.SetActive(false);
   /*var www: WWW = new WWW("http://www.theserazai1.fr/php_files/Toontown/Login.php",form);
    yield www;
	var log2:String= www.text;
	
	if(log2=="username not found")
	{
		after_canvas.SetActive(false);
		log_reg_canvas.SetActive(true);
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = true;
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).text = "Ce nom d'utilisateur n'existe pas";
		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = false;
		loginput_un.text="";
		loginput_un.GetComponent(InputField).ActivateInputField();
		loginput_pw.text="";
		loginput_pw.GetComponent(InputField).ActivateInputField();
	}
	else if(log2=="password not valid")
	{
		after_canvas.SetActive(false);
		log_reg_canvas.SetActive(true);
		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = true;
		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).text = "Le mot de passe est incorrect pour cet utilisateur";
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = false;
		loginput_un.text="";
		loginput_un.GetComponent(InputField).ActivateInputField();
		loginput_pw.text="";
		loginput_pw.GetComponent(InputField).ActivateInputField();
	}
	else if(log2=="password valid")
	{*/
		go_to_menu("Vous vous êtes correctement identifié\nVous allez être redirigé vers le menu");
	//}
}
