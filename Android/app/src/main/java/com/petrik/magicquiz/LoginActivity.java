package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.Map;

/** A LoginActivity a kezdőképernyő. A felhasználó bejelentkezhet a játékba, vagy regisztrálhat. A bejelentkezéshez szükséges adatokat a backend API-n keresztül ellenőrzi. Ha a bejelentkezés sikeres, a felhasználó a DashboardActivity-re irányítódik.
 */
public class LoginActivity extends AppCompatActivity {
    /** A bejelentkezés gomb */
    private Button loginButton;
    /** A felhasználónév megadására szolgáló mező */
    private EditText loginUsername;
    /** A jelszó megadására szolgáló mező */
    private EditText loginPassword;
    /** A bejelentkezés megszakítására szolgáló gomb */
    private Button loginCancelButton;
    /** A bejelentkezéshez szükséges URL */
    private final String requestUrl = "http://10.0.2.2:8000/api/login";
    /** A válasz tartalma */
    private String responseContent = "";
    /** A vissza gomb lenyomásának száma */
    private int backButtonCount = 0;
    /** A bejelentkezési folyamat jelzője */
    private ProgressBar loginProgressBar;
    /** A regisztráció gomb */
    private Button LoginregisterButton;
    /** A hibaüzenet */
    String errorMessage = "";

    @Override
    public void onBackPressed() {
        if (backButtonCount == 0) {
            Toast.makeText(this, "Nyomd meg a vissza gombot újra a kilépéshez", Toast.LENGTH_SHORT).show();
            backButtonCount++;
            new Handler().postDelayed(() -> backButtonCount = 0, 2000);
        } else {
            System.exit(0);
        }
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
/** A loginButton megnyomásakor a felhasználó által megadott felhasználónév és jelszó alapján egy Player objektumot hoz létre,
amelyet JSON formátumba alakít át. A RequestTask segítségével POST kérést küld a backend API-nak a bejelentkezési adatokkal.
Ha a bejelentkezés sikeres, a felhasználó a DashboardActivity-re irányítódik. Ha a bejelentkezés sikertelen, a backend API hibakódját
és hibaüzenetét jeleníti meg a felhasználónak.

 */
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
        /* A LoginregisterButton megnyomásakor a felhasználó a RegisterActivity-re irányítódik, ahol regisztrálhat.
         */
        LoginregisterButton.setOnClickListener(v -> {
            Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
            startActivity(intent);
        });
        loginCancelButton.setOnClickListener(v -> {
            Toast.makeText(this, "Kilépés", Toast.LENGTH_SHORT).show();
            //finish();
            System.exit(0);
        });
    }

    /** Az init metódus inicializálja a LoginActivity elemeit.
     */
    private void init() {
        loginButton = findViewById(R.id.loginButton);
        loginUsername = findViewById(R.id.loginUsername);
        loginPassword = findViewById(R.id.loginPassword);
        loginCancelButton = findViewById(R.id.loginCancelButton);
        loginProgressBar = findViewById(R.id.loginProgressBar);
        LoginregisterButton = findViewById(R.id.LoginregisterButton);
    }
/** A RequestTask osztály felelős a backend API-val való kommunikációért. A doInBackground metódusban POST kérést küld a backend API-nak a megadott adatokkal.

 */
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
                 errorMessage = "Kommunikációs hiba a backenddel! Ellenőrizze az internetkapcsolatot és próbáljon újból belépni!";
            }
            return response;
        }

        @Override
        protected void onPostExecute(Response response) {
            if(errorMessage != "") {
                Toast.makeText(LoginActivity.this, errorMessage, Toast.LENGTH_SHORT).show();
                Intent intent = getIntent();
                loginProgressBar.setVisibility(ProgressBar.GONE);
                startActivity(intent);
                return;
            }
            loginProgressBar.setVisibility(ProgressBar.GONE);
            Gson converter = new Gson();
            if (response.getResponseCode() >= 400) {
                responseContent = response.getContent();
                try {
                    JSONObject jsonResponse = new JSONObject(responseContent);
                    String message = jsonResponse.getString("message");
                    Toast.makeText(LoginActivity.this,
                            message, Toast.LENGTH_SHORT).show();
                } catch (JSONException e) {
                    throw new RuntimeException(e);
                }



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
                    }
                }
            }
        }
    }
}

