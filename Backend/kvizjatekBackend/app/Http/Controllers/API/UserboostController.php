<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Http\Requests\StoreUserboostRequest;
use App\Http\Requests\UpdateUserboostRequest;
use App\Http\Requests\UserIdRequest;
use App\Models\User;
use App\Models\Userboost;
use Illuminate\Http\Request;

    //--------------
    //UserboostController módosítása a lekérdezésekhez --> add addMultipleBoosters a boosterek fevételéhez,
    //mert ez nem történik meg a felhasználó regisztrálásával
    //--------------


class UserboostController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
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
        //
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

    public function resetBoostersForNewGame($userId)
    {
        $userExists = User::find($userId);
        if (!$userExists) {
            return response()->json(['message' => 'User not found'], 404);
        }

        Userboost::where('user_id', $userId)->update(['used' => false]);
        return response()->json(['message' => 'Boosters reset successfully for user']);
    }

    public function updateUserBoosterStatus(UpdateUserboostRequest $request)
    {
        $userId = $request->input('user_id');
        $boosterId = $request->input('booster_id');
    
        $userBoost = Userboost::where('user_id', $userId)->where('booster_id', $boosterId)->first();
    
        if (!$userBoost) {
            return response()->json(['message' => "Booster not found for user_id: $userId, booster_id: $boosterId"], 404);
        }
    
        $userBoost->used = true;
        $userBoost->save();
    
        return response()->json(['message' => 'Booster updated successfully'], 200);
    }

    public function boostersByUserId($userId)
    {
        $userExists = User::find($userId);
        if (!$userExists) {
            return response()->json(['message' => 'User not found'], 404);
        }

        $userBoosts = Userboost::where('user_id', $userId)->with('booster')->get();
        if ($userBoosts->isEmpty()) {
            return response()->json(['message' => 'Nincsenek boosterek ezzel a felhasználói azonosítóval.'], 404);
        }
            return response()->json($userBoosts);
    }

    public function addMultipleBoosters(StoreUserboostRequest $request, $userId)
    {
    $boosterIds = $request['booster_ids'];

    foreach ($boosterIds as $boosterId) {
        $userBoostExists = Userboost::where('user_id', $userId)->where('booster_id', $boosterId)->first();

        if (!$userBoostExists) {
            $userBoost = new Userboost();
            $userBoost->user_id = $userId;
            $userBoost->booster_id = $boosterId;
            $userBoost->used = false;
            $userBoost->save();
        }
    }

    return response()->json(['message' => 'Boosterek sikeresen hozzáadva.'], 201);
    }


}
