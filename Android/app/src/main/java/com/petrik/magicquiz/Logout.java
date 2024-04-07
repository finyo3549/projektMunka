package com.petrik.magicquiz;

import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class Logout {
    private String url = "http://10.0.2.2:8000/api/logout";
    private Context mContext;
    private LogoutListener mListener;


    public void logoutUser() {
        SharedPreferences token = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = token.getString("token", "");
        Map<String, String> headers = new HashMap<>();
        headers.put("Authorization", "Bearer " + tokenString);
        String RequestParams = "";
        RequestTask requestTask = new RequestTask(mContext,url, "POST", RequestParams,  headers, mListener);
        requestTask.execute();
    }

    public interface LogoutListener {
        void onUserLoggedOut();
    }
    public void setListener(LogoutListener listener) {
        this.mListener = listener;
    }

    public Logout(Context context) {
        this.mContext = context;
    }

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        private final LogoutListener mListener;
        String requestUrl;
        String requestType;
        String requestParams;
        Context mContext;
        Map<String, String> headers;


        public RequestTask(Context context, String requestUrl, String requestType, String requestParams, Map<String, String> headers, LogoutListener mListener) {
            this.mContext = context;
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
            this.headers = headers;
            this.mListener = mListener;
        }


        @Override
        protected Response doInBackground(Void... voids) {
            Response response = null;
            try {
                if (requestType.equals("POST")) {
                    response = RequestHandler.postAuthenticated(requestUrl, requestParams, headers.get("Authorization"));
                }
            } catch (IOException e) {

            }
            return response;
        }

        @Override
        protected void onPostExecute(Response response) {
            String responseContent;
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                Toast.makeText(mContext,
                        responseContent, Toast.LENGTH_SHORT).show();
            }
            if (requestType.equals("POST")) {
                if (response.getResponseCode() == 204) {
                    mListener.onUserLoggedOut();
                }
            }
        }
    }
}