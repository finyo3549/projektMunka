package com.petrik.magicquiz;
import static android.content.Context.MODE_PRIVATE;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.google.gson.reflect.TypeToken;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.List;

public class LoadTopics {
    public String url = "http://10.0.2.2:8000/api/topics";
    public  Context mContext;
    private Game gameActivity;
    private List<Topic> topicList;
    public LoadTopics(Context context) {
        this.mContext = context;
    }

    public void getTopicList(final TopicDataLoadedListener listener) {
        SharedPreferences sharedpreferences = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = sharedpreferences.getString("token", "");
        RequestTask requestTask = new RequestTask(mContext, url, "GET", tokenString, listener);
        requestTask.execute();
    }

    public interface TopicDataLoadedListener {
        void onTopicDataLoaded(List<Topic> topicList);
    }

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;
        Context mContext;

        private TopicDataLoadedListener tListener;

        public List<Topic> getTopicList() {
            return topicList;
        }
        public RequestTask(Context context, String requestUrl, String requestType, String requestParams, TopicDataLoadedListener listener) {
            this.mContext = context;
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
            this.tListener = listener;
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
                        Type listType = new TypeToken<List<Topic>>(){}.getType();
                        topicList = converter.fromJson(responseContent, listType);
                        tListener.onTopicDataLoaded(topicList);

                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();

                    }
                }
            }
        }
    }
}
