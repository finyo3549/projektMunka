package com.petrik.magicquiz;
import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.IOException;
import java.util.Map;

public class LoadUserData2 {
    private String name;
    private String email;
    private int score;
    public String url = "http://10.0.2.2:8000/api/user-ranks";
    private static Context mContext;
    private DashboardActivity dashboardActivity;


    public LoadUserData2(Context context) {
        this.mContext = context;
    }

    public void getUserData(final UserDataLoadedListener listener) {
        SharedPreferences token = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = token.getString("token", "");
        RequestTask requestTask = new RequestTask(mContext, url, "GET", tokenString, listener);
        requestTask.execute();
    }

    public LoadUserData2(String name, String email, int score) {
        this.name = name;
        this.email = email;
        this.score = score;
    }

    public String getName() {
        return name;
    }

    public String getEmail() {
        return email;
    }

    public int getScore() {
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
            }
            if (requestType.equals("GET")) {
                if (response.getResponseCode() == 200) {
                    responseContent = response.getContent();

                    try {
                        Map<String, String> responseData = converter.fromJson(responseContent, Map.class);
                        String name = responseData.get("name");
                        String email = responseData.get("email");
                        //int score = Integer.parseInt(responseData.get("score"));

                        SharedPreferences userData = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
                        SharedPreferences.Editor editor = userData.edit();
                        editor.putString("name", name);
                        editor.putString("email", email);
                        //editor.putInt("score", score);
                        editor.apply();
                        mListener.onUserDataLoaded();

                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();

                    }
                }
            }
        }
    }
}
