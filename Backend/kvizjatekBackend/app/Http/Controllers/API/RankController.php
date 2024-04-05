<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Http\Requests\UpdaterankRequest;
use Illuminate\Http\Request;
use App\Models\User;
use App\Models\Rank; // Feltételezve, hogy van Rank modelled

class RankController extends Controller
{
    /**
     * Display the weekly leaderboard.
     */
    public function index()
    {
        $weeklyRanks = Rank::with(['user:id,name'])
                            ->orderByDesc('score')
                            ->get();

        return response()->json($weeklyRanks);
    }

    /**
 * Update a user's rank score.
 *
 * @param  \Illuminate\Http\Request  $request
 * @param  string  $id The user's ID
 * @return \Illuminate\Http\Response
 */
    public function update(UpdaterankRequest $request, $id)
    {
        // Felhasználó keresése
        $user = User::find($id);

        if (!$user) {
            return response()->json(['message' => 'User not found'], 404);
        }

        // A felhasználóhoz kapcsolódó rang lekérdezése vagy létrehozása, ha nem létezik
        $rank = $user->rank()->firstOrCreate();

        // Score frissítése
        $rank->score = $request['score'];
        $rank->save();

        return response()->json([
            'message' => 'Rank updated successfully',
            'data' => [
                'user_id' => $user->id,
                'score' => $rank->score,
            ],
        ], 200);
    }

    /**
     * Reset all scores to 0.
     *
     * @return \Illuminate\Http\Response
     */
    public function reset()
    {
        // Minden Rank rekord score értékének frissítése 0-ra
        Rank::query()->update(['score' => 0]);

        return response()->json(['message' => 'Minden pontszám sikeresen visszaállítva 0-ra.'], 200);
    }


    /**
     * Display the rank of a specific user.
     *
     * @param string $id The user's ID
     */
    public function show(string $id)
    {
        $user = User::find($id, ['id', 'name']); 
    
        if (!$user) {
            return response()->json(['message' => 'User not found'], 404);
        }
    
        // A felhasználóhoz kapcsolódó rang lekérdezése
        $rank = $user->rank()->first(['score']);
    
        return response()->json([
            "name" => $user->name,
            "user_id" => $user->id,
            "score" => $rank ? $rank->score : "No rank data",
        ], 200);
    }
    
}
