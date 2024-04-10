package com.petrik.magicquiz;

public class Player {
    private static Player instance;

    private String name;
    private String password;
    private String email;
    private int score;
    private int id;
    private String gender;

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    private Player() {

    }
    public static Player getInstance() {
        if (instance == null) {
            instance = new Player();
        }
        return instance;
    }
    public String getName() {
        return name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getEmail() {
        return email;
    }

    public int getScore() {
        return score;
    }

    public void setScore(int score) {
        this.score = score;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Player(String email, String password) {
        this.password = password;
        this.email = email;
    }

    public Player(String name, String password, String email, String gender) {
        this.name = name;
        this.password = password;
        this.email = email;
        this.gender = gender;

    }
    public Player(String name, String password, String email, int score) {
        this.name = name;
        this.password = password;
        this.email = email;
        this.score = score;
    }
}
