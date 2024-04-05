package com.petrik.magicquiz;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.util.List;

public class Game extends AppCompatActivity {

    private TextView topicTextview;
    private TextView questionTextview;
    private Button answer0;
    private Button answer1;
    private Button answer2;
    private Button answer3;
    private TextView scoreTextview;
    private ImageView phone_button;
    private ImageView fifty_fifty_button;
    private ImageView audience_button;
    private Button exitButton;
    private int questionNumber = 0;
    private int score = 0;
    private String url = "http://10.0.2.2:8000/api/login";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);
        init();
        Player player = Player.getInstance();
        scoreTextview.setText(String.valueOf(score));
        LoadQuestions loadQuestions = new LoadQuestions(this);
        loadQuestions.getQuestionList(new LoadQuestions.QuestionDataLoadedListener() {
            @Override
            public void onQuestionDataLoaded(List<Question> questionList) {
                topicLoader(questionList);
            }
        });
    }

    private void topicLoader(List<Question> questionList) {
        LoadTopics loadTopics = new LoadTopics(this);
        loadTopics.getTopicList(new LoadTopics.TopicDataLoadedListener() {
            @Override
            public void onTopicDataLoaded(List<Topic> topicList) {
                game(questionList, topicList);
            }
        });
    }

    private void game(List<Question> questionList, List<Topic> topicList) {
        displayQuestion(questionList, topicList);

        answer0.setOnClickListener(v -> {
            checkAnswer(questionList, questionNumber, 0);
            questionNumber++;
            displayQuestion(questionList, topicList);
        });
        answer1.setOnClickListener(v -> {
            checkAnswer(questionList, questionNumber, 1);
            questionNumber++;
            displayQuestion(questionList, topicList);
        });
        answer2.setOnClickListener(v -> {
            checkAnswer(questionList, questionNumber, 2);
            questionNumber++;
            displayQuestion(questionList, topicList);
        });
        answer3.setOnClickListener(v -> {
            checkAnswer(questionList, questionNumber, 3);
            questionNumber++;
            displayQuestion(questionList, topicList);
        });
    }

    private void checkAnswer(List<Question> questionList, int questionNumber, int i) {
        Question currentQuestion = questionList.get(questionNumber);
        List<Answer> answers = currentQuestion.getAnswers();
        if (answers.get(i).getIs_correct() == 1) {
            Toast.makeText(this, "Helyes válasz", Toast.LENGTH_SHORT).show();
            score += 100;
            scoreTextview.setText(String.valueOf(score));
        } else {
            Toast.makeText(this, "Rossz válasz", Toast.LENGTH_SHORT).show();
        }
    }


    private void displayQuestion(List<Question> questionList, List<Topic> topicList) {
        if (questionNumber == questionList.size()) {
            Toast.makeText(this, "Gratulálok, végeztél a játékkal\nAz összpontszámod: " + score, Toast.LENGTH_SHORT).show();
            Player player = Player.getInstance();
            int currentScore = player.getScore();
            player.setScore(score > currentScore ? score : currentScore);
            uploadScore(player);
            Intent intent = new Intent(Game.this, DashboardActivity.class);
        }
        for (Question question : questionList) {
            Question currentQuestion = questionList.get(questionNumber);
            Topic currenttopic = topicList.get(currentQuestion.getTopic_id() - 1);
            topicTextview.setText(currenttopic.getTopicname());
            questionTextview.setText(currentQuestion.getQuestiontext());
            answer0.setText(currentQuestion.getAnswers().get(0).getAnswer_text());
            answer1.setText(currentQuestion.getAnswers().get(1).getAnswer_text());
            answer2.setText(currentQuestion.getAnswers().get(2).getAnswer_text());
            answer3.setText(currentQuestion.getAnswers().get(3).getAnswer_text());
        }
    }

    private void uploadScore(Player player) {
        //Gson converter = new Gson();
        //RequestTask requestTask = new RequestTask(url, "POST", converter.toJson(player));
        //requestTask.execute();
    }

    private void init() {
        topicTextview = findViewById(R.id.topicTextview);
        questionTextview = findViewById(R.id.questionTextview);
        answer0 = findViewById(R.id.answer0);
        answer1 = findViewById(R.id.answer1);
        answer2 = findViewById(R.id.answer2);
        answer3 = findViewById(R.id.answer3);
        scoreTextview = findViewById(R.id.scoreTextview);
        phone_button = findViewById(R.id.phone_button);
        fifty_fifty_button = findViewById(R.id.fifty_fifty_button);
        audience_button = findViewById(R.id.audience_button);
        exitButton = findViewById(R.id.exitButton);

    }
}





