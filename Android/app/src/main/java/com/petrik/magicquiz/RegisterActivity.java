package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.widget.EditText;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.Toast;

import com.google.gson.Gson;

import java.io.IOException;
/** A RegisterActivity egy Activity, amely a regisztrációs felületet valósítja meg. */
public class RegisterActivity extends AppCompatActivity {
    /** A felhasználónév megadására szolgáló mező */
    private EditText registerUsername;
    /** Az email cím megadására szolgáló mező */
    private EditText registerEmail;
    /** A jelszó megadására szolgáló mező */
    private EditText registerPassword;
    /** A regisztráció gomb */
    private Button registerButton;
    /** A regisztráció megszakítására szolgáló gomb */
    private Button registerCancelButton;
    /** A regisztrációhoz szükséges URL */
    private String requestUrl = "http://10.0.2.2:8000/api/register";
    /** A válasz tartalma */
    private String responseContent = "";
    /** A nem kiválasztására szolgáló gomb */
    private RadioButton maleRadioButton;
    /** A férfi nem kiválasztására szolgáló gomb */
    private RadioButton femaleRadioButton;
    /** A nembináris nem kiválasztására szolgáló gomb */
    private RadioButton nonBinaryButton;
    /** A felhasználó neme */
    private String gender = "";

    /** Az Activity létrehozásakor lefutó metódus */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        init();
        registerCancelButton.setOnClickListener(v -> {
            Intent intent = new Intent(RegisterActivity.this, LoginActivity.class);
            startActivity(intent);
        });
        registerButton.setOnClickListener(v -> {
            String username = registerUsername.getText().toString();
            String email = registerEmail.getText().toString();
            String password = registerPassword.getText().toString();
            if (username.isEmpty() || email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Felhasználónév, email vagy jelszó nem lehet üres", Toast.LENGTH_SHORT).show();
                return;
            } else if (!maleRadioButton.isChecked() && !femaleRadioButton.isChecked() && !nonBinaryButton.isChecked()) {
                Toast.makeText(this, "Kérjük válasszon nemet", Toast.LENGTH_SHORT).show();
                return;
            }
            if (maleRadioButton.isChecked()) {
                gender = "male";
            } else if (femaleRadioButton.isChecked()) {
                gender = "female";
            } else if (nonBinaryButton.isChecked()) {
                gender = "nonbinary";
            }
            Player player = new Player(username, password, email, gender);
            Gson converter = new Gson();
                RequestTask requestTask = new RequestTask(requestUrl, "POST", converter.toJson(player));
            requestTask.execute();

        });
    }
/** Az Activity inicializálására szolgáló metódus */
    private void init() {
        registerUsername = findViewById(R.id.registerUsername);
        registerEmail = findViewById(R.id.registerEmail);
        registerPassword = findViewById(R.id.registerPassword);
        registerButton = findViewById(R.id.registerButton);
        registerCancelButton = findViewById(R.id.registerCancelButton);
        maleRadioButton = findViewById(R.id.maleRadioButton);
        femaleRadioButton = findViewById(R.id.femaleRadioButton);
        nonBinaryButton = findViewById(R.id.nonBinaryButton);
    }
/** A RequestTask osztály a hálózati kérés elküldéséért felelős. */
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
                Toast.makeText(RegisterActivity.this,
                        e.toString(), Toast.LENGTH_SHORT).show();
            }
            return response;
        }

        //onPostExecute metódus létrehozása a válasz feldolgozásához
        @Override
        protected void onPostExecute(Response response) {
            super.onPostExecute(response);
            Gson converter = new Gson();
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                Toast.makeText(RegisterActivity.this,
                        responseContent, Toast.LENGTH_SHORT).show();
            }
            if (requestType.equals("POST")) {
                if (response.getResponseCode() == 201) {
                    Toast.makeText(RegisterActivity.this, "Sikeres regisztráció", Toast.LENGTH_SHORT).show();
                    Intent intent = new Intent(RegisterActivity.this, LoginActivity.class);
                    startActivity(intent);
                }
            }
        }
    }
}