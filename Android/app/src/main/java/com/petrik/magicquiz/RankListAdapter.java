package com.petrik.magicquiz;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;

import java.util.List;
/** A RankListAdapter osztály a ranglista megjelenítéséért felelős. */
public class RankListAdapter extends ArrayAdapter<RankItem> {
/** Az alkalmazás kontextusa */
    private Context mContext;
    /** A ranglista elemei */
    private List<RankItem> mRankItems;

    public RankListAdapter(Context context, List<RankItem> rankItems) {
        super(context, 0, rankItems);
        mContext = context;
        mRankItems = rankItems;
    }

    @NonNull
    @Override
    public View getView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        View listItem = convertView;
        if (listItem == null) {
            listItem = LayoutInflater.from(mContext).inflate(R.layout.ranklist, parent, false);
        }

        RankItem currentItem = mRankItems.get(position);
        TextView rankNameTextView = listItem.findViewById(R.id.rankName);
        TextView rankPointTextView = listItem.findViewById(R.id.rankPoint);

        rankNameTextView.setText(currentItem.getName());
        rankPointTextView.setText(String.valueOf(currentItem.getScore()));

        return listItem;
    }
}
