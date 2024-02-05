<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use \App\Models\Player;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\Userboost>
 */
class UserboostFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition(): array
    {
        $userIds = Player::all()->pluck('id')->toArray();

        return [
            'booster1'=>fake()->boolean(),
            'booster2'=>fake()->boolean(),
            'booster3'=>fake()->boolean(),
            'id' => $this->faker->unique()->numberBetween(1, count($userIds)),

        ];
    }
}
