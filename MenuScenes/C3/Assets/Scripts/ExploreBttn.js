
//transform.position = Vector3(Screen.height-GameObject.render.bounds.size(), 0, 0);
//transform.position = Vector3(0, 0, 0);
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