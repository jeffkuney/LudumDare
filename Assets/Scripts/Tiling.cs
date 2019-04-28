using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2; //the offset for errors

    //these check if need an extension
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;

    public bool reverseScale = false; // used if the object is not able to be tiled

    private float spriteWidth = 0f; //the width of the element
    private Camera cam;
    private Transform myTransform;

    void Awake () {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start() {
        SpriteRenderer sRenderer = GetComponent <SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update() {
        //does it still need buddies, if not do nothing
        if (hasALeftBuddy == false || hasARightBuddy == false ) {
            //calculate the cameras extend of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

            //calculate the X position where the camera can see the edge of the sprite
            float edgeVisablePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
            float edgeVisablePositionLeft = (myTransform.position.x - spriteWidth/2) - camHorizontalExtend;

            //checking if we can see the edge of the element, and then calling MakeNewBuddy
            if (cam.transform.position.x >= edgeVisablePositionRight - offsetX && hasARightBuddy == false) {
                MakeNewBuddy (1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisablePositionLeft + offsetX && hasALeftBuddy == false) {
                MakeNewBuddy (-1);
                hasALeftBuddy = true;
            }   
        }

    }

    //this is the function that creates a buddy on the side required
    void MakeNewBuddy (int rightOrLeft) {


//
//There is a major issue here, someone suggested not scaling the images imported, so get some new images
//
           
        //calculating the new position for our new buddy
        //Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
          Vector3 newPosition = new Vector3 (myTransform.position.x + myTransform.localScale.x*spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z); //comment update
        
     
        //instantating our new buddy and storing him in a variable
        Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

        //if not able to tile let's reverse the x size of the object to get rid of ugly seams
        if (reverseScale == true) {
            newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent; //not sure if this is working
        if (rightOrLeft > 0) {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }

    }
}
