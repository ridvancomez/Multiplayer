using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update


    [SerializeField]
    private List<TextMeshProUGUI> playerNamesText;

    [SerializeField]
    private TextMeshProUGUI playerIsExpected;



    void Start()
    {


        //Sunucya bağlan
        //Lobiye bağlan
        //Odaya bağlan

        InvokeRepeating("NameControl", 0, .5f);

        PhotonNetwork.ConnectUsingSettings(); //Bağlanırsa OnConectedToMaster methodu çalışacak
    }

    private void NameControl()
    {

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playerNamesText[i].text = PhotonNetwork.PlayerList[i].NickName;
        }

        if (PhotonNetwork.PlayerList.Length == playerNamesText.Count)
        {
            //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            //{
            //    playerNamesText[i].text = PhotonNetwork.PlayerList[i].NickName;
            //}
            playerIsExpected.text = "";
            CancelInvoke("NameControl");

        }
    }

    private void ClearName()
    {
        foreach (var item in playerNamesText)
        {
            item.text = "----";
        }
        
    }

    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Sunucuya bağlanıldı"); //Bağlanırsa OnJoinedLobby metodu çalışacak
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye bağlanıldı");
        //PhotonNetwork.JoinRandomRoom();//Bağlanırsa OnJoinedRoom metodu çalışacak
        PhotonNetwork.JoinOrCreateRoom("Oda1", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

        //base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya bağlanıldı");

        GameObject myObject = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-4.5f,4.5f), 1, Random.Range(-4.5f, 4.5f)), Quaternion.identity, 0, null);

        string playerName = "M-" + Random.Range(1, 10000).ToString();

        PhotonView pw = myObject.GetComponent<PhotonView>();
        pw.Owner.NickName = playerName;

        //base.OnJoinedRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        foreach (var item in playerNamesText)
        {
            if(item.text.Equals(otherPlayer.NickName))
            {
                item.text = "----";
                playerIsExpected.text = "Oyuncu Bekleniyor";
                ClearName();
                InvokeRepeating("NameControl", 0, .5f);
            }
        }
        //base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Rastgele bir odaya girilemedi");
        //base.OnJoinRandomFailed(returnCode, message);
    }


}
