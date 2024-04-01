<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\Models\User;

class RankController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {


        //$userRanks = DB::table('user_ranks')->orderByDesc('score')->take(10)->get();
         //   return response()->json($userRanks);

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
