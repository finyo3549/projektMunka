package com.petrik.magicquiz;

import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

public class BoosterFragment extends Fragment {
    private TextView boosterTextView;
    private TextView boosterDescriptionTextView;
    private LinearLayout helpTextLayout;
    private LinearLayout boosterHolderLayout;
    private ImageView phone_button;
    private ImageView fifty_fifty_button;
    private ImageView audience_button;
    private LinearLayout boosterDescriptionLayout;
    private TextView boosterName;
    private TextView boosterDescription;
    private Context mContext;
    private Button backButton;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        View rootView = inflater.inflate(R.layout.fragment_booster, container, false);
        init(rootView);
        LoadBoosters loadBoosters = new LoadBoosters(mContext);
        loadBoosters.getBoosters(new LoadBoosters.BoosterDataLoadedListener() {
            @Override
            public void onBoosterDataLoaded() {
                functions();

            }
        });
        return rootView;
    }

    private void functions() {

        phone_button.setOnClickListener(v -> {
            phone_button();
        });
        fifty_fifty_button.setOnClickListener(v -> {
            fifty_fifty_button();
        });
        audience_button.setOnClickListener(v -> {
            audience_button();
        });
    }

    private void audience_button() {
        displayChange(1);
    }

    private void fifty_fifty_button() {
        displayChange(2);
    }

    private void phone_button() {
        displayChange(0);
    }
    private void displayChange(int id) {
        boosterName.setText(LoadBoosters.boosterList.get(id).getBooster_name());
        boosterDescription.setText(LoadBoosters.boosterList.get(id).getBooster_description());
        helpTextLayout.setVisibility(View.GONE);
        boosterHolderLayout.setVisibility(View.GONE);
        boosterDescriptionLayout.setVisibility(View.VISIBLE);
        boosterName.setVisibility(View.VISIBLE);
        boosterDescription.setVisibility(View.VISIBLE);
        backButton.setVisibility(View.VISIBLE);
        backButton.setOnClickListener(v -> {
            helpTextLayout.setVisibility(View.VISIBLE);
            boosterHolderLayout.setVisibility(View.VISIBLE);
            boosterDescriptionLayout.setVisibility(View.GONE);
            boosterName.setVisibility(View.GONE);
            boosterDescription.setVisibility(View.GONE);
            backButton.setVisibility(View.GONE);
        });
    }
    @Override
    public void onAttach(Context context) {
        super.onAttach(context);
        mContext = context;
    }

    private void init(View rootView) {
        boosterTextView = rootView.findViewById(R.id.boosterName);
        boosterDescriptionTextView = rootView.findViewById(R.id.boosterDescription);
        helpTextLayout = rootView.findViewById(R.id.helpTextLayout);
        boosterHolderLayout = rootView.findViewById(R.id.boosterHolderLayout);
        phone_button = rootView.findViewById(R.id.phone_button);
        fifty_fifty_button = rootView.findViewById(R.id.fifty_fifty_button);
        audience_button = rootView.findViewById(R.id.audience_button);
        boosterDescriptionLayout = rootView.findViewById(R.id.boosterDescriptionLayout);
        boosterName = rootView.findViewById(R.id.boosterName);
        boosterDescription = rootView.findViewById(R.id.boosterDescription);
        backButton = rootView.findViewById(R.id.backButton);
    }
}
