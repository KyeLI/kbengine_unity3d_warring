using UnityEngine;
using KBEngine;
using System; 
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class selavatar_ui : MonoBehaviour {
	public static bool started = false;
	public static int windowsize = 0;
	public static string currsel_item = "item1";
	public static string currsel_selListItem = "sel_list_item_focus1";
	public static bool isCreateMode = false;
	private static bool[] showAvatars = new bool[3]{false, false, false};
	private static string[] avatarnames = new string[]{"德古拉", "狼骑", "血精灵", "萨满", "尖刺兽", "暗影"};
	public static Byte changeAvatarItem = 0;
	public static UILabel log_label = null;
	public static UILabel loadAvatar_label = null;
	
	public struct AvatarInfo
	{
		public UInt16 level;
		public string name;
		public Byte roleType;
		public UInt64 dbid;
	}
	
	private static List<AvatarInfo> avatarList = new List<AvatarInfo>();
	
	void reset()
	{
		windowsize = 0;
		currsel_item = "item1";
		currsel_selListItem = "sel_list_item_focus1";
		isCreateMode = false;
	}
	
	void Awake ()     
	{
		log_label = UnityEngine.GameObject.Find("log_label").GetComponent<UILabel>();
		loadAvatar_label = UnityEngine.GameObject.Find("loadAvatar_label").GetComponent<UILabel>();
		
		//autoSetPosition();
		
		UnityEngine.GameObject avataritem1_focus_trigger = UnityEngine.GameObject.Find("item1_focus");
		UnityEngine.GameObject avataritem2_focus_trigger = UnityEngine.GameObject.Find("item2_focus");
		UnityEngine.GameObject avataritem3_focus_trigger = UnityEngine.GameObject.Find("item3_focus");
		UnityEngine.GameObject avataritem4_focus_trigger = UnityEngine.GameObject.Find("item4_focus");
		UnityEngine.GameObject avataritem5_focus_trigger = UnityEngine.GameObject.Find("item5_focus");
		UnityEngine.GameObject avataritem6_focus_trigger = UnityEngine.GameObject.Find("item6_focus");
		
		UnityEngine.GameObject skillitem1_focus_trigger = UnityEngine.GameObject.Find("skill_item1_focus");
		UnityEngine.GameObject skillitem2_focus_trigger = UnityEngine.GameObject.Find("skill_item2_focus");
		UnityEngine.GameObject skillitem3_focus_trigger = UnityEngine.GameObject.Find("skill_item3_focus");
		
		UnityEngine.GameObject createAvatarButton = UnityEngine.GameObject.Find("createAvatarButton");
		UnityEngine.GameObject removeAvatarButton = UnityEngine.GameObject.Find("removeAvatarButton");
		UnityEngine.GameObject backbtn = UnityEngine.GameObject.Find("back");
		UnityEngine.GameObject entergame = UnityEngine.GameObject.Find("entergame");
		UnityEngine.GameObject goto_createavatarmode = UnityEngine.GameObject.Find("goto_createavatarmode");
		
		UnityEngine.GameObject sel_list_item_focus1 = UnityEngine.GameObject.Find("sel_list_item_focus1");
		UnityEngine.GameObject sel_list_item_focus2 = UnityEngine.GameObject.Find("sel_list_item_focus2");
		UnityEngine.GameObject sel_list_item_focus3 = UnityEngine.GameObject.Find("sel_list_item_focus3");
		
		UnityEngine.GameObject fuse_left = UnityEngine.GameObject.Find("fuse_left");
		UnityEngine.GameObject fuse_right = UnityEngine.GameObject.Find("fuse_right");
		UnityEngine.GameObject lianxing_left = UnityEngine.GameObject.Find("lianxing_left");
		UnityEngine.GameObject lianxing_right = UnityEngine.GameObject.Find("lianxing_right");
		UnityEngine.GameObject faxing_left = UnityEngine.GameObject.Find("faxing_left");
		UnityEngine.GameObject faxing_right = UnityEngine.GameObject.Find("faxing_right");
		UnityEngine.GameObject wenshen_left = UnityEngine.GameObject.Find("wenshen_left");
		UnityEngine.GameObject wenshen_right = UnityEngine.GameObject.Find("wenshen_right");
		
		UnityEngine.GameObject removeAvatarOkButton = UnityEngine.GameObject.Find("removeAvatarOkButton");
		UnityEngine.GameObject removeAvatarCanelButton = UnityEngine.GameObject.Find("removeAvatarCanelButton");
		
		UIEventListener.Get(fuse_left).onClick = on_aspectItemClick;   
		UIEventListener.Get(fuse_right).onClick = on_aspectItemClick;   
		UIEventListener.Get(lianxing_left).onClick = on_aspectItemClick;   
		UIEventListener.Get(lianxing_right).onClick = on_aspectItemClick;   
		UIEventListener.Get(faxing_left).onClick = on_aspectItemClick;   
		UIEventListener.Get(faxing_right).onClick = on_aspectItemClick;   
		UIEventListener.Get(wenshen_left).onClick = on_aspectItemClick;   
		UIEventListener.Get(wenshen_right).onClick = on_aspectItemClick;   
		
		UIEventListener.Get(avataritem1_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem1_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(avataritem2_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem2_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(avataritem3_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem3_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(avataritem4_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem4_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(avataritem5_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem5_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(avataritem6_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(avataritem6_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(skillitem1_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(skillitem1_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(skillitem2_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(skillitem2_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(skillitem3_focus_trigger).onClick = on_itemClick;   
		UIEventListener.Get(skillitem3_focus_trigger).onHover = on_itemMouseOver;   
		
		UIEventListener.Get(createAvatarButton).onClick = on_createAvatarClick;   
		UIEventListener.Get(removeAvatarButton).onClick = on_removeAvatarClick;   
		
		UIEventListener.Get(backbtn).onClick = on_backClick;   
		UIEventListener.Get(entergame).onClick = on_entergameClick;   
		UIEventListener.Get(goto_createavatarmode).onClick = on_gotocreateAvatarClick;   
		
		UIEventListener.Get(sel_list_item_focus1).onClick = on_selListItem1Click;   
		UIEventListener.Get(sel_list_item_focus1).onHover = on_selListItemMouseOver; 
		
		UIEventListener.Get(sel_list_item_focus2).onClick = on_selListItem2Click;   
		UIEventListener.Get(sel_list_item_focus2).onHover = on_selListItemMouseOver; 
		
		UIEventListener.Get(sel_list_item_focus3).onClick = on_selListItem3Click;   
		UIEventListener.Get(sel_list_item_focus3).onHover = on_selListItemMouseOver; 
		
		UIEventListener.Get(removeAvatarCanelButton).onClick = on_removeAvatarCanelClick;   
		UIEventListener.Get(removeAvatarOkButton).onClick = on_removeAvatarOkClick;   
	}
	
	// Use this for initialization
	void Start () {
		Common.DEBUG_MSG("selavatar_ui::Start: " + started);
		
		if(started == false)
		{
			started = true;
			log_label.color = UnityEngine.Color.green;
			log_label.text = "请求获取角色列表...";

			Monitor.Enter(KBEngineApp.app.entities);
			Account account = (Account)KBEngineApp.app.player();
			Monitor.Enter(account.avatars);
			Dictionary<UInt64, AVATAR_INFOS> avatars = account.avatars;
			Monitor.Exit(account.avatars);
			Monitor.Exit(KBEngineApp.app.entities);

			onReqAvatarList(avatars);
		
			installEvents();
		} 
		
		windowsize = 30;
		//autoSetPosition();
	}
	
	void OnDestroy()
	{
		KBEngine.Event.deregisterOut(this);

		reset();
		started = false;
	}
	
	void installEvents()
	{
		KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
		KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
		KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
		KBEngine.Event.registerOut("onAvatarEnterWorld", this, "onAvatarEnterWorld");
		KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
	}
	
	void hideModeUI()
	{
		List<UnityEngine.GameObject> createModeObjs = new List<UnityEngine.GameObject>();
		List<UnityEngine.GameObject> selModeObjs = new List<UnityEngine.GameObject>();
		
		createModeObjs.Add(UnityEngine.GameObject.Find("item_selected"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("item1"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item2"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item3"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item4"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item5"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item6"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("item1_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item2_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item3_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item4_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item5_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("item6_focus"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item1"));
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item2"));
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item3"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item1_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item2_focus"));
		createModeObjs.Add(UnityEngine.GameObject.Find("skill_item3_focus"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("avatarlist_label"));
		createModeObjs.Add(UnityEngine.GameObject.Find("skilllist_label"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("create_username"));
		createModeObjs.Add(UnityEngine.GameObject.Find("createAvatarButton"));
		
		createModeObjs.Add(UnityEngine.GameObject.Find("removeAvatarInputName"));
		selModeObjs.Add(UnityEngine.GameObject.Find("removeAvatarInputName"));
		
		selModeObjs.Add(UnityEngine.GameObject.Find("entergame"));
		selModeObjs.Add(UnityEngine.GameObject.Find("goto_createavatarmode"));
		selModeObjs.Add(UnityEngine.GameObject.Find("removeAvatarButton"));
		
		if(isCreateMode == true)
			selModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel1"));
		else
			if(showAvatars[0] == false)
				createModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel1"));
		
		if(isCreateMode == true)
			selModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel2"));
		else
			if(showAvatars[1] == false)
				createModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel2"));
		
		if(isCreateMode == true)
			selModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel3"));
		else
			if(showAvatars[2] == false)
				createModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_panel3"));
		
		selModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_focus"));
		selModeObjs.Add(UnityEngine.GameObject.Find("sel_list_item_selected_focus"));
			
		if(isCreateMode)
		{
			for(int i=0; i<selModeObjs.Count; i++)
			{
				Vector3 pos = selModeObjs[i].transform.localPosition;
				pos.y = 99999.0f;
				selModeObjs[i].transform.localPosition = pos;
			}
			
			hideSelListItemFocus();
			selectItemFocus(UnityEngine.GameObject.Find(currsel_item));
		}
		else{
			for(int i=0; i<createModeObjs.Count; i++)
			{
				Vector3 pos = createModeObjs[i].transform.localPosition;
				pos.y = 99999.0f;
				createModeObjs[i].transform.localPosition = pos;
			}
			
			hideItemFocus();
			
			on_selListItem1Click(UnityEngine.GameObject.Find(currsel_selListItem));
		}
	}
	
	void autoSetPosition()
	{
		if(windowsize == Screen.width + Screen.height)
			return;
		
		UnityEngine.GameObject go = UnityEngine.GameObject.Find("avatars_panel");
		
		windowsize = Screen.width + Screen.height;
		
		Vector3 pos = go.transform.localPosition;
		
		float x = (-((float)(Screen.width / 2))) + ((float)go.GetComponent<UITexture>().width / 2) + 0.5f;
		float y = 0.0f;
		pos.x = x;
		pos.y = y;
		go.transform.localPosition = pos;
		Common.DEBUG_MSG("screen-size: x=" + Screen.width + "y=" + Screen.height);
		Common.DEBUG_MSG("avatar-panel-tu: x=" + go.GetComponent<UITexture>().width + "y=" + go.GetComponent<UITexture>().height);
		Common.DEBUG_MSG("avatar-panel: x=" + x + "y=" + y);
		
		UnityEngine.GameObject fuse_left = UnityEngine.GameObject.Find("fuse_left");
		UnityEngine.GameObject fuse_right = UnityEngine.GameObject.Find("fuse_right");
		UnityEngine.GameObject lianxing_left = UnityEngine.GameObject.Find("lianxing_left");
		UnityEngine.GameObject lianxing_right = UnityEngine.GameObject.Find("lianxing_right");
		UnityEngine.GameObject faxing_left = UnityEngine.GameObject.Find("faxing_left");
		UnityEngine.GameObject faxing_right = UnityEngine.GameObject.Find("faxing_right");
		UnityEngine.GameObject wenshen_left = UnityEngine.GameObject.Find("wenshen_left");
		UnityEngine.GameObject wenshen_right = UnityEngine.GameObject.Find("wenshen_right");
		
		UnityEngine.GameObject avataritem1 = UnityEngine.GameObject.Find("item1");
		UnityEngine.GameObject avataritem2 = UnityEngine.GameObject.Find("item2");
		UnityEngine.GameObject avataritem3 = UnityEngine.GameObject.Find("item3");
		UnityEngine.GameObject avataritem4 = UnityEngine.GameObject.Find("item4");
		UnityEngine.GameObject avataritem5 = UnityEngine.GameObject.Find("item5");
		UnityEngine.GameObject avataritem6 = UnityEngine.GameObject.Find("item6");
		
		UnityEngine.GameObject avataritem1_focus_trigger = UnityEngine.GameObject.Find("item1_focus");
		UnityEngine.GameObject avataritem2_focus_trigger = UnityEngine.GameObject.Find("item2_focus");
		UnityEngine.GameObject avataritem3_focus_trigger = UnityEngine.GameObject.Find("item3_focus");
		UnityEngine.GameObject avataritem4_focus_trigger = UnityEngine.GameObject.Find("item4_focus");
		UnityEngine.GameObject avataritem5_focus_trigger = UnityEngine.GameObject.Find("item5_focus");
		UnityEngine.GameObject avataritem6_focus_trigger = UnityEngine.GameObject.Find("item6_focus");
		
		UnityEngine.GameObject skillitem1 = UnityEngine.GameObject.Find("skill_item1");
		UnityEngine.GameObject skillitem2 = UnityEngine.GameObject.Find("skill_item2");
		UnityEngine.GameObject skillitem3 = UnityEngine.GameObject.Find("skill_item3");
		
		UnityEngine.GameObject skillitem1_focus_trigger = UnityEngine.GameObject.Find("skill_item1_focus");
		UnityEngine.GameObject skillitem2_focus_trigger = UnityEngine.GameObject.Find("skill_item2_focus");
		UnityEngine.GameObject skillitem3_focus_trigger = UnityEngine.GameObject.Find("skill_item3_focus");
		
		UnityEngine.GameObject avatarlist_label = UnityEngine.GameObject.Find("avatarlist_label");
		UnityEngine.GameObject skilllist_label = UnityEngine.GameObject.Find("skilllist_label");
		
		UnityEngine.GameObject create_username = UnityEngine.GameObject.Find("create_username");
		UnityEngine.GameObject createAvatarButton = UnityEngine.GameObject.Find("createAvatarButton");
		UnityEngine.GameObject removeAvatarButton = UnityEngine.GameObject.Find("removeAvatarButton");
		UnityEngine.GameObject removeAvatarInputName = UnityEngine.GameObject.Find("removeAvatarInputName");
		UnityEngine.GameObject backbtn = UnityEngine.GameObject.Find("back");
		UnityEngine.GameObject entergame = UnityEngine.GameObject.Find("entergame");
		UnityEngine.GameObject goto_createavatarmode = UnityEngine.GameObject.Find("goto_createavatarmode");
		
		UnityEngine.GameObject sel_list_item_panel1 = UnityEngine.GameObject.Find("sel_list_item_panel1");
		UnityEngine.GameObject sel_list_item_panel2 = UnityEngine.GameObject.Find("sel_list_item_panel2");
		UnityEngine.GameObject sel_list_item_panel3 = UnityEngine.GameObject.Find("sel_list_item_panel3");
		
		UnityEngine.GameObject human_descr = UnityEngine.GameObject.Find("human_descr");
		UnityEngine.GameObject skills_descr = UnityEngine.GameObject.Find("skills_descr");
		
		hideItemFocus();
		hideSelListItemFocus();

		int width = avataritem1.GetComponent<UITexture>().mainTexture.width;
		int height = avataritem1.GetComponent<UITexture>().mainTexture.height;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		pos.x -= 0.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) + 10.0f;
		avatarlist_label.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 0.0f;
		pos.y -= 15.0f;
		skilllist_label.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 35.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 35.0f;
		avataritem1.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem1_focus_trigger.transform.localPosition = pos;
		pos = avataritem1.transform.localPosition;
		pos.y -= height;
		pos.z = -1.0f;
		avataritem2.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem2_focus_trigger.transform.localPosition = pos;

		pos = avataritem2.transform.localPosition;
		pos.y -= height;
		pos.z = -1.0f;
		avataritem3.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem3_focus_trigger.transform.localPosition = pos;

		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;

		pos.x += 35.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 35.0f;
		avataritem4.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem4_focus_trigger.transform.localPosition = pos;

		pos = avataritem4.transform.localPosition;
		pos.y -= height;
		pos.z = -1.0f;
		avataritem5.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem5_focus_trigger.transform.localPosition = pos;

		pos = avataritem5.transform.localPosition;
		pos.y -= height;
		pos.z = -1.0f;
		avataritem6.transform.localPosition = pos;

		pos.z = -2.0f;
		avataritem6_focus_trigger.transform.localPosition = pos;

		//-----------------------------------------------------------
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 60.0f;
		pos.y -= 55.0f;
		skillitem1.transform.localPosition = pos;
		
		pos.z = -2.0f;
		skillitem1_focus_trigger.transform.localPosition = pos;
		
		pos = skillitem1.transform.localPosition;
		pos.x += 60;
		pos.z = -1.0f;
		skillitem2.transform.localPosition = pos;
		
		pos.z = -2.0f;
		skillitem2_focus_trigger.transform.localPosition = pos;
		
		pos = skillitem2.transform.localPosition;
		pos.x += 60;
		pos.z = -1.0f;
		skillitem3.transform.localPosition = pos;
		
		pos.z = -2.0f;
		skillitem3_focus_trigger.transform.localPosition = pos;
		
		//-----------------------------------------------------------
		selectItemFocus(UnityEngine.GameObject.Find(currsel_item));
		selectSelListItemFocus(UnityEngine.GameObject.Find(currsel_selListItem));
		
		//-----------------------------------------------------------
		pos = create_username.transform.localPosition;
		pos.y = -(Screen.height / 2 - 50.0f);
		create_username.transform.localPosition = pos;
		
		pos = createAvatarButton.transform.localPosition;
		pos.y = -(Screen.height / 2 - 15.0f);
		createAvatarButton.transform.localPosition = pos;
			
		pos = entergame.transform.localPosition;
		pos.y = -(Screen.height / 2 - 15.0f);
		entergame.transform.localPosition = pos;
		
		pos = removeAvatarInputName.transform.localPosition;
		pos.x = -((Screen.width / 2) * 0.15f);
		pos.y = 0.0f;
		removeAvatarInputName.transform.localPosition = pos;
			
		pos = backbtn.transform.localPosition;
		pos.x = (Screen.width / 2 - 100.0f);
		pos.y = -(Screen.height / 2 - 15.0f);
		backbtn.transform.localPosition = pos;
		
		pos = removeAvatarButton.transform.localPosition;
		pos.x = (Screen.width / 2 - 100.0f);
		pos.y = -(Screen.height / 2 - 45.0f);
		removeAvatarButton.transform.localPosition = pos;
		
		pos = goto_createavatarmode.transform.localPosition;
		pos.x = (Screen.width / 2 - 100.0f);
		pos.y = -(Screen.height / 2 - 75.0f);
		goto_createavatarmode.transform.localPosition = pos;
		
		//-----------------------------------------------------------
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 60.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 10.0f;
		sel_list_item_panel1.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 60.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 80.0f;
		sel_list_item_panel2.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		
		pos.x -= 60.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 150.0f;
		sel_list_item_panel3.transform.localPosition = pos;

		//-------------------------------------------------------------
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		pos.x += 20.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 300.0f;
		fuse_left.transform.localPosition = pos;
		pos.x += 30.0f;
		fuse_right.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		pos.x += 20.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 328.0f;
		lianxing_left.transform.localPosition = pos;
		pos.x += 30.0f;
		lianxing_right.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		pos.x += 20.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 355.0f;
		faxing_left.transform.localPosition = pos;
		pos.x += 30.0f;
		faxing_right.transform.localPosition = pos;
		
		pos.x = x;
		pos.y = y;
		pos.z = -1.0f;
		pos.x += 20.0f;
		pos.y += (((float)go.GetComponent<UITexture>().height / 2) - height) - 382.0f;
		wenshen_left.transform.localPosition = pos;
		pos.x += 30.0f;
		wenshen_right.transform.localPosition = pos;
		
		hideModeUI();

		//-----------------------------------------------------------
		pos = human_descr.transform.localPosition;
		
		x = ((float)(Screen.width / 2)) - human_descr.GetComponentInChildren<UITexture>().width / 2 - 0.5f;
		y = 0.0f;
		pos.x = x;
		pos.y = ((float)(Screen.height / 2)) - (skills_descr.GetComponentInChildren<UITexture>().height / 2) - 0.5f;
		human_descr.transform.localPosition = pos;
		
		pos = skills_descr.transform.localPosition;
		
		x = ((float)(Screen.width / 2)) - skills_descr.GetComponentInChildren<UITexture>().width / 2 - 0.5f;
		y = 0.0f;
		pos.x = x;
		pos.y = -100.0f;
		skills_descr.transform.localPosition = pos;
	}
	
	// Update is called once per frame
	void Update () {
		autoSetPosition();
	}
	
	void FixedUpdate()
	{
		updateLoadAvatarInfo();
	}
	
	void updateLoadAvatarInfo()
	{
		if(loadAvatar_label == null || loader.inst == null)
			return;
		
		if(loadAvatar_label.text != "")
		{
			Scene selavatarScene = loader.inst.findScene("selavatar", false);
			if(selavatarScene == null)
				return;

			foreach(KeyValuePair<string, SceneObject> obj in selavatarScene.objs)
			{
				SceneObject sobj = obj.Value;
				if(sobj.asset == null)
					continue;

				Byte rtype = Srouce2roleType(sobj.asset.source);
				
				if(rtype > 0)
				{
					if(rtype == changeAvatarItem)
					{
						if(sobj.asset != null && sobj.asset.isLoaded == false)
						{
							if(sobj.asset.load != null && sobj.asset.load.www != null)
							{
								if(sobj.asset.load.www.error != null)
									loadAvatar_label.text = "加载角色模型失败, 重试中...";
								else
									loadAvatar_label.text = "正在加载角色模型(" + ((int)(sobj.asset.load.www.progress * 100))+ "%)...";
							}
						}
						else
						{
							loadAvatar_label.text = "";
						}
					}
				}
			}
		}
	}
	
	void refreshAvatarListIcon()
	{
		if(avatarList.Count > 0)
		{
			AvatarInfo ainfo = avatarList[0];
			UnityEngine.GameObject.Find("sel_list_item1").GetComponent<UITexture>().mainTexture = 
				UnityEngine.GameObject.Find("item" + ainfo.roleType).GetComponent<UITexture>().mainTexture;
		}
		
		if(avatarList.Count > 1)
		{
			AvatarInfo ainfo = avatarList[1];
			UnityEngine.GameObject.Find("sel_list_item2").GetComponent<UITexture>().mainTexture = 
				UnityEngine.GameObject.Find("item" + ainfo.roleType).GetComponent<UITexture>().mainTexture;
		}
		
		if(avatarList.Count > 2)
		{
			AvatarInfo ainfo = avatarList[2];
			UnityEngine.GameObject.Find("sel_list_item3").GetComponent<UITexture>().mainTexture = 
				UnityEngine.GameObject.Find("item" + ainfo.roleType).GetComponent<UITexture>().mainTexture;
		}
	}
	
	void hideItemFocus()
	{
		UnityEngine.GameObject avataritem_focus = UnityEngine.GameObject.Find("item_focus");
		Vector3 pos = avataritem_focus.transform.position;
		pos.y = -99999.0f;
		avataritem_focus.transform.position = pos;
	}
	
	void showItemFocus(UnityEngine.GameObject item)
	{
		UnityEngine.GameObject item_focus = UnityEngine.GameObject.Find("item_focus");
		Vector3 pos = item.transform.localPosition;
		pos.z = -100.0f;
		item_focus.transform.localPosition = pos;
	}
	
	void hideSelListItemFocus()
	{
		UnityEngine.GameObject avataritem_focus = UnityEngine.GameObject.Find("sel_list_item_focus");
		Vector3 pos = avataritem_focus.transform.position;
		pos.y = -99999.0f;
		avataritem_focus.transform.position = pos;
	}
	
	void showSelListItemFocus(UnityEngine.GameObject item)
	{
		UnityEngine.GameObject item_focus = UnityEngine.GameObject.Find("sel_list_item_focus");
		Vector3 pos = item.transform.parent.localPosition;
		pos.x += 60.0f;
		pos.z = -100.0f;
		item_focus.transform.localPosition = pos;
	}
	
	void selectItemFocus(UnityEngine.GameObject item)
	{
		UnityEngine.GameObject item_selected_focus = UnityEngine.GameObject.Find("item_selected");
		Vector3 pos = item.transform.localPosition;
		pos.z = -101.0f;
		item_selected_focus.transform.localPosition = pos;
	}
	
	void selectSelListItemFocus(UnityEngine.GameObject item)
	{
		UnityEngine.GameObject item_focus = UnityEngine.GameObject.Find("sel_list_item_selected_focus");
		Vector3 pos = item.transform.parent.localPosition;
		pos.x += 60.0f;
		pos.z = -101.0f;
		item_focus.transform.localPosition = pos;
	}
	
	void on_aspectItemClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_aspectItemClick: " + item.name);
		
		SceneObject obj = getCurrentAvatarObject();
		if(obj == null)
			return;
		
		curravatar avatar = obj.gameObject.GetComponent<curravatar>();

		if(item.name == "fuse_left")
		{
			avatar.decFuse();
		}
		else if(item.name == "fuse_right")
		{
			avatar.incFuse();
		}
		else if(item.name == "lianxing_left")
		{
			
		}
		else if(item.name == "lianxing_right")
		{
			
		}
		else if(item.name == "faxing_left")
		{
			
		}
		else if(item.name == "faxing_right")
		{
			
		}
		else if(item.name == "wenshen_left")
		{
			
		}
		else if(item.name == "wenshen_right")
		{
			
		}
	}
	
	void on_itemMouseOver(UnityEngine.GameObject item, bool isOver)
	{
		Common.DEBUG_MSG("on_itemMouseOver: " + item.name + " " + isOver);
		if(isOver == true)
			showItemFocus(item);
		else
			hideItemFocus();
	}
	
	void on_itemClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_itemClick: " + item.name);
		
		UnityEngine.GameObject human_descr = UnityEngine.GameObject.Find("human_descr");
		UnityEngine.GameObject skills_descr = UnityEngine.GameObject.Find("skills_descr");
		
		if(item.name.IndexOf("skill") <= -1)
		{
			currsel_item = item.name;
			
			selectItemFocus(item);
			
			if(currsel_item == "item1_focus")
			{
				changeAvatarItem = 1;
				human_descr.GetComponentInChildren<UILabel>().text = "战士是真正的武器大师，攻击狂野而致命，每一击都体现了千锤百炼的高超剑技。 在战场上，战士们永远都在最前线，坚固的护甲以及不屈的意志，保护同伴，勇往直前，用血肉之躯杀出通往胜利的血路。";
				skills_descr.GetComponentInChildren<UILabel>().text = "必杀技: 毁灭\r\n" +
					"使出全身的力量对敌人造成毁灭性的打击。";
			}
			else if(currsel_item == "item2_focus")
			{
				changeAvatarItem = 2;
				human_descr.GetComponentInChildren<UILabel>().text = "法师是神秘与危险的化身，天地魔力的掌控者。法师使用魔法时天地色变、大地为之颤抖，大范围的魔法技能威力无与伦比。";
						skills_descr.GetComponentInChildren<UILabel>().text = "必杀技: 死神降临\r\n" +
					"使出全身的力量对敌人造成毁灭性的打击。";
			}
			else if(currsel_item == "item3_focus")
			{
				changeAvatarItem = 3;
				human_descr.GetComponentInChildren<UILabel>().text = "1法师是神秘与危险的化身，天地魔力的掌控者。法师使用魔法时天地色变、大地为之颤抖，大范围的魔法技能威力无与伦比。";
			}
			else if(currsel_item == "item4_focus")
			{
				changeAvatarItem = 4;
				human_descr.GetComponentInChildren<UILabel>().text = "2法师是神秘与危险的化身，天地魔力的掌控者。法师使用魔法时天地色变、大地为之颤抖，大范围的魔法技能威力无与伦比。";
			}
			else if(currsel_item == "item5_focus")
			{
				changeAvatarItem = 5;
				human_descr.GetComponentInChildren<UILabel>().text = "3法师是神秘与危险的化身，天地魔力的掌控者。法师使用魔法时天地色变、大地为之颤抖，大范围的魔法技能威力无与伦比。";
			}
			else if(currsel_item == "item6_focus")
			{
				changeAvatarItem = 6;
				human_descr.GetComponentInChildren<UILabel>().text = "未开放这个职业。";
			}
			
			resetSelAvatar();
		}
	}
	
	void on_createAvatarClick(UnityEngine.GameObject obj)
	{
		Common.DEBUG_MSG("on_createAvatarClick: " + obj.name);
		
		UnityEngine.GameObject create_usernameLabel = UnityEngine.GameObject.Find("create_usernameLabel");
		

		string name = create_usernameLabel.GetComponent<UILabel>().text;
		if(name.Length < 2)
		{
			log_label.color = UnityEngine.Color.red;
			log_label.text = "角色名称必须大于2位!";
			return;
		}
		
		log_label.color = UnityEngine.Color.green;
		log_label.text = "请求中请稍后...";
		
		Monitor.Enter(KBEngineApp.app.entities);
		Account account = (Account)KBEngineApp.app.player();
		account.reqCreateAvatar(changeAvatarItem, name);
		Monitor.Exit(KBEngineApp.app.entities);
	}
	
	void on_removeAvatarClick(UnityEngine.GameObject obj)
	{		
		Common.DEBUG_MSG("on_removeAvatarClick: " + obj.name);
		
		UnityEngine.GameObject removeAvatarInput = UnityEngine.GameObject.Find("removeAvatarInputName");
		Vector3 pos = removeAvatarInput.transform.localPosition;
		pos.y = 0.0f;
		removeAvatarInput.transform.localPosition = pos;
	}
	
	void on_backClick(UnityEngine.GameObject obj)
	{
		if(obj != null)
			Common.DEBUG_MSG("on_backClick: " + obj.name);
		
		log_label.color = UnityEngine.Color.green;
		log_label.text = "";
		windowsize = 90;
		
		if(isCreateMode == true)
		{
			isCreateMode = false;
			refreshAvatarListIcon();
			return;
		}
		
		// back to login
		loader.inst.enterScene("login");
	}
	
	void on_gotocreateAvatarClick(UnityEngine.GameObject obj)
	{
		if(obj != null)
			Common.DEBUG_MSG("on_gotocreateAvatarClick: " + obj.name);
		
		isCreateMode = true;
		windowsize = 0;
		changeAvatarItem = 1;
		resetSelAvatar();
		
		List < string > loads = new List < string >();
		loads.Add("avatar1.unity3d");
		loads.Add("avatar2.unity3d");
		loads.Add("avatar3.unity3d");
		loads.Add("avatar4.unity3d");
		loads.Add("avatar_wpgroup.unity3d");
		
		for(int i=0; i<loads.Count; i++)
		{
			string loadRes = loads[i];
			Asset asset = null;
			
			foreach(KeyValuePair<string, Asset> assetsCache in Scene.assetsCache)
			{
				if(assetsCache.Value.source == loadRes)
				{
					asset = assetsCache.Value;
					break;
				}
			}
			
			if(asset == null)
			{
				Common.ERROR_MSG("on_gotocreateAvatarClick: not found asset " + loadRes);
				continue;
			}
			
			asset.loadLevel = Asset.LOAD_LEVEL.LEVEL_ENTER_AFTER;
			if(i == 0)
				asset.loadPri = 1;
				
			loader.inst.loadPool.addLoad(asset);
		}
		
		if(loads.Count > 0)
			loader.inst.loadPool.start();
	}
	
	void on_entergameClick(UnityEngine.GameObject obj)
	{
		Common.DEBUG_MSG("on_entergameClick: " + obj.name);

		int idx = -1;
		if(currsel_selListItem == "sel_list_item_focus1")
		{
			idx = 0;
		}
		else if(currsel_selListItem == "sel_list_item_focus2")
		{
			idx = 1;
		}
		else if(currsel_selListItem == "sel_list_item_focus3")
		{
			idx = 2;
		}
		
		if(idx == -1)
		{
			log_label.color = UnityEngine.Color.red;
			log_label.text = "请选择一个角色。";
			return;
		}
		
		AvatarInfo info = avatarList[idx];
		
		log_label.color = UnityEngine.Color.green;
		log_label.text = "请求中请稍后...";
		
		Monitor.Enter(KBEngineApp.app.entities);
		if (KBEngineApp.app.player ().className == "Account") 
		{
			Account account = (Account)KBEngineApp.app.player ();
			account.selectAvatarGame (info.dbid);
		}
		else
		{
			Common.ERROR_MSG("on_entergameClick: already enter to game!");
		}
		
		Monitor.Exit(KBEngineApp.app.entities);
		
	}

	void on_selListItemMouseOver(UnityEngine.GameObject item, bool isOver)
	{
		if(isOver == true)
			showSelListItemFocus(item);
		else
			hideSelListItemFocus();
	}
	
	void resetSelAvatar()
	{
		bool loadAvatar = false;
		
		if(loadAvatar_label != null)
			loadAvatar_label.text = "";
		
		foreach(KeyValuePair<string, SceneObject> obj in loader.inst.findScene("selavatar", false).objs)
		{
			SceneObject sobj = obj.Value;
			Byte rtype = Srouce2roleType(sobj.asset.source);
			
			if(rtype > 0)
			{
				Common.DEBUG_MSG("resetSelAvatar: rtype=" + rtype + ", source=" + sobj.asset.source);
				curravatar avatar = null;
				if(sobj.gameObject != null)
				{
					avatar = sobj.gameObject.GetComponent<curravatar>();
				}
				
				if(rtype == changeAvatarItem)
				{
					sobj.position = sobj.position;
					sobj.eulerAngles = sobj.eulerAngles;
					sobj.scale = sobj.scale;
					
					if(avatar != null)
					{
						loadAvatar = (sobj.asset.isLoaded == false);
						NGUITools.SetActive(avatar.transform.gameObject, true);
					}
					else
						loadAvatar = true;
				}
				else
				{
					if(avatar != null)
						NGUITools.SetActive(avatar.transform.gameObject, false);
				}
			}
		}
		
		if(loadAvatar == true)
		{
			if(loadAvatar_label != null)
				loadAvatar_label.text = "正在加载角色模型(0%)...";
		}
	}
	
	SceneObject getCurrentAvatarObject()
	{
		foreach(KeyValuePair<string, SceneObject> obj in loader.inst.findScene("selavatar", false).objs)
		{
			SceneObject sobj = obj.Value;
			Byte rtype = Srouce2roleType(sobj.asset.source);
			if(rtype > 0 && rtype == changeAvatarItem)
			{
				return sobj;
			}
		}
		
		return null;
	}
	
	void on_selListItem1Click(UnityEngine.GameObject item)
	{
		if(item == null)
			return;
		
		currsel_selListItem = item.name;
		changeAvatarItem = 0;

		if(avatarList.Count > 0)
		{
			AvatarInfo info = avatarList[0];
			changeAvatarItem = info.roleType;
		}
	
		selectSelListItemFocus(item);
		
		UnityEngine.GameObject human_descr = UnityEngine.GameObject.Find("human_descr");
		UnityEngine.GameObject skills_descr = UnityEngine.GameObject.Find("skills_descr");
		human_descr.GetComponentInChildren<UILabel>().text = "角色目前还很脆弱哦， 您当前可以找XXX去做任务获得经验。";
		skills_descr.GetComponentInChildren<UILabel>().text = "您可以去兽人洞穴击杀boss获得xx技能书来提升打击能力。";
		
		resetSelAvatar();
	}
	
	void on_selListItem2Click(UnityEngine.GameObject item)
	{
		currsel_selListItem = item.name;
		changeAvatarItem = 0;
		
		if(avatarList.Count > 0)
		{
			AvatarInfo info = avatarList[1];
			changeAvatarItem = info.roleType;
		}
		
		selectSelListItemFocus(item);
		
		UnityEngine.GameObject human_descr = UnityEngine.GameObject.Find("human_descr");
		UnityEngine.GameObject skills_descr = UnityEngine.GameObject.Find("skills_descr");
		human_descr.GetComponentInChildren<UILabel>().text = "已不再是新手了， 可以去xx领地刷boss。";
		skills_descr.GetComponentInChildren<UILabel>().text = "您可以去兽人洞穴击杀boss获得xx技能书来提升打击能力。";
		
		resetSelAvatar();
	}

	void on_selListItem3Click(UnityEngine.GameObject item)
	{
		currsel_selListItem = item.name;
		changeAvatarItem = 0;
		
		if(avatarList.Count > 0)
		{
			AvatarInfo info = avatarList[2];
			changeAvatarItem = info.roleType;
			
			Monitor.Enter(KBEngineApp.app.entities);
			Account account = (Account)KBEngineApp.app.player();
			Monitor.Enter(account.avatars);
			if((UInt64)account.lastSelCharacter == 0)
				account.lastSelCharacter = info.dbid;
			Monitor.Exit(KBEngineApp.app.entities);
		}
		
		selectSelListItemFocus(item);
		
		UnityEngine.GameObject human_descr = UnityEngine.GameObject.Find("human_descr");
		UnityEngine.GameObject skills_descr = UnityEngine.GameObject.Find("skills_descr");
		human_descr.GetComponentInChildren<UILabel>().text = "角色目前很强壮哦， 去竞技场和人pk刷排名吧。";
		skills_descr.GetComponentInChildren<UILabel>().text = "您的打击能力已经非常高了， 可以尝试提升防御等其他能力。";
		
		resetSelAvatar();
	}

	public void on_removeAvatarCanelClick(UnityEngine.GameObject obj)
	{
		UnityEngine.GameObject removeAvatarInput = UnityEngine.GameObject.Find("removeAvatarInputName");
		UnityEngine.GameObject removeavatarnameLabel = UnityEngine.GameObject.Find("removeavatarnameLabel");
		removeavatarnameLabel.GetComponent<UILabel>().text = "";
		Vector3 pos = removeAvatarInput.transform.localPosition;
		pos.y = 9999.0f;
		removeAvatarInput.transform.localPosition = pos;
		log_label.text = "";
	}
	
	public void on_removeAvatarOkClick(UnityEngine.GameObject obj)
	{
		UnityEngine.GameObject removeAvatarInput = UnityEngine.GameObject.Find("removeAvatarInputName");
		UnityEngine.GameObject removeavatarnameLabel = UnityEngine.GameObject.Find("removeavatarnameLabel");
		
		bool found = false;
		for(int i=0; i<avatarList.Count; i++)
		{
			AvatarInfo info = avatarList[i];
			if(info.name == removeavatarnameLabel.GetComponent<UILabel>().text)
			{
				found = true;
				break;
			}
		}
		
		if(found == false)
		{
			log_label.color = UnityEngine.Color.red;
			log_label.text = "没有找到该角色!";
			return;
		}
		
		Monitor.Enter(KBEngineApp.app.entities);
		Account account = (Account)KBEngineApp.app.player();
		account.reqRemoveAvatar(removeavatarnameLabel.GetComponent<UILabel>().text);
		Monitor.Exit(KBEngineApp.app.entities);
		
		on_removeAvatarCanelClick(obj);
	}
		
	public void onReqAvatarList(Dictionary<UInt64, AVATAR_INFOS> getAvatarList)
	{
		log_label.color = UnityEngine.Color.green;
		log_label.text = "";
			
		Monitor.Enter(KBEngineApp.app.entities);
		Account account = (Account)KBEngineApp.app.player();
		UInt64 lastSelCharacter = (UInt64)account.lastSelCharacter;
		Monitor.Exit(KBEngineApp.app.entities);
		
		List < string > loads = new List < string >();
		avatarList.Clear();
		
		Common.DEBUG_MSG("selavatar_ui::onReqAvatarList: " + getAvatarList.Count);
		showAvatars[0] = showAvatars[1] = showAvatars[2] = false;
		
		bool hasClickSelAvatar = false;
		
		if(getAvatarList.Count > 0)
		{
			int idx = 0;
			foreach(UInt64 dbid in getAvatarList.Keys)
			{
				AVATAR_INFOS info = getAvatarList[dbid];
				AvatarInfo ainfo;
				ainfo.roleType = (Byte)info.roleType;
				ainfo.name = (string)info.name;
				ainfo.level = (UInt16)info.level;
				ainfo.dbid = (UInt64)info.dbid;
				
				showAvatars[idx++] = true;

				if(lastSelCharacter == ainfo.dbid)
				{
					currsel_selListItem = "sel_list_item_focus" + idx;
					on_selListItem1Click(UnityEngine.GameObject.Find(currsel_selListItem));
					hasClickSelAvatar = true;
				}
				
				UnityEngine.GameObject sel_list_title = UnityEngine.GameObject.Find("sel_list_title" + idx);
				sel_list_title.GetComponent<TextMesh>().text = avatarnames[(int)ainfo.roleType] + "\r\n" + ainfo.level + "级\r\n" + ainfo.name;
				
				avatarList.Add(ainfo);
				
				string loadRes = roleType2Srouce(ainfo.roleType);
				
				if(loadRes != "")
				{
					bool found = false;
					for(int ii=0; ii<loads.Count; ii++)
					{
						if(loads[ii] == loadRes)
						{
							found = true;
							break;
						}
					}
					
					if(found == false)
					{
						loads.Add(loadRes);
					}
				}
			}
			
			if(hasClickSelAvatar == false)
			{
				currsel_selListItem = "sel_list_item_focus1";
				on_selListItem1Click(UnityEngine.GameObject.Find(currsel_selListItem));
			}
		}
		else
		{		
			log_label.color = UnityEngine.Color.red;
			log_label.text = "请创建一个角色!";
			on_gotocreateAvatarClick(null);
		}
		
		Monitor.Exit(account.avatars);
		
		loads.Add("avatar_wpgroup.unity3d");
		
		for(int i=0; i<loads.Count; i++)
		{
			string loadRes = loads[i];
			Asset asset = null;
			
			foreach(KeyValuePair<string, Asset> assetsCache in Scene.assetsCache)
			{
				if(assetsCache.Value.source == loadRes)
				{
					asset = assetsCache.Value;
					break;
				}
			}
			
			if(asset == null)
			{
				Common.ERROR_MSG("onReqAvatarList: not found asset " + loadRes);
				continue;
			}
			
			asset.loadLevel = Asset.LOAD_LEVEL.LEVEL_ENTER_AFTER;
			if(i == 0 || getAvatarList.Count == 0)
				asset.loadPri = 1;
			
			loader.inst.loadPool.addLoad(asset);
		}
		
		if(loads.Count > 0)
			loader.inst.loadPool.start();
		
		windowsize = 50;
		refreshAvatarListIcon();
	}
	
	private string roleType2Srouce(Byte type)
	{
		string loadRes = "";
		switch(type)
		{
			case 1:
				loadRes = "avatar1.unity3d";
				break;
			case 2:
				loadRes = "avatar2.unity3d";
				break;
			case 3:
				loadRes = "avatar3.unity3d";
				break;
			case 4:
				loadRes = "avatar4.unity3d";
				break;
			default:
				Common.DEBUG_MSG("selavatar_ui::onReqAvatarList: roleType(" + type + ") is error!");
				break;
		};
		
		return loadRes;
	}

	private Byte Srouce2roleType(string source)
	{
		if("avatar1.unity3d" == source)
			return 1;

		if("avatar2.unity3d" == source)
			return 2;
		
		if("avatar3.unity3d" == source)
			return 3;
		
		if("avatar4.unity3d" == source)
			return 4;
		
		return 0;
	}
	
	public void onCreateAvatarResult(Byte retcode, object info, Dictionary<UInt64, AVATAR_INFOS> avatarList)
	{
		if(retcode != 0)
		{
			log_label.color = UnityEngine.Color.red;
			log_label.text = "服务器返回错误:" + KBEngineApp.app.serverErr(retcode) + "!";
			return;
		}
		
		on_backClick(null);
		onReqAvatarList(avatarList);
	}
	
	public void onRemoveAvatar(UInt64 dbid)
	{
		if(dbid == 0)
		{
			log_label.color = UnityEngine.Color.red;
			log_label.text = "服务器删除角色返回错误!";
			return;
		}

		Monitor.Enter(KBEngineApp.app.entities);
		Account account = (Account)KBEngineApp.app.player();
		Monitor.Enter(account.avatars);
		Dictionary<UInt64, AVATAR_INFOS> avatars = account.avatars;
		Monitor.Exit(account.avatars);
		Monitor.Exit(KBEngineApp.app.entities);

		onReqAvatarList(avatars);
	}
	
	public void onAvatarEnterWorld(UInt64 rndUUID, Int32 eid, KBEngine.Avatar avatar)
	{
		log_label.color = UnityEngine.Color.green;
		log_label.text = "加载场景中...";
		KBEngine.Event.deregisterOut(this);
	}
	
	public void onDisconnected()
	{
		log_label.color = UnityEngine.Color.red;
		log_label.text = "与服务器的连接已经中断!";
		
		InvokeRepeating("onDisconnected_gobackToLogin", 3.0f, 0.1f);
	}
	
	void onDisconnected_gobackToLogin()
	{
		CancelInvoke("onDisconnected_gobackToLogin");
		on_backClick(null);
		
		if(isCreateMode == true)
			on_backClick(null);
	}
}
