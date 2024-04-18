package com.petrik.magicquiz;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.RadioButton;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import java.util.List;
/** A KezdolapFragment egy Fragment, amely a játék kezdőlapját jeleníti meg. */
public class KezdolapFragment extends Fragment implements GameResultListener{
    /** A rankListView ListView deklarálása */
    private ListView rankListView;
    /** A dashboardProgressBar ProgressBar deklarálása */
    private ProgressBar dashboardProgressBar;
    /** A GameResultListener interfész onGameFinished metódusának implementálása, mely újratölti a ranklistát*/
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
    /** A startGame metódus, mely elindítja a játékot a TOPIC_KEY-ben megadott témakörrel */
private void startGame(int topic) {
        Intent intent = new Intent(getActivity(), Game.class);
        intent.putExtra("TOPIC_KEY", topic);
        startActivity(intent);
    }
    /** A onCreateView metódus, mely a fragment layoutját állítja be, és betölti a ranklistát */
    @SuppressLint("MissingInflatedId")
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_kezdolap, container, false);
        Button button_startGame = rootView.findViewById(R.id.button_startGame);
        rankListView = rootView.findViewById(R.id.listView_ranklist);
        dashboardProgressBar = rootView.findViewById(R.id.dashboardProgressBar);
        dashboardProgressBar.setVisibility(View.VISIBLE);
        button_startGame.setOnClickListener(v -> {
            alertDialog();
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

    /** Az alertDialog metódus, mely egy AlertDialog-ot jelenít meg, és lehetőséget ad a felhasználónak, hogy válasszon témakört vagy random kapjon kérdéseket témakörökből */
    private void alertDialog() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
        builder.setTitle("Beállítások:");
        builder.setMessage("Milyen témaköröket szeretnél?");
        builder.setPositiveButton("Random", (dialog, which) -> {
            startGame(0);
        });
        builder.setNegativeButton("Én választok témakört", (dialog, which) -> {
            topicSelector();
        });
        builder.create().show();

    }
/** A topicSelector metódus, mely egy AlertDialog-ot jelenít meg, és lehetőséget ad a felhasználónak, hogy válasszon témakört */
    private void topicSelector() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setTitle("Válassz témakört");
        View view = LayoutInflater.from(getActivity()).inflate(R.layout.radio_button_alertdialog_layout, null);
        builder.setView(view);
        RadioButton radioButton1 = view.findViewById(R.id.radio_button1);
        RadioButton radioButton2 = view.findViewById(R.id.radio_button2);
        RadioButton radioButton3 = view.findViewById(R.id.radio_button3);
        RadioButton radioButton4 = view.findViewById(R.id.radio_button4);
        RadioButton radioButton5 = view.findViewById(R.id.radio_button5);
        radioButton1.setChecked(true);
        builder.setPositiveButton("OK", (dialog, which) -> {
            if (radioButton1.isChecked()) {
                startGame(1);
            } else if (radioButton2.isChecked()) {
                startGame(2);
            } else if (radioButton3.isChecked()) {
                startGame(3);
            } else if (radioButton4.isChecked()) {
                startGame(4);
            } else if (radioButton5.isChecked()) {
                startGame(5);
            }
        });
        builder.setNegativeButton("Mégse", (dialog, which) -> {
            dialog.dismiss();
        });

        AlertDialog alertDialog = builder.create();
        alertDialog.show();
    }


}

