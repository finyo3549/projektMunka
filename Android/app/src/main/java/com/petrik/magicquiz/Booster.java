package com.petrik.magicquiz;
/** A Booster osztály egy booster-t reprezentál a backend API-ból.

 */
public class Booster {
    /** booster neve */
    private String booster_name = "";
    /** booster leírása */
    private String booster_description = "";


    public String getBooster_name() {
        return booster_name;
    }

    public void setBooster_name(String booster_name) {
        this.booster_name = booster_name;
    }

    public String getBooster_description() {
        return booster_description;
    }

    public void setBooster_description(String booster_description) {
        this.booster_description = booster_description;
    }
}
