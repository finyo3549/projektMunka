package com.petrik.magicquiz;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;

public class DashboardActivity extends AppCompatActivity {

private Toolbar toolbar;
private Button menuButton;
private DrawerLayout drawerLayout;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_dashboard);
        init();
        setSupportActionBar(toolbar);
        getSupportActionBar().setTitle("");
        menuButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                drawerLayout.openDrawer(GravityCompat.START);
            }
    });
    }
    public void init() {
        toolbar = findViewById(R.id.toolbar);
        menuButton = findViewById(R.id.menuButton);
        drawerLayout = findViewById(R.id.DrawerLayout);
    }
}