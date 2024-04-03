package com.petrik.magicquiz;

public class RankItem {
    private String name;
    private int score;

    public RankItem(String name, int point) {
        this.name = name;
        this.score = score;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getScore() {
        return score;
    }

    public void setPoint(int score) {
        this.score = score;
    }
}
