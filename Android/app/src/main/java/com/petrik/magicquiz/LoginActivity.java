package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import java.io.IOException;
import java.util.Map;

public class LoginActivity extends AppCompatActivity {

    private Button loginButton;
    private EditText loginUsername;
    private EditText loginPassword;
    private Button loginCancelButton;
    private String requestUrl = "http://10.0.2.2:8000/api/login";
    private String responseContent = "";
    private String token = "";
    private int backButtonCount = 0;
    private ProgressBar loginProgressBar;
    private Button LoginregisterButton;

    @Override
    public void onBackPressed() {
        if (backButtonCount == 0) {
            Toast.makeText(this, "Nyomd meg a vissza gombot újra a kliépéshez", Toast.LENGTH_SHORT).show();
            backButtonCount++;
            new Handler().postDelayed(new Runnable() {
                @Override
                public void run() {
                    backButtonCount = 0;
                }
            }, 2000);
        } else {
        System.exit(0);        }
    }
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login2);
        SharedPreferences sharedPreferences = getSharedPreferences("userdata", Context.MODE_PRIVATE);
        if(sharedPreferences.contains("token")) {
            Intent intent = new Intent(LoginActivity.this, DashboardActivity.class);
            startActivity(intent);
        }
        init();

        loginButton.setOnClickListener(v -> {
            String email = loginUsername.getText().toString();
            String password = loginPassword.getText().toString();
            if (email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Felhasználónév/jelszó nem lehet üres", Toast.LENGTH_SHORT).show();
            } else {
                Player player = new Player(email, password);
                Gson converter = new Gson();
                RequestTask requestTask = new RequestTask(requestUrl, "POST", converter.toJson(player));
                loginProgressBar.setVisibility(ProgressBar.VISIBLE);
                requestTask.execute();

            }
        });
        LoginregisterButton.setOnClickListener(v -> {
            Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
            startActivity(intent);
        });
        loginCancelButton.setOnClickListener(v -> {
            Intent intent = new Intent(LoginActivity.this, MainActivity.class);
            startActivity(intent);
            finish();
        });
    }

    private void init() {
        loginButton = findViewById(R.id.loginButton);
        loginUsername = findViewById(R.id.loginUsername);
        loginPassword = findViewById(R.id.loginPassword);
        loginCancelButton = findViewById(R.id.loginCancelButton);
        loginProgressBar = findViewById(R.id.loginProgressBar);
        LoginregisterButton = findViewById(R.id.LoginregisterButton);
    }

    private class RequestTask extends AsyncTask<Void, Void, Response> {
        String requestUrl;
        String requestType;
        String requestParams;

        public RequestTask(String requestUrl, String requestType, String requestParams) {
            this.requestUrl = requestUrl;
            this.requestType = requestType;
            this.requestParams = requestParams;
        }

        @Override
        protected Response doInBackground(Void... voids) {
            Response response = null;
            try {
                if (requestType.equals("POST")) {
                    response = RequestHandler.post(requestUrl, requestParams);
                }
            } catch (IOException e) {
                LoginActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Toast.makeText(LoginActivity.this,
                                e.toString(), Toast.LENGTH_SHORT).show();
                    }
                });
            }
            return response;
        }

        @Override
        protected void onPostExecute(Response response) {
            loginProgressBar.setVisibility(ProgressBar.GONE);
            Gson converter = new Gson();
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                Toast.makeText(LoginActivity.this,
                        responseContent, Toast.LENGTH_SHORT).show();

            }
            if (requestType.equals("POST")) {
                if (response.getResponseCode() == 200) {
                    responseContent = response.getContent();
                    try {
                        Map<String, Object> responseData = converter.fromJson(responseContent, Map.class);
                        String token = (String)responseData.get("token");
                        int id = ((Double) responseData.get("user_id")).intValue();
                        Toast.makeText(LoginActivity.this, "Sikeres bejelentkezés", Toast.LENGTH_SHORT).show();
                        SharedPreferences sharedPreferences = getSharedPreferences("userdata", Context.MODE_PRIVATE);
                        SharedPreferences.Editor editor = sharedPreferences.edit();
                        editor.putString("token", token);
                        editor.putInt("user_id", id);
                        editor.apply();
                        Intent intent = new Intent(LoginActivity.this, DashboardActivity.class);
                        startActivity(intent);
                    } catch (JsonSyntaxException e) {
                        e.printStackTrace();
                        Toast.makeText(LoginActivity.this, "Error parsing token", Toast.LENGTH_SHORT).show();
                    }
                }
            }
        }
    }
}

