<?php

namespace App\Http\Controllers\API;

use App\Http\Controllers\Controller;
use App\Models\User;
use Illuminate\Support\Facades\File;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Storage;

//--------------
//UserController módosítása, eredetileg ez volt a PlayerController,
// de átalakítottam hogy a users modellt használja és lekérje az adatokat a users táblából
// a felhasználó adatainak egyszerű lekéréseihez/módosításaihoz
//--------------

class UserController extends Controller
{
    /**
     * Display a listing of all users.
     */
    public function index()
    {
        $this->authorize('index',User::class);
        return User::all();
    }

    /**
     * Store a newly created user in storage.
     * Currently, this method is not implemented.
     */
    public function store(Request $request)
    {
        // Method for creating a new user
    }

    /**
     * Display the specified user.
     *
     * @param string $id The user's ID
     */
    public function show(string $id)
    {
        // Find the user by ID
        $user = User::find($id);

        // Check if the user exists
        if (is_null($user)) {
            return response()->json(['message' => "User not found with id: $id"], 404);
        } else {
            return response()->json($user, 200);
        }
    }

    /**
     * Update the specified user in storage.
     *
     * @param Request $request The request object
     * @param string $id The user's ID
     */
    public function update(Request $request, $id)
    {
        $user = User::findOrFail($id);

        //Update avatar under development
        // // Képfeltöltés kezelése
        // if ($request->hasFile('avatar')) {
        //     $avatar = $request->file('avatar');
        //     $filename = time() . '.' . $avatar->getClientOriginalExtension();
        //     $avatar->move(public_path('avatars/'), $filename);

        //     // Régi kép törlése, ha van
        //     $oldFilename = $user->avatar;
        //     if ($oldFilename && file_exists(public_path('avatars/' . $oldFilename))) {
        //         File::delete(public_path('avatars/' . $oldFilename));
        //     }

        //     // Beállítjuk közvetlenül az avatar attribútumot
        //     $user->avatar = $filename;
        // }

        // Egyéb attribútumok frissítése, ha szükséges
        $user->fill($request->only(['name', 'email', 'gender', 'is_admin', 'is_active', 'avatar']));

        // Jelszó frissítése, ha szükséges és hashelése
        if ($request->filled('password')) {
            $user->password = Hash::make($request->password);
        }

        $user->save();

        return response()->json($user);
    }

    /**
     * Remove the specified user from storage.
     *
     * @param string $id The user's ID
     */

    public function inactivate($id)
    {
        $user = User::find($id);
        if (is_null($user)) {
            return response()->json(['message' => "User not found with id: $id"], 404);
        } else {

            try {
                $this->authorize('inactivate',$user);
                $user->is_active = 0;
                $user->save();
                $user->tokens->each->delete();
                return response()->json(['message' => "$user->name inaktiválva lett!"]);
            } catch (\Illuminate\Auth\Access\AuthorizationException $e) {
                return response()->json(['message' => 'Nincs jogosultsága ennek a funkciónak a használatára!'], 403);
            }
        }
    }
}
