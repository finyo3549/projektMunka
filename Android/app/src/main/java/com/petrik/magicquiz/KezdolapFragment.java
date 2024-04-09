package com.petrik.magicquiz;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import java.util.List;

public class KezdolapFragment extends Fragment implements GameResultListener{
    private ListView rankListView;
    private ProgressBar dashboardProgressBar;
    @Override
    public void onGameFinished() {
        dashboardProgressBar.setVisibility(View.VISIBLE);
        LoadRanklist loadRanklist = new LoadRanklist(getContext());
        loadRanklist.getRanklist(rankItems -> {
            rankItems.sort((o1, o2) -> Integer.compare(o2.getScore(), o1.getScore()));
            List<RankItem> top10RankItems = rankItems.subList(0, Math.min(rankItems.size(), 10));
            RankListAdapter adapter = new RankListAdapter(getContext(), top10RankItems);
            rankListView.setAdapter(adapter);
            dashboardProgressBar.setVisibility(View.GONE);
        });
    }

    @SuppressLint("MissingInflatedId")
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_kezdolap, container, false);
        Button button_startGame = rootView.findViewById(R.id.button_startGame);
        rankListView = rootView.findViewById(R.id.listView_ranklist);
        dashboardProgressBar = rootView.findViewById(R.id.dashboardProgressBar);
        dashboardProgressBar.setVisibility(View.VISIBLE);
        button_startGame.setOnClickListener(v -> {
            Intent intent = new Intent(getActivity(), Game.class);
            startActivity(intent);
        });
        LoadRanklist loadRanklist = new LoadRanklist(getContext());
        loadRanklist.getRanklist(rankItems -> {
            rankItems.sort((o1, o2) -> Integer.compare(o2.getScore(), o1.getScore()));

            List<RankItem> top10RankItems = rankItems.subList(0, Math.min(rankItems.size(), 10));

            RankListAdapter adapter = new RankListAdapter(getContext(), top10RankItems);
            rankListView.setAdapter(adapter);
            dashboardProgressBar.setVisibility(View.GONE);
        });

        return rootView;
    }



}

