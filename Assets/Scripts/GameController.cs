using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
  public Text questionText;
  public SimpleObjectPool answerButtonObjectPool;
  public Transform answerButtonParent;

  private DataController dataController;
  private RoundData currentRoundData;
  private QuestionData[] questionPool;

  private bool isRoundActive;
  private float timeRemaining;
  private int questionIndex;
  private int playerScore;

  private List<GameObject> answerButtonGameObjects = new List<GameObject>();
  // Start is called before the first frame update
  void Start()
  {
    dataController = FindObjectOfType<DataController>();
    currentRoundData = dataController.GetCurrentRoundData();
    questionPool = currentRoundData.questions;
    timeRemaining = currentRoundData.timeLimitInSeconds;

    playerScore = 0;
    questionIndex = 0;

    ShowQuestion();
    isRoundActive = true;
  }

  private void ShowQuestion()
  {
    RemoveAnswerButtons();
    QuestionData questionData = questionPool[questionIndex];
    questionText.text = questionData.questionText;

    for(int i = 0; i < questionData.answers.Length; i++)
    {
      GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
      answerButtonGameObject.transform.SetParent(answerButtonParent);
      answerButtonGameObjects.Add(answerButtonGameObject);

      AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
      answerButton.Setup(questionData.answers[i]);
    }
  }

  private void RemoveAnswerButtons()
  {
    while (answerButtonGameObjects.Count > 0)
    {
      answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
      answerButtonGameObjects.RemoveAt(0);
    }
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
