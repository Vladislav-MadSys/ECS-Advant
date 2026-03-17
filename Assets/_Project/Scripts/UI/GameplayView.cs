using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceText;

        public void UpdateBalance(string newBalance)
        {
            balanceText.text = newBalance;
        }
    }
}
