using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BlackPad.DropCube.Menus
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _inputScore;
        [SerializeField] TMP_InputField _inputName;

        public UnityEvent<string, int> _submitScoreEvent;

        public void SubmitScore()
        {
            _submitScoreEvent.Invoke(_inputName.text, int.Parse(_inputScore.text));
        }
    }
}
