<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use \App\Models\Player;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Booster>
 */
class BoosterFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $userIds = Player::all()->pluck('id')->toArray();
    $boosters = ['felező' => 100, 'telefonhívás' => 200, 'közönség' => 300];

    $randomBoosterName = array_rand($boosters);

    return [
        'id' => $this->faker->unique()->randomElement($userIds),
        'boostername' => $randomBoosterName,
        'credit' => $boosters[$randomBoosterName],
    ];
    }
}
