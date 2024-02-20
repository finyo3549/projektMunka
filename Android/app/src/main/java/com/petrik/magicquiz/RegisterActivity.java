package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.EditText;
import android.widget.Button;

public class RegisterActivity extends AppCompatActivity {
    private EditText registerUsername;
    private EditText registerEmail;
    private EditText registerPassword;
    private Button registerButton;
    private Button registerCancelButton;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        init();
        registerCancelButton.setOnClickListener(v -> {
            Intent intent = new Intent(RegisterActivity.this, MainActivity.class);
            startActivity(intent);
        });
    }
    private void init(){
        registerUsername = findViewById(R.id.registerUsername);
        registerEmail = findViewById(R.id.registerEmail);
        registerPassword = findViewById(R.id.registerPassword);
        registerButton = findViewById(R.id.registerButton);
        registerCancelButton = findViewById(R.id.registerCancelButton);
    }
}