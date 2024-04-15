package com.petrik.magicquiz;
/** A RankItem osztály a ranglistában szereplő játékosok adatainak tárolásáért felelős. */
public class RankItem {
    /** A játékos neve */
    private String name;
    /** A játékos pontszáma */
    private int score;
    /** A RankItem konstruktora
     * @param name a játékos neve
     * @param point a játékos pontszáma */

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
