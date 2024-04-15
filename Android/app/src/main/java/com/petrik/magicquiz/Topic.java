package com.petrik.magicquiz;
/** A Topic osztály a témák adatainak tárolásáért felelős. */
public class Topic {
    /** A témák azonosítója */
    private int id;
    /** A témák neve */
    private String topicname;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getTopicname() {
        return topicname;
    }

    public void setTopicname(String topicname) {
        this.topicname = topicname;
    }
}
