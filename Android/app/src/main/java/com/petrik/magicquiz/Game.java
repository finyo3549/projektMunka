package com.petrik.magicquiz;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.google.gson.Gson;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class Game extends AppCompatActivity implements GameResultListener {

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

    private String url = "http://10.0.2.2:8000/api/user-ranks";

    private GameResultListener gameResultListener;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);
        init();
        Player player = Player.getInstance();
        scoreTextview.setText(String.valueOf(score));
        gameResultListener = (GameResultListener) this;
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
        try {
            if (questionNumber == questionList.size()) {
                Toast.makeText(this, "Gratulálok, végeztél a játékkal\nAz összpontszámod: " + score, Toast.LENGTH_SHORT).show();
                Player player = Player.getInstance();
                int currentScore = player.getScore();
                player.setScore(score > currentScore ? score : currentScore);
                uploadScore(player);
            } else {
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
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    private void uploadScore(Player player) throws JSONException {
        try {
            Gson converter = new Gson();
            SharedPreferences sharedPreferences = getSharedPreferences("userdata", MODE_PRIVATE);
            String tokenString = sharedPreferences.getString("token", "");
            Map<String, String> headers = new HashMap<>();
            headers.put("Authorization", "Bearer " + tokenString);
            url = url + "/" + player.getId();
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("score", String.valueOf(score));
            String requestBody = jsonObject.toString();
            RequestTask requestTask = new RequestTask(url, "PUT", requestBody, headers);
            requestTask.execute();
        } catch (JSONException e) {
            e.printStackTrace();
        }
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

    @Override
    public void onGameFinished() {
        Intent intent = new Intent(Game.this, DashboardActivity.class);
        startActivity(intent);
    }

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        private final Map<String, String> headers;
        String requestUrl;
        String requestType;
        String requestParams;

        public RequestTask(String requestUrl, String requestType, String requestParams, Map<String, String> headers) {
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
            this.headers = headers;
        }

        @Override
        protected Response doInBackground(Void... voids) {
            Response response = null;
            try {
                if (requestType.equals("PUT")) {
                    response = RequestHandler.put(requestUrl, requestParams, headers.get("Authorization"));
                }
            } catch (IOException e) {
                Game.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(Game.this,
                                e.toString(), Toast.LENGTH_SHORT).show();
                    }
                });
            }
            return response;
        }

        @Override
        protected void onPostExecute(Response response) {
            Gson converter = new Gson();
            String responseContent;
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                Toast.makeText(Game.this,
                        responseContent, Toast.LENGTH_SHORT).show();
                System.exit(0);

            }
            if (requestType.equals("PUT")) {
                if (response.getResponseCode() == 200) {
                    responseContent = response.getContent();
                    try {
                        Map<String, Object> responseData = converter.fromJson(responseContent, Map.class);
                        gameResultListener.onGameFinished();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }
    }
}





