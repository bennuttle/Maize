

function OnMouseUp(){

//load level
Application.LoadLevel(1);

}

function OnMouseHover(){
Debug.Log("Hovering Explore");
}

function Update(){
//quit game if escape key is pressed
if (Input.GetKey(KeyCode.Escape)) { Application.Quit();
}
}