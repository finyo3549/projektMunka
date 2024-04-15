package com.petrik.magicquiz;
/** A Response osztály a válaszok adatainak tárolásáért felelős. */
public class Response {
    /** A válasz státuszkódja */
    private int responseCode;
    /** A válasz tartalma */
    private String content;
    /** A Response konstruktora
     * @param responseCode a válasz státuszkódja
     * @param content a válasz tartalma */
    public Response(int responseCode, String content) {
        this.responseCode = responseCode;
        this.content = content;
    }

    public int getResponseCode() {
        return responseCode;
    }

    public void setResponseCode(int responseCode) {
        this.responseCode = responseCode;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }
}
