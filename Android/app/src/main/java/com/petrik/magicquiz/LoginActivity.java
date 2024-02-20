package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends AppCompatActivity {

    private Button loginButton;
    private EditText loginUsername;
    private EditText loginPassword;
    private Button loginCancelButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login2);
        init();

        loginButton.setOnClickListener(v -> {
            String email = loginUsername.getText().toString();
            String password = loginPassword.getText().toString();
            if(email.isEmpty() || password.isEmpty()) {
                Toast.makeText(this, "Felhasználónév/jelszó nem lehet üres", Toast.LENGTH_SHORT).show();
            }
            else {
                Intent intent = new Intent(LoginActivity.this, DashboardActivity.class);
                startActivity(intent);
            }
    });

            loginCancelButton.setOnClickListener(v -> {
                Intent intent = new Intent(LoginActivity.this, MainActivity.class);
                startActivity(intent);
            });
    }
    private void init() {
        loginButton = findViewById(R.id.loginButton);
        loginUsername = findViewById(R.id.loginUsername);
        loginPassword = findViewById(R.id.loginPassword);
        loginCancelButton = findViewById(R.id.loginCancelButton);
    }
}
