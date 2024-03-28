using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Volume PostProcessingVolume;
    public Image BlackImage;
    public float VignetteFadeSpeed = 2f;
    public RectTransform MenuRect;
    public List<Button> MenuButtons;

    public CameraController CamController;
    public AgentCharacter SwampQueenCharacter;
    public Transform CharacterDoorPosition;

    private Vignette vignettePP;
    
    private void Awake()
    {
        //Just for mockup purposes:
        MenuButtons.ForEach(button => button.onClick.AddListener(HandleButtonClick));

        PostProcessingVolume.profile.TryGet(out vignettePP);
    }

    private void HandleButtonClick()
    {
        Debug.Log("Menu click!");
        MenuRect.gameObject.SetActive(false);

        CamController.AnimateFOV();
        SwampQueenCharacter.SetAgentDest(CharacterDoorPosition.position);

        StartCoroutine(AnimateIntensity());
    }

    IEnumerator AnimateIntensity()
    {
        while (vignettePP.intensity.value <= .9f)
        {
            vignettePP.intensity.value += Time.deltaTime * VignetteFadeSpeed;
            yield return null;
        }

        BlackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
