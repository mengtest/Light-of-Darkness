using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {

    public static CursorManager instance;

    public Texture2D cursor_normal;
    public Texture2D cursor_npcTalk;
    public Texture2D cursor_attack;
    public Texture2D cursor_lockTarget;
    public Texture2D cursor_pick;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNormal()
    {
        Cursor.SetCursor(cursor_normal,hotspot,mode);
    }

    public void SetNPCTalk()
    {
        Cursor.SetCursor(cursor_npcTalk, hotspot, mode);
    }
}
