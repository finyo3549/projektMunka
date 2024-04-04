package com.petrik.magicquiz;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import java.util.List;

public class Game extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);
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
        Question question = questionList.get(0);
        List<Answer> answers = question.getAnswers();
        for (Answer answer : answers) {
            if (answer.getIs_correct() == 1) {
                // correct answer
            }
        }
    }
}





