package com.petrik.magicquiz;

import android.os.Bundle;
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
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_kezdolap, container, false);

        ListView rankListView = rootView.findViewById(R.id.listView_ranklist);
        List<RankItem> rankItems = new ArrayList<>();
        //IDE megcsinálni az API hívást hogy feltöltse
        RankListAdapter adapter = new RankListAdapter(getContext(), rankItems);
        rankListView.setAdapter(adapter);

        return rootView;
    }


}

