package com.petrik.magicquiz;
import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.widget.Toast;

import com.google.android.material.navigation.NavigationView;
import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.IOException;
import java.util.Map;

public class LoadUserData {
    private String name;
    private String email;
    private int user_id;
    private int score;
    public String url = "http://10.0.2.2:8000/api/user-ranks/";
    private static Context mContext;
    private DashboardActivity dashboardActivity;


    public LoadUserData(Context context) {
        this.mContext = context;
    }

    public void getUserData(final UserDataLoadedListener listener) {
        SharedPreferences sharedpreferences = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = sharedpreferences.getString("token", "");
        user_id = sharedpreferences.getInt("user_id", 0);
        url = url + user_id;
        RequestTask requestTask = new RequestTask(mContext, url, "GET", tokenString, listener);
        requestTask.execute();
    }

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
    public interface UserDataLoadedListener {
        void onUserDataLoaded();
    }
    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;
        Context mContext;


        private UserDataLoadedListener mListener;

        public RequestTask(Context context, String requestUrl, String requestType, String requestParams, UserDataLoadedListener listener) {
            this.mContext = context;
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
            this.mListener = listener;

        }


        @Override
        protected Response doInBackground(Void... voids) {
            Response response = null;
            try {
                if (requestType.equals("GET")) {
                    response = RequestHandler.get(requestUrl, requestParams);
                }
            } catch (IOException e) {

            }
            return response;
        }

        @Override
        protected void onPostExecute(Response response) {
            Gson converter = new Gson();
            String responseContent;
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                Toast.makeText(mContext,
                        responseContent, Toast.LENGTH_SHORT).show();
                System.exit(0);

            }
            if (requestType.equals("GET")) {
                if (response.getResponseCode() == 200) {
                    responseContent = response.getContent();

                    try {
                        Map<String, Object> responseData = converter.fromJson(responseContent, Map.class);
                        String name = (String)responseData.get("name");
                        String email = (String)responseData.get("email");
                        score = ((Double) responseData.get("score")).intValue();
                        SharedPreferences userData = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
                        SharedPreferences.Editor editor = userData.edit();
                        editor.putString("name", name);
                        editor.putString("email", email);
                        editor.putInt("score", score);
                        editor.apply();
                        Player player = Player.getInstance();
                        player.setName(name);
                        player.setEmail(email);
                        player.setScore(score);
                        player.setId(user_id);
                        mListener.onUserDataLoaded();

                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();

                    }
                }
            }
        }
    }
}
