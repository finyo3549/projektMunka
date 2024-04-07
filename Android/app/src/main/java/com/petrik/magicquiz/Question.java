package com.petrik.magicquiz;

import java.util.List;

public class Question {
    private int id;
    private String question_text;
    private int topic_id;
    private List<Answer> answers;
    public List<Answer> getAnswers() {
        return answers;
    }

    public void setAnswers(List<Answer> answers) {
        this.answers = answers;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getQuestiontext() {
        return question_text;
    }

    public void setQuestiontext(String questiontext) {
        this.question_text = questiontext;
    }

    public int getTopic_id() {
        return topic_id;
    }

    public void setTopic_id(int topic_id) {
        this.topic_id = topic_id;
    }
}
