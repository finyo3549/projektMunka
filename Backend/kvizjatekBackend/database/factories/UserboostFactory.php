<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\Userboost;
use App\Models\User;
use App\Models\Booster;

class UserboostFactory extends Factory
{
    protected $model = Userboost::class;

    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition()
    {
        // Biztosítjuk, hogy csak létező user és booster ID-k kerüljenek kiválasztásra
        $userIds = User::pluck('id')->toArray();
        $boosterIds = Booster::pluck('id')->toArray();

        // Ellenőrizzük, hogy vannak-e rendelkezésre álló ID-k
        if (empty($userIds) || empty($boosterIds)) {
            throw new \Exception('No users or boosters available for Userboost factory');
        }

        return [
            'user_id' => $this->faker->randomElement($userIds),
            'booster_id' => $this->faker->randomElement($boosterIds),
            'used' => $this->faker->boolean(0), // 20% esély, hogy true (használt) legyen, 80%, hogy false
        ];
    }
}

