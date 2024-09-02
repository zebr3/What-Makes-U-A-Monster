using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public Image MapDisplayImage;
    public Image FloorDisplayImage;
    public RectTransform redDotRectTransform;


    public Sprite groundFloorSprite;
    public Sprite belowGroundFloorSprite;

    public Sprite groundFloorDisplaySprite;
    public Sprite belowGroundFloorDisplaySprite;

    public Vector2 lowerCampusPosition;
    public Vector2 upperCampusPosition;
    public Vector2 mainHallwayPosition;
    public Vector2 classroomPosition;
    public Vector2 fightroomPosition;
    public Vector2 libraryPosition;
    public Vector2 mensaPosition;
    public Vector2 lowerHallwayPosition;
    public Vector2 dormroomPosition;
    public Vector2 storageroomPosition;




    private void Update()
    {
        UpdateMapUI();
    }

    private void UpdateMapUI()
    {


        string currentSceneName = SceneManager.GetActiveScene().name;


        if (currentSceneName.Equals("3FirstCampusScene"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = lowerCampusPosition;
        }
        if (currentSceneName.Equals("13.4LowerCampus"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = lowerCampusPosition;
        }
        if (currentSceneName.Equals("13.3UpperCampus"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = upperCampusPosition;
        }
        if (currentSceneName.Equals("6MainHallway"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = mainHallwayPosition;
        }
        if (currentSceneName.Equals("13.0LeftHallway"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = mainHallwayPosition;
        }
        if (currentSceneName.Equals("DirtyHallway"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = mainHallwayPosition;
        }
        if (currentSceneName.Contains("Fightroom"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = fightroomPosition;
        }
        if (currentSceneName.Contains("KellyQuestEnd"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = fightroomPosition;
        }
        if (currentSceneName.Contains("Minigame"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = fightroomPosition;
        }
        if (currentSceneName.Contains("Classroom"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = classroomPosition;
        }
        if (currentSceneName.Contains("Mensa"))
        {
            MapDisplayImage.sprite = groundFloorSprite;
            FloorDisplayImage.sprite = groundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = mensaPosition;
        }
        if (currentSceneName.Equals("7LowerHallway"))
        {
            MapDisplayImage.sprite = belowGroundFloorSprite;
            FloorDisplayImage.sprite = belowGroundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = lowerHallwayPosition;
        }
        if (currentSceneName.Equals("13.5Lowerhallway"))
        {
            MapDisplayImage.sprite = belowGroundFloorSprite;
            FloorDisplayImage.sprite = belowGroundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = lowerHallwayPosition;
        }
        if (currentSceneName.Contains("Dormroom"))
        {
            MapDisplayImage.sprite = belowGroundFloorSprite;
            FloorDisplayImage.sprite = belowGroundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = dormroomPosition;
        }
        if (currentSceneName.Contains("Storageroom"))
        {
            MapDisplayImage.sprite = belowGroundFloorSprite;
            FloorDisplayImage.sprite = belowGroundFloorDisplaySprite;
                redDotRectTransform.anchoredPosition = storageroomPosition;
            }
        if (currentSceneName.Contains("Library"))
        {
            MapDisplayImage.sprite = belowGroundFloorSprite;
            FloorDisplayImage.sprite = belowGroundFloorDisplaySprite;
            redDotRectTransform.anchoredPosition = libraryPosition;
        }


    }
}
