using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
	private void Start()
	{
		//Cursor.visible = false;
	}
	void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(cursorPos.x, cursorPos.y);
    }
}
