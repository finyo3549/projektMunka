<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Http\Requests\RegisterRequest;
use Illuminate\Http\Request;
use App\Models\User;
use App\Http\Requests\LoginRequest;
use App\Models\Rank;


class AuthController extends Controller
{
    //--------------
    //Regisztráció átalakítása, hogy létrehozzon egy alapértelmezett avatart valamint további oszlopok felvétele (PL.:avatart,gender)
    //--------------
    public function register(RegisterRequest $request)
    {
        // Alapértelmezett avatar kép kiválasztása a nem alapján
        $defaultAvatar = 'unknown_avatar.png'; // Alapértelmezés, ha a nem nem ismert
        if ($request->gender === 'male') {
            $defaultAvatar = 'male_avatar.png';
        } elseif ($request->gender === 'female') {
            $defaultAvatar = 'female_avatar.png';
        }
    
        // Felhasználó létrehozása az adatbázisban az alapértelmezett avatarral
        $user = User::create([
            'name' => $request->name,
            'email' => $request->email,
            'password' => password_hash($request->password, PASSWORD_DEFAULT),
            'avatar' => $defaultAvatar, // Itt adjuk meg az alapértelmezett avatart
            'gender' => $request->gender,
        ]);
    
        // Rank létrehozása (ha szükséges)
        Rank::create([
            'user_id' => $user->id,
            'score' => 0
        ]);
    
        return response()->json([
            "message" => "User created successfully",
            "user" => $user
        ], 201);
    }
    

    public function login(LoginRequest $request){
        $user = User::where("email", $request->email)->first();

        if(!$user || !\Hash::check($request->password, $user->password)){
            return response()->json([
                "message" => "Incorrect username or password"
            ], 401);
        }
        $token = $user->createToken("AuthToken")->plainTextToken;
        return response()->json([
            "token" => $token,
            "user_id" => $user->id
        ], 200);
    }
    public function logout(Request $request ){
        $user = auth()->user();
        $user->currentAccessToken()->delete();
        $allTokens = $user->tokens;
        return response()->noContent();
        }
}
