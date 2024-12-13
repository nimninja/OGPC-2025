using UnityEngine;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{

[SerializeField] private Button ServerBtn;
[SerializeField] private Button HostBtn;
[SerializeField] private Button ClientBtn;

private void Awake(){
    ServerBtn.onClick.AddListener(() => {
        Network Manager.Singleton.StartServer();
    });
    HostBtnBtn.onClick.AddListener(() => {
        Network Manager.Singleton.StartHost();
    });
    ClientBtn.onClick.AddListener(() => {
        Network Manager.Singleton.StartClient();
    });
}

}
