<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\User;
use App\Models\Rank;

class RankController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $responseData = [];
        $users = User::all();
        foreach ($users as $user) {
            $rank = $user->rank;
            

            $responseData[] = [
                "name" => $user->name,
                "user_id" => $user->id,
                "score" => $rank ? $rank->score : null,
                "email" => $user->email
            ];
        }

        return response()->json($responseData, 200);
    }


    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        $user = User::find($id);
        $rank = $user->rank;

        if (!$user) {
            return response()->json(['message' => 'Felhasználó nem található'], 404);
        }

        return response()->json([
            "name" => $user->name,
            "user_id" => $user->id,
            "score" => $rank->score,
            "email" => $user->email
        ], 200);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        //
    }
}
