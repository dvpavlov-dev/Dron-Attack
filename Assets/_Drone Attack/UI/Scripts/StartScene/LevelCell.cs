using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Drone_Attack.UI.Scripts.StartScene
{
    public class LevelCell : MonoBehaviour
    {
        public Action<int> OnSelectedLevel {get; set;}

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _titleText;

        private int _index;
        private TypeCell _typeCell;

        public void SetupCell(int index, TypeCell typeCell)
        {
            _typeCell = typeCell;
            _index = index;
            _titleText.text = (index + 1).ToString();
            
            switch (typeCell)
            {
                case TypeCell.PASS:
                    _backgroundImage.color = Color.green;
                    break;
                
                case TypeCell.CURRENT:
                    _backgroundImage.color = Color.yellow;
                    break;
                
                case TypeCell.NOT_PASS:
                    _backgroundImage.color = Color.red;
                    break;
                
                default:
                    _backgroundImage.color = Color.red;
                    break;
            }
        }

        public void SelectLevel()
        {
            if (_typeCell == TypeCell.NOT_PASS)
            {
                return;
            }
            
            OnSelectedLevel?.Invoke(_index);
        }
    }

    public enum TypeCell
    {
        PASS,
        CURRENT,
        NOT_PASS,
    }
}
