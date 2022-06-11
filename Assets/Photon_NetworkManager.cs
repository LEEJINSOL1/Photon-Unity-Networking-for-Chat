
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class Photon_NetworkManager : MonoBehaviourPunCallbacks
{

    public Text status_text, nickname, roomname, chat_info,user_list;
    public InputField Input_field;
    public PhotonView pv;
    string _msg,nick,room;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1200, 500, true);
    }

    // Update is called once per frame
    void Update()
    {
        status_text.text = PhotonNetwork.NetworkClientState.ToString();
        
        updatePlayerList();


    }

    public void updatePlayerList()
    {
        string test = "";

        foreach(var player in PhotonNetwork.PlayerList)
        {
            test = test + player + "\n";
        }
        user_list.text = test;
    }

    #region 채팅
    public void Send()
    {
        pv = GameObject.Find("Photon_NetworkManager").GetComponent<PhotonView>();
        pv.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + Input_field.text);
        Input_field.text = "";
    }
    [PunRPC]
    void ChatRPC(string msg)
    {
        _msg = _msg + msg + "\n";
        chat_info.text = _msg;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            print("PhotonNetwork.IsConnected");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings(); //Photon Online Server에 접속하기 가장 중요
        }
    }
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("room1", new RoomOptions { MaxPlayers = 6 }, null);

        roomname.text = PhotonNetwork.CurrentRoom.Name.ToString();
    }

    #endregion

    #region 콜백함수
    public override void OnConnectedToMaster()
    {
        nick = Random.Range(1, 100).ToString();
        PhotonNetwork.LocalPlayer.NickName = nick;
        nickname.text = PhotonNetwork.LocalPlayer.NickName.ToString();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("연결 끊김 " + nick);
    }

    public override void OnCreatedRoom()
    {
        print("방 만들기 완료 누가? ->" + nick);
        print("방 이름은 ? ->" + roomname);
    }

    public override void OnJoinedRoom()
    {
        print("방 참가완료 누가? ->" + nick);
        print("방 이름은 ? ->" + roomname);
    }
    #endregion
}
