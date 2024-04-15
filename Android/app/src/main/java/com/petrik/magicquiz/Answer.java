package com.petrik.magicquiz;

/** Az Answer osztály egy választ reprezentál a backend API-ból.
Az osztály tartalmazza a válasz azonosítóját, a hozzá tartozó kérdés azonosítóját, a válasz szövegét és azt, hogy helyes-e a válasz. */

public class Answer {
    /** válasz azonosítója */
    private int id;
    /** hozzá tartozó kérdés azonosítója */
    private int question_id;
    /** válasz szövege */

    private String answer_text;
    /** helyes-e a válasz */
    private int is_correct;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getQuestion_id() {
        return question_id;
    }

    public void setQuestion_id(int question_id) {
        this.question_id = question_id;
    }

    public String getAnswer_text() {
        return answer_text;
    }

    public void setAnswer_text(String answer_text) {
        this.answer_text = answer_text;
    }

    public int getIs_correct() {
        return is_correct;
    }

    public void setIs_correct(int is_correct) {
        this.is_correct = is_correct;
    }
}
