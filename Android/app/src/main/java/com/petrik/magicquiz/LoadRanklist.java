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
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
/** A LoadRanklist osztály a ranglisták betöltéséért felelős. */
public class LoadRanklist {
    /** A felhasználó neve */
    private String name;
    /** A felhasználó email címe */
    private String email;
    /** A felhasználó id-je */
    private int user_id;
    /** A felhasználó pontszáma */
    private int score;
    /** Az alkalmazás kontextusa */
    public  Context mContext;

    /** A raklista betöltő metódusa */
    public void getRanklist(final RankListLoadedListener listener) {
        SharedPreferences sharedpreferences = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = sharedpreferences.getString("token", "");
        String url = "http://10.0.2.2:8000/api/user-ranks/";
        RequestTask requestTask = new RequestTask(mContext, url, "GET", tokenString,listener);
        requestTask.execute();
    }
    /** A ranglista betöltésének eseménykezelője */
    public interface RankListLoadedListener {
        void onRanklistLoaded(List<RankItem> rankItems);
    }
    /** A LoadRanklist konstruktora
     * @param context az alkalmazás kontextusa */
    public LoadRanklist(Context context) {
        this.mContext = context;
    }
       /** A RequestTask osztály a backend API-val való kommunikációért felelős. */

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;
        Context mContext;
        private RankListLoadedListener mListener;


        public RequestTask(Context context, String requestUrl, String requestType, String requestParams , RankListLoadedListener listener) {
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
                        Type listType = new TypeToken<List<RankItem>>(){}.getType();
                        List<RankItem> rankItems = converter.fromJson(responseContent, listType);
                        mListener.onRanklistLoaded(rankItems);
                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();

                    }
                }
            }
        }
    }
}
