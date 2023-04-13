using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SG
{
    public class UIController : MonoBehaviour
    {
        [Header("Sliders")]
        public Slider _musicSlider, _sfxSlider;

        [Header("Menus")]
        public GameObject volumeWindow;
        public GameObject pauseBorder;
        public GameObject optionsFirstButton;
        public GameObject pauseFirstButton;

        [Header("Icons")]
        [SerializeField] private Sprite[] musicSprites;
        [SerializeField] private Sprite[] sfxSprites;

        [SerializeField] private Image musicButton;
        [SerializeField] private Image sfxButton;

        public void ChangeMusicSprite()
        {
            if (musicButton.sprite == musicSprites[0])
            {
                musicButton.sprite = musicSprites[1];
                return;
            }

            musicButton.sprite = musicSprites[0];
        }

        public void ChangeSFXSprite()
        {
            if (sfxButton.sprite == sfxSprites[0])
            {
                sfxButton.sprite = sfxSprites[1];
                return;
            }

            sfxButton.sprite = sfxSprites[0];
        }

        private void Start()
        {
            volumeWindow.SetActive(false);
            AudioManager.Instance.musicSource.volume = _musicSlider.value;
            AudioManager.Instance.sfxSource.volume = _sfxSlider.value;
        }

        public void OpenVolume()
        {
            volumeWindow.SetActive(true);
            pauseBorder.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
        }
        public void CloseVolume()
        {
            volumeWindow.SetActive(false);
            pauseBorder.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }

        public void ToggleMusic()
        {
            AudioManager.Instance.ToggleMusic();
        }

        public void ToggleSFX()
        {
            AudioManager.Instance.ToggleSFX();
        }

        public void MusicVolume()
        {
            AudioManager.Instance.MusicVolume(_musicSlider.value);
        }
        public void SfxVolume()
        {
            AudioManager.Instance.SfxVolume(_sfxSlider.value);
        }
    }
}

