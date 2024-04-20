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
/** A LoadBoosters osztály a booster-ek betöltéséért felelős. */
public class LoadBoosters {
    /** Az alkalmazás kontextusa */
    private Context mContext;
    /** Az API URL-je */
    public String url = "http://10.0.2.2:8000/api/boosters";
    /** A booster-ek listája */
    public static List<Booster> boosterList;
    /** A LoadBoosters konstruktora
     * @param context az alkalmazás kontextusa */
    public LoadBoosters(Context context) {
        this.mContext = context;
    }
    /** A booster-ek lekérdezéséért felelős metódus
     * @param listener a booster-ek betöltésének eseménykezelője */
    public void getBoosters(final BoosterDataLoadedListener listener) {

        SharedPreferences sharedpreferences = mContext.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = sharedpreferences.getString("token", "");
        RequestTask requestTask = new RequestTask(mContext, url, "GET", tokenString, listener);
        requestTask.execute();
    }
    /** A booster-ek betöltésének eseménykezelője */

    public interface BoosterDataLoadedListener {
        void onBoosterDataLoaded();
    }
/** A RequestTask osztály a backend API-val való kommunikációért felelős. */
    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;
        private LoadBoosters.BoosterDataLoadedListener bListener;

        public RequestTask(Context context, String requestUrl, String requestType, String requestParams, LoadBoosters.BoosterDataLoadedListener listener) {
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
            this.bListener = listener;
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
                        Type listType = new TypeToken<List<Booster>>() {
                        }.getType();
                        boosterList = converter.fromJson(responseContent, listType);
                        bListener.onBoosterDataLoaded();

                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();

                    }
                }
            }
        }
    }
}