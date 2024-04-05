package com.petrik.magicquiz;

import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.IOException;
import java.util.Map;

public class Logout {
    private String url = "http://10.0.2.2:8000/api/logout";
    private Context mContext;

    public void logoutUser(final LogoutListener listener) {
        SharedPreferences token = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = token.getString("token", "");
        RequestTask requestTask = new RequestTask(mContext, url, "POST", tokenString, listener);
        requestTask.execute();
    }

    public interface LogoutListener {
        void onUserLoggedOut();
    }

    public Logout(Context context) {
        this.mContext = context;
    }

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;
        Context mContext;

        private LogoutListener mListener;

        public RequestTask(Context context, String requestUrl, String requestType, String requestParams, LogoutListener listener) {
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
                if (requestType.equals("POST")) {
                    response = RequestHandler.post(requestUrl, requestParams);
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