package com.petrik.magicquiz;

public class LoadUserData {
    private String name;
    private String email;
    private int score;

    public LoadUserData(String name, String email, int point) {
        this.name = name;
        this.email = email;
        this.score = point;
    }

    public String getName() {
        return name;
    }

    public String getEmail() {
        return email;
    }

    public int getPoint() {
        return score;
    }
}
