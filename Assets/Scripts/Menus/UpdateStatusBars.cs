using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatusBars : MonoBehaviour
{
    public Image health;
    public Image resolve;
    public Material glowMat;
    private Material defaultMat;
    public TextMeshProUGUI shurikenCount;

    public static UpdateStatusBars instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        defaultMat = resolve.material;
    }

    void Update()
    {
        UpdateStatusBar(health, PlayerState.GetPlayerStatus().GetHP(), PlayerState.GetPlayerStatus().GetTotalHP());
        UpdateStatusBar(resolve, PlayerState.GetPlayerStatus().GetResolve(), PlayerState.GetPlayerStatus().GetTotalResolve());

        if (PlayerState.GetPlayerStatus().GetResolve() == PlayerState.GetPlayerStatus().GetTotalResolve())
        {
            resolve.material = glowMat;
        }
        else
        {
            resolve.material = defaultMat;
        }

        shurikenCount.text = PlayerState.GetPlayerStatus().GetShurikenCount().ToString();
    }

    void UpdateStatusBar(Image bar, float curr, float total)
    {
        bar.fillAmount = curr / total;
    }
}
