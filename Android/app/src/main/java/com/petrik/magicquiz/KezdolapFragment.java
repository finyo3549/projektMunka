package com.petrik.magicquiz;

import static android.content.Context.MODE_PRIVATE;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import java.util.ArrayList;
import java.util.List;

public class KezdolapFragment extends Fragment {
    public String url = "http://10.0.2.2:8000/api/user-ranks/";

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_kezdolap, container, false);
        ListView rankListView = rootView.findViewById(R.id.listView_ranklist);
        LoadRanklist loadRanklist = new LoadRanklist(getContext());
        loadRanklist.getRanklist(new LoadRanklist.RankListLoadedListener() {
            @Override
            public void onRanklistLoaded(List<RankItem> rankItems) {
                RankListAdapter adapter = new RankListAdapter(getContext(), rankItems);
                rankListView.setAdapter(adapter);
            }
        });

        return rootView;
    }


}

