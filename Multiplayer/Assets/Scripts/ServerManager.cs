using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update


    private List<TextMeshProUGUI> playerNamesText = new();


    private TextMeshProUGUI playerIsExpected;

    [SerializeField]
    private Text serverInfo;

    [SerializeField]
    private InputField userNameInputField;

    [SerializeField]
    private InputField roomNameInputField;

    private string userName;
    private string roomName;

    private GameObject[] playerLocations;



    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Bağlanırsa OnConectedToMaster methodu çalışacak

        if (PhotonNetwork.IsConnected)
            serverInfo.text = "Servere bağlandı";

        DontDestroyOnLoad(gameObject);
    }

    private void NameControl()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playerNamesText[i].text = PhotonNetwork.PlayerList[i].NickName;
        }

        if (PhotonNetwork.PlayerList.Length == playerNamesText.Count)
        {
            playerIsExpected.text = "";
            //GameObject.FindGameObjectWithTag("PlayerIsExpected").AddComponent<TextMeshProUGUI>().text = "";

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


    public void SetUpRoom()
    {
        SceneManager.LoadScene(1);
        userName = userNameInputField.text;
        roomName = roomNameInputField.text;

        PhotonNetwork.JoinLobby();
    }

    public void LogIn()
    {
        SceneManager.LoadScene(1);
        userName = userNameInputField.text;
        roomName = roomNameInputField.text;

        PhotonNetwork.JoinLobby();
    }

    //public override void OnConnectedToMaster()
    //{
    //    //base.OnConnectedToMaster();
    //    Debug.Log("Sunucuya bağlanıldı"); //Bağlanırsa OnJoinedLobby metodu çalışacak

    //}

    public override void OnJoinedLobby()
    {
        playerIsExpected = GameObject.FindGameObjectWithTag("PlayerIsExpected").GetComponent<TextMeshProUGUI>();
        
        
        GameObject[] playerNamesObjects = GameObject.FindGameObjectsWithTag("PlayerName");
        playerNamesObjects = playerNamesObjects.OrderBy(x => x.name).ToArray();

        
        foreach (var item in playerNamesObjects)
        {
            playerNamesText.Add(item.GetComponent<TextMeshProUGUI>());
        }

        

        if (userName != "" && roomName != "")
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
        }
        InvokeRepeating("NameControl", 0, .5f);

        //PhotonNetwork.JoinOrCreateRoom("Oda1", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

        //base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya bağlanıldı");

        playerLocations = GameObject.FindGameObjectsWithTag("Location").OrderBy(x => x.name).ToArray();
        

        GameObject myObject = PhotonNetwork.Instantiate("Player", playerLocations[PhotonNetwork.PlayerList.Length].transform.position, Quaternion.Euler(0,90,0), 0, null);

        //string playerName = "M-" + Random.Range(1, 10000).ToString();

        PhotonView pw = myObject.GetComponent<PhotonView>();
        pw.Owner.NickName = userName;

        //base.OnJoinedRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        foreach (var item in playerNamesText)
        {
            if (item.text.Equals(otherPlayer.NickName))
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
