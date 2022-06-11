
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

    #region ä��
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
            PhotonNetwork.ConnectUsingSettings(); //Photon Online Server�� �����ϱ� ���� �߿�
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

    #region �ݹ��Լ�
    public override void OnConnectedToMaster()
    {
        nick = Random.Range(1, 100).ToString();
        PhotonNetwork.LocalPlayer.NickName = nick;
        nickname.text = PhotonNetwork.LocalPlayer.NickName.ToString();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("���� ���� " + nick);
    }

    public override void OnCreatedRoom()
    {
        print("�� ����� �Ϸ� ����? ->" + nick);
        print("�� �̸��� ? ->" + roomname);
    }

    public override void OnJoinedRoom()
    {
        print("�� �����Ϸ� ����? ->" + nick);
        print("�� �̸��� ? ->" + roomname);
    }
    #endregion
}
