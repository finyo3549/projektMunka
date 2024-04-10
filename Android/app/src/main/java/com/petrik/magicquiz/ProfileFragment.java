package com.petrik.magicquiz;

import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.text.Spannable;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import org.w3c.dom.Text;


public class ProfileFragment extends Fragment {
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_profile, container, false);
        TextView usernameTextView = rootView.findViewById(R.id.profileName);
        TextView emailTextView = rootView.findViewById(R.id.profileEmail);
        TextView scoreTextView = rootView.findViewById(R.id.profileScore);
        TextView genderTextView = rootView.findViewById(R.id.profileGender);
        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("userdata", getContext().MODE_PRIVATE);
        String name = sharedPreferences.getString("name", "");
        String email = sharedPreferences.getString("email", "");
        int score = sharedPreferences.getInt("score", 0);
        String gender = sharedPreferences.getString("gender", "");
        SpannableString spannableStringname = new SpannableString("Név: " + name);
        SpannableString spannableStringemail = new SpannableString("Email: " + email);
        SpannableString spannableStringPontszám = new SpannableString("Pontszám: " + score);
        SpannableString spannableStringGender = new SpannableString("Nem: " + gender);
        spannableStringname.setSpan(new ForegroundColorSpan(Color.parseColor("#FFEB3B")), 0, 4, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE);
        spannableStringemail.setSpan(new ForegroundColorSpan(Color.parseColor("#FFEB3B")), 0, 6, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE);
        spannableStringPontszám.setSpan(new ForegroundColorSpan(Color.parseColor("#FFEB3B")), 0, 9, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE);
        spannableStringGender.setSpan(new ForegroundColorSpan(Color.parseColor("#FFEB3B")), 0, 4, Spannable.SPAN_EXCLUSIVE_EXCLUSIVE);
        usernameTextView.setText(spannableStringname);
        emailTextView.setText(spannableStringemail);
        scoreTextView.setText(spannableStringPontszám);
        genderTextView.setText(spannableStringGender);
        return rootView;
    }

}
