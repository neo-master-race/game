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

@Header ("Textures")
var confirm_over:Texture;
var confirm_notover:Texture;
var back_over:Texture;
var back_notover:Texture;

@Header ("GameObjects")
var register_button:GameObject;
var login_button:GameObject;
var confirm_button:GameObject;
var back_arrow:GameObject;
@Space(5)
var GOinput_un:GameObject;
var GOinput_pw:GameObject;
var GOinput_pwrep:GameObject;
var GOloginput_un:GameObject;
var GOloginput_pw:GameObject;
var log_reg_canvas:GameObject;
var after_canvas:GameObject;




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


function OnPointerEnter_GUI () {
	switch(this.name)
	{
		case "Confirm_Text":
		case "Register_Text":
		case "Login_Text":
		case "Login_After_Text":
			this.transform.parent.GetComponent(RawImage).texture=confirm_over;
			break;
		case "Back_Text":
			GameObject.Find("Back_Button").GetComponent(RawImage).texture=back_over;
			GameObject.Find("Back_Text").GetComponent(Text).text="BACK";
			break;
	}
}

function OnPointerExit_GUI () {
	switch(this.name)
	{
		case "Confirm_Text":
		case "Register_Text":
		case "Login_Text":
		case "Login_After_Text":
			this.transform.parent.GetComponent(RawImage).texture=confirm_notover;
			break;
		case "Back_Text":
			GameObject.Find("Back_Button").GetComponent(RawImage).texture=back_notover;
			GameObject.Find("Back_Text").GetComponent(Text).text="";
			break;
	}
}

function OnPointerClick_GUI () {
	switch(this.name)
	{
		case "Confirm_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=1;
			break;

		case "Login_Text":
		case "Login_After_Text":
			GameObject.Find("Script_Source").GetComponent(Register_login).user=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).pass=null;
			GameObject.Find("Script_Source").GetComponent(Register_login).passrep=null;
			after_canvas.SetActive(false);
			log_reg_canvas.SetActive(true);
			GOloginput_un.SetActive(true);
			GOloginput_pw.SetActive(true);
			register_button.SetActive(false);
			login_button.SetActive(false);
			confirm_button.SetActive(true);
			back_arrow.SetActive(true);

			GameObject.Find("Username_Text_Log").GetComponent(Text).enabled=true;
			GameObject.Find("Password_Text_Log").GetComponent(Text).enabled=true;

			GameObject.Find("Title_Log_Reg").GetComponent(Text).text="LOGIN";

			GameObject.Find("Script_Source").GetComponent(Register_login).start_action="Login";
			break;

		case "Register_Text":
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
			confirm_button.SetActive(true);
			back_arrow.SetActive(true);

			GameObject.Find("Username_Text").GetComponent(Text).enabled=true;
			GameObject.Find("Password_Text").GetComponent(Text).enabled=true;
			GameObject.Find("PasswordRe_Text").GetComponent(Text).enabled=true;

			GameObject.Find("Title_Log_Reg").GetComponent(Text).text="REGISTER";

			GameObject.Find("Script_Source").GetComponent(Register_login).start_action="Register";
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
			confirm_button.SetActive(false);
			back_arrow.SetActive(false);

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
	if((Input.GetKeyDown(KeyCode.Return) || GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start==1)
	&& GameObject.Find("Script_Source").GetComponent(Register_login).start_action=="Register")
    {
    	GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=0;
    	if(GameObject.Find("Script_Source").GetComponent(Register_login).user==null)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "You need to type a username to register";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
			input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).user.length>20)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Username length can't exceed 20 characters";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.text = "";
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	/*else if(GameObject.Find("Script_Source").GetComponent(Register_login).user=="Create a Toon")
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "This username is invalid";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.GetComponent(InputField).ActivateInputField();
    	}*/
    	else if("♫" in user)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Username cannot contain this symbol: ♫";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).user.length<3)
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Username length require at least 3 characters";
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_un.text = "";
    		input_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass==null)
    	{
    		GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).text = "You need to type a password to register";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass.length<6)
    	{
    		GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).text = "Password length require at least 6 characters";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass!=passrep)
    	{
    		GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = true;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).text = "The two passwords doesn't match";
			GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
    		input_pw.text = "";
    		input_pwrep.text = "";
    		input_pw.GetComponent(InputField).ActivateInputField();
    	}
    	else
    	{
    		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("Password_Text_Fail").GetComponent(Text).enabled = false;
			GameObject.Find("PasswordRe_Text_Fail").GetComponent(Text).enabled = false;
    		towwwsign();
    	}
    }
    if((Input.GetKeyDown(KeyCode.Return) || GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start==1)
	&& GameObject.Find("Script_Source").GetComponent(Register_login).start_action=="Login")
    {
    	GameObject.Find("Script_Source").GetComponent(Register_login).confirm_start=0;
    	if(GameObject.Find("Script_Source").GetComponent(Register_login).user==null || GameObject.Find("Script_Source").GetComponent(Register_login).user.length==0)
    	{
    		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = true;
			GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).text = "You need to type your username to log in";
			GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = false;
			loginput_un.GetComponent(InputField).ActivateInputField();
    	}
    	else if(GameObject.Find("Script_Source").GetComponent(Register_login).pass==null || GameObject.Find("Script_Source").GetComponent(Register_login).pass.length==0)
    	{
    		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).enabled = true;
			GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).text = "You need to type your password to log in";
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
	form.AddField("username",GameObject.Find("Script_Source").GetComponent(Register_login).user);
    form.AddField("password",GameObject.Find("Script_Source").GetComponent(Register_login).pass);
    form.AddField("passwordrep",GameObject.Find("Script_Source").GetComponent(Register_login).passrep);
	after_canvas.SetActive(true);
	GameObject.Find("Success_Register").GetComponent(Text).enabled=true;
	GameObject.Find("Success_Login").GetComponent(Text).enabled=false;
	GameObject.Find("Success_Register").GetComponent(Text).text="Creating your account...";
	log_reg_canvas.SetActive(false);
    var www: WWW = new WWW("http://www.theserazai1.fr/php_files/Toontown/register_account.php",form);
    yield www;
	var log2:String= www.text;
	if(log2=="Pseudo Already Used")
	{
		after_canvas.SetActive(false);
		log_reg_canvas.SetActive(true);
		input_un.text = "";
		GameObject.Find("Username_Text_Fail").GetComponent(Text).enabled = true;
		GameObject.Find("Username_Text_Fail").GetComponent(Text).text = "Username is already used";
		input_un.GetComponent(InputField).ActivateInputField();
	}
	else
	{
		signform.AddField("username",user);
		signform.AddField("password",pass);
		var account_creating: WWW = new WWW("http://www.theserazai1.fr/php_files/Toontown/successful_register.php",signform);
    	yield account_creating;
    	after_canvas.SetActive(true);
		GameObject.Find("Success_Register").GetComponent(Text).text="You have successfully registered\nto Toontown Rewritten V2.0";
		
		
		log_reg_canvas.SetActive(true);
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
		confirm_button.SetActive(false);
		back_arrow.SetActive(false);
		log_reg_canvas.SetActive(false);
		login_button.SetActive(false);
		register_button.SetActive(false);
	}
}

function towwwlog() {
	form.AddField("username",GameObject.Find("Script_Source").GetComponent(Register_login).user);
    form.AddField("password",GameObject.Find("Script_Source").GetComponent(Register_login).pass);
	after_canvas.SetActive(true);
	GameObject.Find("Success_Register").GetComponent(Text).enabled=false;
	GameObject.Find("Success_Login").GetComponent(Text).enabled=true;
	GameObject.Find("Success_Login").GetComponent(Text).text="Connecting...";
	log_reg_canvas.SetActive(false);
    var www: WWW = new WWW("http://www.theserazai1.fr/php_files/Toontown/Login.php",form);
    yield www;
	var log2:String= www.text;
	
	if(log2=="username not found")
	{
		after_canvas.SetActive(false);
		log_reg_canvas.SetActive(true);
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = true;
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).text = "This username was not found";
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
		GameObject.Find("Password_Text_Fail_Log").GetComponent(Text).text = "The password you entered is incorrect";
		GameObject.Find("Username_Text_Fail_Log").GetComponent(Text).enabled = false;
		loginput_un.text="";
		loginput_un.GetComponent(InputField).ActivateInputField();
		loginput_pw.text="";
		loginput_pw.GetComponent(InputField).ActivateInputField();
	}
	else if(log2=="password valid")
	{
		after_canvas.SetActive(true);
		GameObject.Find("Success_Register").GetComponent(Text).enabled=false;
		GameObject.Find("Success_Login").GetComponent(Text).enabled=true;
		GameObject.Find("Success_Login").GetComponent(Text).text="You have successfully logged in\nYou will enter the Toon World";
		log_reg_canvas.SetActive(false);
    	yield WaitForSeconds(5.0);
    	//SceneManager.LoadScene("Choose_Toon");
	}
}
