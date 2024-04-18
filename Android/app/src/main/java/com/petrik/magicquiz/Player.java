package com.petrik.magicquiz;
/** A Player osztály a játékos adatainak tárolásáért felelős. */
public class Player {
    /** Az egyetlen példány a játékosból */
    private static Player instance;
    /** A játékos neve */
    private String name;
    /** A játékos jelszava */
    private String password;
    /** A játékos email címe */
    private String email;
    /** A játékos pontszáma */
    private int score;
    /** A játékos azonosítója */
    private int id;
    /** A játékos neme */
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
    /** A Player osztály konstruktora */
    public Player(String email, String password) {
        this.password = password;
        this.email = email;
    }
    /** A Player osztály konstruktora */
    public Player(String name, String password, String email, String gender) {
        this.name = name;
        this.password = password;
        this.email = email;
        this.gender = gender;

    }
    /** A Player osztály konstruktora */
    public Player(String name, String password, String email, int score) {
        this.name = name;
        this.password = password;
        this.email = email;
        this.score = score;
    }
}
