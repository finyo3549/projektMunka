<?php

namespace App\Policies;

use App\Models\User;
use App\Models\Rank;


class RankPolicy
{
    public function rankreset(User $authenticatedUser)
    {
        \Log::info('Rank reset method called by user: ' . $authenticatedUser->id);

        $isAdmin = $authenticatedUser->is_admin == 1;

        // Log the result of the admin check
        \Log::info('User is admin: ' . ($isAdmin ? 'Yes' : 'No'));

        // Return the result of the admin check
        return $isAdmin;
    }

    /**
     * Determine whether the user can view any models.
     */
    public function viewAny(User $user): bool
    {
        //
    }

    /**
     * Determine whether the user can view the model.
     */
    public function view(User $user, rank $rank): bool
    {
        //
    }

    /**
     * Determine whether the user can create models.
     */
    public function create(User $user): bool
    {
        //
    }

    /**
     * Determine whether the user can update the model.
     */
    public function update(User $user, rank $rank): bool
    {
        //
    }

    /**
     * Determine whether the user can delete the model.
     */
    public function delete(User $user, rank $rank): bool
    {
        //
    }

    /**
     * Determine whether the user can restore the model.
     */
    public function restore(User $user, rank $rank): bool
    {
        //
    }

    /**
     * Determine whether the user can permanently delete the model.
     */
    public function forceDelete(User $user, rank $rank): bool
    {
        //
    }
}
