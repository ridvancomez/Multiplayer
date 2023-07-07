using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ServerDocumentation : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //Sunucuya bağlan
        //Lobiye bağlan
        //Odaya bağlan

        //PhotonNetwork.ConnectUsingSettings(); // server a bağlanma isteği yapıldı
        //PhotonNetwork.JoinLobby(); // lobiye bağlan
        //PhotonNetwork.JoinRoom("Oda1"); // Odaya bağlan
        //PhotonNetwork.JoinRandomRoom(); // Rastgele bir odaya bağlan
        //PhotonNetwork.CreateRoom("oda Adi", oda_ayarlari); // oda oluştur
        //PhotonNetwork.JoinOrCreateRoom("oda Adi", oda_ayarlari, TypedLobby.Default); // oda varsa bağlan yoksa oluştur
        //PhotonNetwork.LeaveRoom(); //Odadan çık
        //PhotonNetwork.LeaveLobby(); //Lobiden çık
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Sunucuya bağlanıldı");
        //base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye bağlanıldı");
        //base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya bağlanıldı");
        //base.OnJoinedRoom();
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden çıkıldı");
        //base.OnLeftLobby();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Odadan çıkldı");
        //base.OnLeftRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);

        Debug.Log("Herhangi bir odaya girilemedi");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        //base.OnCreateRoomFailed(returnCode, message);

        Debug.Log("Oda oluşturulamadı");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Rastgele bir odaya girilemedi");
        //base.OnJoinRandomFailed(returnCode, message);
    }


}
