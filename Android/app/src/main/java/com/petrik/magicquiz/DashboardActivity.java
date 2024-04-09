package com.petrik.magicquiz;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;

import android.content.Intent;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.material.navigation.NavigationView;

public class DashboardActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {

    private Toolbar toolbar;
    private DrawerLayout drawerLayout;
    private TextView navHeaderUsername;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_dashboard);
        init();
        setSupportActionBar(toolbar);
        getSupportActionBar().setTitle("");
        SharedPreferences token = this.getSharedPreferences("userdata", MODE_PRIVATE);
        String tokenString = token.getString("token", "");
        LoadUserData loadUserData = new LoadUserData(this);
        loadUserData.getUserData(new LoadUserData.UserDataLoadedListener() {
            @Override
            public String onUserDataLoaded(String errorMessage) {
                if(errorMessage != null) {
                    Intent intent = new Intent(DashboardActivity.this, LoginActivity.class);
                    SharedPreferences sharedPreferences = getSharedPreferences("userdata", MODE_PRIVATE);
                    sharedPreferences.edit().clear().commit();
                    startActivity(intent);
                }
                initDrawer();
                return errorMessage;
            }
        });
    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem menuItem) {
        int itemId = menuItem.getItemId();
        if (itemId == R.id.nav_kezdolap) {
            Toast.makeText(this, "Kezdőlap", Toast.LENGTH_SHORT).show();
            getSupportFragmentManager().beginTransaction().replace(R.id.fragment_container, new KezdolapFragment()).commit();
        } else if (itemId == R.id.nav_profile) {
            getSupportFragmentManager().beginTransaction().replace(R.id.fragment_container, new ProfileFragment()).commit();
        } else if (itemId == R.id.nav_booster) {
            getSupportFragmentManager().beginTransaction().replace(R.id.fragment_container, new BoosterFragment()).commit();
        } else if (itemId == R.id.nav_logout) {
            logout();
        }
        drawerLayout.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    public void onBackPressed() {
        if (drawerLayout.isDrawerOpen(GravityCompat.START)) {
            drawerLayout.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    public void init() {
        toolbar = findViewById(R.id.toolbar);
        drawerLayout = findViewById(R.id.DrawerLayout);
    }

    private void logout() {
        Toast.makeText(this, "Logout", Toast.LENGTH_SHORT).show();

        try {
            SharedPreferences sharedPreferences = getSharedPreferences("userdata", MODE_PRIVATE);
            Logout logout = new Logout(this);
            logout.setListener(new Logout.LogoutListener() {
                @Override
                public void onUserLoggedOut() {
                    SharedPreferences.Editor editor = sharedPreferences.edit();
                    editor.clear();
                    editor.commit();
                    Intent intent = new Intent(DashboardActivity.this, LoginActivity.class);
                    startActivity(intent);
                }
            });
            logout.logoutUser();
        } catch (Exception e) {
            Toast.makeText(this, "Hiba a felhasználói adatok törlésekor!", Toast.LENGTH_SHORT).show();
            //finish();
            //System.exit(0);
        }
        ;

    }

    private void initDrawer() {
        NavigationView navigationView = findViewById(R.id.nav_view);
        View headerView = navigationView.getHeaderView(0);
        navHeaderUsername = headerView.findViewById(R.id.nav_header_username);
        String name = getSharedPreferences("userdata", MODE_PRIVATE).getString("name", "");
        navHeaderUsername.setText(name);
        navigationView.setNavigationItemSelectedListener(this);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawerLayout.addDrawerListener(toggle);
        toggle.syncState();
        getSupportFragmentManager().beginTransaction().replace(R.id.fragment_container, new KezdolapFragment()).commit();
        navigationView.setCheckedItem(R.id.nav_kezdolap);

    }
}


